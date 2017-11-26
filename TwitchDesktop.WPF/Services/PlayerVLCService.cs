using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Vlc.DotNet.Core;

namespace TwitchDesktop.WPF.Services
{
    public class PlayerVLCService
    {
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
            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            if (currentDirectory == null)
                return;

            DirectoryInfo vlcLibDirectory = new DirectoryInfo(Path.Combine(currentDirectory, (IntPtr.Size == 4) ? @"..\..\lib\x86\" : @"..\..\lib\x64\"));
            _vlcMediaPlayer = new VlcMediaPlayer(vlcLibDirectory);
        }

        public void Play(Uri playPathUri)
        {
            if (_vlcMediaPlayer.IsPlaying())
                _vlcMediaPlayer.Stop();

            _vlcMediaPlayer.SetMedia(playPathUri, "no-video");
            _vlcMediaPlayer.Play();
        }

        public void Stop()
        {
            if (_vlcMediaPlayer.IsPlaying())
                _vlcMediaPlayer.Stop();
        }

        public void Pause()
        {
            if (_vlcMediaPlayer.IsPlaying())
                _vlcMediaPlayer.Pause();
        }
    }
}
