using api.Models;

namespace api.Repository

{
    public interface IContactsRepository
    {
     
      Task<List<ContactsModel>> findAll();
      Task<ContactsModel> findById(int id);
      Task<ContactsModel> createUser(ContactsModel contactsModel);
      Task<ContactsModel> updateUser(ContactsModel contactsModel, int id);
      Task<bool> deleteUser(int id);
    }
}