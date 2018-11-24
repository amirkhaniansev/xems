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
using System.Windows.Shapes;
using Xems.ViewModels;

namespace Xems.Views.Windows
{
    /// <summary>
    /// Interaction logic for XemsMsgBox.xaml
    /// </summary>
    public partial class XemsMsgBox : Window
    {
        public XemsMsgBox()
        {
            InitializeComponent();

            this.DataContext = new XemsViewModelBase();
        }

        public static void Show(string message)
        {
            var window = new XemsMsgBox();
            window.message.Text = message;
            window.Show();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
