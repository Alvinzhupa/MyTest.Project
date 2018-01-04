using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using MvcCookieAuthSample.Data;
using Microsoft.EntityFrameworkCore;
using MvcCookieAuthSample.Models;
using Microsoft.AspNetCore.Identity;

namespace MvcCookieAuthSample
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
            services.AddMvc();
            //添加数据库连接配置
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]);
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>()//注入配置
                .AddEntityFrameworkStores<ApplicationDbContext>()//添加EnetityFramework的存储的对象
                .AddDefaultTokenProviders()//在命名空间Microsoft.AspNetCore.Identity下
                ;

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(configureOptions =>
                {
                    configureOptions.LoginPath = "/Account/Login";
                });

            services.Configure<IdentityOptions>(config =>
            {
                config.Password.RequiredLength = 8;
                config.Password.RequireUppercase = false;
                config.Password.RequireLowercase = false;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            //2.指定添加验证
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
