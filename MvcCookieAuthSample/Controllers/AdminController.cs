using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcCookieAuthSample.Models;
using Microsoft.AspNetCore.Authorization;

namespace MvcCookieAuthSample.Controllers
{
    public class AdminController : Controller
    {
        //4.����Ҫ�ļ���֤�ĵ�ַ�ϼ��ϱ�ǩ
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}