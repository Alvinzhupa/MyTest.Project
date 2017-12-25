using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.AspNetCore.Routing;

namespace WebHostTest
{


    public class Startup
    {
        public static int times { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IConfiguration configuration, IApplicationLifetime lifetime)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //MapGet 需要先引入命名空间 using Microsoft.AspNetCore.Routing;
            app.UseRouter(builder => builder.MapGet("action", async context =>
            {
                await context.Response.WriteAsync("this is action..");
            }));

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("11111111");
                await next.Invoke();//如果这一步不返回,那么后续的管道将不会执行
            });

            app.Use(next =>
            {
                return async (context) =>
                {
                    await context.Response.WriteAsync("abc");
                    await next(context);//如果这一步不返回,那么后续的管道将不会执行
                };
            });

            app.UseRouter(c => c.MapGet("action2", async context =>
             {
                 await context.Response.WriteAsync("actionre2");
             }));

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("发大水富家大室开放进口零食大荆防颗粒电视剧啊六块腹肌");
            });
        }
    }
}
