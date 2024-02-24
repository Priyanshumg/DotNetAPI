using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.RequestModel
{
    public class RegisterModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
    }
}
