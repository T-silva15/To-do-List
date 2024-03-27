using System.Runtime.CompilerServices;
using UTAD.ToDoList.WPF.Models.Shared;

namespace UTAD.ToDoList.WPF.Models
{
    public class Tarefa : BaseModel
    {
        public string titulo { get; set; }
        public string descricao { get; set; }
        public DateOnly dataInicio {  get; set; }
        public DateOnly dataTermino { get; set; }
        public int nivelImportancia { get; set; }
        public bool estado { get; set; }
        public Periodicidade periodicidade { get; set; }
        public Alerta alertaAntecipacao { get; set; }
        public Alerta alertaExecucao { get; set; }
    }
}
