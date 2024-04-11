using UTAD.ToDoList.WPF.Models.Shared;

namespace UTAD.ToDoList.WPF.Models
{
    public class Periodicidade : BaseModel
    {
        public int Ocorrencia { get; set; } = 0;
        public enum Tipo { dia, semana, mes, ano }
        public enum DiasSemana { segunda, terca, quarta, quinta, sexta, sabado, domingo }
    }
}
