using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce521.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{SD.SUPER_ADMIN_ROLE},{SD.ADMIN_ROLE},{SD.EMPLOYEE_ROLE}")]
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        public IActionResult NotFoundPage()
        {
            return View();
        }
    }
}
