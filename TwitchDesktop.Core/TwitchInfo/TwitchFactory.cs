using TwitchDesktop.Core.TwitchInfo.Implementation;

namespace TwitchDesktop.Core.TwitchInfo
{
    public class TwitchFactory
    {
        #region Singleton implementation
        private static TwitchFactory _instance = null;
        public static TwitchFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TwitchFactory();
                }
                return _instance;
            }
        }
        #endregion

        #region Private Attributes
        private Twitch _defaultTwitch = Twitch.TwitchData;
        #endregion

        #region Public Methods
        public ITwitch GetTwitch()
        {
            return GetTwitch(_defaultTwitch);
        }

        ITwitch picker = null;
        public ITwitch GetTwitch(Twitch type)
        {
            switch (type)
            {
                case Twitch.TwitchData:
                    picker = picker ?? new TwitchData();
                    break;
                default:
                    picker = GetTwitch(_defaultTwitch);
                    break;
            }
            return picker;
        }
        #endregion
    }
}
