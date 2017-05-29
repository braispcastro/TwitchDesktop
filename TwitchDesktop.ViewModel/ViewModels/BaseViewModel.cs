using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TwitchDesktop.ViewModel.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Constructor
        public BaseViewModel()
        {

        }
    }
}
