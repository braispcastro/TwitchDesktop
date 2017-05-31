using TwitchDesktop.Common;
using TwitchDesktop.ViewModel.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

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
            //TODO: Start follows timer
            Dispatcher.Invoke(() =>
            {
                //((MainView)Window.GetWindow(this)).StartTimerEvent();
                ((MainView)Application.Current.MainWindow).FrameContent.Navigate(new HomeView());
            });
        }

        #endregion
    }
}
