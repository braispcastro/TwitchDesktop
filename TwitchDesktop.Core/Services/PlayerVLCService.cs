using System;
using System.IO;
using System.Reflection;
using TwitchDesktop.Common.Enumerables;
using Vlc.DotNet.Core;

namespace TwitchDesktop.Core.Services
{
    public class PlayerVLCService
    {
        #region Properties

        public PlayerState PlayerState { get; set; }
        public Uri PlayPathUri { get; set; }

        public bool IsPlaying
        {
            get { return _vlcMediaPlayer.IsPlaying(); }
        }

        public bool IsMuted
        {
            get { return _vlcMediaPlayer.Audio.IsMute; }
        }

        public int Volume
        {
            get { return _vlcMediaPlayer.Audio.Volume; }
            set { _vlcMediaPlayer.Audio.Volume = value; }
        }

        #endregion

        #region Singleton implementation
        private static PlayerVLCService _instance = null;
        public static PlayerVLCService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PlayerVLCService();
                }
                return _instance;
            }
        }
        #endregion

        private VlcMediaPlayer _vlcMediaPlayer;

        public PlayerVLCService()
        {
            PlayerState = PlayerState.Stopped;
            PlayPathUri = null;

            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            if (currentDirectory == null)
                return;

            DirectoryInfo vlcLibDirectory = new DirectoryInfo(Path.Combine(currentDirectory, (IntPtr.Size == 4) ? @"..\..\lib\x86\" : @"..\..\lib\x64\"));
            _vlcMediaPlayer = new VlcMediaPlayer(vlcLibDirectory);
            _vlcMediaPlayer.Audio.Volume = 50;
        }

        #region Public Methods

        public void Play()
        {
            try
            {
                if (PlayPathUri != null)
                {
                    Play(PlayPathUri);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        public void Play(Uri playPathUri)
        {
            try
            {
                PlayPathUri = playPathUri;

                if (_vlcMediaPlayer.IsPlaying())
                    _vlcMediaPlayer.Stop();

                _vlcMediaPlayer.SetMedia(PlayPathUri, "no-video");
                _vlcMediaPlayer.Play();
                PlayerState = PlayerState.Playing;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        public void Stop()
        {
            try
            {
                if (_vlcMediaPlayer.IsPlaying())
                {
                    _vlcMediaPlayer.Stop();
                    PlayPathUri = null;
                    PlayerState = PlayerState.Stopped;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        public void Pause()
        {
            try
            {
                if (_vlcMediaPlayer.IsPlaying())
                {
                    _vlcMediaPlayer.Pause();
                    PlayerState = PlayerState.Paused;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        public void ToggleMute()
        {
            try
            {
                _vlcMediaPlayer.Audio.IsMute = !_vlcMediaPlayer.Audio.IsMute;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        #endregion
    }
}
