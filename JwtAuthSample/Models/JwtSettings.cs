using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthSample.Models
{
    public class JwtSettings
    {
        /// <summary>
        /// 谁颁发的
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 给谁使用
        /// </summary>
        public string Audience { get; set; }

        public string SecretKey { get; set; }
    }
}
