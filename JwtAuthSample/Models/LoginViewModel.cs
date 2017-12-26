﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace JwtAuthSample.Models
{
    public class LoginViewModel
    {
        [Required]
        public string User { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
