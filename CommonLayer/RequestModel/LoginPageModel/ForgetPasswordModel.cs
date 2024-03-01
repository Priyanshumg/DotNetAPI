using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace CommonLayer.RequestModel
{
    public class ForgetPasswordModel
    {
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string token { get; set; }
    }
}
