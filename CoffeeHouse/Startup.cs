using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoffeeHouse.Data;
using CoffeeHouse.Mapping;
using CoffeeHouse.Repository.Implements;
using CoffeeHouse.Repository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeHouse
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            ////Specifies that the database context will use an in-memory database
            //services.AddDbContext<CoffeeDbContext>(opt => opt.UseInMemoryDatabase("CoffeeShop"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<CoffeeDbContext>().AddDefaultTokenProviders();

            //Connection to localdb
            var connection = @"Server=(localdb)\MSSQLLocalDB; Database=E:\DESKTOP\3\KDPM\COFFEEHOUSE.MDF;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<CoffeeDbContext>
                (options => options.UseSqlServer(connection));

            services.AddScoped<IDrinkRepository, DrinkRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ITableRepository, TableRepository>();
            services.AddScoped<IShoppingCartService>(sp => ShoppingCartService.GetCart(sp));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddMemoryCache();

            //AutoMapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/UnAuthorized";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseStatusCodePagesWithReExecute("/Error/{0}");
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                );
            });

            //DbInitializer.Seed(app);
            //DbInitializer.CreateRoles(serviceProvider).Wait();
            //CreateRoles(serviceProvider).Wait();
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string[] roleNames = { "Admin", "User", "Staff" };
            IdentityResult roleResult;
            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1 
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            IdentityUser user = await UserManager.FindByEmailAsync("admin@gmail.com");
            if (user == null)
            {
                user = new IdentityUser()
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                };
                await UserManager.CreateAsync(user, "Test@123");
            }
            await UserManager.AddToRoleAsync(user, "Admin");

            IdentityUser user1 = await UserManager.FindByEmailAsync("staff@gmail.com");
            if (user1 == null)
            {

                user1 = new IdentityUser()
                {
                    UserName = "staff@gmail.com",
                    Email = "staff@gmail.com",
                };
                await UserManager.CreateAsync(user1, "Test@123")
                ;
            }
            await UserManager.AddToRoleAsync(user1, "Staff");

            IdentityUser user2 = await UserManager.FindByEmailAsync("user@gmail.com");
            if (user2 == null)
            {
                user2 = new IdentityUser()
                {
                    UserName = "user@gmail.com",
                    Email = "user@gmail.com",
                };
                await UserManager.CreateAsync(user2, "Test@123")
                ;
            }
            await UserManager.AddToRoleAsync(user2, "User");
        }
    }
}
