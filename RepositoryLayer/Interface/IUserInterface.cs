using CommonLayer.RequestModel;
using CommonLayer.RequestModel.LoginPageModel;
using RepositoryLayer.Enitity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserInterface
    {
        public UserEntity UserRegistration(RegisterModel model);
        public string UserLogin(LoginModel model);
        public ForgetPasswordModel ForgetPassword(string UserEmail);
        public bool ResetPassword(string Email, ResetPasswordModel model);
    }
}
