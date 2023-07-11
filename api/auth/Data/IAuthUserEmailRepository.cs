using api.auth.Model;

namespace api.auth.Data
{
    public interface IAuthUserEmailRepository
    {
        
        AuthUserEmail Create(AuthUserEmail authUserEmail);

        AuthUserEmail Update(int id, AuthUserEmail authUserEmail);

        AuthUserEmail findById(int id);
    }    
}