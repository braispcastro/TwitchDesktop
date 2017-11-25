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
        private string _liveCount;
        private string _totalCount;
        private bool _needAuth;
        private bool _authed;
        private string _timerValue;
        private double _refreshTimer;

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

        public string LiveCount
        {
            get { return _liveCount; }
            set
            {
                _liveCount = value;
                NotifyPropertyChanged();
            }
        }

        public string TotalCount
        {
            get { return _totalCount; }
            set
            {
                _totalCount = value;
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

        public bool Authed
        {
            get { return _authed; }
            set
            {
                _authed = value;
                NotifyPropertyChanged();
            }
        }

        public string TimerValue
        {
            get { return _timerValue; }
            set
            {
                _timerValue = value;
                NotifyPropertyChanged();
            }
        }

        public double RefreshTimer
        {
            get { return _refreshTimer; }
            set
            {
                _refreshTimer = value;
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
            RefreshTimer = Configuration.RefreshTimer;
            UserImage = !string.IsNullOrEmpty(Configuration.UserLogo) ? Configuration.UserLogo : "/TwitchDesktop.WPF;component/Resources/Images/user_image.png";
            UsernameText = !string.IsNullOrEmpty(Configuration.Username) ? Configuration.Username : string.Empty;
            NeedAuth = !Configuration.UserAuthenticated;
            Authed = Configuration.UserAuthenticated;
            RefreshCounters();
        }
        
        public void Loaded()
        {
        }

        #region Public Methods

        public void RefreshCounters()
        {
            TotalCount = Authed ? MainViewModel.TotalChannelsCount : string.Empty;
            LiveCount = Authed ? MainViewModel.LiveChannelsCount : string.Empty;
        }

        public void SliderValueChanged(double value)
        {
            TimerValue = string.Format("{0}min.", value);
            Configuration.RefreshTimer = value;
        }

        #endregion
    }
}
