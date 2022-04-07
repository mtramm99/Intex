using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Intex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.ML.OnnxRuntime;
using System.Net;

namespace Intex
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration temp)
        {
            Configuration = temp;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //services.AddHttpsRedirection(options =>
            //{
            //    options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
            //    options.HttpsPort = 5001;
            //});

            string ebdb = Environment.GetEnvironmentVariable("ebdb");
            string identity = Environment.GetEnvironmentVariable("identity");

            //services.AddDbContext<CollisionsDbContext>(options =>
            //{
            //    options.UseMySql(ebdb);
            //}); ///////////////////////////////////////////////////////////////////////// Use in production

            services.AddDbContext<CollisionsDbContext>(options =>
            {
                options.UseMySql(Configuration["ConnectionStrings:CollisionsDbConnection"]);
            });


            services.AddSingleton<InferenceSession>(
                new InferenceSession("wwwroot/crashModel.onnx"));

            services.AddHsts(options =>
            {
               // options.Preload = true;
                //options.IncludeSubDomains = true;
                //options.MaxAge = TimeSpan.FromDays(60);
                //options.ExcludedHosts.Add("utahcollisionresources.com");
            });

            //services.AddSingleton<InferenceSession>(
            //    new InferenceSession("crashModel.onnx"));


            //For admin login
            //services.AddDbContext<AppIdentityDBContext>(options =>
            //    options.UseMySql(identity)); //////////////////////////////////// Use in production


            services.AddDbContext<AppIdentityDBContext>(options =>
                options.UseMySql(Configuration["ConnectionStrings:IdentityConnection"]));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDBContext>();

            services.AddScoped<ICollisionsRepository, EFCollisionsRepository>();

            services.AddRazorPages();

            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddServerSideBlazor();

            // Require better 
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 12;
                options.Password.RequiredUniqueChars = 1;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseHsts();
            //}

            app.UseHsts();


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            // For admin login
            app.UseAuthentication();
            app.UseAuthorization();

            // CSP header
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Content-Security-Policy", "default-src 'self';");
                await next();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("typepage",
                    "{collisionType}/Page{pageNum}",
                    new { Controller = "Home", action = "Index" });

                endpoints.MapControllerRoute("Paging",
                    "Page{pageNum}",
                    new { Controller = "Home", action = "Index", pageNum = 1 });

                endpoints.MapControllerRoute("type",
                    "{collisionType}",
                    new { Controller = "Home", action = "Index", pageNum = 1 });

                endpoints.MapDefaultControllerRoute();

                endpoints.MapRazorPages();

                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/admin/{*catchall}", "/Admin/Index");
            });

            IdentitySeedData.EnsurePopulated(app);
        }        
    }
}
