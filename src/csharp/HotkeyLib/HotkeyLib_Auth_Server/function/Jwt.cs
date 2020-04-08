using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace HotkeyLib_Auth_Server.Function
{
    public static class Jwt
    {
        /// <summary>
        /// 새로운 jwt를 만듭니다.
        /// </summary>
        /// <param name="securityKey">암호화 키</param>
        /// <param name="claims">항목</param>
        /// <returns>jwt 값</returns>
        public static string NewJwt(string securityKey, Dictionary<string, string> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.Select(s => new Claim(s.Key, s.Value)).ToArray()),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey)),
                    SecurityAlgorithms.HmacSha512),
                Expires = DateTime.UtcNow.AddHours(9).AddYears(200),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        /// <summary>
        /// jwt 의 유효성을 확인합니다.
        /// </summary>
        /// <param name="authToken">jwt 값</param>
        /// <param name="key">암호화 키</param>
        /// <returns>유효한 토큰은 true 아니면 false 를 반환합니다.</returns>
        public static bool ValidateToken(string authToken, string key)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = GetValidationParameters(key);

                SecurityToken validatedToken;
                IPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static TokenValidationParameters GetValidationParameters(string key)
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = false,
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidIssuer = "Sample",
                ValidAudience = "Sample",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
            };
        }

        /// <summary>
        /// 토큰의 값을 읽어옵니다.
        /// 토큰의 유효성은 검사하지 않습니다.
        /// </summary>
        /// <param name="jwtToken">jwt 값</param>
        /// <param name="algorithmType">암호화 타입</param>
        /// <returns>토큰의 값 또는 null</returns>
        public static Dictionary<string, string> GetDataFromToken(string jwtToken, string algorithmType = "HS512")
        {
            var jwtSecurityToken = new JwtSecurityToken(jwtEncodedString: jwtToken);
            if (jwtSecurityToken.SignatureAlgorithm == algorithmType)
            {
                return jwtSecurityToken.Claims.ToDictionary(s => s.Type, s => s.Value);
            }

            return null;
        }
    }
}
