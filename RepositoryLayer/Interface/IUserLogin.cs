using CommonLayer.RequestModel;
using RepositoryLayer.Enitity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserLogin
    {
        public UserEntity UserLogin(LoginModel model);
    }
}
