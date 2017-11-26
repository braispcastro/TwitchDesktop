using TwitchDesktop.Model.CVO;
using System;
using TwitchDesktop.Model.DAO;
using TwitchDesktop.Common;
using System.Collections.Generic;
using TwitchDesktop.Model.Rest;
using System.Globalization;
using System.Net;

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
                Configuration.UserId = userdata.UserId;
                Configuration.Username = userdata.Username;
                Configuration.UserLogo = userdata.Logo;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        public List<StreamChannelCVO> GetFollowedStreamsLive()
        {
            List<StreamChannelCVO> result = new List<StreamChannelCVO>();

            try
            {
                var streamInfo = twitchRest.GetLiveFollowedStreams("all", 0);
                result = BuildStreamsCVO(streamInfo);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return result;
        }

        public int GetFollowedStreamsCount()
        {
            int result = 0;

            try
            {
                var streamInfo = twitchRest.GetTotalFollowedStreams(Configuration.UserId, 0);
                result = streamInfo._total;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return result;
        }

        public string GetAudioFromChannel(string channel)
        {
            string streamFile;

            try
            {
                var streamInfo = twitchRest.GetTokenAndSignature(channel);
                var audioUrl = string.Format(Constants.TwitchPlaylistUrl, channel, streamInfo.sig, streamInfo.token);
                streamFile = string.Format(@"{0}\{1}.m3u8", Constants.StreamFolderName, channel);
                using (var client = new WebClient())
                {
                    client.DownloadFile(audioUrl, streamFile);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                streamFile = string.Empty;
            }

            return streamFile;
        }

        #endregion

        #region Private Methods

        private UserCVO BuildUserCVO(UserDAO userInfo)
        {
            if (userInfo == null)
                return null;

            return new UserCVO
            {
                UserId = userInfo._id,
                Username = userInfo.display_name,
                Logo = userInfo.logo
            };
        }

        private List<StreamChannelCVO> BuildStreamsCVO(StreamsDAO streamsInfo)
        {
            if (streamsInfo == null || streamsInfo.streams == null)
                return null;

            NumberFormatInfo numberFormat = new NumberFormatInfo()
            {
                NumberGroupSizes = new[] { 3 },
                NumberGroupSeparator = "."
            };
            List<StreamChannelCVO> auxList = new List<StreamChannelCVO>();
            foreach(var stream in streamsInfo.streams)
            {
                StreamChannelCVO aux = new StreamChannelCVO
                {
                    ChannelName = stream.channel.display_name,
                    Follows = stream.channel.followers.ToString("N0", numberFormat),
                    Game = stream.channel.game,
                    IsPlaylist = stream.is_playlist,
                    Logo = stream.channel.logo,
                    Preview = GetPreviewUrl(stream.preview.template),
                    StreamTitle = stream.channel.status,
                    Url = stream.channel.url,
                    Viewers = stream.viewers,
                    ViewersDisplay = stream.viewers.ToString("N0", numberFormat),
                    Views = stream.channel.views.ToString("N0", numberFormat)
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
            string imageUrl = string.Format(url, width, height);

            //Hack para forzar el refresco del preview cada vez que se actualiza la lista
            string imageHack = imageUrl + "?forceRefresh=" + new Random().Next();

            return imageHack;
        }

        #endregion
    }
}
