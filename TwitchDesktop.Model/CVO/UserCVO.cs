using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchDesktop.Model.CVO
{
    public class UserCVO
    {
        private string _username;
        private string _logo;

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string Logo
        {
            get { return _logo; }
            set { _logo = value; }
        }
    }
}
