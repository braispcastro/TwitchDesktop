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
using TwitchDesktop.Model.CVO;

namespace TwitchDesktop.ViewModel.ViewModels
{
    public class FollowingViewModel : BaseViewModel
    {
        private ObservableCollection<StreamChannelCVO> _followsList;
        private ICommand _browserCommand;
        private ICommand _playerCommand;

        public ObservableCollection<StreamChannelCVO> FollowsList
        {
            get { return _followsList; }
            set
            {
                _followsList = value;
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

        //Constructor
        public FollowingViewModel()
        {

        }

        public void Loaded()
        {
            UpdateList();
        }

        #region Public Functions

        public void UpdateList()
        {
            FollowsList = new ObservableCollection<StreamChannelCVO>();
            FollowsList = new ObservableCollection<StreamChannelCVO>(MainViewModel.StreamList);
        }

        #endregion

        #region Private Functions

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
                string livestreamer = AppDomain.CurrentDomain.BaseDirectory + "livestreamer\\livestreamer.exe";
                string quality = "best"; //Configuration.GetQualityString((Quality)Configuration.StreamQuality);
                string channelurl = stream.Url;
                string cliendId = Constants.TwitchClientId;
                string vlcPath = "D:\\Program Files\\VideoLAN\\VLC\\vlc.exe";
                string command = String.Format("/C \"\"{0}\" --player \"{1}\" --http-header Client-ID={2} {3} {4}\"", livestreamer, vlcPath, cliendId, channelurl, quality);

                if (File.Exists(livestreamer))
                {
                    Process process = new Process();
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = command;
                    process.StartInfo = startInfo;
                    process.Start();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        #endregion
    }
}
