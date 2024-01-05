using FinanceDataAPI.Entidades;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FinanceDataAPI.Services
{
    public class TokenService
    {
        public static object GenerateToken(Employee employee)
        {
            // Gerar uma chave de 256 bits
            byte[] keyBytes = Key.Get256BitKey(); // 256 bits / 8 = 32 bytes

            // Usar a chave gerada para assinar o token JWT
            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim("employeeId", employee.id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfig);
            var tokenString = tokenHandler.WriteToken(token);

            return new
            {
                token = tokenString,
                //key = Convert.ToBase64String(keyBytes) // Retorna a chave como uma string base64 para armazenamento seguro
            };
        }
    }
}
