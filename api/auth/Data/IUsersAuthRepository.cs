using api.Models.auth.Model;

namespace api.Models.auth.Data
{
    public interface IUsersAuthRepository
    {
        UsersAuth Create(UsersAuth usersAuth);
        UsersAuth getByEmail(string Email);
        UsersAuth getById(int id);
    //  '   List<UsersAuth> getUsers();'

    
    }
}