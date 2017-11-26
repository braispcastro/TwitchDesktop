using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TwitchDesktop.Common;
using TwitchDesktop.Common.Enumerables;
using TwitchDesktop.Core.Services;
using TwitchDesktop.Core.TwitchInfo;
using TwitchDesktop.Model.CVO;

namespace TwitchDesktop.ViewModel.ViewModels
{
    public class FollowingViewModel : BaseViewModel
    {
        private readonly ITwitch twitchData;
        private ObservableCollection<StreamChannelCVO> _followsList;
        private ICommand _browserCommand;
        private ICommand _playerCommand;
        private double _scrollWidth;
        private double _maxScrollBar = 960;

        public ObservableCollection<StreamChannelCVO> FollowsList
        {
            get { return _followsList; }
            set
            {
                _followsList = value;
                NotifyPropertyChanged();
            }
        }

        public double ScrollWidth
        {
            get { return _scrollWidth; }
            set
            {
                _scrollWidth = value;
                NotifyPropertyChanged();
            }
        }

        #region Commands

        public ICommand BrowserCommand
        {
            get { return _browserCommand ?? (_browserCommand = new RelayCommand<StreamChannelCVO>(OnBrowserCommand)); }
        }

        public ICommand PlayerCommand
        {
            get { return _playerCommand ?? (_playerCommand = new RelayCommand<StreamChannelCVO>(OnPlayerCommand)); }
        }

        #endregion

        #region Event Handlers

        public delegate void RadioPressedEventHandler();
        public RadioPressedEventHandler RadioPressedEvent;
        private void OnRadioPressedChangeEvent()
        {
            RadioPressedEvent?.Invoke();
        }

        #endregion

        //Constructor
        public FollowingViewModel()
        {
            twitchData = TwitchFactory.Instance.GetTwitch();
            ScrollWidth = 0;
        }

        public void Loaded()
        {
            UpdateList();
        }

        #region Public Methods

        public void UpdateList()
        {
            FollowsList = new ObservableCollection<StreamChannelCVO>();
            FollowsList = new ObservableCollection<StreamChannelCVO>(MainViewModel.StreamList);
        }

        public void UpdateScroll(double topScroll, double maxScroll)
        {
            if (topScroll == 0)
            {
                //Estoy arriba del todo
                ScrollWidth = 0;
            }
            else if (topScroll == maxScroll)
            {
                //Estoy abajo del todo
                ScrollWidth = _maxScrollBar;
            }
            else
            {
                //Estoy en el medio
                double percentage = (topScroll * 100) / maxScroll;
                ScrollWidth = (percentage * _maxScrollBar) / 100;
            }
        }

        #endregion

        #region Private Methods

        private void OnBrowserCommand(StreamChannelCVO stream)
        {
            try
            {
                Process.Start(stream.Url);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        private void OnPlayerCommand(StreamChannelCVO stream)
        {
            try
            {
                var streamFile = twitchData.GetAudioFromChannel(stream.ChannelName.ToLowerInvariant());
                Uri uri = new Uri(AppDomain.CurrentDomain.BaseDirectory + streamFile);
                PlayerVLCService.Instance.Play(uri);
                OnRadioPressedChangeEvent();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        private double GetScrollValue()
        {
            return ScrollWidth;
        }

        #endregion
    }
}
