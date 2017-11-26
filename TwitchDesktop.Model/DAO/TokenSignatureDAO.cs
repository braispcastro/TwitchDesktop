using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchDesktop.Model.DAO
{
    public class TokenSignatureDAO
    {
        public string token { get; set; }
        public string sig { get; set; }
        public bool mobile_restricted { get; set; }
    }
}
