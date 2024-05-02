using Syncfusion.UI.Xaml.Scheduler;
using Syncfusion.Windows.Shared;
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
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("PT-pt");
        }

        private void ButtonForward_Click(object sender, RoutedEventArgs e)
        {
            schedule.Forward();
        }

        private void ButtonBackward_Click(object sender, RoutedEventArgs e)
        {
            schedule.Backward();
        }

        private void BtnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void schedule_SelectionChanged(object sender, Syncfusion.UI.Xaml.Scheduler.SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = schedule.SelectedDate;
        }
    }
}
