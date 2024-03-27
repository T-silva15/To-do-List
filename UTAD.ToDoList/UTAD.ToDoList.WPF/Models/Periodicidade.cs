using UTAD.ToDoList.WPF.Models.Shared;

namespace UTAD.ToDoList.WPF.Models
{
    public class Periodicidade : BaseModel
    {
        public string tipo {  get; set; }
        public IList<String> diasSemana { get; set; }

        // construtor por defeito
        public Periodicidade()
        {
            tipo = string.Empty;
            diasSemana = new List<String>();
        }

        // construtor com parâmetros
        public Periodicidade(string _tipo, IList<string> _diasSemana)
        {
            tipo = _tipo;
            diasSemana = _diasSemana;
        }
    }
}
