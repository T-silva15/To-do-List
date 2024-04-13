using Microsoft.Win32;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
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
            App = (App)App.Current;
        }
    }
}