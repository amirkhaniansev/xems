using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.XmlConfiguration;
using GalaSoft.MvvmLight.CommandWpf;
using UsersApiConsumer.Core;
using UsersApiConsumer.Models;
using Xems.Globals;
using Xems.Models;
using Xems.Resources;
using Xems.Views.Windows;

namespace Xems.ViewModels
{
    public class SignUpWindowViewModel : LoadableWindowViewModel
    {
        private SignUpInfo _userSignUpInfo;

        private bool _isSignUpAvailable;

        private ICommand _signUpCommand;

        private ICommand _hyperLinkCommand;

        public SignUpInfo UserSignUpInfo
        {
            get => this._userSignUpInfo;

            set => this.Set(Constants.UserSignUpInfo, ref this._userSignUpInfo, value);
        }

        public ICommand SignUpCommand => this._signUpCommand;

        public ICommand HyperLinkCommand => this._hyperLinkCommand;

        public SignUpWindowViewModel()
        {
            this._userSignUpInfo = new SignUpInfo();
            this._signUpCommand = new RelayCommand(this.Execute, this.CanExecute);
            this._hyperLinkCommand = new RelayCommand(this.ChangeWindow);
            this._isSignUpAvailable = true;
        }

        private void ChangeWindow()
        {
            this.ChangeWindows(((App)App.Current).MainWindow, new SignInWindow());
        }

        private bool CanExecute()
        {
            return this.ValidateAddress() &&
                   this.ValidateEmail() &&  
                   this.ValidateNames() &&
                   this.ValidatePassword() &&
                   this.ValidatePhone();
        }

        private async void Execute()
        {
            if(!this._isSignUpAvailable)
                return;

            this.SetVisibilities(Visibility.Visible, Visibility.Collapsed, true);
            this._isSignUpAvailable = false;

            try
            {
                var app = (App) App.Current;

                var response = await app.UsersApiClient.SignUpUserAsync(this._userSignUpInfo);

                if (response.ResponseStatus != ResponseStatus.Success)
                {
                    XemsMsgBox.Show(response.ResponseStatus.ToString());
                    return;
                }

                var verificationResponse =
                    await app.UsersApiClient.AddVerificationKeyAsync(this._userSignUpInfo.Username);

                if (verificationResponse.ResponseStatus != ResponseStatus.Success)
                {
                    XemsMsgBox.Show(verificationResponse.ResponseStatus.ToString());
                    return;
                }

                this.ChangeWindows(app.MainWindow,new ConfirmationWindow(
                    new ConfirmationWindowViewModel(this._userSignUpInfo.Username)));
            }
            catch (Exception)
            {
                XemsMsgBox.Show(Strings.UnknownError);
            }
            finally
            {
                this.SetVisibilities(Visibility.Collapsed, Visibility.Visible, false);
                this._isSignUpAvailable = true;
            }
        }

        private bool ValidatePhone()
        {
            if (string.IsNullOrEmpty(this._userSignUpInfo.Phone))
                return false;

            return this._userSignUpInfo.Phone.All(char.IsDigit);
        }
               

        private bool ValidatePassword()
        {
            if (string.IsNullOrEmpty(this._userSignUpInfo.Password) || 
                string.IsNullOrEmpty(this._userSignUpInfo.ConfirmPassword))
                return false;

            return this._userSignUpInfo.Password.Length > 7 &&
                   this._userSignUpInfo.ConfirmPassword.Length > 7 &&
                   this._userSignUpInfo.Password == this._userSignUpInfo.ConfirmPassword;
        }

        private bool ValidateNames()
        {
            return (string.IsNullOrEmpty(this._userSignUpInfo.FirstName) ||
                    string.IsNullOrEmpty(this._userSignUpInfo.LastName) ||
                    string.IsNullOrEmpty(this._userSignUpInfo.MiddleName) ||
                    string.IsNullOrEmpty(this._userSignUpInfo.Username)) == false;
        }

        private bool ValidateEmail()
        {
            if (string.IsNullOrEmpty(this._userSignUpInfo.Email))
                return false;

            try
            {
                new MailAddress(this._userSignUpInfo.Email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool ValidateAddress()
        {
            return !(string.IsNullOrEmpty(this._userSignUpInfo.Country) ||
                    string.IsNullOrEmpty(this._userSignUpInfo.State) ||
                    string.IsNullOrEmpty(this._userSignUpInfo.CityOrVillage) ||
                    string.IsNullOrEmpty(this._userSignUpInfo.Street) ||
                    string.IsNullOrEmpty(this._userSignUpInfo.Building) ||
                    string.IsNullOrEmpty(this._userSignUpInfo.ZipCode));
        }
    }
}
