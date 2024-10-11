using ExoBlazor_API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExoBlazor_API.Tools
{
    public class JwtGenerator
    {
        public static readonly string SecretKey = "La S3uLe difF3rence c0ncr3Te 4Vec dES bR1que5, c'e5T 9Ue v0uS aPpel32 ç4 d3s taRTe5";

        public string GenerateToken(User user)
        {
            // Signature
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            // Payload
            Claim[] myClaims = new Claim[]
            {
                new Claim("Login", user.Login),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
            };

            // Generate Token
            JwtSecurityToken token = new JwtSecurityToken(
                claims: myClaims,
                signingCredentials: credentials,
                expires: DateTime.Now.AddMinutes(15),
                issuer: "ExoBlazor.com"
                );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(token);
        }
    }
}
