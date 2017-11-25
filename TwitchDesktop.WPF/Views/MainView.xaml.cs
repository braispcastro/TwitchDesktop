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
using TwitchDesktop.WPF.Views;

namespace TwitchDesktop.WPF
{
    /// <summary>
    /// Lógica de interacción para MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainViewModel MainViewModel
        {
            get { return (MainViewModel)DataContext; }
        }

        //Constructor
        public MainView()
        {
            InitializeComponent();

            MainViewModel.ApplicationStateChangeEvent += ApplicationState_Changed;
            MainViewModel.PageChangeEvent += Page_Changed;
            MainViewModel.UpdateFollowsListEvent += UpdateFollows;
        }

        #region Public Functions

        public void StartTimerEvent()
        {
            MainViewModel.StartFollowsTimer();
        }

        #endregion

        #region Private Functions

        public void ChangePage(OptionButton option)
        {
            Page_Changed(option);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainViewModel.Loaded();
        }

        private void TopBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(70, 49, 119));
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(75, 54, 124));
        }

        private void ApplicationState_Changed(ApplicationState state)
        {
            switch (state)
            {
                case ApplicationState.Close:
                    Application.Current.Shutdown();
                    break;
                case ApplicationState.Minimize:
                    WindowState = WindowState.Minimized;
                    break;
            }
        }

        private void Page_Changed(OptionButton option)
        {
            switch (option)
            {
                case OptionButton.Home:
                    Dispatcher.Invoke(() => FrameContent.Navigate(new HomeView()));
                    break;
                case OptionButton.Following:
                    Dispatcher.Invoke(() => FrameContent.Navigate(new FollowingView()));
                    break;
                case OptionButton.Settings:
                    Dispatcher.Invoke(() => FrameContent.Navigate(new SettingsView()));
                    break;
                case OptionButton.Auth:
                    Dispatcher.Invoke(() => FrameContent.Navigate(new AuthView()));
                    break;
            }
        }

        private void UpdateFollows()
        {
            Dispatcher.Invoke(() =>
            {
                if (FrameContent.Content is FollowingView)
                {
                    ((FollowingView)FrameContent.Content).FollowingViewModel.UpdateList();
                }
                else if (FrameContent.Content is SettingsView)
                {
                    ((SettingsView)FrameContent.Content).SettingsViewModel.RefreshCounters();
                }
            });
        }


        #endregion
    }
}
