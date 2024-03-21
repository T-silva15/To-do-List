using UTAD.ToDoList.WPF.Models.Shared;

namespace UTAD.ToDoList.WPF.Models
{
    public class Periodicidade : BaseModel
    {
        public string tipo {  get; set; }
        public IList<String> DiasSemana { get; set; }

    }
}
