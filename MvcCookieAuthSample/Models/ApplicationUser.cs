using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MvcCookieAuthSample.Models
{
    /// <summary>
    /// 继承至Identity的组件,这个类就是用户的信息类,包含了帐号密码,ID等
    /// </summary>
    public class ApplicationUser : IdentityUser<int>//如果要改变ID,则直接加到这个泛型中,默认是GUID,
    {
        
    }
}
