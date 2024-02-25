using CommonLayer.RequestModel;
using ManagerLayer.Interface;
using RepositoryLayer.Enitity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Services
{
    public class UserManager : IUserManager
    {
        private readonly IUserInterface repository;
        public UserManager(IUserInterface repository)
        {
            this.repository = repository;
        }

        public UserEntity UserRegistration(RegisterModel model)
        {
            return repository.UserRegistration(model);
        }

        public UserEntity UserLogin(LoginModel model)
        {
            return repository.UserLogin(model);
        }
    }
}
