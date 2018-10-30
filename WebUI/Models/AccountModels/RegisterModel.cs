using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Models.AccountModels
{
    public class RegisterModel
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Nickname { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}