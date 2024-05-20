using Syncfusion.UI.Xaml.Scheduler;
using Syncfusion.Windows.Controls;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Eventing.Reader;
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
using UTAD.ToDoList.WPF.Models.Shared;


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
            // validação
            if (tbNome.Text == "")
            {
                MessageBox.Show("A tarefa deve ter nome!", "Aviso!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if ((dpInicio.SelectedDate < dpTermino.SelectedDate && tpInicio.Value >= tpTermino.Value && cbTodoDia.IsChecked == false) || dpInicio.SelectedDate > dpTermino.SelectedDate)
            {
                MessageBox.Show("A data de início deve ser anterior à data de término!", "Aviso!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // tarefa no modelo
            Tarefa tarefa = new Tarefa();
            tarefa.Titulo = tbNome.Text;


            // lê do date picker a data e as horas e minutos do time picker
            DateTime dateI = dpInicio.SelectedDate.Value.Date;
            DateTime dateT = dpTermino.SelectedDate.Value.Date;

            // verifica se tarefa é de dia inteiro
            if (cbTodoDia.IsChecked == true)
            {
                tarefa.DiaInteiro = true;
                tarefa.DataInicio = dateI;
                tarefa.DataTermino = dateT;
            }
            else
            {
                tarefa.DiaInteiro = false;
                TimeSpan timeI = tpInicio.Value.Value.TimeOfDay;
                TimeSpan timeT = tpTermino.Value.Value.TimeOfDay;
                tarefa.DataInicio = dateI + timeI;
                tarefa.DataTermino = dateT + timeT;
            }
            

            tarefa.Descricao = tbDescricao.Text;

            // estado
            if (rbPorIniciar.IsChecked == true)
            {
                tarefa.Estado = Estado.Por_Iniciar;
            }
            else if (rbEmExecucao.IsChecked == true)
            {
                tarefa.Estado = Estado.Em_Execucao;
            }
            else if (rbTerminada.IsChecked == true)
            {
                tarefa.Estado = Estado.Terminada;
            }


            // prioridade 
            SolidColorBrush cor = new SolidColorBrush();

            // utilizador nao escolheu prioridade (prioridade pouco importante)
            tarefa.NivelImportancia = NivelImportancia.Pouco_Importante;
            cor = new BrushConverter().ConvertFrom("#87FF81") as SolidColorBrush;

            if (rbNormal.IsChecked == true)
            {
                tarefa.NivelImportancia = NivelImportancia.Normal;
                cor = new BrushConverter().ConvertFrom("#849EEA") as SolidColorBrush;
            }
            else if (rbImportante.IsChecked == true)
            {
                tarefa.NivelImportancia = NivelImportancia.Importante;
                cor = new BrushConverter().ConvertFrom("#FE8A5F") as SolidColorBrush;

            }
            else if (rbPrioritaria.IsChecked == true)
            {
                tarefa.NivelImportancia = NivelImportancia.Prioritaria;
                cor = new BrushConverter().ConvertFrom("#E85671") as SolidColorBrush;
            }

            ObservableCollection<Models.SchedulerReminder> listaAlertas = new();

            // alerta antecipação
            if (cbAlAnt15Min.IsChecked == true)
            {
                Alerta alerta = new Alerta();
                alerta.Data = tarefa.DataInicio.AddMinutes(-15);
                alerta.Mensagem = "A tarefa " + tarefa.Titulo + " vai começar dentro de 15 minutos!";
                alerta.Tipo = TipoA.popup;
                alerta.EstadoAlerta = false;
                tarefa.ListaAlertaAnt.Add(alerta);
               

                Models.SchedulerReminder reminder = new();
                reminder.ReminderTimeInterval = new TimeSpan(0, 15, 0);
                reminder.ReminderAlertTime = alerta.Data;
                reminder.Dismissed = false;
                listaAlertas.Add(reminder);

            }
            if (cbAlAnt30Min.IsChecked == true)
            {
                Alerta alerta = new Alerta();
                alerta.Data = tarefa.DataInicio.AddMinutes(-30);
                alerta.Mensagem = "A tarefa " + tarefa.Titulo + " vai começar dentro de 30 minutos!";
                alerta.Tipo = TipoA.popup;
                alerta.EstadoAlerta = false;
                tarefa.ListaAlertaAnt.Add(alerta);

                Models.SchedulerReminder reminder = new();
                reminder.ReminderTimeInterval = new TimeSpan(0, 15, 0);
                reminder.ReminderAlertTime = alerta.Data;
                reminder.Dismissed = false;
                listaAlertas.Add(reminder);
            }
            if (cbAlAnt60Min.IsChecked == true)
            {
                Alerta alerta = new Alerta();
                alerta.Data = tarefa.DataInicio.AddHours(-1);
                alerta.Mensagem = "A tarefa " + tarefa.Titulo + " vai começar dentro de 60 minutos!";
                alerta.Tipo = TipoA.popup;
                alerta.EstadoAlerta = false;
                tarefa.ListaAlertaAnt.Add(alerta);

                Models.SchedulerReminder reminder = new();
                reminder.ReminderTimeInterval = new TimeSpan(0, 15, 0);
                reminder.ReminderAlertTime = alerta.Data;
                reminder.Dismissed = false;
                listaAlertas.Add(reminder);
            }

            // alerta execução
            if (cbAlNR15Min.IsChecked == true)
            {
                Alerta alerta = new Alerta();
                alerta.Data = tarefa.DataTermino.AddMinutes(15);
                alerta.Mensagem = "A tarefa " + tarefa.Titulo + " começou há 15 minutos atrás!";
                alerta.Tipo = TipoA.popup;
                alerta.EstadoAlerta = false;
                tarefa.ListaAlertaNaoExec.Add(alerta);

                Models.SchedulerReminder reminder = new();
                reminder.ReminderTimeInterval = new TimeSpan(0, 15, 0);
                reminder.ReminderAlertTime = alerta.Data;
                reminder.Dismissed = false;
                listaAlertas.Add(reminder);
            }
            if (cbAlNR30Min.IsChecked == true)
            {
                Alerta alerta = new Alerta();
                alerta.Data = tarefa.DataTermino.AddMinutes(30);
                alerta.Mensagem = "A tarefa " + tarefa.Titulo + " começou há 30 minutos atrás!";
                alerta.Tipo = TipoA.popup;
                alerta.EstadoAlerta = false;
                tarefa.ListaAlertaNaoExec.Add(alerta);

                Models.SchedulerReminder reminder = new();
                reminder.ReminderTimeInterval = new TimeSpan(0, 15, 0);
                reminder.ReminderAlertTime = alerta.Data;
                reminder.Dismissed = false;
                listaAlertas.Add(reminder);
            }
            if (cbAlNR60Min.IsChecked == true)
            {
                Alerta alerta = new Alerta();
                alerta.Data = tarefa.DataTermino.AddHours(1);
                alerta.Mensagem = "A tarefa " + tarefa.Titulo + " começou há 60 minutos atrás!";    
                alerta.Tipo = TipoA.popup;
                alerta.EstadoAlerta = false;
                tarefa.ListaAlertaNaoExec.Add(alerta);

                Models.SchedulerReminder reminder = new();
                reminder.ReminderTimeInterval = new TimeSpan(0, 15, 0);
                reminder.ReminderAlertTime = alerta.Data;
                reminder.Dismissed = false;
                listaAlertas.Add(reminder);
            }

            // periodicidade
            string recurrence = "";
            if (PerDiario.IsChecked == true)
            {
                tarefa.Periodicidade = new Periodicidade();
                tarefa.Periodicidade.Tipo = TipoP.DAILY;
                if (tbIntervalo.Text == "")
                    tarefa.Periodicidade.Intervalo = 1;
                tarefa.Periodicidade.Intervalo = Convert.ToInt32(tbIntervalo.Text);
                if (tbQuantidade.Text == "")
                    tarefa.Periodicidade.Quantidade = 0;
                tarefa.Periodicidade.Quantidade = Convert.ToInt32(tbQuantidade.Text);
                recurrence = "FREQ=DAILY;INTERVAL=" + tarefa.Periodicidade.Intervalo + ";COUNT=" + tarefa.Periodicidade.Quantidade;
            }
            else if (PerSemanal.IsChecked == true)
            {
                tarefa.Periodicidade = new Periodicidade();
                tarefa.Periodicidade.Tipo = TipoP.WEEKLY;
                if (tbIntervalo.Text == "")
                    tarefa.Periodicidade.Intervalo = 1;
                tarefa.Periodicidade.Intervalo = Convert.ToInt32(tbIntervalo.Text);
                if (tbQuantidade.Text == "")
                    tarefa.Periodicidade.Quantidade = 0;
                tarefa.Periodicidade.Quantidade = Convert.ToInt32(tbQuantidade.Text);
                recurrence = "FREQ=WEEKLY;INTERVAL=" + tarefa.Periodicidade.Intervalo + ";COUNT=" + tarefa.Periodicidade.Quantidade;

            }
            else if (PerMensal.IsChecked == true)
            {
                tarefa.Periodicidade = new Periodicidade();
                tarefa.Periodicidade.Tipo = TipoP.MONTHLY;
                if (tbIntervalo.Text == "")
                    tarefa.Periodicidade.Intervalo = 1;
                tarefa.Periodicidade.Intervalo = Convert.ToInt32(tbIntervalo.Text);
                if (tbQuantidade.Text == "")
                    tarefa.Periodicidade.Quantidade = 0;
                tarefa.Periodicidade.Quantidade = Convert.ToInt32(tbQuantidade.Text);
                recurrence = "FREQ=MONTHLY;INTERVAL=" + tarefa.Periodicidade.Intervalo + ";COUNT=" + tarefa.Periodicidade.Quantidade;
            }

            App.Perfil.ListaTarefas.Add(tarefa);
            App.scheduler.AdicionarTarefa(tarefa.Titulo, tarefa.Descricao, listaAlertas,  tarefa.DataInicio, tarefa.DataTermino,tarefa.DiaInteiro ,cor, tarefa.Id, recurrence);

            this.Close();
        }

        private void BtnSair_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dpInicio.SelectedDate = DateTime.Now;
            dpTermino.SelectedDate = DateTime.Now;
            sfCalendario.ItemsSource = App.scheduler.Meetings;
        }
    }
}