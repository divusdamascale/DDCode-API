using DDCode.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DDCode.API.Repositories.Implementation
{
    public class TokenRepository(IConfiguration config) : ITokenRepository
    {
        public string CreateToken(IdentityUser user,List<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Email),
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role,role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));

            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims:claims,
                expires:DateTime.Now.AddMinutes(15),
                signingCredentials:credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
