using api.auth.Data;
using api.auth.Model;
using Microsoft.EntityFrameworkCore;

namespace api.auth.Services
{
    public class AuthUserEmailService : IAuthUserEmailRepository
    {
        private readonly AuthUserEmailDbContext _authUserEmailDbContext;

        public AuthUserEmailService(AuthUserEmailDbContext authUserEmailDbContext)
        {
            _authUserEmailDbContext = authUserEmailDbContext;
        }

        public AuthUserEmail findById(int id)
        {
            return _authUserEmailDbContext.authUserEmails.FirstOrDefault(x=> x.Id == id);
            
        }
        public AuthUserEmail Create(AuthUserEmail authUserEmail)
        {
            try{

       
            _authUserEmailDbContext.authUserEmails.Add(authUserEmail);
            authUserEmail.Id = _authUserEmailDbContext.SaveChanges();
            // authUserEmail.UserAuthId = _authUserEmailDbContext.SaveChanges();
            

            return authUserEmail; 

            }
            
            catch(Exception e){
                throw new Exception("Error", e);
            }
        }

        public AuthUserEmail Update(int id, AuthUserEmail authUserEmail)
        {
            AuthUserEmail findUserById = findById(id);

            if(findById == null)
            {
                throw new Exception("User not found");
            }    
            
            findUserById.Email = authUserEmail.Email;
            findUserById.UserAuthId = authUserEmail.UserAuthId;

            _authUserEmailDbContext.authUserEmails.Update(findUserById);
            _authUserEmailDbContext.SaveChanges();
            
            return findUserById;
            
        
            
        }

        


    }
}