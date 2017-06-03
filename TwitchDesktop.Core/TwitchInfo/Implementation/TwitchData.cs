using TwitchDesktop.Model.CVO;
using System;
using TwitchDesktop.Model.DAO;
using TwitchDesktop.Common;
using System.Collections.Generic;
using TwitchDesktop.Model.Rest;

namespace TwitchDesktop.Core.TwitchInfo.Implementation
{
    public class TwitchData : ITwitch
    {
        private TwitchApi twitchRest;
        private const int width = 456;
        private const int height = 257;

        public TwitchData()
        {
            twitchRest = new TwitchApi();
        }

        #region Interface Methods

        public bool CheckValidToken()
        {
            bool result;

            try
            {
                result = true;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                result = false;
            }

            return result;
        }

        public void GetUserInfo()
        {
            try
            {
                var userInfo = twitchRest.GetUserInfo();
                var userdata = BuildUserCVO(userInfo);
                Configuration.UserAuthenticated = !string.IsNullOrEmpty(userdata.Username);
                Configuration.Username = userdata.Username;
                Configuration.UserLogo = userdata.Logo;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        public List<StreamChannelCVO> GetFollowedStreams()
        {
            List<StreamChannelCVO> result = new List<StreamChannelCVO>();

            try
            {
                var streamInfo = twitchRest.GetFollowedStreams();
                result = BuildStreamsCVO(streamInfo);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return result;
        }

        #endregion

        #region Private Methods

        private UserCVO BuildUserCVO(UserDAO userInfo)
        {
            if (userInfo == null)
                return null;

            return new UserCVO
            {
                Username = userInfo.display_name,
                Logo = userInfo.logo
            };
        }

        private List<StreamChannelCVO> BuildStreamsCVO(StreamsDAO streamsInfo)
        {
            if (streamsInfo == null || streamsInfo.streams == null)
                return null;

            List<StreamChannelCVO> auxList = new List<StreamChannelCVO>();
            foreach(var stream in streamsInfo.streams)
            {
                StreamChannelCVO aux = new StreamChannelCVO
                {
                    ChannelName = stream.channel.display_name,
                    Follows = stream.channel.followers,
                    Game = stream.channel.game,
                    IsPlaylist = stream.is_playlist,
                    Logo = stream.channel.logo,
                    Preview = GetPreviewUrl(stream.preview.template),
                    StreamTitle = stream.channel.status,
                    Url = stream.channel.url,
                    Viewers = stream.viewers,
                    Views = stream.channel.views
                };

                auxList.Add(aux);
            }

            return auxList;
        }

        private string GetPreviewUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                return string.Empty;

            url = url.Replace("width", "0").Replace("height", "1");
            return string.Format(url, width, height);
        }

        #endregion
    }
}
