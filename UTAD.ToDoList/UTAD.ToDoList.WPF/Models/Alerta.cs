using UTAD.ToDoList.WPF.Models.Shared;

namespace UTAD.ToDoList.WPF.Models
{
    public class Alerta: BaseModel
    {
        public string Mensagem {  get; set; }
        public DateTime DataHora { get; set; }

        // Tipo de alerta (1 - Alerta Windows | 2 - Email)  
        public enum Tipo {popup, mail }
        public bool Estado { get; set; }

        // construtor por defeito
        public Alerta() 
        {
            Mensagem = string.Empty;
            DataHora = DateTime.MinValue;
            Estado = false;
        }
        // construtor com parâmetros 
        public Alerta(string _Mensagem, DateTime _DataHora, int _Tipos, bool _Estado)
        {
            Mensagem = _Mensagem;
            DataHora = _DataHora;
            Estado = _Estado;
        }
    }
}
