using ECommerce521.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ECommerce521.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{SD.SUPER_ADMIN_ROLE},{SD.ADMIN_ROLE},{SD.EMPLOYEE_ROLE}")]
    public class CategoryController : Controller
    {
        //private ApplicationDbContext _context = new();
        private readonly IRepository<Category> _categoryRepository;// = new Repository<Category>();

        public CategoryController(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryRepository.GetAsync(tracked: false);

            return View(categories.AsEnumerable());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Category());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            await _categoryRepository.CreateAsync(category);
            await _categoryRepository.CommitAsync();

            TempData["success-notification"] = "Add Category Successfully";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = $"{SD.SUPER_ADMIN_ROLE},{SD.ADMIN_ROLE}")]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryRepository.GetOneAsync(e => e.Id == id);

            if (category is null)
                return RedirectToAction(nameof(HomeController.NotFoundPage), "Home", new { area = "Admin" });

            return View(category);
        }

        [HttpPost]
        [Authorize(Roles = $"{SD.SUPER_ADMIN_ROLE},{SD.ADMIN_ROLE}")]
        public async Task<IActionResult> Edit(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            _categoryRepository.Update(category);
            await _categoryRepository.CommitAsync();

            TempData["success-notification"] = "Update Category Successfully";

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = $"{SD.SUPER_ADMIN_ROLE},{SD.ADMIN_ROLE}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryRepository.GetOneAsync(e => e.Id == id);

            if (category is null)
                return RedirectToAction(nameof(HomeController.NotFoundPage), "Home", new { area = "Admin" });

            _categoryRepository.Delete(category);
            await _categoryRepository.CommitAsync();

            TempData["success-notification"] = "Delete Category Successfully";

            return RedirectToAction(nameof(Index));
        }
    }
}
