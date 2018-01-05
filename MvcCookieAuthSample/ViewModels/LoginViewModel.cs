using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCookieAuthSample.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "必须填")]

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required(ErrorMessage = "必须填")]
        
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
    }
}
