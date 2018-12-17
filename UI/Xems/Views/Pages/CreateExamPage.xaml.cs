using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xems.ViewModels;

namespace Xems.Views.Pages
{
    /// <summary>
    /// Interaction logic for CreateExamPage.xaml
    /// </summary>
    public partial class CreateExamPage : Page
    {
        public CreateExamPage()
        {
            InitializeComponent();

            this.DataContext = new CreateExamViewModel();
        }

        private async void UIElement_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
                return;

            var vm = this.DataContext as CreateExamViewModel;

            if(vm == null)
                return;
            
            await vm.AddStudentExecute(this._studentTextBox.Text);
        }
    }
}
