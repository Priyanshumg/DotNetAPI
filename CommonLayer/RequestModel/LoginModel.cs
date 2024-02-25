using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.RequestModel
{
    public class LoginModel : RegisterModel
    {
        // For UserEmail values
        public string User_Email { get; set; }

        // For User PassWord 
        public string User_Passwords { get; set; }

    }
}
