using Microsoft.Win32;
using System.Diagnostics;
using System.Text;
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
            App = (App)Application.Current;
            //Loaded += Loading_Animation;
        }

        private void Loading_Animation(object sender, RoutedEventArgs e)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation
            {
                From = 0,
                To = this.Width,
                Duration = TimeSpan.FromSeconds(5)
            };

            DoubleAnimation heightAnimation = new DoubleAnimation
            {
                From = 0,
                To = this.Height,
                Duration = TimeSpan.FromSeconds(5)
            };

            this.BeginAnimation(UIElement.OpacityProperty, widthAnimation);
            this.BeginAnimation(UIElement.OpacityProperty, heightAnimation);
        }

        private void BtnPerfil_Click(object sender, RoutedEventArgs e)
        {
            App.ViewPerfil = new ViewPerfil();
            App.ViewPerfil.Show();
            this.Hide();
        }

        private void btnNova_Tarefa_Click(object sender, RoutedEventArgs e)
        {
            App.ViewNovaTarefa = new ViewNovaTarefa();
            App.ViewNovaTarefa.Show();
        }
    }
}