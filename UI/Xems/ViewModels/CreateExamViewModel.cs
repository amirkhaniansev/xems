using System;
using System.Windows;
using System.Windows.Input;
using ExamsApiConsumer.Models;
using GalaSoft.MvvmLight.CommandWpf;
using UsersApiConsumer.Core;
using Xems.Globals;
using Xems.Views.Windows;

namespace Xems.ViewModels
{
    public class CreateExamViewModel : LoadablePageViewModel
    {
        private string _currentStudent;

        private Exam _exam;

        private Test _currentTest;

        private int _currentQuestionNumber;

        private bool _isDoneAvailable;

        private Question _currentQuestion;

        private ICommand _addStudentCommand;

        private ICommand _addTestCommand;

        private ICommand _addQuestionCommand;

        private ICommand _addExamCommand;

        public Test CurrentTest
        {
            get => this._currentTest;

            set => this.Set("CurrentTest", ref this._currentTest, value);
        }

        public Question CurrentQuestion
        {
            get => this._currentQuestion;

            set => this.Set("CurrentQuestion", ref this._currentQuestion, value);
        }

        public string CurrentStudent
        {
            get => this._currentStudent;

            set => this.Set("CurrentStudent", ref this._currentStudent, value);
        }

        public Exam Exam
        {
            get => this._exam;

            set => this.Set("Exam", ref this._exam, value);
        }

        public int CurrentQuestionNumber
        {
            get => this._currentQuestionNumber;

            set => this.Set("CurrentQuestionNumber", ref this._currentQuestionNumber, value);
        }

        public ICommand AddStudentCommand =>
            this._addStudentCommand ?? (
                this._addStudentCommand = new RelayCommand(this.AddStudentExecute));

        public ICommand AddTestCommand =>
            this._addTestCommand ?? (
                this._addTestCommand = new RelayCommand(this.AddTestExecute));

        public ICommand AddQuestionCommand =>
            this._addQuestionCommand ?? (
                this._addQuestionCommand = new RelayCommand(this.AddQuestionExecute));

        public ICommand AddExamCommand =>
            this._addExamCommand ?? (
                this._addExamCommand = new RelayCommand(this.AddExamExecute));

        private async void AddStudentExecute()
        {
            try
            {
                if (this.IsLoading)
                    return;

                this.IsLoading = true;
                this.SetVisibilities(Visibility.Visible, true);

                var userResponse = await App.UsersApiClient.GetUserByUsernameAsync(this.CurrentStudent);

                var user = userResponse.Data;

                if (userResponse.ResponseStatus != ResponseStatus.Success)
                {
                    XemsMsgBox.Show("User is not found.");
                    return;
                }

                if (this.Exam.Students.Contains(user.Id))
                {
                    XemsMsgBox.Show("User already exists.");
                    return;
                }

                this.Exam.Students.Add(user.Id);

                XemsMsgBox.Show("User is added");
            }
            catch (Exception)
            {
                XemsMsgBox.Show("Error occured");
            }
            finally
            {
                this.IsLoading = false;
                this.SetVisibilities(Visibility.Collapsed, false);
            }
        }

        private async void AddExamExecute()
        {
            try
            {
                var response = await App.ExamsApiClient.PostExam(this.Exam);

                if (response.ResponseStatus != ExamsApiConsumer.Core.ResponseStatus.Success)
                {
                    XemsMsgBox.Show("Unable to add exam");
                    return;
                }

                XemsMsgBox.Show("Exam is successfully added");
            }
            catch (Exception)
            {
                XemsMsgBox.Show("Something went wrong while adding exam");
            }
        }

        private void AddTestExecute()
        {
            this.Exam.Tests.Add(this.CurrentTest);
            this.CurrentTest = new Test();
            this.CurrentQuestionNumber = 1;
        }

        private void AddQuestionExecute()
        {
            this.CurrentTest.Questions.Add(this.CurrentQuestion);
            this.CurrentQuestion = new Question();
            this.CurrentQuestionNumber++;
        }
    }
}