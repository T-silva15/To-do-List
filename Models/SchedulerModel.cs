using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Media;

namespace UTAD.ToDoList.WPF.Models
{
    public class SchedulerModel : INotifyPropertyChanged
    {
        private ObservableCollection<Meeting> meetings;
        public event PropertyChangedEventHandler? PropertyChanged;
        public SchedulerModel()
        {
            Meetings = new ObservableCollection<Meeting>();
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("PT-pt");
        }

        public ObservableCollection<Meeting> Meetings
        {
            get { return meetings; }
            set
            {
                meetings = value;
                RaiseOnPropertyChanged("Meetings");
            }
        }

        /// <summary>
        /// Função que recebe os dados de uma nova tarefa e a adiciona ao calendário, com base na classe Meeting
        /// </summary>
        /// <param name="nome">Título da Tarefa</param>
        /// <param name="descricao">Notas da Tarefa</param>
        /// <param name="inicio">Data Inicial</param>
        /// <param name="fim">Data de Término</param>
        /// <param name="cor">Cor da Tarefa -> associada à prioridade 
        /// (vermelho -> Prioritária)
        /// (laranja -> Importante)
        /// (verde -> Normal) 
        /// (azul -> Pouco Importante</param>
        /// <param name="id">Id da Tarefa, gerado automaticamente devido à classe BaseModel</param>
        /// <param name="recurrence">Recurrência da Tarefa, vezes a repetir e intervalo de repetição</param>
        public void AdicionarTarefa(string nome, string descricao, ObservableCollection<SchedulerReminder> Alertas, DateTime inicio, DateTime fim, bool IsAllDay, SolidColorBrush cor, string id, string recurrence)
        {
            // tarefa no modelo
            Meeting meeting = new Meeting();
            meeting.Name = nome;
            meeting.Description = descricao;
            meeting.From = inicio;
            meeting.To = fim;
            meeting.BackgroundColor = cor;
            meeting.Id = id;
            meeting.RecurrenceRule = recurrence;
            meeting.AllDay = IsAllDay;
            foreach (SchedulerReminder reminder in Alertas)
            {
                reminder.Data = meeting;
            }
            meeting.Reminders = Alertas;
            Meetings.Add(meeting);

            RaiseOnPropertyChanged("Meetings");
        }

        /// <summary>
        /// Evento que é chamado sempre que uma propriedade é alterada, avisa o calendário que a coleção de tarefas foi alterada.
        /// </summary>
        /// <param name="propertyName">Nome da Propriedade modificada</param>
        public void RaiseOnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Função que carrega as tarefas do perfil do utilizador para o calendário,
        /// é executada sempre que o calendário é carregado.
        /// </summary>
        /// <param name="tarefas">Lista de tarefas armazenada no utilizador</param>
        public void CarregarTarefas(List<Tarefa> tarefas)
        { 
            if (Meetings.Count > 0)
                Meetings.Clear();
            foreach (Tarefa tarefa in tarefas)
            {
                Meeting meeting = new Meeting();
                meeting.Name = tarefa.Titulo;
                meeting.Description = tarefa.Descricao;
                meeting.From = tarefa.DataInicio;
                meeting.To = tarefa.DataTermino;
                meeting.AllDay = tarefa.DiaInteiro;
                if (tarefa.NivelImportancia == NivelImportancia.Pouco_Importante)
                    meeting.BackgroundColor = new BrushConverter().ConvertFrom("#87FF81") as SolidColorBrush;
                else if (tarefa.NivelImportancia == NivelImportancia.Normal)
                    meeting.BackgroundColor = new BrushConverter().ConvertFrom("#849EEA") as SolidColorBrush;
                else if (tarefa.NivelImportancia == NivelImportancia.Importante)
                    meeting.BackgroundColor = new BrushConverter().ConvertFrom("#FE8A5F") as SolidColorBrush;
                else if (tarefa.NivelImportancia == NivelImportancia.Prioritaria)
                    meeting.BackgroundColor = new BrushConverter().ConvertFrom("#E85671") as SolidColorBrush;
                meeting.Id = tarefa.Id;
                if (tarefa.Periodicidade != null)
                {
                    meeting.RecurrenceRule = "FREQ=" + tarefa.Periodicidade.Tipo.ToString().ToUpper() + ";INTERVAL=" + tarefa.Periodicidade.Intervalo + ";COUNT=" + tarefa.Periodicidade.Quantidade;
                }
                if (tarefa.ListaAlertaAnt != null || tarefa.ListaAlertaNaoExec != null)
                {
                    ObservableCollection<SchedulerReminder> reminders = new ObservableCollection<SchedulerReminder>();
                    foreach (Alerta alerta in tarefa.ListaAlertaAnt)
                    {
                        SchedulerReminder reminder = new SchedulerReminder();


                        reminder.ReminderTimeInterval = new TimeSpan(0, 15, 0);
                        reminder.ReminderAlertTime = alerta.Data;
                        if (alerta.Data < DateTime.Now)
                            reminder.Dismissed = true;
                        else
                            reminder.Dismissed = false;
                        reminders.Add(reminder);
                    }

                    foreach (Alerta alerta in tarefa.ListaAlertaNaoExec)
                    {
                        SchedulerReminder reminder = new SchedulerReminder();
                        reminder.ReminderTimeInterval = new TimeSpan(0, -15, 0);
                        reminder.ReminderAlertTime = alerta.Data;
                        if (alerta.Data < DateTime.Now)
                            reminder.Dismissed = true;
                        else
                            reminder.Dismissed = false;
                        reminder.Data = meeting;
                        reminders.Add(reminder);
                    }

                    meeting.Reminders = reminders;
                }

                Meetings.Add(meeting);

            }
            RaiseOnPropertyChanged("Meetings");
        }

