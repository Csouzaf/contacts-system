using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Repository
{
    public interface IUserRegisteredRepository
    {
        UserRegisteredModel CreateContacts(UserRegisteredModel userRegisteredModel);
    }
}