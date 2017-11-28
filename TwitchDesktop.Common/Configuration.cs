using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TwitchDesktop.Common
{
    public class Configuration
    {
        public static string AppVersion
        {
            get { return string.Format("v{0}.{1}", Assembly.GetEntryAssembly().GetName().Version.Major, Assembly.GetEntryAssembly().GetName().Version.Minor); }
        }

        public static string AccessToken
        {
            get { return Settings.Default.AccessToken; }
            set
            {
                Settings.Default.AccessToken = value;
                Settings.Default.Save();
            }
        }

        public static double RefreshTimer
        {
            get { return Settings.Default.RefreshTimer; }
            set
            {
                Settings.Default.RefreshTimer = value;
                Settings.Default.Save();
            }
        }

        private static bool? _userAuthenticated;
        public static bool UserAuthenticated
        {
            get { return _userAuthenticated ?? false; }
            set { _userAuthenticated = value; }
        }

        private static long _userId;
        public static long UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        private static string _username;
        public static string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        private static string _userLogo;
        public static string UserLogo
        {
            get { return _userLogo; }
            set { _userLogo = value; }
        }
    }
}
