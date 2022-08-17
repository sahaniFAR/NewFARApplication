using FARApplication.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FARApplication.Web
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
            // services.AddControllers()
            services.AddControllersWithViews();

            // Add Filter to the container.
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<AuthorizeActionFilterAttribute>();
            });

            services.AddHttpContextAccessor();




            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                // not necessary
                //options.MinimumSameSitePolicy = SameSiteMode.None;
            })
            .AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;

                options.IdleTimeout = TimeSpan.FromMinutes(55);
            });
            services.AddSingleton<IConfiguration>(Configuration);

            //   services.AddMvc(options => options.EnableEndpointRouting = false);

            //services.AddDbContext<FARContext>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("FARDatabase")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }


            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}