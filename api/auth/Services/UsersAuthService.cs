
using api.Models.auth.Data;
using api.Models.auth.Model;
using Microsoft.EntityFrameworkCore;
namespace api.Models.auth.Services
{
    public class UsersAuthService : IUsersAuthRepository
    {

      private readonly UsersAuthDBContext _usersAuthDBContext;
  
      public UsersAuthService(UsersAuthDBContext usersAuthDBContext) 
      {
        _usersAuthDBContext = usersAuthDBContext;
       
      }

      public UsersAuth Create(UsersAuth usersAuth)
      {
        try{
          
         _usersAuthDBContext.usersAuth.Add(usersAuth);
         usersAuth.Id = _usersAuthDBContext.SaveChanges();

         return usersAuth;
        }
        
        catch(Exception e){
          throw new Exception ("Não foi possível criar o usuário", e);
        }
      }   
  
        public UsersAuth getByEmail(string email)
        {
        
          return _usersAuthDBContext.usersAuth.FirstOrDefault(u => u.Email == email );

        }

        public UsersAuth getById(int id)
        {
          return _usersAuthDBContext.usersAuth.FirstOrDefault(u => u.Id == id);
        }


    }
}