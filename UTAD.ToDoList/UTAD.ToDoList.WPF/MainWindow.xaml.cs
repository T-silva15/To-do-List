using Microsoft.Win32;
using Syncfusion.UI.Xaml.Scheduler;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
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

        // Botão de visualização de perfil
        private void BtnPerfil_Click(object sender, RoutedEventArgs e)
        {
            App.ViewPerfil = new ViewPerfil();
            App.ViewPerfil.ShowDialog(); // bloqueia o utilizador de mexer no Dashboard
        }

        // Botões de adicionar e editar tarefas
        private void BtnNova_Tarefa_Click(object sender, RoutedEventArgs e)
        {
            App.ViewNovaTarefa = new ViewNovaTarefa();
            App.ViewNovaTarefa.Show();
        }

        private void BtnEditar_Tarefa_Click(object sender, RoutedEventArgs e)
        {
            App.ViewEditarTarefa = new ViewEditarTarefa();
            App.ViewEditarTarefa.Show();
        }

        // Botão de fechar janela e guardar o perfil do utilizador
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Perfil.GuardarPerfil();
        }

        // Botões de navegação no calendário
        private void ButtonForward_Click(object sender, RoutedEventArgs e)
        {
            sfCalendario.Forward();
        }

        private void ButtonBackward_Click(object sender, RoutedEventArgs e)
        {
            sfCalendario.Backward();
        }

        // Botões de visualização do calendário
        private void BtnDia_Click(object sender, RoutedEventArgs e)
        {
            sfCalendario.ViewType = SchedulerViewType.Day;
        }

        private void BtnSemana_Click(object sender, RoutedEventArgs e)
        {
            sfCalendario.ViewType = SchedulerViewType.Week;
        }

        private void BtnSemanaTrab_Click(object sender, RoutedEventArgs e)
        {
            sfCalendario.ViewType = SchedulerViewType.WorkWeek;
        }

        private void BtnMes_Click(object sender, RoutedEventArgs e)
        {
            sfCalendario.ViewType = SchedulerViewType.Month;
        }


        // Carregar as tarefas no calendário quando a janela é carregada
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sfCalendario.ItemsSource = App.scheduler.Meetings;
            // Carregar as tarefas no calendário com base no perfil do utilizador
            App.scheduler.CarregarTarefas(App.Perfil.ListaTarefas);
        }


        // Botões de ordenação por estado
        private void btnTarefasTodas_Click(object sender, RoutedEventArgs e)
        {
            App.scheduler.CarregarTarefas(App.Perfil.ListaTarefas);
        }

        private void btnTarefasCompletas_Click(object sender, RoutedEventArgs e)
        {
            App.scheduler.CarregarTarefasEstado(App.Perfil.ListaTarefas, Estado.Terminada);
        }

        private void btnTarefasExecucao_Click(object sender, RoutedEventArgs e)
        {
            App.scheduler.CarregarTarefasEstado(App.Perfil.ListaTarefas, Estado.Em_Execucao);
        }

        private void btnTarefasPendentes_Click(object sender, RoutedEventArgs e)
        {
            App.scheduler.CarregarTarefasEstado(App.Perfil.ListaTarefas, Estado.Por_Iniciar);
        }


        // Botões de ordenação por nível de importância
        private void btnPrioritaria_Click(object sender, RoutedEventArgs e)
        {
            App.scheduler.CarregarTarefasNivelImportancia(App.Perfil.ListaTarefas, NivelImportancia.Prioritaria);
        }

        private void btnImportante_Click(object sender, RoutedEventArgs e)
        {
            App.scheduler.CarregarTarefasNivelImportancia(App.Perfil.ListaTarefas, NivelImportancia.Importante);
        }

        private void btnNormal_Click(object sender, RoutedEventArgs e)
        {
            App.scheduler.CarregarTarefasNivelImportancia(App.Perfil.ListaTarefas, NivelImportancia.Normal);
        }

        private void btnPoucoImp_Click(object sender, RoutedEventArgs e)
        {
            App.scheduler.CarregarTarefasNivelImportancia(App.Perfil.ListaTarefas, NivelImportancia.Pouco_Importante);
        }

        
    }
}