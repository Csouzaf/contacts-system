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
        

        public async Task<List<ContactsModel>> findAll()
        {
            return await _usersDbContextModel.contactsModel.ToListAsync();
        }

        public async Task<ContactsModel> findById(int id)
        {
            return await _usersDbContextModel.contactsModel.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ContactsModel> createUser(ContactsModel contactsModel)
        {
            try{
                 await _usersDbContextModel.contactsModel.AddAsync(contactsModel);
                 await _usersDbContextModel.SaveChangesAsync();
                 return contactsModel;

            }catch(Exception e){
                throw new Exception("Não foi possível criar o usuário", e);
                
            }
          
        }

        public async Task<ContactsModel> updateUser(ContactsModel contactsModel, int id)
        {
         
           ContactsModel findUsersById = await findById(id);

           if(findById == null){
            throw new Exception("Não foi possível encontrar o usuário");
           }

           findUsersById.Nome = contactsModel.Nome;
           findUsersById.Email = contactsModel.Email;
           findUsersById.Telefone = contactsModel.Telefone;
           
           _usersDbContextModel.contactsModel.Update(findUsersById);
           
           await _usersDbContextModel.SaveChangesAsync();

           return findUsersById;
        }

        public async Task<bool> deleteUser(int id)
        {
            ContactsModel findUsersById = await findById(id);

            if(findUsersById == null)
            {
                throw new Exception("Não foi possível encontrar o usuário");
            }

            _usersDbContextModel.contactsModel.Remove(findUsersById);
            
            await _usersDbContextModel.SaveChangesAsync();

            return true;
            
        }

       
    }




}
