using E_Commerce_Core.Enities.Identity;
using E_Commerce_Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Services
{
    public class TokenService : ITokenService
    {
        //private readonly UserManager<ApplicationUser> _userManager;

        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        

        public  string GenerateToken(ApplicationUser user)
        {
            
            var claims = new List<Claim> { 
            
                new Claim(ClaimTypes.Email,user.Email),
                new (ClaimTypes.Name,user.DisplayName)
            
            };

            //var roles= await _userManager.GetRolesAsync(user);
            //claims.AddRange(roles.Select(role=>new Claim(ClaimTypes.Role,role))) ;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));

            var cerdentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _configuration["Token:Issuer"],
                Audience = _configuration["Token:Audience"],
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = cerdentials,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token= tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);


            throw new NotImplementedException();
        }
    }
}
