﻿using CommonLayer.RequestModel;
using RepositoryLayer.Enitity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserInterface
    {
        public UserEntity UserRegistration(RegisterModel model);
        public UserEntity UserLogin(LoginModel model);
        public bool ResetPassword(string Email, ResetPasswordModel resetPassWordModel);
    }
}
