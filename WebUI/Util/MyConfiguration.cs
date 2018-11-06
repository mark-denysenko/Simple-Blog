using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Util
{
    public static class MyConfiguration
    {
        public const string NO_AVATAR_PIC = "NoAvatar.png";
        public const string USER_AVATAR_FOLDER = "UserAvatars";
        public const string USER_AVATAR_PATH = "~/Content/images/" + USER_AVATAR_FOLDER + "/";
        public const string USER_AVATAR_FULL_PATH = @"C:\Users\Mark\source\repos\NTierSolution\WebUI\Content\images\" + USER_AVATAR_FOLDER + "\\";
        public const string NO_PHOTO_PATH = "~/Content/images/" + NO_AVATAR_PIC;
        public const string PHOTO_HEIGHT = "400";
        public const string PHOTO_WIDTH = "400";
        public const int POST_PER_PAGE = 5;
    }
}