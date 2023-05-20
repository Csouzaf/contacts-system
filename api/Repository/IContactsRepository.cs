using api.Models;

namespace api.Repository

{
    public interface IContactsRepository
    {
        
      Task<UsersModel> createUser(UsersModel userModel);
      
    }
}