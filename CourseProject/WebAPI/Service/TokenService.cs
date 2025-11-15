using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer.DTO_s;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public class TokenService
    {
        private readonly IConfiguration _config;
        public TokenService(IConfiguration config)
        {
            _config = config;
        }
        public string GenerateToken(UserDto user)
        {
            var key = Encoding.UTF8.GetBytes(_config["JWT:Key"]);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier, user.FullName),
                new(ClaimTypes.Role, user.RoleName),
            };
            var token = new JwtSecurityToken
            (
                claims: claims,
                signingCredentials: credentials,
                expires: DateTime.Now.AddMinutes(30)
            );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
