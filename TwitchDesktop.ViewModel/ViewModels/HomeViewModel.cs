using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TwitchDesktop.Common.Enumerables;
using TwitchDesktop.Core.Services;

namespace TwitchDesktop.ViewModel.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private ICommand _playPauseCommand;
        private ICommand _stopCommand;
        private ICommand _speakerCommand;
        private string _playPauseImage;
        private string _speakerImage;
        private string _volumeText;
        private bool _isPlaying;

        #region Properties

        public string PlayPauseImage
        {
            get { return _playPauseImage; }
            set
            {
                _playPauseImage = value;
                NotifyPropertyChanged();
            }
        }

        public string SpeakerImage
        {
            get { return _speakerImage; }
            set
            {
                _speakerImage = value;
                NotifyPropertyChanged();
            }
        }

        public int Volume
        {
            get { return PlayerVLCService.Instance.Volume; }
            set
            {
                PlayerVLCService.Instance.Volume = value;
                VolumeText = string.Format("{0}%", (value / 2).ToString());
                NotifyPropertyChanged();
            }
        }

        public string VolumeText
        {
            get { return _volumeText; }
            set
            {
                _volumeText = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsPlaying
        {
            get { return _isPlaying; }
            set
            {
                _isPlaying = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Commands

        public ICommand PlayPauseCommand
        {
            get { return _playPauseCommand ?? (_playPauseCommand = new RelayCommand(OnPlayPauseCommand)); }
        }

        public ICommand StopCommand
        {
            get { return _stopCommand ?? (_stopCommand = new RelayCommand(OnStopCommand)); }
        }

        public ICommand SpeakerCommand
        {
            get { return _speakerCommand ?? (_speakerCommand = new RelayCommand(OnSpeakerCommand)); }
        }

        #endregion

        //Constructor
        public HomeViewModel()
        {
            SpeakerImage = "/TwitchDesktop.WPF;component/Resources/Icons/ico_volumeon_purple.png";
            PlayPauseImage = PlayerVLCService.Instance.PlayerState == PlayerState.Playing 
                ? "/TwitchDesktop.WPF;component/Resources/Icons/ico_pause_purple.png"
                : "/TwitchDesktop.WPF;component/Resources/Icons/ico_play_purple.png";
        }

        public void Loaded()
        {
            IsPlaying = PlayerVLCService.Instance.PlayerState != PlayerState.Stopped;
        }

        #region Public Methods

        #endregion

        #region Private Methods

        private void OnPlayPauseCommand()
        {
            switch (PlayerVLCService.Instance.PlayerState)
            {
                case PlayerState.Playing:
                    PlayerVLCService.Instance.Pause();
                    PlayPauseImage = "/TwitchDesktop.WPF;component/Resources/Icons/ico_play_purple.png";
                    break;
                case PlayerState.Paused:
                    PlayerVLCService.Instance.Play();
                    PlayPauseImage = "/TwitchDesktop.WPF;component/Resources/Icons/ico_pause_purple.png";
                    break;
            }
        }

        private void OnStopCommand()
        {
            PlayerVLCService.Instance.Stop();
            IsPlaying = false;
        }

        private void OnSpeakerCommand()
        {
            PlayerVLCService.Instance.ToggleMute();
            SpeakerImage = PlayerVLCService.Instance.IsMuted 
                ? "/TwitchDesktop.WPF;component/Resources/Icons/ico_volumeoff_purple.png"
                : "/TwitchDesktop.WPF;component/Resources/Icons/ico_volumeon_purple.png";
        }

        #endregion
    }
}
