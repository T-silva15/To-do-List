﻿using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Resources;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UTAD.ToDoList.WPF.Models;

using System.Globalization;
using System.Threading;


namespace UTAD.ToDoList.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public App App { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            App = (App)Application.Current;
        }

        private void BtnPerfil_Click(object sender, RoutedEventArgs e)
        {
            App.ViewPerfil = new ViewPerfil();
            App.ViewPerfil.ShowDialog(); // bloqueia o utilizador de mexer no Dashboard
        }

        private void btnNova_Tarefa_Click(object sender, RoutedEventArgs e)
        {
            App.ViewNovaTarefa = new ViewNovaTarefa();
            App.ViewNovaTarefa.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Perfil.GuardarPerfil();
        }

        private void ButtonForward_Click(object sender, RoutedEventArgs e)
        {
            schedule.Forward();
        }

        private void ButtonBackward_Click(object sender, RoutedEventArgs e)
        {
            schedule.Backward();
        }
    }
}