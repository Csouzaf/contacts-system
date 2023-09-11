using api.Models;

namespace api.Repository

{
    public interface IContactsRepository
    {
     
      List<ContactsModel> findAll();
      ContactsModel findById(int id);
      ContactsModel createUser(ContactsModel contactsModel);
      ContactsModel updateUser(ContactsModel contactsModel, int id);
      bool deleteUser(int id);
    }
}