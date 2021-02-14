using AutoMapper;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
            services.AddDbContext<RecipeContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("LocalDb"))
            );

            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IFoodCategoryRepository, FoodCategoryRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();

            services.AddIdentity<User, Role>(opt =>
            {
                // Kullanýcý Þifre kurallarý
                opt.Password.RequireDigit = false; // þifrede rakam olmalý mý ?
                opt.Password.RequiredLength = 4; // þifre uzunluðu en az kaç olmalý ?
                opt.Password.RequiredUniqueChars = 0; // Özel karakterlerden farklý farklý kaç tane olmak zorunda ?
                opt.Password.RequireNonAlphanumeric = false; // özel karakter olmak zorunda mý?
                opt.Password.RequireLowercase = false; // Kucuk harf zorunlu mu ?
                opt.Password.RequireUppercase = false; // Buyuk harf zorunlu mu ?

                // Kullanýcý adý ve email ayarlarý
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+$"; // kullanýcý adýnda hangi karakterler olacaksa yazýyoruz.
                opt.User.RequireUniqueEmail = true; // 1 email ile tek kayýt olabilir.
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
                    HttpOnly = true, // sadece http üzerinden cookie bilgilerine eriþilebilir, js vs eriþilemez.
                    SameSite = SameSiteMode.Strict, // Cross Site Requset Forgery saldýrýsý önler. sahte istekler gönderimi. kullanýcýnýn farkýnda olmadan
                                                    // coockie bilgileriyle servera istek gönderilmesidir. Strict ile coockie bilgilerinin sadece kendi sitemizden geldiðinde iþlenmesi.
                    SecurePolicy = CookieSecurePolicy.SameAsRequest // SameAsRequest http ve https alabilir. Always yapacagým bilgilerin sadece https üzerinden gelmesi. 
                };
                opt.SlidingExpiration = true; // Tekrar þifre giriþi gerekmez.
                opt.ExpireTimeSpan = System.TimeSpan.FromHours(9); // Süresi.
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
