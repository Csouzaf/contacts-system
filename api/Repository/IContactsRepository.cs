using api.Models;

namespace api.Repository

{
    public interface IContactsRepository
    {
     
      Task<List<UsersModel>> findAll();
      Task<UsersModel> findById(int id);
      Task<UsersModel> createUser(UsersModel userModel);
      Task<UsersModel> updateUser(UsersModel usersModel, int id);
      Task<bool> deleteUser(int id);
    }
}