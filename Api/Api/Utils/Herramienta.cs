using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Api.Utils
{
    public class Herramienta
    {
        public static string EncriptarContrasena(string contrasena)
        {
            try
            {
                SHA256 sha256 = SHA256.Create();

                ASCIIEncoding encoding = new ASCIIEncoding();

                StringBuilder sb = new StringBuilder();

                byte[] stream = sha256.ComputeHash(encoding.GetBytes(contrasena));

                for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);

                return sb.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string GenerarToken(IConfiguration config, string usuario)
        {
            try
            {
                var token = new JwtSecurityToken(config["Jwt:Issuer"],
                    config["Jwt:Audience"],
                    claims: new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuario),
                    },
                    expires: DateTime.Now.AddMinutes(double.Parse(config["Jwt:ExpireTime"])),
                    signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"])),
                    SecurityAlgorithms.HmacSha256));

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
