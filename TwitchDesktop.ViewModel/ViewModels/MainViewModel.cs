using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using TwitchDesktop.Common.Enumerables;

namespace TwitchDesktop.ViewModel.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private ICommand _minimizeCommand;
        private ICommand _closeCommand;
        private ICommand _homeCommand;
        private ICommand _followingCommand;
        private ICommand _settingsCommand;
        private bool _homeSelected;
        private bool _followingSelected;
        private bool _settingsSelected;
        private OptionButton _optionSelected;

        //Constructor
        public MainViewModel()
        {
            
        }

        /// <summary>
        /// Se ejecuta cuando se carga la vista
        /// </summary>
        public void Loaded()
        {
            //Vista inicial
            SelectButton(OptionButton.Home);
        }

        #region Event Handlers

        public delegate void ApplicationStateChangeEventHandler(ApplicationState state);
        public ApplicationStateChangeEventHandler ApplicationStateChangeEvent;
        private void OnApplicationStateChangeEvent(ApplicationState state)
        {
            ApplicationStateChangeEvent?.Invoke(state);
        }

        public delegate void PageChangeEventHandler(OptionButton option);
        public PageChangeEventHandler PageChangeEvent;
        private void OnPageChangeEvent(OptionButton option)
        {
            PageChangeEvent?.Invoke(option);
        }

        #endregion

        #region Commands

        public ICommand MinimizeCommand
        {
            get { return _minimizeCommand ?? (_minimizeCommand = new RelayCommand(OnMinimizeCommand)); }
        }

        public ICommand CloseCommand
        {
            get { return _closeCommand ?? (_closeCommand = new RelayCommand(OnCloseCommand)); }
        }

        public ICommand HomeCommand
        {
            get { return _homeCommand ?? (_homeCommand = new RelayCommand(OnHomeCommand)); }
        }

        public ICommand FollowingCommand
        {
            get { return _followingCommand ?? (_followingCommand = new RelayCommand(OnFollowingCommand)); }
        }

        public ICommand SettingsCommand
        {
            get { return _settingsCommand ?? (_settingsCommand = new RelayCommand(OnSettingsCommand)); }
        }

        #endregion

        #region Public Properties

        public OptionButton OptionSelected
        {
            get { return _optionSelected; }
            set { _optionSelected = value; }
        }

        public bool HomeSelected
        {
            get { return _homeSelected; }
            set
            {
                _homeSelected = value;
                NotifyPropertyChanged();
            }
        }

        public bool FollowingSelected
        {
            get { return _followingSelected; }
            set
            {
                _followingSelected = value;
                NotifyPropertyChanged();
            }
        }

        public bool SettingsSelected
        {
            get { return _settingsSelected; }
            set
            {
                _settingsSelected = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Private Functions

        private void OnMinimizeCommand()
        {
            OnApplicationStateChangeEvent(ApplicationState.Minimize);
        }

        private void OnCloseCommand()
        {
            OnApplicationStateChangeEvent(ApplicationState.Close);
        }

        private void OnHomeCommand()
        {
            SelectButton(OptionButton.Home);
        }

        private void OnFollowingCommand()
        {
            //if (!string.IsNullOrEmpty(ChannelsCount) && ChannelsCount != "0")
                SelectButton(OptionButton.Following);
        }

        private void OnSettingsCommand()
        {
            SelectButton(OptionButton.Settings);
        }

        private void SelectButton(OptionButton button)
        {
            OptionSelected = button;
            HomeSelected = (OptionSelected == OptionButton.Home);
            FollowingSelected = (OptionSelected == OptionButton.Following);
            SettingsSelected = (OptionSelected == OptionButton.Settings);
            OnPageChangeEvent(OptionSelected);
        }

        #endregion
    }
}
