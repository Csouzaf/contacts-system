using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.Models.auth.Model;
using Microsoft.IdentityModel.Tokens;

namespace api.auth.jwt
{
    public class JwtService
    {
        private string secureKey = "703273357638792F423F4528482B4D6251655468566D597133743677397A24432646294A404E635266556A586E5A7234753778214125442A472D4B6150645367566B59703373357638792F423F4528482B4D6251655468576D5A7134743777397A24432646294A404E635266556A586E3272357538782F4125442A472D4B6150";
        public string generateJwt(int id)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));

            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            
         
            
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, id.ToString()), //NOTE - User.Identify.Name
                    // new Claim(ClaimTypes.Name, usersAuth.Name)
                    //new Claim(ClaimTypes.Role, usersAuth.Role) - User.IsInRole
                  
                };
           
             
            var header = new JwtHeader(credentials);

            var payload = new JwtPayload(id.ToString(), null, claims, null , DateTime.UtcNow.AddHours(4));
          
            var securityToken = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }



        public JwtSecurityToken verifyJwt(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var key = Encoding.ASCII.GetBytes(secureKey);
            
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters(){
                
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false

            }, out SecurityToken validatedToken);
            
            return (JwtSecurityToken) validatedToken;
        
        }


    }   
      
}