using System;
using System.Windows;
using System.Windows.Input;
using AuthTokenService;
using GalaSoft.MvvmLight.CommandWpf;
using Xems.Globals;
using Xems.Models;
using Xems.Resources;
using Xems.Views.Windows;

namespace Xems.ViewModels
{
    public class SignInWindowViewModel : LoadableWindowViewModel
    {
        private bool _isSignInAvailable;

        private SignInInfo _signInInfo;

        private ICommand _signInCommand;

        private ICommand _hyperLinkCommand;
        
        public SignInInfo SignInInfo
        {
            get => this._signInInfo;

            set => this.Set(Constants.SignInInfo, ref this._signInInfo, value);
        }

        public ICommand SignInCommand => this._signInCommand;

        public ICommand HyperLinkCommand => this._hyperLinkCommand;

        public SignInWindowViewModel()
        {
            this._signInInfo = new SignInInfo();
            this._signInCommand = new RelayCommand(this.Execute, this.CanExecute);
            this._hyperLinkCommand = new RelayCommand(this.ChangeWindow);
            this._isSignInAvailable = true;
        }

        private void ChangeWindow()
        {
            this.ChangeWindows(((App)App.Current).MainWindow, new SignUpWindow());
        }

        private async void Execute()
        {
            if(!this._isSignInAvailable)
                return;
            
            this.SetVisibilities(Visibility.Visible, Visibility.Collapsed, true);
            this._isSignInAvailable = false;

            try
            {
                var app = (App) App.Current;

                var response = await app.TokenProvider.SignInAsync(
                    this._signInInfo.Username, this._signInInfo.Password);

                if (response == TokenStatus.Error)
                {
                    XemsMsgBox.Show(Strings.InvalidCredentials);
                    return;
                }

                XemsUser.Default.Username = this._signInInfo.Username;
                XemsUser.Default.Save();
                
                this.ChangeWindows(app.MainWindow, new MainWindow());
            }
            catch (Exception)
            {
                XemsMsgBox.Show(Strings.UnknownError);
            }
            finally
            {
                this.SetVisibilities(Visibility.Collapsed, Visibility.Visible, false);
                this._isSignInAvailable = true;
            }
        }

        private bool CanExecute()
        {
            if (this._signInInfo.Username == null || this._signInInfo.Password == null)
                return false;

            return this._signInInfo.Username.Length > 0 && this._signInInfo.Password.Length > 7;
        }
    }
}