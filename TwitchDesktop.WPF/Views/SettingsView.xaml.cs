using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TwitchDesktop.Common.Enumerables;
using TwitchDesktop.ViewModel.ViewModels;

namespace TwitchDesktop.WPF.Views
{
    /// <summary>
    /// Lógica de interacción para SettingsView.xaml
    /// </summary>
    public partial class SettingsView : Page
    {
        public SettingsViewModel SettingsViewModel
        {
            get { return (SettingsViewModel)DataContext; }
        }

        //Constructor
        public SettingsView()
        {
            InitializeComponent();
            SettingsViewModel.LoginPressedEvent += Login_Pressed;
        }

        #region Private Functions

        private void SettingsView_Loaded(object sender, RoutedEventArgs e)
        {
            SettingsViewModel.Loaded();
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(125, 91, 190));
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(100, 65, 164));
        }

        private void Login_Pressed()
        {
            ((MainView)Application.Current.MainWindow).FrameContent.Navigate(new AuthView());
            ((MainView)Application.Current.MainWindow).ChangePage(OptionButton.Auth);
        }

        private void SliderRefreshTimer_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SettingsViewModel.SliderValueChanged(e.NewValue);
            //((MainView)Application.Current.MainWindow).MainViewModel.RefreshTimerValue();
        }

        #endregion
    }
}