        /// <summary>
        /// Função que carrega as tarefas do perfil do utilizador para o calendário,
        /// com base no estado passado como parâmetro, utilizada nos botões de ordenação.
        /// </summary>
        /// <param name="tarefas">Lista de tarefas armazenada no utilizador</param>
        /// <param name="estado">Estado das tarefas a serem mostradas</param>
        public void CarregarTarefasEstado(List<Tarefa> tarefas, Estado estado)
        {
            if (Meetings.Count > 0)
                Meetings.Clear();
            foreach (Tarefa tarefa in tarefas)
            {
                if (tarefa.Estado == estado)
                {
                    Meeting meeting = new Meeting();
                    meeting.Name = tarefa.Titulo;
                    meeting.Description = tarefa.Descricao;
                    meeting.From = tarefa.DataInicio;
                    meeting.To = tarefa.DataTermino;
                    if (tarefa.NivelImportancia == NivelImportancia.Pouco_Importante)
                        meeting.BackgroundColor = new BrushConverter().ConvertFrom("#87FF81") as SolidColorBrush;
                    else if (tarefa.NivelImportancia == NivelImportancia.Normal)
                        meeting.BackgroundColor = new BrushConverter().ConvertFrom("#849EEA") as SolidColorBrush;
                    else if (tarefa.NivelImportancia == NivelImportancia.Importante)
                        meeting.BackgroundColor = new BrushConverter().ConvertFrom("#FE8A5F") as SolidColorBrush;
                    else if (tarefa.NivelImportancia == NivelImportancia.Prioritaria)
                        meeting.BackgroundColor = new BrushConverter().ConvertFrom("#E85671") as SolidColorBrush;
                    meeting.Id = tarefa.Id;
                    if (tarefa.Periodicidade != null)
                    {
                        meeting.RecurrenceRule = "FREQ=" + tarefa.Periodicidade.Tipo.ToString().ToUpper() + ";INTERVAL=" + tarefa.Periodicidade.Intervalo + ";COUNT=" + tarefa.Periodicidade.Quantidade;
                    }
                    Meetings.Add(meeting);
                }
            }
            RaiseOnPropertyChanged("Meetings");
        }


        /// <summary>
        /// Função que carrega as tarefas do perfil do utilizador para o calendário,
        /// com base no nível de importância passado como parâmetro, utilizada nos botões de ordenação.
        /// </summary>
        /// <param name="tarefas">Lista de tarefas armazenada no utilizador</param>
        /// <param name="nivel">Nivel de Importância das tarefas a serem mostradas</param>
        public void CarregarTarefasNivelImportancia(List<Tarefa> tarefas, NivelImportancia nivel)
        {
            if (Meetings.Count > 0)
                Meetings.Clear();
            foreach (Tarefa tarefa in tarefas)
            {
                if (tarefa.NivelImportancia == nivel)
                {
                    Meeting meeting = new Meeting();
                    meeting.Name = tarefa.Titulo;
                    meeting.Description = tarefa.Descricao;
                    meeting.From = tarefa.DataInicio;
                    meeting.To = tarefa.DataTermino;
                    if (tarefa.NivelImportancia == NivelImportancia.Pouco_Importante)
                        meeting.BackgroundColor = new BrushConverter().ConvertFrom("#87FF81") as SolidColorBrush;
                    else if (tarefa.NivelImportancia == NivelImportancia.Normal)
                        meeting.BackgroundColor = new BrushConverter().ConvertFrom("#849EEA") as SolidColorBrush;
                    else if (tarefa.NivelImportancia == NivelImportancia.Importante)
                        meeting.BackgroundColor = new BrushConverter().ConvertFrom("#FE8A5F") as SolidColorBrush;
                    else if (tarefa.NivelImportancia == NivelImportancia.Prioritaria)
                        meeting.BackgroundColor = new BrushConverter().ConvertFrom("#E85671") as SolidColorBrush;
                    meeting.Id = tarefa.Id;
                    if (tarefa.Periodicidade != null)
                    {
                        meeting.RecurrenceRule = "FREQ=" + tarefa.Periodicidade.Tipo.ToString().ToUpper() + ";INTERVAL=" + tarefa.Periodicidade.Intervalo + ";COUNT=" + tarefa.Periodicidade.Quantidade;
                    }
                    Meetings.Add(meeting);
                }
            }
            RaiseOnPropertyChanged("Meetings");
        }

