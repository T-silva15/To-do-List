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

namespace UTAD.ToDoList.WPF
{
    /// <summary>
    /// Interaction logic for ViewLogin.xaml
    /// </summary>
    public partial class ViewLogin : Window
    {
        public App App;

        public ViewLogin()
        {
            App = (App)Application.Current;
            InitializeComponent();
        }

        private void btnRegistar_Click(object sender, RoutedEventArgs e)
        {
            App.ViewRegisto = new ViewRegisto();
            App.ViewRegisto.Show();
        }

        private void btnRegistar_Copy_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
