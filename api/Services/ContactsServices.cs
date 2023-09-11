using api.Errors;
using api.Models;
using api.Models.auth.Data;
using api.Models.auth.Model;
using api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{

    public class ContactsServices : IContactsRepository
    {
        private readonly UsersDbContextModel _usersDbContextModel;
       
        
        public ContactsServices(UsersDbContextModel usersDbContextModel)
        {
            _usersDbContextModel = usersDbContextModel;
        }
        

        public List<ContactsModel> findAll()
        {
            return _usersDbContextModel.contactsModel.ToList();
        }

        public ContactsModel findById(int id)
        {
            return _usersDbContextModel.contactsModel.FirstOrDefault(x => x.Id == id);
        }

        public  ContactsModel createUser(ContactsModel contactsModel)
        {
            try{
                 _usersDbContextModel.contactsModel.Add(contactsModel);
                 _usersDbContextModel.SaveChanges();
                 return contactsModel;

            }catch(Exception e){
                throw new Exception("Não foi possível criar o usuário", e);
                
            }
          
        }

        public  ContactsModel updateUser(ContactsModel contactsModel, int id)
        {
         
           ContactsModel findUsersById = findById(id);

           if(findById == null){
            throw new Exception("Não foi possível encontrar o usuário");
           }

           findUsersById.Nome = contactsModel.Nome;
           findUsersById.Email = contactsModel.Email;
           findUsersById.Telefone = contactsModel.Telefone;
           
           _usersDbContextModel.contactsModel.Update(findUsersById);
           
           _usersDbContextModel.SaveChanges();

           return findUsersById;
        }

        public  bool deleteUser(int id)
        {
            ContactsModel findUsersById = findById(id);

            if(findUsersById == null)
            {
                throw new Exception("Não foi possível encontrar o usuário");
            }

            _usersDbContextModel.contactsModel.Remove(findUsersById);
            
            _usersDbContextModel.SaveChanges();

            return true;
            
        }

       
    }




}
