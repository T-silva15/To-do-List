using Syncfusion.UI.Xaml.Scheduler;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class ViewEditarTarefa : Window
    {
        public App App;

        public ViewEditarTarefa()
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
            if ((dpInicio.SelectedDate < dpTermino.SelectedDate && tpInicio.Value >= tpTermino.Value) || dpInicio.SelectedDate > dpTermino.SelectedDate)
            {
                MessageBox.Show("A data de início deve ser anterior à data de término!", "Aviso!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // remover tarefa antiga
            App.Perfil.ListaTarefas.Remove((Tarefa)cbTarefas.SelectedItem);
            
            // tarefa no modelo
            Tarefa tarefa = new Tarefa();
            tarefa.Titulo = tbNome.Text;


            // lê do date picker a data e as horas e minutos do time picker
            DateTime dateI = dpInicio.SelectedDate.Value.Date;
            TimeSpan timeI = tpInicio.Value.Value.TimeOfDay;
            tarefa.DataInicio = dateI + timeI;
            DateTime dateT = dpTermino.SelectedDate.Value.Date;
            TimeSpan timeT = tpTermino.Value.Value.TimeOfDay;
            tarefa.DataTermino = dateT + timeT; 
            
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
            App.scheduler.AdicionarTarefa(tarefa.Titulo, tarefa.Descricao, listaAlertas, tarefa.DataInicio, tarefa.DataTermino, cor, tarefa.Id, recurrence);

            this.Close();
        }

        private void BtnSair_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnRemover_Click(object sender, RoutedEventArgs e)
        {
            App.scheduler.RemoverMeeting(((Tarefa)cbTarefas.SelectedItem).Id);
            App.Perfil.ListaTarefas.Remove((Tarefa)cbTarefas.SelectedItem);
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dpInicio.SelectedDate = DateTime.Now;
            dpTermino.SelectedDate = DateTime.Now;
            sfCalendario.ItemsSource = App.scheduler.Meetings;
            cbTarefas.ItemsSource = App.Perfil.ListaTarefas;
        }

        private void cbTarefas_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cbTarefas.SelectedItem is Tarefa selectedModel)
            {
                tbNome.Text = selectedModel.Titulo;
                tbDescricao.Text = selectedModel.Descricao;
                dpInicio.SelectedDate = selectedModel.DataInicio.Date;
                dpTermino.SelectedDate = selectedModel.DataTermino.Date;
                tpInicio.Value = selectedModel.DataInicio;
                tpTermino.Value = selectedModel.DataTermino;

                // estado
                if (selectedModel.Estado == Estado.Por_Iniciar)
                {
                    rbPorIniciar.IsChecked = true;
                }
                else if (selectedModel.Estado == Estado.Em_Execucao)
                {
                    rbEmExecucao.IsChecked = true;
                }
                else if (selectedModel.Estado == Estado.Terminada)
                {
                    rbTerminada.IsChecked = true;
                }

                // prioridade
                if (selectedModel.NivelImportancia == NivelImportancia.Pouco_Importante)
                {
                    rbPoucoImportante.IsChecked = true;
                }
                else if (selectedModel.NivelImportancia == NivelImportancia.Normal)
                {
                    rbNormal.IsChecked = true;
                }
                else if (selectedModel.NivelImportancia == NivelImportancia.Importante)
                {
                    rbImportante.IsChecked = true;
                }
                else if (selectedModel.NivelImportancia == NivelImportancia.Prioritaria)
                {
                    rbPrioritaria.IsChecked = true;
                }

                // periodicidade
                if (selectedModel.Periodicidade != null)
                {
                    if (selectedModel.Periodicidade.Tipo == TipoP.DAILY)
                    {
                        PerDiario.IsChecked = true;
                    }
                    else if (selectedModel.Periodicidade.Tipo == TipoP.WEEKLY)
                    {
                        PerSemanal.IsChecked = true;
                    }
                    else if (selectedModel.Periodicidade.Tipo == TipoP.MONTHLY)
                    {
                        PerMensal.IsChecked = true;
                    }

                    tbIntervalo.Text = selectedModel.Periodicidade.Intervalo.ToString();
                    tbQuantidade.Text = selectedModel.Periodicidade.Quantidade.ToString();
                }

                

                // alerta antecipação
                foreach (Alerta alerta in selectedModel.ListaAlertaAnt)
                {
                    if (alerta.Data == selectedModel.DataInicio.AddMinutes(-15))
                    {
                        cbAlAnt15Min.IsChecked = true;
                    }
                    if (alerta.Data == selectedModel.DataInicio.AddMinutes(-30))
                    {
                        cbAlAnt30Min.IsChecked = true;
                    }
                    if (alerta.Data == selectedModel.DataInicio.AddHours(-1))
                    {
                        cbAlAnt60Min.IsChecked = true;
                    }
                    // alerta execução
                }
                foreach (Alerta alerta in selectedModel.ListaAlertaNaoExec)
                {
                    if (alerta.Data == selectedModel.DataTermino.AddMinutes(15))
                    {
                        cbAlNR15Min.IsChecked = true;
                    }
                    if (alerta.Data == selectedModel.DataTermino.AddMinutes(30))
                    {
                        cbAlNR30Min.IsChecked = true;
                    }
                    if (alerta.Data == selectedModel.DataTermino.AddHours(1))
                    {
                        cbAlNR60Min.IsChecked = true;
                    }
                }
            }
        }
    }
}
