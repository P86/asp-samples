using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PolicyBasedAuthentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Token : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Issue()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret token key"));

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.NameId, "Some user name"),
                new Claim(JwtRegisteredClaimNames.Birthdate, new DateTime(1986, 1, 1).ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, "Arek")
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature),
            });

            return Ok(tokenHandler.WriteToken(token));
        }
    }
}
