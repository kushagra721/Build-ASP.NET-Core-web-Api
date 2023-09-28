using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZwalks.API.Repo
{
    public class JwtTokenRepo : JwtInterface
    {
        private readonly IConfiguration configuration;

        public JwtTokenRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        string JwtInterface.CreateJwtToken(IdentityUser user, List<string> roles)
        {
           
            //create claims 
            var claim = new List<Claim>();
            claim.Add(new Claim(ClaimTypes.Email, user.Email));
            
            foreach(var role in roles)
            {
                claim.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],claim,expires:DateTime.Now.AddMinutes(15),signingCredentials:credentials);

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
