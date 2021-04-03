using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Kaizen.Models;
using Kaizen.Utilities.Services;

namespace Kaizen.Utilities
{
    public class JwtGenerator : IJwtToken
    {

       public readonly SymmetricSecurityKey _key;
       public JwtGenerator(IConfiguration config)
       {
          _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokenkey"]));
       }

        public string createToken(AppUser user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.NameId, user.UserName));

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor();
            tokenDescriptor.Subject = new ClaimsIdentity(claims);
            tokenDescriptor.Expires = DateTime.Now.AddDays(8);
            tokenDescriptor.SigningCredentials = creds;

            var tokenhandler = new JwtSecurityTokenHandler();
            var token = tokenhandler.CreateToken(tokenDescriptor);
            return tokenhandler.WriteToken(token);
        }


    }
}