using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebUI.Util;

namespace WebUI.HtmlHelpers
{
    public static class PhotoHelper
    {
        public static string ShowUserPhoto(this HtmlHelper helper, string userName)
        {    
            var userPhotoPath = Path.Combine(MyConfiguration.USER_AVATAR_PATH, userName);
            userPhotoPath = Directory.GetFiles(MyConfiguration.USER_AVATAR_FULL_PATH, userName + "*").FirstOrDefault()?.Split(new[] {'\\', '/' })?.Last();

            FileInfo userPhoto = new FileInfo(userPhotoPath ?? MyConfiguration.NO_PHOTO_PATH);

            TagBuilder tag = new TagBuilder("img");
            tag.MergeAttribute("src", userPhoto.FullName);
            tag.MergeAttribute("height", MyConfiguration.PHOTO_HEIGHT);
            tag.MergeAttribute("width",  MyConfiguration.PHOTO_WIDTH);
            tag.MergeAttribute("alt", "user photo");
            tag.AddCssClass("user-photo");

            //tag.MergeAttribute("referrerpolicy", "no-referrer-when-downgrade");

            //return MvcHtmlString.Create(tag.ToString());
            if (userPhotoPath != null)
                userPhotoPath = "/" + MyConfiguration.USER_AVATAR_FOLDER + "/" + userPhotoPath;
            return userPhotoPath ?? MyConfiguration.NO_AVATAR_PIC;
        }
    }
}