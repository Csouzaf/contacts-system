using api.Errors;
using api.Models;
using api.Repository;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{

    public class ContactsServices : IContactsRepository
    {
        private readonly UsersDbContextModel _usersDbContextModel;

        public async Task<List<UsersModel>> findAll()
        {
            return await _usersDbContextModel.usersModels.ToListAsync();
        }

        public async Task<UsersModel> findById(int id)
        {
            return await _usersDbContextModel.usersModels.FirstOrDefaultAsync(x => x.Id == id);
        }

        public ContactsServices(UsersDbContextModel usersDbContextModel)
        {
            _usersDbContextModel = usersDbContextModel;
        }
        
        
        public async Task<UsersModel> createUser(UsersModel userModel)
        {
            try{
                 await _usersDbContextModel.usersModels.AddAsync(userModel);
                 await _usersDbContextModel.SaveChangesAsync();
                 return userModel;

            }catch(Exception e){
                throw new Exception("Não foi possível criar o usuário", e);
                
            }
          
        }

        public async Task<UsersModel> updateUser(UsersModel usersModel, int id)
        {
         
           UsersModel findUsersById = await findById(id);

           if(findById == null){
            throw new Exception("Não foi possível encontrar o usuário");
           }

           findUsersById.Nome = usersModel.Nome;
           findUsersById.Email = usersModel.Email;
           findUsersById.Telefone = usersModel.Telefone;
           
           _usersDbContextModel.usersModels.Update(findUsersById);
           
           await _usersDbContextModel.SaveChangesAsync();

           return findUsersById;
        }

        public async Task<bool> deleteUser(int id)
        {
            UsersModel findUsersById = await findById(id);

            if(findUsersById == null)
            {
                throw new Exception("Não foi possível encontrar o usuário");
            }

            _usersDbContextModel.usersModels.Remove(findUsersById);
            
            await _usersDbContextModel.SaveChangesAsync();

            return true;
            
        }

       
    }




}
