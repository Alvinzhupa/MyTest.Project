using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MvcCookieAuthSample.Controllers
{
    public class AccountController : Controller
    {

        public IActionResult Login()
        {

            return View();
        }

  
        public IActionResult Register()
        {
            return View();
        }


        public IActionResult MakeLogin()
        {
            //3.指定验证成功的方式

            //3.1.需要先指定角色
            List<Claim> list = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"jesse"),
                new Claim(ClaimTypes.Role,"admin")
            };

            //3.2创建一个Identity
            //必须指定Authentication类型,否则不会识别到
            var claminIdentity = new ClaimsIdentity(list, CookieAuthenticationDefaults.AuthenticationScheme);

            //3.3注册
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claminIdentity));

            return Ok();
        }

        public IActionResult LoginOut()
        {

            //4.登出的方法
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok();
        }

        public IActionResult ImgDown()
        {
            WebClient my = new WebClient();
            byte[] mybyte;
            mybyte = my.DownloadData("http://pan.baidu.com/share/qrcode?w=150&h=150&url=html2canvas.hertzen.com/dist/html2canvas.js");
            //var ms = new MemoryStream(mybyte);
            // System.Drawing.Image img;
            // img = System.Drawing.Image.FromStream(ms);
            //ms.GetBuffer();
            // img.Save("D:\\a.gif", ImageFormat.Gif);   //保存


            //var bytes = StreamToBytes(s);
            string base64 = Convert.ToBase64String(mybyte);


            return Ok("<img src='" + base64 + "'>");
        }


    }


}