using ECommerce521.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce521.Areas.Customer.Controllers
{
    [Authorize]
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Promotion> _promotionRepository;

        public CartController(UserManager<ApplicationUser> userManager, IRepository<Cart> cartRepository, IRepository<Product> productRepository, IRepository<Promotion> promotionRepository)
        {
            _userManager = userManager;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _promotionRepository = promotionRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int count)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user is null) return NotFound();

            var product = await _productRepository.GetOneAsync(e => e.Id == productId, tracked: false);

            if(product is null) return NotFound();

            var cart = await _cartRepository.GetOneAsync(e => e.ApplicationUserId == user.Id && e.ProductId == product.Id);

            if(cart is not null)
                cart.Count += count;
            else
            {
                await _cartRepository.CreateAsync(new Cart
                {
                    ProductId = productId,
                    Count = count,
                    ApplicationUserId = user.Id,
                    Price = (product.Price - (product.Price * (product.Discount / 100)))
                });
            }
                
            await _cartRepository.CommitAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index(string code)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user is null) return NotFound();

            var carts = await _cartRepository.GetAsync(e => e.ApplicationUserId == user.Id, includes: [e => e.Product]);

            if(code is not null)
            {
                var promotion = await _promotionRepository.GetOneAsync(e => e.Code == code && e.isValid && e.ValidTo > DateTime.UtcNow && e.MaxUsage > 0/* && carts.Select(e => e.ProductId).ToList().Contains(e.ProductId)*/);

                if (promotion is null)
                    TempData["error-notification"] = "Invalid Code";
                else
                {
                    bool founded = false;
                    foreach (var item in carts)
                    {

                        if (item.ProductId == promotion.ProductId)
                        {
                            item.Price -= (item.Price * (promotion.Discount / 100));
                            promotion.MaxUsage -= 1;

                            if(promotion.MaxUsage == 0)
                                promotion.isValid = false;

                            await _cartRepository.CommitAsync();
                            TempData["success-notification"] = "Apply Code";

                            founded = true;
                            break;
                        }
                    }

                    if(!founded)
                        TempData["error-notification"] = "Invalid Code";
                }
            }

            return View(carts);
        }

        public async Task<IActionResult> IncrementCount(int productId)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user is null) return NotFound();

            var cart = await _cartRepository.GetOneAsync(e => e.ApplicationUserId == user.Id && e.ProductId == productId);

            if (cart is null) return NotFound();

            cart.Count += 1;
            await _cartRepository.CommitAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DecrementCount(int productId)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user is null) return NotFound();

            var cart = await _cartRepository.GetOneAsync(e => e.ApplicationUserId == user.Id && e.ProductId == productId);

            if (cart is null) return NotFound();

            if (cart.Count > 1)
            {
                cart.Count -= 1;
                await _cartRepository.CommitAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteItem(int productId)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user is null) return NotFound();

            var cart = await _cartRepository.GetOneAsync(e => e.ApplicationUserId == user.Id && e.ProductId == productId);

            if (cart is null) return NotFound();

            _cartRepository.Delete(cart);
            await _cartRepository.CommitAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
