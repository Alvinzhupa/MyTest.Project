using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using JwtAuthSample.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JwtAuthSample
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
            //1.添加验证配置

            //在appSettings的配置中要设置好,这个配置需要在第一级中
            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));//注册并注入

            var jwtSettings = new JwtSettings();
            Configuration.Bind("JwtSettings", jwtSettings); //绑定实例

            //请一定要注意这个和Authorization的区别
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;//不知道是干啥用的
            })
            .AddJwtBearer(o =>
            {
                //o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                //{
                //    ValidIssuer = jwtSettings.Issuer,
                //    ValidAudience = jwtSettings.Audience,
                //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)) //对称加密
                //};

                //以下两步是修改 token验证的方式
                o.SecurityTokenValidators.Clear();//清楚验证数组
                o.SecurityTokenValidators.Add(new MyTokenValidator()); //添加验证方式

                o.Events = new JwtBearerEvents()
                {
                    //这里是修改获取token的方式
                    OnMessageReceived = (context =>
                    {
                        var token = context.Request.Headers["mytoken"];
                        context.Token = token.FirstOrDefault();
                        return Task.CompletedTask;
                    })
                };
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //2.使用验证
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
