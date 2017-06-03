using TwitchDesktop.Common;
using TwitchDesktop.ViewModel.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using TwitchDesktop.Common.Enumerables;
using System;

namespace TwitchDesktop.WPF.Views
{
    /// <summary>
    /// Lógica de interacción para AuthView.xaml
    /// </summary>
    public partial class AuthView : Page
    {
        private AuthViewModel AuthViewModel
        {
            get { return (AuthViewModel)DataContext; }
        }

        //Constructor
        public AuthView()
        {
            InitializeComponent();

            AuthViewModel.FinishAuthEvent += FinishAuth_Event;
        }

        #region Private Methods

        private void AuthView_Loaded(object sender, RoutedEventArgs e)
        {
            ((MainView)Application.Current.MainWindow).btnHome.IsEnabled = false;
            ((MainView)Application.Current.MainWindow).btnFollowing.IsEnabled = false;
            ((MainView)Application.Current.MainWindow).btnSettings.IsEnabled = false;

            if (string.IsNullOrEmpty(Configuration.AccessToken) || AuthViewModel.InvalidSavedToken())
            {
                AuthViewModel.InitAuthServer();
                webBrowser.Navigate(string.Format("{0}/oauth2/authorize?response_type=code&client_id={1}&redirect_uri={2}&scope=user_read+user_subscriptions&state=qwerty",
                    Constants.TwitchBaseUrl, Constants.TwitchClientId, Constants.TwitchRedirectUri));
            }
        }

        private void WebBrowser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            mshtml.IHTMLDocument2 doc = webBrowser.Document as mshtml.IHTMLDocument2;
            //doc.parentWindow.execScript("document.body.style.zoom=0.915;");
            doc.body.style.overflow = "hidden";
        }

        private void FinishAuth_Event()
        {
            Dispatcher.Invoke(() =>
            {
                try
                {
                    ((MainView)Application.Current.MainWindow).StartTimerEvent();
                    ((MainView)Application.Current.MainWindow).ChangePage(OptionButton.Settings);

                    ((MainView)Application.Current.MainWindow).btnHome.IsEnabled = true;
                    ((MainView)Application.Current.MainWindow).btnFollowing.IsEnabled = true;
                    ((MainView)Application.Current.MainWindow).btnSettings.IsEnabled = true;
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                }
            });
        }

        #endregion
    }
}
