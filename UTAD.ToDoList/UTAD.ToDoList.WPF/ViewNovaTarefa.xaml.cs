using Syncfusion.UI.Xaml.Scheduler;
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
    /// Interaction logic for ViewNovaTarefa.xaml
    /// </summary>
    public partial class ViewNovaTarefa : Window
    {
        public Application App;

        public ViewNovaTarefa()
        {
            App = (App)Application.Current;
            InitializeComponent();
        }

        private void ButtonForward_Click(object sender, RoutedEventArgs e)
        {
            schedule.Forward();
        }

        private void ButtonBackward_Click(object sender, RoutedEventArgs e)
        {
            schedule.Forward();
        }
    }
}
