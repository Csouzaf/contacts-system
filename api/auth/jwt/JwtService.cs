using System.Reflection.Metadata.Ecma335;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.Models.auth.Data;
using api.Models.auth.Model;
using Microsoft.AspNetCore.Mvc;

using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;


namespace api.auth.jwt
{
    public class JwtService
    {
        
        private readonly IUsersAuthRepository _usersAuthRepository;
        private readonly JwtService _jwtService;

        private readonly UserManager<IdentityUser> _userManager;
        
        public JwtService(IUsersAuthRepository usersAuthRepository, UserManager<IdentityUser> userManager)
        {
            _usersAuthRepository = usersAuthRepository;
            _userManager = userManager;
        
        }
 
        private string masterKey = "703273357638792F423F4528482B4D6251655468566D597133743677397A24432646294A404E635266556A586E5A7234753778214125442A472D4B6150645367566B59703373357638792F423F4528482B4D6251655468576D5A7134743777397A24432646294A404E635266556A586E3272357538782F4125442A472D4B6150";
        public string generateJwt(int id, string Name)
        {
            var encodeKey = Encoding.ASCII.GetBytes(masterKey);

            var verifySymmetricBytesEncodeKey = new SymmetricSecurityKey(encodeKey);
            
            var verifySimmetricBytesAndAlgorithmsSignature = new SigningCredentials(verifySymmetricBytesEncodeKey, SecurityAlgorithms.HmacSha256Signature);

            var username = _usersAuthRepository.getName(Name);
            
     
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()), //NOTE - User.Identify.Name
                new Claim(ClaimTypes.Name, username.Name )
                
                // new Claim(ClaimTypes.Name, usersAuth.Name)
                //new Claim(ClaimTypes.Role, usersAuth.Role) - User.IsInRole
            };


            var header = new JwtHeader(verifySimmetricBytesAndAlgorithmsSignature);

            var payload = new JwtPayload(id.ToString(), null, claims, null, DateTime.UtcNow.AddHours(4));

            var securityToken = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

//SECTION 2 - Second method using UsersAuth for public string generateJwt(UsersAuth usersAuth)
        // public string generateJwt(UsersAuth usersAuth)
        // {
        //     var encodeKey = Encoding.ASCII.GetBytes(masterKey);

        //     var verifySymmetricBytesEncodeKey = new SymmetricSecurityKey(encodeKey);
            
        //     var verifySimmetricBytesAndAlgorithmsSignature = new SigningCredentials(verifySymmetricBytesEncodeKey, SecurityAlgorithms.HmacSha256Signature);


        //     var claims = new List<Claim>
        //     {
        //         new Claim(ClaimTypes.NameIdentifier, usersAuth.Name) //NOTE - User.Identify.Name
        //         // new Claim(ClaimTypes.Name, usersAuth.Name)
        //         //new Claim(ClaimTypes.Role, usersAuth.Role) - User.IsInRole
        //     };


        //     var header = new JwtHeader(verifySimmetricBytesAndAlgorithmsSignature);

        //     var payload = new JwtPayload(usersAuth.Id.ToString(), null, claims, null, DateTime.UtcNow.AddHours(4));

        //     var securityToken = new JwtSecurityToken(header, payload);

        //     return new JwtSecurityTokenHandler().WriteToken(securityToken);
        // }

    //SECTION 3- other way

    //    public string generateJwt(int id)
    //     {
    //         var tokenHandler = new JwtSecurityTokenHandler();

    //         var encodeKey = Encoding.ASCII.GetBytes(masterKey);

    //         var tokenDescriptor = new SecurityTokenDescriptor
    //         {
    //             Subject = new ClaimsIdentity(new[] 
    //             {
    //                 new Claim(ClaimTypes.NameIdentifier, id.ToString())
    //             }),
               
    //             Expires = DateTime.UtcNow.AddHours(3),
    //             SigningCredentials =  new SigningCredentials(new SymmetricSecurityKey(encodeKey), SecurityAlgorithms.HmacSha256Signature),

    //         };

    //         var token = tokenHandler.CreateToken(tokenDescriptor);
            
    //         return tokenHandler.WriteToken(token);
              
    //     }


        public JwtSecurityToken verifyJwt(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var encodeKey = Encoding.ASCII.GetBytes(masterKey);
            
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters(){
                
                IssuerSigningKey = new SymmetricSecurityKey(encodeKey),
                ValidateIssuerSigningKey = true,
                //NOTE - Who issue the token in a security way
                ValidateIssuer = false,
               //NOTE - Who the token is meant for 
                ValidateAudience = false

            }, out SecurityToken validatedToken);
            
            return (JwtSecurityToken) validatedToken;
        
        }
    }   
      
}