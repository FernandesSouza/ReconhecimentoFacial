using DetectarFace.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DetectarFace.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _chave;

        public TokenService(IConfiguration config)
        {
            _chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["chaveSecreta"]!));

        }



        public string GerarToken(bool validado)
        {
            if (!validado)
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new Claim("validado", validado.ToString(), ClaimValueTypes.Boolean)
            };

            var credencials = new SigningCredentials(_chave, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials = credencials,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }


        }
           
    }




        



