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
using TwitchDesktop.ViewModel.ViewModels;

namespace TwitchDesktop.WPF.Views
{
    /// <summary>
    /// Lógica de interacción para HomeView.xaml
    /// </summary>
    public partial class HomeView : Page
    {
        private HomeViewModel HomeViewModel
        {
            get { return (HomeViewModel)DataContext; }
        }

        //Constructor
        public HomeView()
        {
            InitializeComponent();
        }

        #region Private Methods

        private void HomeView_Loaded(object sender, RoutedEventArgs e)
        {
            HomeViewModel.Loaded();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            HomeViewModel.Volume = (int)e.NewValue;
        }

        #endregion
    }
}
