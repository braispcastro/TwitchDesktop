using TwitchDesktop.Common;
using TwitchDesktop.Core.Server;
using TwitchDesktop.Core.TwitchInfo;
using TwitchDesktop.Model.CVO;
using System.Threading.Tasks;

namespace TwitchDesktop.ViewModel.ViewModels
{
    public class AuthViewModel : BaseViewModel
    {
        private readonly ITwitch twitchData;
        public Program svcProgram;

        public delegate void FinishAuthEventHandler();
        public FinishAuthEventHandler FinishAuthEvent;
        private void OnFinishAuthEvent()
        {
            FinishAuthEvent?.Invoke();
        }

        public AuthViewModel()
        {
            twitchData = TwitchFactory.Instance.GetTwitch();

            svcProgram = new Program();
            svcProgram.FinishAuthEvent += FinishAuth_Event;
        }

        #region Public Functions

        public void InitAuthServer()
        {
            Task.Run(() => svcProgram.DoWork());
        }

        public bool InvalidSavedToken()
        {
            //TODO: Check token
            return twitchData.CheckValidToken();
        }

        #endregion

        #region Private Methods

        private async void FinishAuth_Event()
        {
            await Task.Run(() => twitchData.GetUserInfo());
            OnFinishAuthEvent();
        }

        #endregion
    }
}
