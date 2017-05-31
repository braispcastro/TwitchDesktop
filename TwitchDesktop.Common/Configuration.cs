using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchDesktop.Common
{
    public class Configuration
    {
        public static string AccessToken
        {
            get { return Settings.Default.AccessToken; }
            set
            {
                Settings.Default.AccessToken = value;
                Settings.Default.Save();
            }
        }

        public static bool? _userAuthenticated;
        public static bool UserAuthenticated
        {
            get { return _userAuthenticated ?? false; }
            set { _userAuthenticated = value; }
        }

        public static string _username;
        public static string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public static string _userLogo;
        public static string UserLogo
        {
            get { return _userLogo; }
            set { _userLogo = value; }
        }
    }
}
