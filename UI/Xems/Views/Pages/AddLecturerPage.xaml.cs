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
    /// Interaction logic for AddLecturerPage.xaml
    /// </summary>
    public partial class AddLecturerPage : Page
    {
        public AddLecturerPage()
        {
            InitializeComponent();

            this.DataContext = new AddLecturerViewModel();
        }
    }
}
