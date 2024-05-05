using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace UTAD.ToDoList.WPF.Models
{
    public class SchedulerModel : INotifyPropertyChanged
    {
        private ObservableCollection<Meeting> meetings;
        public event PropertyChangedEventHandler PropertyChanged;
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
        }

        public void RaiseOnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
