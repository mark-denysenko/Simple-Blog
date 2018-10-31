using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models.AccountModels
{
    public class ProfileModel
    {
        public int    UserId       { get; set; }
        public string Email        { get; set; }
        public string Nickname     { get; set; }
        public string PasswordHash { get; set; }

        public int TotalPosts      { get; set; }
        public int TotalComments   { get; set; }
    }
}