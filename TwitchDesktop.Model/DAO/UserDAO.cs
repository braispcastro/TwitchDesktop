using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchDesktop.Model.DAO
{
    public class UserDAO
    {
        public string display_name { get; set; }
        public long _id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string logo { get; set; }
        public string email { get; set; }
        public bool partnered { get; set; }
    }
}
