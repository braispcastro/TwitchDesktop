using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchDesktop.Common
{
    public static class Constants
    {
        public static string ApplicationName = "BasicTwitchDesktop";
        public static string TwitchClientId = "igbad6rqn1g5olsckvtybwxqbl9q5f";
        public static string TwitchClientSecret = "tkrjevt18xby918ithvhqlv9r8282z";
        public static string TwitchRedirectUri = "http://localhost:8080/twitch/callback";
        public static string TwitchBaseUrl = "https://api.twitch.tv/kraken";
        public static string TwitchApiUrl = "https://api.twitch.tv/api";
        public static string TwitchPlaylistUrl = "https://usher.ttvnw.net/api/channel/hls/{0}.m3u8?allow_audio_only=true&sig={1}&token={2}";
        public static string DefaultUserImage = "/TwitchDesktop.WPF;component/Resources/Images/user_image.png";
        public static string StreamFolderName = "streams";
    }
}
