using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api.Services
{
    public class UserRegisteredService : IUserRegisteredRepository
    {
        private readonly UserRegisteredDbContext _userRegisteredDbContext;

        public UserRegisteredService(UserRegisteredDbContext userRegisteredDbContext)
        {
            _userRegisteredDbContext = userRegisteredDbContext;
        }

       
        public UserRegisteredModel CreateContacts(UserRegisteredModel userRegisteredModel)
        {
            try{
                _userRegisteredDbContext.Add(userRegisteredModel);
                _userRegisteredDbContext.SaveChanges();
                return userRegisteredModel;
            }

            catch(DbException dbException){
                throw new Exception("Error", dbException);
            }

     
        }

    }
}