using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Thoughtful.Domain.Model;

namespace Thoughtful.Dal
{
    public class JwtService
    {
        private readonly IConfiguration _config;
        public JwtService(IConfiguration config)
        {
            _config = config;
        }
        public string createToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,user.Email)
            };

            //key to hash token

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtConfig:Key"]));

            //credentials to assign toekn and encrypt the key
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims), // subject contains the informations that will in token
                Expires = DateTime.Now.AddDays(7), //token validity
                SigningCredentials = creds
            };

            //token handler to create the token
            var tokenHandler = new JwtSecurityTokenHandler();

            //create token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            //Return through tokekHandler token string 
            return tokenHandler.WriteToken(token);
        }
    }
}
