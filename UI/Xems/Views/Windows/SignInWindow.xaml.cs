﻿using System;
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
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        private readonly SignInWindowViewModel _vm;

        public SignInWindow()
        {
            InitializeComponent();

            this.DataContext = this._vm = new SignInWindowViewModel();
        }

        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            this._vm.SignInInfo.Password = ((PasswordBox) sender).Password;
        }
    }
}
