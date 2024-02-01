using Agency.DAL;
using Agency.Models;
using Agency.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Agency;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<AppDbContext>(
            opt => opt.UseSqlServer(
                builder.Configuration.GetConnectionString(
                    "default")));
        builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
        {
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;

            options.User.RequireUniqueEmail = true;

            options.Lockout.MaxFailedAccessAttempts = 3;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);
        }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
        builder.Services.ConfigureApplicationCookie(
            cfg =>
            {
                cfg.LoginPath = $"/Admin/Account/Login";
            });

        builder.Services.AddScoped<LayoutService>();



        var app = builder.Build();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            "default",
            "{area=exists}/{controller=home}/{action=index}/{id?}");

        app.MapControllerRoute(
            "default",
            "{controller=home}/{action=index}/{id?}");

        
        app.Run();
    }
}