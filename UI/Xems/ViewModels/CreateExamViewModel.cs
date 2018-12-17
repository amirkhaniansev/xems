using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ExamsApiConsumer.Enums;
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
        
        private Question _currentQuestion;
        
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

        public ICommand AddTestCommand =>
            this._addTestCommand ?? (
                this._addTestCommand = new RelayCommand(this.AddTestExecute));

        public ICommand AddQuestionCommand =>
            this._addQuestionCommand ?? (
                this._addQuestionCommand = new RelayCommand(this.AddQuestionExecute));

        public ICommand AddExamCommand =>
            this._addExamCommand ?? (
                this._addExamCommand = new RelayCommand(this.AddExamExecute));

        public CreateExamViewModel()
        {
            this._exam = new Exam
            {
                CreatorUserId = XemsUser.Default.Id,
                ExamType = ExamType.Default,
                HasGrade = true,
                MaxGrade = 20M,
                Modifiers = new List<int>(),
                Students = new List<int>(),
                Tests = new List<Test>()
            };

            this._currentTest = new Test
            {
                CreatorId = XemsUser.Default.Id,
                Questions = new List<Question>()
            };

            this._currentQuestion = new Question
            {
                QuestionType = QuestionType.WithVariants,
                Variants = new List<Variant>
                {
                    new Variant
                    {
                        VariantSymbol = "a)"
                    },
                    new Variant
                    {
                        VariantSymbol = "b)"
                    },
                    new Variant
                    {
                        VariantSymbol = "c)"
                    },
                    new Variant
                    {
                        VariantSymbol = "d)"
                    }
                }
            };

            this._currentQuestionNumber = 1;

        }

        public async Task AddStudentExecute(string student)
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
            this.CurrentTest.Created = DateTime.Now;

            this.Exam.Tests.Add(this.CurrentTest);

            this.CurrentTest.Questions.Clear();
            this.CurrentTest.Duration = null;
            this.CurrentTest.Point = null;
            this.CurrentQuestionNumber = 1;
        }

        private void AddQuestionExecute()
        {
            this.CurrentTest.Questions.Add(this.CurrentQuestion);

            foreach (var variant in this.CurrentQuestion.Variants)
            {
                variant.Text = string.Empty;
            }

            this.CurrentQuestionNumber++;
        }
    }
}