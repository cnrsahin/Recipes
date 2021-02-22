using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Recipes.Service.Core.Abstract;
using Recipes.Service.Core.Concrete.Entities;
using Recipes.Service.Data.Contexts;
using Recipes.Service.Data.EntityFramework;
using Recipes.UI.AutoMapper;
using System;

namespace Recipes.UI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSession();
            services.AddAutoMapper(typeof(Profiles));

            services.AddDbContext<RecipeContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("LocalDb"))
            );

            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IFoodCategoryRepository, FoodCategoryRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();

            services.AddIdentity<User, Role>(opt =>
            {
                // Kullan�c� �ifre kurallar�
                opt.Password.RequireDigit = false; // �ifrede rakam olmal� m� ?
                opt.Password.RequiredLength = 4; // �ifre uzunlu�u en az ka� olmal� ?
                opt.Password.RequiredUniqueChars = 0; // �zel karakterlerden farkl� farkl� ka� tane olmak zorunda ?
                opt.Password.RequireNonAlphanumeric = false; // �zel karakter olmak zorunda m�?
                opt.Password.RequireLowercase = false; // Kucuk harf zorunlu mu ?
                opt.Password.RequireUppercase = false; // Buyuk harf zorunlu mu ?

                // Kullan�c� ad� ve email ayarlar�
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+$"; // kullan�c� ad�nda hangi karakterler olacaksa yaz�yoruz.
                opt.User.RequireUniqueEmail = true; // 1 email ile tek kay�t olabilir.
            }).AddEntityFrameworkStores<RecipeContext>();


            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.FromMinutes(15);
            });

            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = new PathString("/Admin/Auth/Login");
                opt.LogoutPath = new PathString("/Admin/Auth/Logout");
                opt.Cookie = new CookieBuilder
                {
                    Name = "Recipes",
                    HttpOnly = true, // sadece http �zerinden cookie bilgilerine eri�ilebilir, js vs eri�ilemez.
                    SameSite = SameSiteMode.Strict, // Cross Site Requset Forgery sald�r�s� �nler. sahte istekler g�nderimi. kullan�c�n�n fark�nda olmadan
                                                    // coockie bilgileriyle servera istek g�nderilmesidir. Strict ile coockie bilgilerinin sadece kendi sitemizden geldi�inde i�lenmesi.
                    SecurePolicy = CookieSecurePolicy.SameAsRequest // SameAsRequest http ve https alabilir. Always yapacag�m bilgilerin sadece https �zerinden gelmesi. 
                };
                opt.SlidingExpiration = true; // Tekrar �ifre giri�i gerekmez.
                opt.ExpireTimeSpan = System.TimeSpan.FromHours(9); // S�resi.
                opt.AccessDeniedPath = new PathString("/Admin/Auth/AccessDenied");
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }

            app.UseSession();
            app.UseStaticFiles();
            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "Admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
                    );

                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
