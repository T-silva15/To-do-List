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
    [Serializable] 
    public class SchedulerModel : INotifyPropertyChanged
    {
        private ObservableCollection<Meeting> meetings;
        public event PropertyChangedEventHandler? PropertyChanged;
        public SchedulerModel()
        {
            Meetings = new ObservableCollection<Meeting>();
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


        public void AdicionarTarefa(string nome, string descricao, DateTime inicio, DateTime fim, SolidColorBrush cor, string id, string recurrence)
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
            Meetings.Add(meeting);

            RaiseOnPropertyChanged("Meetings");
        }

        public void RaiseOnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void CarregarTarefas(List<Tarefa> tarefas)
        {
            foreach (Tarefa tarefa in tarefas)
            {
                Meeting meeting = new Meeting();
                meeting.Name = tarefa.Titulo;
                meeting.Description = tarefa.Descricao;
                meeting.From = tarefa.DataInicio;
                meeting.To = tarefa.DataTermino;
                if (tarefa.NivelImportancia == NivelImportancia.Pouco_Importante)
                    meeting.BackgroundColor = Brushes.LightGreen;
                else if (tarefa.NivelImportancia == NivelImportancia.Normal)
                    meeting.BackgroundColor = Brushes.LightBlue;
                else if (tarefa.NivelImportancia == NivelImportancia.Importante)
                    meeting.BackgroundColor = Brushes.Orange;
                else if (tarefa.NivelImportancia == NivelImportancia.Prioritaria)
                    meeting.BackgroundColor = Brushes.Red;
                meeting.Id = tarefa.Id;
                if (tarefa.Periodicidade != null)
                {
                    meeting.RecurrenceRule = "FREQ=" + tarefa.Periodicidade.Tipo.ToString().ToUpper() + ";INTERVAL=" + tarefa.Periodicidade.Intervalo + ";COUNT=" + tarefa.Periodicidade.Quantidade;
                }
                Meetings.Add(meeting);

            }
            RaiseOnPropertyChanged("Meetings");
        }
    }
}
