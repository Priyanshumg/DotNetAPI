using CommonLayer.RequestModel;
using RepositoryLayer.Enitity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserInterface
    {
        public UserEntity UserRegistration(RegisterModel model);
    }
}
