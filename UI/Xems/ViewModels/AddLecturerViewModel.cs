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
    public class AddLecturerViewModel : LoadablePageViewModel
    {
        private LecturerBase _lecturer;

        private ICommand _addLecturerCommand;

        public LecturerBase Lecturer
        {
            get => this._lecturer;

            set => this.Set(Constants.Lecturer, ref this._lecturer, value);
        }

        public ICommand AddLecturerCommand => this._addLecturerCommand;

        public AddLecturerViewModel()
        {
            this._lecturer = new LecturerBase
            {
                UserId = XemsUser.Default.Id
            };

            this._addLecturerCommand = new RelayCommand(
                this.AddLecturerExecute, this.CanAddLecturerExecute);
            
        }

        private async void AddLecturerExecute()
        {
            try
            {
                this.IsLoading = true;
                this.SetVisibilities(Visibility.Visible, true);

                var response = await this.App.UsersApiClient.AddLecturerProfileAsync(this.Lecturer);

                if (response.ResponseStatus != ResponseStatus.Success)
                {
                    XemsMsgBox.Show(response.ResponseStatus.ToString());
                    return;
                }

                XemsMsgBox.Show(Strings.LecturerAddSuccess);
            }
            catch (Exception)
            {
                XemsMsgBox.Show(Strings.UnknownError);
            }
            finally
            {
                this.IsLoading = false;
                this.SetVisibilities(Visibility.Collapsed, false);
            }
        }

        public bool CanAddLecturerExecute()
        {
            return !(string.IsNullOrEmpty(this._lecturer.Degree) ||
                     string.IsNullOrEmpty(this._lecturer.Thesis) ||
                     string.IsNullOrEmpty(this._lecturer.CurrentUniversity) ||
                     string.IsNullOrEmpty(this._lecturer.Occupation) ||
                     this._lecturer.WorkStartingDate == null ||
                     this._lecturer.WorkStartingDate == default(DateTime) ||
                     this.IsLoading);
        }
    }
}