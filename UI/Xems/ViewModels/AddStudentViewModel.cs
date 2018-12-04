using System;
using System.Linq.Expressions;
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
    public class AddStudentViewModel : LoadablePageViewModel
    {
        private StudentBase _student;

        private ICommand _addStudentCommand;

        public StudentBase Student
        {
            get => this._student;

            set => this.Set(Constants.Student, ref this._student, value);
        }

        public ICommand AddStudentCommand => this._addStudentCommand;

        public AddStudentViewModel()
        {
            this._student = new StudentBase
            {
                UserId = XemsUser.Default.Id
            };
         
            this._addStudentCommand = new RelayCommand(
                this.AddStudentExecute, this.CanAddStudentExecute); 
        }

        public async void AddStudentExecute()
        {
            try
            {
                this.IsLoading = true;
                this.SetVisibilities(Visibility.Visible, true);

                var response = await this.App.UsersApiClient.AddStudentProfileAsync(this._student);

                if (response.ResponseStatus != ResponseStatus.Success)
                {
                    XemsMsgBox.Show(response.ResponseStatus.ToString());
                    return;
                }

                XemsMsgBox.Show(Strings.StudentAddSuccess);
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

        public bool CanAddStudentExecute()
        {
            return !(string.IsNullOrEmpty(this._student.Department) ||
                     string.IsNullOrEmpty(this._student.CurrentUniversity) ||
                     string.IsNullOrEmpty(this._student.Occupation) ||
                     this._student.EntranceDate == null ||
                     this._student.EntranceDate == default(DateTime) ||
                     this.IsLoading);
        }
    }
}