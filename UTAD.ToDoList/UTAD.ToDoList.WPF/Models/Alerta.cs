using UTAD.ToDoList.WPF.Models.Shared;

namespace UTAD.ToDoList.WPF.Models
{
    public class Alerta: BaseModel
    {
        public string mensagem {  get; set; }
        public DateTime dataHora { get; set; }
        public IList<String> tipos { get; set; }
        public bool estado { get; set; } = false;

    }
}
