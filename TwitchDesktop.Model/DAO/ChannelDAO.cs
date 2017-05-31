using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchDesktop.Model.DAO
{
    public class ChannelDAO
    {
        public long _id { get; set; }
        public string broadcaster_language { get; set; }
        public DateTime created_at { get; set; }
        public string display_name { get; set; }
        public long followers { get; set; }
        public string game { get; set; }
        public string language { get; set; }
        public string logo { get; set; }
        public bool mature { get; set; }
        public string name { get; set; }
        public bool partner { get; set; }
        public string profile_banner { get; set; }
        public string profile_banner_background_color { get; set; }
        public string status { get; set; }
        public string updated_at { get; set; }
        public string url { get; set; }
        public string video_banner { get; set; }
        public long views { get; set; }
    }
}
