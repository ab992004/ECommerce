using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce521.Utilities.DBInitializer
{
    public class DBInitializer : IDBInitializer
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public DBInitializer(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        public void Initialize()
        {
            //if(_context.Database.GetPendingMigrations().Any())
            //{
            //    _context.Database.Migrate();
            //}

            if (_roleManager.Roles.IsNullOrEmpty())
            {
                _roleManager.CreateAsync(new(SD.SUPER_ADMIN_ROLE)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new(SD.ADMIN_ROLE)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new(SD.EMPLOYEE_ROLE)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new(SD.CUSTOMER_ROLE)).GetAwaiter().GetResult();

                _userManager.CreateAsync(new()
                {
                    Email = "superadmin@eraasoft.com",
                    EmailConfirmed = true,
                    UserName = "SuperAdmin",
                    Name = "SuperAdmin"
                }, "Admin123$").GetAwaiter().GetResult();

                var user = _userManager.FindByNameAsync("SuperAdmin").GetAwaiter().GetResult();

                _userManager.AddToRoleAsync(user!, SD.SUPER_ADMIN_ROLE).GetAwaiter().GetResult();
            }
        }
    }
}
