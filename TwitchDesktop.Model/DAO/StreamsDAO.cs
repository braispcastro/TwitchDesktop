using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchDesktop.Model.DAO
{
    public class StreamsDAO
    {
        public int _total { get; set; }
        public List<StreamDAO> streams { get; set; }
    }
}
