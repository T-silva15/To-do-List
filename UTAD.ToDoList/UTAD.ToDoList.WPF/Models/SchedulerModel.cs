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
