using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
    public partial class ViewRegisto : Window
    {
        public App App;

        public ViewRegisto()
        {
            InitializeComponent();
            App = (App)Application.Current;
        }

        private void btnInserirImagem_Click(object sender, RoutedEventArgs e)
        {
            App.Perfil = new Perfil();

            OpenFileDialog openFileDialog = new();

            // filtro de ficheiros de imagem
            openFileDialog.Filter = "Ficheiros de Imagem|*.jpg;*.jpeg;*.png";
            
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
        }

        private void btnRegistar_Click(object sender, RoutedEventArgs e)
        {
            // verifica se os campos estão preenchidos
            if (tbNome.Text == "" || tbEmail.Text == "" || App.ConvertToPlainText(tbPassword.SecurePassword) == "")
            {
                MessageBox.Show("Preencha todos os campos!", "Erro de Preenchimento!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // verifica se o email é válido
            if (App.IsValidEmail(tbEmail.Text) == false)
            {
                MessageBox.Show("Email inválido!", "Erro de Preenchimento!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // verifica se o utilizador já existe
            string folderName = "to-do list";
            string fileName = tbNome.Text + ".json";
            string environmentPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string filePath = System.IO.Path.Combine(environmentPath, folderName, fileName);
            if (File.Exists(filePath))
            {
                MessageBox.Show("Utilizador já existe!", "Erro de Preenchimento!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            App.Perfil.Nome = tbNome.Text;
            App.Perfil.Email = tbEmail.Text;
            App.Perfil.Password = App.ConvertToPlainText(tbPassword.SecurePassword);
            
            App.Perfil.GuardarPerfil();

            this.Close();
            
        }
    }
}
