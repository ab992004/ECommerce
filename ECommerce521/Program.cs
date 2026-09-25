using ECommerce521.Utilities;
using ECommerce521.Utilities.DBInitializer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace ECommerce521
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var connectionString =
                    builder.Configuration.GetConnectionString("DefaultConnection")
                        ?? throw new InvalidOperationException("Connection string"
                        + "'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(confi =>
            {
                confi.User.RequireUniqueEmail = true;
                confi.Password.RequiredLength = 8;
                confi.Password.RequireNonAlphanumeric = false;
                confi.Lockout.MaxFailedAccessAttempts = 3;
                confi.SignIn.RequireConfirmedEmail = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Source - https://stackoverflow.com/a
            // Posted by animalito maquina, modified by community. See post 'Timeline' for change history
            // Retrieved 2025-11-28, License - CC BY-SA 4.0

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                //options.ExpireTimeSpan = 
            });

            //Singleton, Transient, Scoped

            builder.Services.AddScoped<IRepository<Category>, Repository<Category>>();
            builder.Services.AddScoped<IRepository<Brand>, Repository<Brand>>();
            builder.Services.AddScoped<IRepository<Product>, Repository<Product>>();
            builder.Services.AddScoped<IProductSubImageRepository, ProductSubImageRepository>();
            builder.Services.AddScoped<IRepository<ApplicationUserOTP>, Repository<ApplicationUserOTP>>();
            builder.Services.AddScoped<IRepository<Cart>, Repository<Cart>>();
            builder.Services.AddScoped<IRepository<Promotion>, Repository<Promotion>>();
            builder.Services.AddTransient<IEmailSender, EmailSender>();
            builder.Services.AddScoped<IDBInitializer, DBInitializer>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var initializer = scope.ServiceProvider.GetRequiredService<IDBInitializer>();
                initializer.Initialize();
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
