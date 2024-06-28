using UTAD.ToDoList.WPF.Models.Shared;

namespace UTAD.ToDoList.WPF.Models
{
    public enum TipoA { popup, mail }

    public class Alerta: BaseModel
    {
        public string Mensagem {  get; set; }
        public DateTime Data { get; set; }

        public TipoA Tipo { get; set; }
        public bool EstadoAlerta { get; set; }

        // construtor sem parâmetros
        public Alerta()
        {
            Mensagem = "";
            Data = DateTime.Now;
            EstadoAlerta = false;
        }
        // construtor com parâmetros 
        public Alerta(string _Mensagem, DateTime _Data, bool _Estado)
        {
            Mensagem = _Mensagem;
            Data = _Data;
            EstadoAlerta = _Estado;
        }
    }
}