        /// <summary>
        /// Função que carrega as tarefas do perfil do utilizador para o calendário,
        /// carrega apenas as tarefas de dia inteiro, utilizada nos botões de ordenação.
        /// </summary>
        /// <param name="tarefas">Lista de tarefas a filtrar e carregar</param>
        public void CarregarTarefasDiaInteiro(List<Tarefa> tarefas)
        {
            if (Meetings.Count > 0)
                Meetings.Clear();
            foreach (Tarefa tarefa in tarefas)
            {
                if (tarefa.DiaInteiro == true)
                {
                    Meeting meeting = new Meeting();
                    meeting.Name = tarefa.Titulo;
                    meeting.Description = tarefa.Descricao;
                    meeting.From = tarefa.DataInicio;
                    meeting.To = tarefa.DataTermino;
                    meeting.AllDay = tarefa.DiaInteiro;
                    if (tarefa.NivelImportancia == NivelImportancia.Pouco_Importante)
                        meeting.BackgroundColor = new BrushConverter().ConvertFrom("#87FF81") as SolidColorBrush;
                    else if (tarefa.NivelImportancia == NivelImportancia.Normal)
                        meeting.BackgroundColor = new BrushConverter().ConvertFrom("#849EEA") as SolidColorBrush;
                    else if (tarefa.NivelImportancia == NivelImportancia.Importante)
                        meeting.BackgroundColor = new BrushConverter().ConvertFrom("#FE8A5F") as SolidColorBrush;
                    else if (tarefa.NivelImportancia == NivelImportancia.Prioritaria)
                        meeting.BackgroundColor = new BrushConverter().ConvertFrom("#E85671") as SolidColorBrush;
                    meeting.Id = tarefa.Id;
                    if (tarefa.Periodicidade != null)
                    {
                        meeting.RecurrenceRule = "FREQ=" + tarefa.Periodicidade.Tipo.ToString().ToUpper() + ";INTERVAL=" + tarefa.Periodicidade.Intervalo + ";COUNT=" + tarefa.Periodicidade.Quantidade;
                    }
                    Meetings.Add(meeting);
                }
            }

            RaiseOnPropertyChanged("Meetings");
        }

        public void CarregarTarefasComRepeticao(List<Tarefa> tarefas)
        {
            if (Meetings.Count > 0)
                Meetings.Clear();
            foreach (Tarefa tarefa in tarefas)
            {
                if (tarefa.Periodicidade != null)
                {
                    Meeting meeting = new Meeting();
                    meeting.Name = tarefa.Titulo;
                    meeting.Description = tarefa.Descricao;
                    meeting.From = tarefa.DataInicio;
                    meeting.To = tarefa.DataTermino;
                    meeting.AllDay = tarefa.DiaInteiro;
                    if (tarefa.NivelImportancia == NivelImportancia.Pouco_Importante)
                        meeting.BackgroundColor = new BrushConverter().ConvertFrom("#87FF81") as SolidColorBrush;
                    else if (tarefa.NivelImportancia == NivelImportancia.Normal)
                        meeting.BackgroundColor = new BrushConverter().ConvertFrom("#849EEA") as SolidColorBrush;
                    else if (tarefa.NivelImportancia == NivelImportancia.Importante)
                        meeting.BackgroundColor = new BrushConverter().ConvertFrom("#FE8A5F") as SolidColorBrush;
                    else if (tarefa.NivelImportancia == NivelImportancia.Prioritaria)
                        meeting.BackgroundColor = new BrushConverter().ConvertFrom("#E85671") as SolidColorBrush;
                    meeting.Id = tarefa.Id;
                    if (tarefa.Periodicidade != null)
                    {
                        meeting.RecurrenceRule = "FREQ=" + tarefa.Periodicidade.Tipo.ToString().ToUpper() + ";INTERVAL=" + tarefa.Periodicidade.Intervalo + ";COUNT=" + tarefa.Periodicidade.Quantidade;
                    }
                    Meetings.Add(meeting);
                }
            }

            RaiseOnPropertyChanged("Meetings");
        }

        /// <summary>
        /// Função que remove uma tarefa do calendário, com base no id da tarefa passado como parâmetro
        /// </summary>
        /// <param name="id">Id da tarefa a remover</param>
        public void RemoverMeeting(string id)
        {
            foreach (Meeting meeting in Meetings)
            {
                if (meeting.Id == id)
                {
                    Meetings.Remove(meeting);
                    break;
                }
            }
        }
    
    }
}
