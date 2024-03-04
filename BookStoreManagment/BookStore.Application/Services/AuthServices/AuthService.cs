using Microsoft.Extensions.Configuration;   // Iconfiguration ishlashi uchun
using Microsoft.IdentityModel.Tokens;       // SymmetricSecurityKey, SigningCredentials, EpochTime ishlashi uchun
using BookStore.Domain.Entities.DTOs;        // User model ishlashi uchun 
using System.Globalization;                 // CultureInfo ishlashi uchun    
using System.IdentityModel.Tokens.Jwt;      // JwtRegisteredClaimNames, JwtSecurityToken ishlashi uchun
using System.Security.Claims;               // Claim ishlashi uchun 
using System.Text;
using System.Text.Json;                          // Encoding ishlashi uchun

namespace BookStore.Application.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private IConfiguration _config;
        public AuthService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(UserDTO userDTO)
        {
            IEnumerable<int> permissionsId=new List<int>();
            if (userDTO.Role == "Admin")
                permissionsId = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
            else if (userDTO.Role == "Client")
                permissionsId = new List<int>() { 5, 6, 7, 8 };

            string permmisionJson=JsonSerializer.Serialize(permissionsId);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]!));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            int expirePeriod = int.Parse(_config["JWT:Expire"]!);

            List<Claim> claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,EpochTime.GetIntDate(DateTime.UtcNow).ToString(CultureInfo.InvariantCulture),ClaimValueTypes.Integer64),

                new Claim("UserName",userDTO.UserName!),
                new Claim("Password",userDTO.Password!),
                new Claim(ClaimValueTypes.Email,userDTO.Email),
                new Claim(ClaimTypes.Role,userDTO.Role),
                new Claim("permissions",permmisionJson)
            };

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudence"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirePeriod),
                signingCredentials: credentials);

            string Token = new JwtSecurityTokenHandler().WriteToken(token);
            return Token;
        }
    }
}
