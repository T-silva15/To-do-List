using Syncfusion.UI.Xaml.Scheduler;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ViewNovaTarefa.xaml
    /// </summary>
    public partial class ViewNovaTarefa : Window
    {
        public App App;

        public ViewNovaTarefa()
        {
            App = (App)Application.Current;
            InitializeComponent();
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("PT-pt");
        }

        private void ButtonForward_Click(object sender, RoutedEventArgs e)
        {
            sfCalendario.Forward();
        }

        private void ButtonBackward_Click(object sender, RoutedEventArgs e)
        {
            sfCalendario.Backward();
        }

        private void BtnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            if (App.Perfil.ListaTarefas == null)
            {
                App.Perfil.ListaTarefas = new ObservableCollection<Tarefa>();
            }
            Tarefa tarefa = new Tarefa();
            tarefa.Titulo = tbNome.Text;
            tarefa.DataInicio = dpInicio.SelectedDate.Value;
            tarefa.DataTermino = dpTermino.SelectedDate.Value;
            tarefa.Descricao = tbDescricao.Text;
           
            

            
        }

        private void BtnSair_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dpInicio.SelectedDate = DateTime.Now;
            dpTermino.SelectedDate = DateTime.Now;
        }
        
    }
}
