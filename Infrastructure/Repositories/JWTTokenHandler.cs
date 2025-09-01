using Application.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;
using Domain.Entities;
using System.Data;

namespace Infrastructure.Repositories
{
    public class JWTTokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;
        public JWTTokenHandler(IConfiguration _configuration)
        {

            this._configuration = _configuration;
        }

        public Task<string> CreateToken(Customer customer)
        {
            //create claim
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, customer.FirstName));
            claims.Add(new Claim(ClaimTypes.Surname, customer.LastName));
            claims.Add(new Claim(ClaimTypes.Email, customer.Email));

            // add roles to claim
            claims.Add(new Claim(ClaimTypes.Role, ((RoleStatus)customer.Role).ToString()));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
                );

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
