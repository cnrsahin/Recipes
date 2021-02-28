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
                // Kullanici Sifre kurallari
                opt.Password.RequireDigit = false; // Sifrede rakam olmali mi ?
                opt.Password.RequiredLength = 4; // Sifre uzunlugu en az kac olmali ?
                opt.Password.RequiredUniqueChars = 0; // Ozel karakterlerden farkli farkli kac tane olmak zorunda ?
                opt.Password.RequireNonAlphanumeric = false; // Ozel karakter olmak zorunda mi?
                opt.Password.RequireLowercase = false; // Kucuk harf zorunlu mu ?
                opt.Password.RequireUppercase = false; // Buyuk harf zorunlu mu ?

                // Kullanici adi ve email ayarlari
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+$"; // kullanici adinda hangi karakterler olacaksa yaziyoruz.
                opt.User.RequireUniqueEmail = true; // 1 email ile tek kayit olabilir.
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
                    HttpOnly = true, // sadece http uzerinden cookie bilgilerine erisilebilir, js vs erisilemez.
                    SameSite = SameSiteMode.Strict, // Cross Site Requset Forgery saldirisi onler. 
                                                    // sahte istekler gonderimi. kullanicinin farkinda olmadan
                                                    // coockie bilgileriyle servera istek gonderilmesidir. 
                                                    // Strict ile coockie bilgilerinin sadece kendi sitemizden geldiginde islenmesi.
                    SecurePolicy = CookieSecurePolicy.SameAsRequest // SameAsRequest http ve https alabilir. Always yapacagim bilgilerin sadece https uzerinden gelmesi icin. 
                };
                opt.SlidingExpiration = true; // Tekrar sifre girisi gerekmez.
                opt.ExpireTimeSpan = System.TimeSpan.FromHours(9); // Suresi.
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
