using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Models.AccountModels
{
    public class LoginModel
    {
        public string Nickname { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}