using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchDesktop.Model.CVO
{
    public class StreamChannelCVO
    {
        private string _channelName;
        private long _viewers;
        private string _viewersDisplay;
        private string _follows;
        private string _views;
        private string _streamTitle;
        private string _game;
        private string _logo;
        private string _url;
        private string _preview;
        private bool _isPlaylist;

        public string ChannelName
        {
            get { return _channelName; }
            set { _channelName = value; }
        }

        public long Viewers
        {
            get { return _viewers; }
            set { _viewers = value; }
        }

        public string ViewersDisplay
        {
            get { return _viewersDisplay; }
            set { _viewersDisplay = value; }
        }

        public string Follows
        {
            get { return _follows; }
            set { _follows = value; }
        }

        public string Views
        {
            get { return _views; }
            set { _views = value; }
        }

        public string StreamTitle
        {
            get { return _streamTitle; }
            set { _streamTitle = value; }
        }

        public string Game
        {
            get { return _game; }
            set { _game = value; }
        }

        public string Logo
        {
            get { return _logo; }
            set { _logo = value; }
        }

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        public string Preview
        {
            get { return _preview; }
            set { _preview = value; }
        }

        public bool IsPlaylist
        {
            get { return _isPlaylist; }
            set { _isPlaylist = value; }
        }
    }
}
