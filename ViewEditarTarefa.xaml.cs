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
            if ((dpInicio.SelectedDate < dpTermino.SelectedDate && tpInicio.Value >= tpTermino.Value && cbTodoDia.IsChecked == false) || dpInicio.SelectedDate > dpTermino.SelectedDate)
            {
                MessageBox.Show("A data de início deve ser anterior à data de término!", "Aviso!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (cbAlertaAntCustom.IsChecked == true && (tbAlertaAntCustom.Text == "" || !int.TryParse(tbAlertaAntCustom.Text, out int n)))
            {
                MessageBox.Show("O alerta de antecipação deve ser um número inteiro!", "Aviso!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (cbAlertaNECustom.IsChecked == true && (tbAlertaNECustom.Text == "" || !int.TryParse(tbAlertaNECustom.Text, out int n2)))
            {
                MessageBox.Show("O alerta de não execução deve ser um número inteiro!", "Aviso!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (PerDiario.IsChecked == true && PerSemanal.IsChecked == true || PerDiario.IsChecked == true && PerMensal.IsChecked == true || PerSemanal.IsChecked == true && PerMensal.IsChecked == true)
            {
                MessageBox.Show("Deve escolher apenas um tipo de periodicidade!", "Aviso!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // remover tarefa antiga
            App.Perfil.ListaTarefas.Remove((Tarefa)cbTarefas.SelectedItem);
            App.scheduler.RemoverMeeting(((Tarefa)cbTarefas.SelectedItem).Id);

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
                reminder.ReminderTimeInterval = new TimeSpan(0, 30, 0);
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
                reminder.ReminderTimeInterval = new TimeSpan(1, 0, 0);
                reminder.ReminderAlertTime = alerta.Data;
                reminder.Dismissed = false;
                listaAlertas.Add(reminder);
            }
            if (cbAlertaAntCustom.IsChecked == true)
            {
                Alerta alerta = new Alerta();
                alerta.Data = tarefa.DataInicio.AddMinutes(-Convert.ToInt32(tbAlertaAntCustom.Text));
                alerta.Mensagem = "A tarefa " + tarefa.Titulo + " vai começar dentro de " + tbAlertaAntCustom.Text + " minutos!";
                alerta.Tipo = TipoA.popup;
                alerta.EstadoAlerta = false;
                tarefa.ListaAlertaAnt.Add(alerta);

                Models.SchedulerReminder reminder = new();
                reminder.ReminderTimeInterval = new TimeSpan(0, Convert.ToInt32(tbAlertaAntCustom.Text), 0);
                reminder.ReminderAlertTime = alerta.Data;
                reminder.Dismissed = false;
                listaAlertas.Add(reminder);
            }

            // alerta execução
            if (cbAlNR15Min.IsChecked == true)
            {
                Alerta alerta = new Alerta();
                alerta.Data = tarefa.DataInicio.AddMinutes(15);
                alerta.Mensagem = "A tarefa " + tarefa.Titulo + " começou há 15 minutos atrás!";
                alerta.Tipo = TipoA.popup;
                alerta.EstadoAlerta = false;
                tarefa.ListaAlertaNaoExec.Add(alerta);

                Models.SchedulerReminder reminder = new();
                reminder.ReminderTimeInterval = new TimeSpan(0, -15, 0);
                reminder.ReminderAlertTime = alerta.Data;
                reminder.Dismissed = false;
                listaAlertas.Add(reminder);
            }
            if (cbAlNR30Min.IsChecked == true)
            {
                Alerta alerta = new Alerta();
                alerta.Data = tarefa.DataInicio.AddMinutes(30);
                alerta.Mensagem = "A tarefa " + tarefa.Titulo + " começou há 30 minutos atrás!";
                alerta.Tipo = TipoA.popup;
                alerta.EstadoAlerta = false;
                tarefa.ListaAlertaNaoExec.Add(alerta);

                Models.SchedulerReminder reminder = new();
                reminder.ReminderTimeInterval = new TimeSpan(0, -30, 0);
                reminder.ReminderAlertTime = alerta.Data;
                reminder.Dismissed = false;
                listaAlertas.Add(reminder);
            }
            if (cbAlNR60Min.IsChecked == true)
            {
                Alerta alerta = new Alerta();
                alerta.Data = tarefa.DataInicio.AddHours(1);
                alerta.Mensagem = "A tarefa " + tarefa.Titulo + " começou há 60 minutos atrás!";
                alerta.Tipo = TipoA.popup;
                alerta.EstadoAlerta = false;
                tarefa.ListaAlertaNaoExec.Add(alerta);

                Models.SchedulerReminder reminder = new();
                reminder.ReminderTimeInterval = new TimeSpan(-1, 0, 0);
                reminder.ReminderAlertTime = alerta.Data;
                reminder.Dismissed = false;
                listaAlertas.Add(reminder);
            }
            if (cbAlertaNECustom.IsChecked == true)
            {
                Alerta alerta = new Alerta();
                alerta.Data = tarefa.DataInicio.AddMinutes(Convert.ToInt32(tbAlertaNECustom.Text));
                alerta.Mensagem = "A tarefa " + tarefa.Titulo + " começou há " + tbAlertaNECustom.Text + " minutos atrás!";
                alerta.Tipo = TipoA.popup;
                alerta.EstadoAlerta = false;
                tarefa.ListaAlertaNaoExec.Add(alerta);

                Models.SchedulerReminder reminder = new();
                reminder.ReminderTimeInterval = new TimeSpan(0, -Convert.ToInt32(tbAlertaAntCustom.Text), 0);
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
                if (tbIntervalo.Text == "" || !int.TryParse(tbIntervalo.Text, out int x))
                    tarefa.Periodicidade.Intervalo = 1;
                else
                    tarefa.Periodicidade.Intervalo = Convert.ToInt32(tbIntervalo.Text);
                if (tbQuantidade.Text == "" || !int.TryParse(tbQuantidade.Text, out int y))
                    tarefa.Periodicidade.Quantidade = 99999;
                else
                    tarefa.Periodicidade.Quantidade = Convert.ToInt32(tbQuantidade.Text);
                recurrence = "FREQ=DAILY;INTERVAL=" + tarefa.Periodicidade.Intervalo + ";COUNT=" + tarefa.Periodicidade.Quantidade;
            }
            else if (PerSemanal.IsChecked == true)
            {
                tarefa.Periodicidade = new Periodicidade();
                tarefa.Periodicidade.Tipo = TipoP.WEEKLY;
                if (tbIntervalo.Text == "" || !int.TryParse(tbIntervalo.Text, out int x))
                    tarefa.Periodicidade.Intervalo = 1;
                else
                    tarefa.Periodicidade.Intervalo = Convert.ToInt32(tbIntervalo.Text);
                if (tbQuantidade.Text == "" || !int.TryParse(tbQuantidade.Text, out int y))
                    tarefa.Periodicidade.Quantidade = 99999;
                else
                    tarefa.Periodicidade.Quantidade = Convert.ToInt32(tbQuantidade.Text);
                recurrence = "FREQ=WEEKLY;INTERVAL=" + tarefa.Periodicidade.Intervalo + ";COUNT=" + tarefa.Periodicidade.Quantidade;

            }
            else if (PerMensal.IsChecked == true)
            {
                tarefa.Periodicidade = new Periodicidade();
                tarefa.Periodicidade.Tipo = TipoP.MONTHLY;
                if (tbIntervalo.Text == "" || !int.TryParse(tbIntervalo.Text, out int x))
                    tarefa.Periodicidade.Intervalo = 1;
                else
                    tarefa.Periodicidade.Intervalo = Convert.ToInt32(tbIntervalo.Text);
                if (tbQuantidade.Text == "" || !int.TryParse(tbQuantidade.Text, out int y))
                    tarefa.Periodicidade.Quantidade = 99999;
                else
                    tarefa.Periodicidade.Quantidade = Convert.ToInt32(tbQuantidade.Text);
                recurrence = "FREQ=MONTHLY;INTERVAL=" + tarefa.Periodicidade.Intervalo + ";COUNT=" + tarefa.Periodicidade.Quantidade;
            }

            App.Perfil.ListaTarefas.Add(tarefa);
            App.scheduler.AdicionarTarefa(tarefa.Titulo, tarefa.Descricao, listaAlertas, tarefa.DataInicio, tarefa.DataTermino, tarefa.DiaInteiro, cor, tarefa.Id, recurrence);

            this.Close();
        }

        private void BtnSair_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnLimpar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Tem a certeza que deseja limpar a lista de tarefas?", "Aviso!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                App.Perfil.ListaTarefas.Clear();
                App.scheduler.Meetings.Clear();
                this.Close();
            }
            else
            {
                return;
            }
        }

        private void BtnRemover_Click(object sender, RoutedEventArgs e)
        {
            if (cbTarefas.SelectedItem == null)
            {
                MessageBox.Show("Selecione uma tarefa para remover!", "Aviso!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // remover tarefa da lista do perfil e da lista do scheduler
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
            // limpa interface
            tbNome.Text = "";
            tbDescricao.Text = "";
 
            dpInicio.SelectedDate = DateTime.Now;
            dpTermino.SelectedDate = DateTime.Now;
            tpInicio.Value = DateTime.Now;
            tpTermino.Value = DateTime.Now;
            cbTodoDia.IsChecked = false;
            
            rbPorIniciar.IsChecked = false;
            rbEmExecucao.IsChecked = false;
            rbTerminada.IsChecked = false;
            
            rbPoucoImportante.IsChecked = true;
            rbNormal.IsChecked = false;
            rbImportante.IsChecked = false;
            rbPrioritaria.IsChecked = false;
            
            PerDiario.IsChecked = false;
            PerSemanal.IsChecked = false;
            PerMensal.IsChecked = false;
            tbIntervalo.Text = "";
            tbQuantidade.Text = "";
            
            cbAlAnt15Min.IsChecked = false;
            cbAlAnt30Min.IsChecked = false;
            cbAlAnt60Min.IsChecked = false;
            cbAlertaAntCustom.IsChecked = false;
            tbAlertaAntCustom.Text = "";
            cbAlNR15Min.IsChecked = false;
            cbAlNR30Min.IsChecked = false;
            cbAlNR60Min.IsChecked = false;
            cbAlertaNECustom.IsChecked = false;
            tbAlertaNECustom.Text = "";
            

            if (cbTarefas.SelectedItem is Tarefa selectedModel)
            {
                tbNome.Text = selectedModel.Titulo;
                tbDescricao.Text = selectedModel.Descricao;
                dpInicio.SelectedDate = selectedModel.DataInicio.Date;
                dpTermino.SelectedDate = selectedModel.DataTermino.Date;
                tpInicio.Value = selectedModel.DataInicio;
                tpTermino.Value = selectedModel.DataTermino;

                // dia inteiro
                if (selectedModel.DiaInteiro == true)
                {
                    cbTodoDia.IsChecked = true;
                }
                else
                {
                    cbTodoDia.IsChecked = false;
                }

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
                    if (alerta.Data != selectedModel.DataInicio.AddMinutes(-15) && alerta.Data != selectedModel.DataInicio.AddMinutes(-30) && alerta.Data != selectedModel.DataInicio.AddHours(-1))
                    {
                        cbAlertaAntCustom.IsChecked = true;
                        tbAlertaAntCustom.Text = (selectedModel.DataInicio - alerta.Data).Minutes.ToString();
                    }
                }

                // alerta não execução
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
                    if (alerta.Data != selectedModel.DataTermino.AddMinutes(15) && alerta.Data != selectedModel.DataTermino.AddMinutes(30) && alerta.Data != selectedModel.DataTermino.AddHours(1))
                    {
                        cbAlertaNECustom.IsChecked = true;
                        tbAlertaNECustom.Text = (alerta.Data - selectedModel.DataTermino).Minutes.ToString();
                    }
                }
            }
        }
    }
}
