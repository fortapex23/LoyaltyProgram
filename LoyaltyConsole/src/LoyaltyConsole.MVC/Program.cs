using LoyaltyConsole.MVC.Services.Implementations;
using LoyaltyConsole.MVC.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace LoyaltyConsole.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //builder.Services.AddAuthentication("Cookies")
            //.AddCookie("Cookies", options =>
            //{
            //    options.LoginPath = "Admin/Auth/AdminLogin";
            //});

            builder.Services.AddRegisterService();
            builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddHttpClient<ICrudService, CrudService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7027/api/");
            });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Admin/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
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
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            var conn = builder.Configuration.GetConnectionString("DefaultConnection");
            Console.WriteLine($"[DEBUG] Using connection string: {conn}");

            app.Run();
        }
    }
}