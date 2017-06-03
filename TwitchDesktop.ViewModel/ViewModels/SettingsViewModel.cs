using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TwitchDesktop.Common;

namespace TwitchDesktop.ViewModel.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private ICommand _loginCommand;
        private string _userImage;
        private string _usernameText;
        private bool _needAuth;

        public string UserImage
        {
            get { return _userImage; }
            set
            {
                _userImage = value;
                NotifyPropertyChanged();
            }
        }

        public string UsernameText
        {
            get { return _usernameText; }
            set
            {
                _usernameText = value;
                NotifyPropertyChanged();
            }
        }

        public bool NeedAuth
        {
            get { return _needAuth; }
            set
            {
                _needAuth = value;
                NotifyPropertyChanged();
            }
        }

        #region Commands

        public ICommand LoginCommand
        {
            get { return _loginCommand ?? (_loginCommand = new RelayCommand(OnLoginPressedChangeEvent)); }
        }

        #endregion

        #region Event Handlers

        public delegate void LoginPressedEventHandler();
        public LoginPressedEventHandler LoginPressedEvent;
        private void OnLoginPressedChangeEvent()
        {
            LoginPressedEvent?.Invoke();
        }

        #endregion

        //Constructor
        public SettingsViewModel()
        {
            UserImage = !string.IsNullOrEmpty(Configuration.UserLogo) ? Configuration.UserLogo : "/TwitchDesktop.WPF;component/Resources/Images/user_image.png";
            UsernameText = !string.IsNullOrEmpty(Configuration.Username) ? Configuration.Username : string.Empty;
            NeedAuth = !Configuration.UserAuthenticated;
        }
        
        public void Loaded()
        {

        }
    }
}
