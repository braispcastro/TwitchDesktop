using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using TwitchDesktop.Common.Enumerables;
using TwitchDesktop.Core.TwitchInfo;
using TwitchDesktop.Model.CVO;

namespace TwitchDesktop.ViewModel.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public static List<StreamChannelCVO> StreamList;

        private readonly ITwitch twitchData;
        private ICommand _minimizeCommand;
        private ICommand _closeCommand;
        private ICommand _homeCommand;
        private ICommand _followingCommand;
        private ICommand _settingsCommand;
        private OptionButton _optionSelected;
        private Timer followsTimer;
        private bool _homeSelected;
        private bool _followingSelected;
        private bool _settingsSelected;
        private string _channelsCount;

        public OptionButton OptionSelected
        {
            get { return _optionSelected; }
            set { _optionSelected = value; }
        }

        public bool HomeSelected
        {
            get { return _homeSelected; }
            set
            {
                _homeSelected = value;
                NotifyPropertyChanged();
            }
        }

        public bool FollowingSelected
        {
            get { return _followingSelected; }
            set
            {
                _followingSelected = value;
                NotifyPropertyChanged();
            }
        }

        public bool SettingsSelected
        {
            get { return _settingsSelected; }
            set
            {
                _settingsSelected = value;
                NotifyPropertyChanged();
            }
        }

        public string ChannelsCount
        {
            get { return _channelsCount; }
            set
            {
                _channelsCount = value;
                NotifyPropertyChanged();
            }
        }

        public bool EnableFollowing
        {
            get { return !string.IsNullOrEmpty(ChannelsCount) && ChannelsCount != "0"; }
        }

        #region Commands

        public ICommand MinimizeCommand
        {
            get { return _minimizeCommand ?? (_minimizeCommand = new RelayCommand(OnMinimizeCommand)); }
        }

        public ICommand CloseCommand
        {
            get { return _closeCommand ?? (_closeCommand = new RelayCommand(OnCloseCommand)); }
        }

        public ICommand HomeCommand
        {
            get { return _homeCommand ?? (_homeCommand = new RelayCommand(OnHomeCommand)); }
        }

        public ICommand FollowingCommand
        {
            get { return _followingCommand ?? (_followingCommand = new RelayCommand(OnFollowingCommand)); }
        }

        public ICommand SettingsCommand
        {
            get { return _settingsCommand ?? (_settingsCommand = new RelayCommand(OnSettingsCommand)); }
        }

        #endregion

        #region Event Handlers

        public delegate void ApplicationStateChangeEventHandler(ApplicationState state);
        public ApplicationStateChangeEventHandler ApplicationStateChangeEvent;
        private void OnApplicationStateChangeEvent(ApplicationState state)
        {
            ApplicationStateChangeEvent?.Invoke(state);
        }

        public delegate void PageChangeEventHandler(OptionButton option);
        public PageChangeEventHandler PageChangeEvent;
        private void OnPageChangeEvent(OptionButton option)
        {
            PageChangeEvent?.Invoke(option);
        }

        public delegate void UpdateFollowsListEventHandler();
        public UpdateFollowsListEventHandler UpdateFollowsListEvent;
        private void OnUpdateFollowsList()
        {
            UpdateFollowsListEvent?.Invoke();
        }

        #endregion

        //Constructor
        public MainViewModel()
        {
            twitchData = TwitchFactory.Instance.GetTwitch();
            ChannelsCount = string.Empty;
            StreamList = new List<StreamChannelCVO>();

            followsTimer = new Timer();
            followsTimer.Elapsed += CheckTimer_Elapsed;
            followsTimer.Interval = 180000; //3 minutos
        }
        
        public void Loaded()
        {
            //Vista inicial
            SelectButton(OptionButton.Home);
        }

        #region Public Functions

        public void StartFollowsTimer()
        {
            if (!followsTimer.Enabled)
            {
                GetFollowsList();
                followsTimer.Enabled = true;
            }
        }

        public void StopFollowsTimer()
        {
            if (followsTimer.Enabled)
                followsTimer.Enabled = false;
        }

        #endregion

        #region Private Functions

        private void OnMinimizeCommand()
        {
            OnApplicationStateChangeEvent(ApplicationState.Minimize);
        }

        private void OnCloseCommand()
        {
            OnApplicationStateChangeEvent(ApplicationState.Close);
        }

        private void OnHomeCommand()
        {
            SelectButton(OptionButton.Home);
        }

        private void OnFollowingCommand()
        {
            SelectButton(OptionButton.Following);
        }

        private void OnSettingsCommand()
        {
            SelectButton(OptionButton.Settings);
        }

        private void SelectButton(OptionButton button)
        {
            OptionSelected = button;
            HomeSelected = (OptionSelected == OptionButton.Home);
            FollowingSelected = (OptionSelected == OptionButton.Following);
            SettingsSelected = (OptionSelected == OptionButton.Settings);
            OnPageChangeEvent(OptionSelected);
        }

        private void CheckTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            GetFollowsList();
        }

        private async void GetFollowsList()
        {
            await Task.Run(() =>
            {
                //Ordeno por viewers y solo añado a la lista los canales en directo (sin listas de reproducción)
                StreamList = twitchData.GetFollowedStreams().Where(x => !x.IsPlaylist).OrderByDescending(y => y.Viewers).ToList();
                ChannelsCount = StreamList.Count.ToString();
            });

            OnUpdateFollowsList();
        }

        #endregion
    }
}
