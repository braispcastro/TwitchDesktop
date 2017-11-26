using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TwitchDesktop.Common;

namespace TwitchDesktop.WPF
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                var channelsFolder = AppDomain.CurrentDomain.BaseDirectory + Constants.StreamFolderName;
                if (!Directory.Exists(channelsFolder))
                {
                    Directory.CreateDirectory(channelsFolder);
                }

                DirectoryInfo di = new DirectoryInfo(channelsFolder);
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            base.OnStartup(e);
        }
    }
}
