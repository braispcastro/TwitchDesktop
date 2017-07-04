using TwitchDesktop.Model.CVO;
using System.Collections.Generic;

namespace TwitchDesktop.Core.TwitchInfo
{
    public interface ITwitch
    {
        bool CheckValidToken();
        void GetUserInfo();
        List<StreamChannelCVO> GetFollowedStreamsLive();
        int GetFollowedStreamsCount();
    }
}
