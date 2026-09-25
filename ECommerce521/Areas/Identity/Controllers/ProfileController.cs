using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce521.Areas.Identity.Controllers
{
    [Area("Identity")]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            //UserInfoVM userInfoVM = new()
            //{
            //    Name = user.Name,
            //    Address = user.Address,
            //    Email = user.Email,
            //    PhoneNumber = user.PhoneNumber,
            //    UserName = user.UserName
            //};

            UserInfoVM userInfoVM = user.Adapt<UserInfoVM>();

            return View(userInfoVM);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateInfo(UserInfoVM userInfoVM)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            var user = await _userManager.GetUserAsync(User);

            user.Name = userInfoVM.Name;
            user.Email = userInfoVM.Email;
            user.PhoneNumber = userInfoVM.PhoneNumber;
            user.Address = userInfoVM.Address;

            //user = userInfoVM.Adapt<ApplicationUser>();

            await _userManager.UpdateAsync(user);
            TempData["success-notification"] = "Update Profile Successfully";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdatePassword(string CurrentPassword, string NewPassword)
        {
            if (CurrentPassword is null && NewPassword is null)
            {
                TempData["error-notification"] = "You must write CurrentPassword & NewPassword";
                return RedirectToAction("Index");
            }

            var user = await _userManager.GetUserAsync(User);
            var result = await _userManager.ChangePasswordAsync(user, CurrentPassword, NewPassword);

            if(!result.Succeeded)
            {
                TempData["error-notification"] = String.Join(", ", result.Errors.Select(e => e.Code));
                return RedirectToAction("Index");
            }

            TempData["success-notification"] = "Update Password Successfully";
            return RedirectToAction("Index");
        }
    }
}
