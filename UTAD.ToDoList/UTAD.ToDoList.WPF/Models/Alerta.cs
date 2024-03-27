using UTAD.ToDoList.WPF.Models.Shared;

namespace UTAD.ToDoList.WPF.Models
{
    public class Alerta: BaseModel
    {
        public string mensagem {  get; set; }
        public DateTime dataHora { get; set; }

        // tipo de alerta (1 - Alerta Windows | 2 - Email)  
        public int tipo { get; set; }
        public bool estado { get; set; }

        // construtor por defeito
        public Alerta() 
        {
            mensagem = string.Empty;
            dataHora = DateTime.MinValue;
            tipo = 1;
            estado = false;
        }
        // construtor com parâmetros 
        public Alerta(string _mensagem, DateTime _dataHora, int _tipos, bool _estado)
        {
            mensagem = _mensagem;
            dataHora = _dataHora;
            tipo = _tipos;
            estado = _estado;
        }
    }
}
