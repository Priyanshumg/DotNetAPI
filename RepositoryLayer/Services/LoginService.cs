using CommonLayer.RequestModel;
using RepositoryLayer.Context;
using RepositoryLayer.Enitity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace RepositoryLayer.Services
{
    public class LoginService : IUserLogin
    {
        private readonly UserContext context;

        public LoginService(UserContext context)
        {
            this.context = context;
        }
        public UserEntity UserLogin(LoginModel model)
        {
            UserEntity userEntity = new UserEntity();
            if (userEntity.UserEmail != null)
            {
                if (userEntity.UserEmail == model.User_Email)
                {
                    if (userEntity.UserPassword == model.UserPassword)
                    {
                        return userEntity;
                    }
                    else
                    {
                        throw new Exception("Invalid Password");
                    }
                }
                else
                {
                    throw new Exception("Invalid UserName,Create new Account or Add ");
                }
            }
            else
            {
                return null;
            }
        }
    }
}
