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
using UTAD.ToDoList.WPF.Models;

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

        private void BtnRegistar_Click(object sender, RoutedEventArgs e)
        {
            App.ViewRegisto = new ViewRegisto();
            App.ViewRegisto.Show();
        }

        private void BtnLoginClick(object sender, RoutedEventArgs e)
        {
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "to-do list");

            path = System.IO.Path.Combine(path, tbNome.Text) + ".json";

            if (System.IO.File.Exists(path))
            {
                
                string? jsonString = System.IO.File.ReadAllText(path);
                App.Perfil = System.Text.Json.JsonSerializer.Deserialize<Perfil>(jsonString);

                if ((App.ConvertToPlainText(tbPassword.SecurePassword)) == App.Perfil.Password)
                {
                    App.MainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Password incorreta!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Utilizador não encontrado");
            }
        }
    }
}
