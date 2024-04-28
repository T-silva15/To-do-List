using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class ViewPerfil : Window
    {

        public App App { get; set; }

        public ViewPerfil()
        {
            App = (App)Application.Current;
            InitializeComponent();
        }



        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "to-do list");
            path = System.IO.Path.Combine(path, App.Perfil.Nome) + ".json";

            // elimina ficheiro antigo se houver alterações
            if (tbNome.Text != App.Perfil.Nome || tbEmail.Text != App.Perfil.Email || tbPassword.Text != App.Perfil.Password)
            {
                if(File.Exists(path))
                {
                    File.Delete(path);
                }  
            }

            App.Perfil.Nome = tbNome.Text;
            App.Perfil.Email = tbEmail.Text;
            App.Perfil.Password = tbPassword.Text;
            this.Close();
        }

        private void btnAlterarImagem_Click(object sender, RoutedEventArgs e)
        {
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "to-do list");
            path = System.IO.Path.Combine(path, App.Perfil.Nome) + ".json";

            OpenFileDialog openFileDialog = new();

            // filtro de ficheiros de imagem
            openFileDialog.Filter = "Ficheiros de Imagem|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

            // após selecionar a imagem, muda imagem da view e guarda no objeto perfil
            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage img = new BitmapImage(new Uri(openFileDialog.FileName));
                ftPerfil.Source = img;
                App.Perfil.Fotografia = openFileDialog.FileName;
            }
            else
            {
                App.Perfil.Fotografia = "";
            }

            // elimina ficheiro antigo se houver alterações
            if (tbNome.Text != App.Perfil.Nome || tbEmail.Text != App.Perfil.Email || tbPassword.Text != App.Perfil.Password)
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }

        }

        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ViewPerfil_Loaded(object sender, RoutedEventArgs e)
        {
            tbNome.Text = App.Perfil.Nome;
            tbEmail.Text = App.Perfil.Email;
            tbPassword.Text = App.Perfil.Password;
            if (App.Perfil.Fotografia != "")
            {
                ftPerfil.Source = new BitmapImage(new Uri(App.Perfil.Fotografia));
            }
            else
            {
                ftPerfil.Source = new BitmapImage(new Uri("C:\\code\\LABPSW\\UTAD.ToDoList\\UTAD.ToDoList.WPF\\Images\\Camera.png"));
            }
        }
    }
}
