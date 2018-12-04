using System;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using UsersApiConsumer.Core;
using UsersApiConsumer.Models;
using Xems.Globals;
using Xems.Resources;
using Xems.Views.Windows;

namespace Xems.ViewModels
{
    public class ConfirmationWindowViewModel : LoadableWindowViewModel
    {
        private Verification _verification;

        private readonly ICommand _verifyCommand;

        private bool _isConfirmAvailable;

        public Verification Verification
        {
            get => this._verification;

            set => this.Set(Constants.Verification, ref this._verification, value);
        }

        public ICommand VerifyCommand => this._verifyCommand;

        public ConfirmationWindowViewModel(string username)
        {
            this._verification = new Verification
            {
                Username = username
            };

            this._isConfirmAvailable = true;
            this._verifyCommand = new RelayCommand(this.Execute, this.CanExecute);
        }

        private async void Execute()
        {
            if (!this._isConfirmAvailable)
                return;

            this.SetVisibilities(Visibility.Visible, Visibility.Collapsed, true);
            this._isConfirmAvailable = false;

            try
            {
                var app = ((App) App.Current);

                var response = await app.UsersApiClient.VerifyUserAsync(this._verification);

                if (response.ResponseStatus != ResponseStatus.Success)
                {
                    XemsMsgBox.Show(Strings.InvalidVerificationCode);
                    return;
                }
                
                this.ChangeWindows(app.MainWindow, new SignInWindow());
            }
            catch (Exception)
            {
                XemsMsgBox.Show(Strings.UnknownError);
            }
            finally
            {
                this.SetVisibilities(Visibility.Collapsed, Visibility.Visible, false);
                this._isConfirmAvailable = true;
            }
        }

        private bool CanExecute()
        {
            if (string.IsNullOrEmpty(this._verification.VerificationKey))
                return false;

            return this._verification.VerificationKey.Length >= 8;
        }
    }
}