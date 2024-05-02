using UTAD.ToDoList.WPF.Models.Shared;

namespace UTAD.ToDoList.WPF.Models
{
    public class Tarefa : BaseModel
    {
        public string Titulo { get; set; }
        public DateTime DataInicio {  get; set; }
        public DateTime DataTermino { get; set; }

        public enum NivelImportancia { Pouco_Importante, Normal, Important, Prioritaria}
        public enum Estado { Por_Iniciar, Em_Execucao, Terminada  }

        // atributos opcionais
        public string? Descricao { get; set; }
        public Periodicidade? Periodicidade { get; set; }
        public Alerta? AlertaAntecipacao { get; set; }
        public Alerta? AlertaExecucao { get; set; }

        // construtor por defeito
        public Tarefa()
        {
            Titulo = string.Empty;
            Descricao = string.Empty;
            DataInicio = DateTime.MinValue;
            DataTermino = DateTime.MaxValue;
            Periodicidade = new Periodicidade();
            AlertaAntecipacao = new Alerta();
            AlertaExecucao = new Alerta();
        }
        // construtor com parâmetros
        public Tarefa(string _Titulo, string _Descricao, DateTime _DataInicio, DateTime _DataTermino, int _NivelImportancia, 
            Periodicidade _Periodicidade, Alerta _AlertaAntecipacao, Alerta _AlertaExecucao)
        {
            Titulo = _Titulo;
            Descricao = _Descricao;
            DataInicio = _DataInicio;
            DataTermino = _DataTermino;
            Periodicidade = _Periodicidade;
            AlertaAntecipacao = _AlertaAntecipacao;
            AlertaExecucao = _AlertaExecucao;
        }
    }
}

