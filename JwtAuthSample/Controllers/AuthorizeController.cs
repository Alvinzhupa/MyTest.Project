using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JwtAuthSample.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;//这里包需要重新引用的
using System.IdentityModel.Tokens.Jwt;

namespace JwtAuthSample.Controllers
{
    [Produces("application/json")]
    [Route("api/Authorize")]
    public class AuthorizeController : Controller
    {
        private JwtSettings _jwtSettings;


        public AuthorizeController(IOptions<JwtSettings> jwtSettingsAccesser)
        {
            //依赖注入获取配置
            //这里首先需要在StartUp那里进行注册与注入
            _jwtSettings = jwtSettingsAccesser.Value;
        }

        // GET: api/Authorize
        [HttpGet]
        public IActionResult GetToken(LoginViewModel model)
        {
            //这里是判断传入的参数是否验证通过
            if (ModelState.IsValid)
            {
                //这里就是登录密码的判断
                if (model.Password != "123" || model.User != "abc")
                {
                    return BadRequest();
                }


                //以下步骤是颁发token

                //1.new 一个claim
                var claim = new Claim[]
                {
                    new Claim(ClaimTypes.Name,"abc"),
                    new Claim(ClaimTypes.Role,"admin")
                };

                //对称加密Key
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
                //使用加密的方式
                var sreds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //生成tocken的配置,上面那么多操作都是为了创建token作铺垫
                var tocken = new JwtSecurityToken
                    (
                   issuer: _jwtSettings.Issuer,
                   audience: _jwtSettings.Audience,
                   claims: claim,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: sreds
                    );
                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(tocken) }); //这里调用jwt token生成方法 生成token
            }

            return BadRequest();
        }
    }
}
