using System.Reflection.Metadata;
using System.Data.SqlTypes;
using api.auth.Data;
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

        //  usersAuth.Id = 
         _usersAuthDBContext.SaveChanges();

         return usersAuth;
        }
        
        
        catch(SqlNullValueException e){
      
          throw new SqlNullValueException ("Não foi possível criar o usuário", e);
        }
      }   
  
   //NOTE - Find user by email for it will be authenticated and to do crud
        public UsersAuth getByEmail(string email)
        {
        
          return _usersAuthDBContext.usersAuth.FirstOrDefault(u => u.Email == email );

        }

        public UsersAuth getName(string name)
        {
          return _usersAuthDBContext.usersAuth.FirstOrDefault(n => n.Name == name);
        }

        public UsersAuth getById(int id)
        {
          return _usersAuthDBContext.usersAuth.FirstOrDefault(u => u.Id == id);
        }


    }
}