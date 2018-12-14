using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AuthTokenService;
using ControlzEx;
using GalaSoft.MvvmLight.CommandWpf;
using MaterialDesignThemes.Wpf;
using Xems.Globals;
using Xems.Views.Pages;
using Xems.Views.Windows;

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

        private object _frameContent;

        private ICommand _openMenuCommand;

        private ICommand _closeMenuCommand;

        private ICommand _exitCommand;

        private ICommand _windowStateCommand;

        private ICommand _minimizeCommand;

        private ICommand _signOutCommand;

        private ICommand _openAddLecturer;

        private ICommand _openAddStudent;

        private ICommand _openCreateExamPageCommand;

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

        public object FrameContent
        {
            get => this._frameContent;

            set => this.Set(Constants.FrameContent, ref this._frameContent, value);
        }

        public ICommand OpenMenuCommand => this._openMenuCommand;

        public ICommand CloseMenuCommand => this._closeMenuCommand;

        public ICommand ExitCommand => this._exitCommand;

        public ICommand WindowStateCommand => this._windowStateCommand;

        public ICommand WindowMinimizeCommand => this._minimizeCommand;

        public ICommand SignOutCommand => this._signOutCommand;

        public ICommand OpenAddLecturerCommand => this._openAddLecturer;

        public ICommand OpenAddStudentCommand => this._openAddStudent;

        public ICommand OpenCreateExamPageCommand => this._openCreateExamPageCommand;

        public MainWindowViewModel()
        {
            this.Name = XemsUser.Default.Username;
            this.CurrentProfileType = XemsUser.Default.CurrentProfile;
            this._profilePhotoUrl = "..//..//DSC_1769.JPG";
            this._windowStateIcon = PackIconKind.WindowMaximize;

            this._closeMenuCommand = new RelayCommand(
                () => this.SetMenuVisibilities(Visibility.Visible,Visibility.Collapsed));
            this._openMenuCommand = new RelayCommand(
                () => this.SetMenuVisibilities(Visibility.Collapsed, Visibility.Visible));
            this._openAddLecturer = new RelayCommand(
                () => this.ChangeFrameContent(new AddLecturerPage()));
            this._openAddStudent = new RelayCommand(
                () => this.ChangeFrameContent(new AddStudentPage()));
            this._openCreateExamPageCommand = new RelayCommand(
                () => this.ChangeFrameContent(new CreateExamPage()));


            this._windowStateCommand = new RelayCommand(this.ChangeWindowState);
            this._minimizeCommand = new RelayCommand(() => this.WindowState = WindowState.Minimized);
            this._exitCommand = new RelayCommand(Application.Current.Shutdown);
            this._signOutCommand = new RelayCommand(this.SignOutExecute);
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

        private async void SignOutExecute()
        {
            try
            {
                var app = (App) App.Current;

                var response = await app.TokenProvider.SignOutAsync();

                if (response == TokenStatus.Error)
                {
                    XemsMsgBox.Show("Sign out failed");
                    return;
                }

                XemsUser.Default.Reset();

                this.ChangeWindows(app.MainWindow, new SignInWindow());
            }
            catch (Exception)
            {
                XemsMsgBox.Show("Unknown error");
            }
        }


        private void ChangeFrameContent(Page page)
        {
            if (this.FrameContent?.GetType() == page.GetType())
                return;

            this.FrameContent = page;
        }
    }
}