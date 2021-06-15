using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

namespace AspTable
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
            services.AddDbContext<DataContext>(
                options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

            //services.AddIdentity<ApplicationUser,Role>().AddEntityFrameworkStores<DbContext>();
            services.AddIdentity<IdentityUser, IdentityRole>()
               .AddEntityFrameworkStores<DataContext>()
               .AddDefaultTokenProviders();
            services.AddControllersWithViews();
            services.AddControllers().AddNewtonsoftJson();
            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddRazorPages().AddNewtonsoftJson();
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.MimeTypes = new[]
                {
            // Default
            "text/plain",
            "text/css",
            "application/javascript",
            "text/html",
            "application/xml",
            "text/xml",
            "application/json",
            "text/json",
            // Custom
            "image/svg+xml"
           };
            });
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                //About Us
                endpoints.MapControllerRoute(
                    name: "AboutUs",
                    pattern: "about-us",
                    defaults: new { controller = "Home", action = "AboutUs" });
                //terms-of-use
                endpoints.MapControllerRoute(
                     name: "TearmsOfUse",
                     pattern: "terms-of-use",
                     defaults: new { Controller = "Home", Action = "TearmsOfUse" });
                //Blog details
                endpoints.MapControllerRoute(
                        name: "BlogDetails",
                        pattern: "blog/{value}",
                        defaults: new { Controller = "Home", Action = "BlogDetails" });

            });
        }
    }
}
