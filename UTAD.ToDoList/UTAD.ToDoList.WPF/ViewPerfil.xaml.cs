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

namespace UTAD.ToDoList.WPF
{
    /// <summary>
    /// Interaction logic for ViewPerfil.xaml
    /// </summary>
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

        }

        private void btnAlterarImagem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
