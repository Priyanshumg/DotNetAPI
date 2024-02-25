using CommonLayer.RequestModel;
using RepositoryLayer.Enitity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Interface
{
    public interface IUserManager
    {
        public UserEntity UserRegistration(RegisterModel model);
        public UserEntity UserLogin(LoginModel model);
    }
}
    