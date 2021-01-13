using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using WebDinner.Models;
using WebDinner.Models.ViewModels;
using WebDinner.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;

namespace WebDinner
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ProductsDBContext>(opts => opts
                .UseSqlServer(Configuration["Data:Products:ConnectionString"]));

            services.AddDbContext<AppIdentityDbContext>(opts => opts
                .UseSqlServer(Configuration["Data:ProductIdentity:ConnectionString"]));
            services.AddIdentity<IdentityUser, IdentityRole>(opts =>
                 opts.User.AllowedUserNameCharacters = "абвгдежзийклмнопрстуфхцчшщьъ ыэюяАБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЭЮЯ" +
                 "ABCDEFGHIJKLMNOPQRSTUWVXYZabcdefghijklmnopqrstuwvxyz")
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IMealRepository, MealRepository>();
            services.AddTransient<IFoodRepository, FoodRepository>();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IOrderRepository, OrderRepository>();

            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);
            services.AddMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();
            app.UseMvc(routes => {
                routes.MapRoute(name: "pagination", template: "options/Page{currentPage}", 
                    defaults: new { Controller = "Home", action = "GetFirstDishes" });
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
            });
            IdentitySeedData.EnsurePopulated(app);
        }
    }
}
