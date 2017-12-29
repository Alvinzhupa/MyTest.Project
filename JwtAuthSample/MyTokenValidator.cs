using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthSample
{
    public class MyTokenValidator : ISecurityTokenValidator
    {
        public bool CanValidateToken => true;

        public int MaximumTokenSizeInBytes { get; set; }

        public bool CanReadToken(string securityToken)
        {
            return true;
        }

        public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            validatedToken = null;
            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);

            //这里的用意应该是创建一个验证,返回到下一步中
            if (securityToken == "abc")
            {
                var claims = new Claim[]
                 {
                     new Claim(ClaimTypes.Name,"abc"),
                     new Claim(ClaimTypes.Role,"admin"),
                     new Claim(ClaimsIdentity.DefaultRoleClaimType,"user")
                 };
                identity.AddClaims(claims);
            }

            var principal = new ClaimsPrincipal(identity);

            return principal;
        }
    }
}
