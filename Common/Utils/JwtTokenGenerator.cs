using Common.Library.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Common.Library.Utils
{
    public static class JwtTokenGenerator
    {
        public static string GenerateJwtToken(JwtTokenGeneratorModel jwtTokenGeneratorModel)
        {
            string token = string.Empty;
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenGeneratorModel.Key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var securityToken = new JwtSecurityToken(
                    jwtTokenGeneratorModel.Issuer,
                    jwtTokenGeneratorModel.Audiance,
                    jwtTokenGeneratorModel.Claims,
                    jwtTokenGeneratorModel.NotBefore,
                    DateTime.UtcNow.AddMinutes(jwtTokenGeneratorModel.ExpireAfter),
                    credentials
                    );

                token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            }
            catch (Exception)
            {
                throw;
            }

            return token;
        }
    }
}
