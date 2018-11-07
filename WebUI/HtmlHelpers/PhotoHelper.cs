using System.IO;
using System.Linq;
using System.Web.Mvc;
using WebUI.Util;

namespace WebUI.HtmlHelpers
{
    public static class PhotoHelper
    {
        public static string ShowUserPhoto(this HtmlHelper helper, string userName)
        {    
            string userPhoto = Directory.GetFiles(MyConfiguration.USER_AVATAR_FULL_PATH, userName + "*")
                .FirstOrDefault()
                ?.Split('\\', '/')
                .Last();

            return userPhoto == null 
                ? MyConfiguration.NO_PHOTO_PATH
                : MyConfiguration.USER_AVATAR_PATH + userPhoto;
        }
    }
}