using System.Windows;
using System.Windows.Input;
using ControlzEx;
using GalaSoft.MvvmLight.CommandWpf;
using MaterialDesignThemes.Wpf;
using Xems.Globals;

namespace Xems.ViewModels
{
    public class MainWindowViewModel : XemsViewModelBase
    {
        private string _name;

        private string _profilePhotoUrl;

        private string _currentProfileType;

        private Visibility _closeMenuVisibility;

        private Visibility _openMenuVisibility;

        private WindowState _windowState;

        private PackIconKind _windowStateIcon;

        private ICommand _openMenuCommand;

        private ICommand _closeMenuCommand;

        private ICommand _exitCommand;

        private ICommand _windowStateCommand;

        private ICommand _minimizeCommand;

        public string Name
        {
            get => this._name;

            set => this.Set(Constants.Name, ref this._name, value);
        }

        public string ProfilePhotoUrl
        {
            get => this._profilePhotoUrl;

            set => this.Set(Constants.ProfilePhotoUrl, ref this._profilePhotoUrl, value);
        }

        public string CurrentProfileType
        {
            get => this._currentProfileType;

            set => this.Set(Constants.CurrentProfileType, ref this._currentProfileType, value);
        }

        public Visibility OpenMenuVisibility
        {
            get => this._openMenuVisibility;

            set => this.Set(Constants.OpenMenuVisibility, ref this._openMenuVisibility, value);
        }

        public Visibility CloseMenuVisibility
        {
            get => this._closeMenuVisibility;

            set => this.Set(Constants.CloseMenuVisibility, ref this._closeMenuVisibility, value);
        }

        public WindowState WindowState
        {
            get => this._windowState;

            set => this.Set(Constants.WindowState, ref this._windowState, value);
        }

        public PackIconKind WindowStateIcon
        {
            get => this._windowStateIcon;

            set => this.Set(Constants.WindowStateIcon, ref this._windowStateIcon, value);
        }

        public ICommand OpenMenuCommand => this._openMenuCommand;

        public ICommand CloseMenuCommand => this._closeMenuCommand;

        public ICommand ExitCommand => this._exitCommand;

        public ICommand WindowStateCommand => this._windowStateCommand;

        public ICommand WindowMinimizeCommand => this._minimizeCommand;

        public MainWindowViewModel()
        {
            this.Name = "Sevak";
            this._currentProfileType = "Student";
            this._profilePhotoUrl = "..//..//DSC_1769.JPG";
            this._windowStateIcon = PackIconKind.WindowMaximize;

            this._closeMenuCommand = new RelayCommand(
                () => this.SetMenuVisibilities(Visibility.Visible,Visibility.Collapsed));
            this._openMenuCommand = new RelayCommand(
                () => this.SetMenuVisibilities(Visibility.Collapsed, Visibility.Visible));
            this._windowStateCommand = new RelayCommand(this.ChangeWindowState);
            this._minimizeCommand = new RelayCommand(() => this.WindowState = WindowState.Minimized);
            this._exitCommand = new RelayCommand(Application.Current.Shutdown);
        }

        private void SetMenuVisibilities(Visibility openMenuVisibility, Visibility closeMenuVisibility)
        {
            this.OpenMenuVisibility = openMenuVisibility;
            this.CloseMenuVisibility = closeMenuVisibility;
        }

        private void ChangeWindowState()
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                this.WindowStateIcon = PackIconKind.WindowMaximize;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                this.WindowStateIcon = PackIconKind.WindowRestore;
            }
        }
            
    }
}