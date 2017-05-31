using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchDesktop.Model.DAO
{
    public class StreamDAO
    {
        public long _id { get; set; }
        public int average_fps { get; set; }
        public ChannelDAO channel { get; set; }
        public DateTime created_at { get; set; }
        public int delay { get; set; }
        public string game { get; set; }
        public bool is_playlist { get; set; }
        public PreviewDAO preview { get; set; }
        public int video_height { get; set; }
        public long viewers { get; set; }
    }
}
