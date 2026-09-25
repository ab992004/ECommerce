using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ECommerce521.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{SD.SUPER_ADMIN_ROLE},{SD.ADMIN_ROLE}")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.AsNoTracking().AsQueryable();

            return View(users);
        }

        public async Task<IActionResult> LockUnLock(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
                return NotFound();

            user.LockoutEnabled = !user.LockoutEnabled;

            if (!user.LockoutEnabled)
                user.LockoutEnd = DateTime.UtcNow.AddDays(30);
            else
                user.LockoutEnd = null;

            await _userManager.UpdateAsync(user);
            return RedirectToAction(nameof(Index));
        }
    }
}
