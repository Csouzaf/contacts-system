using api.Errors;
using api.Models;
using api.Repository;

namespace api.Services
{

    public class ContactsServices : IContactsRepository
    {
        private readonly UsersDbContextModel _usersDbContextModel;

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
                if(e is InvalidCastException || e is FormatException || e is OverflowException)
                {
                    throw new BadRequestException("Erro na digitação do tipo. Verifique se está digitando número ou letra no campo certo");
                }
                
            }
           return null;
        }
    }




}
