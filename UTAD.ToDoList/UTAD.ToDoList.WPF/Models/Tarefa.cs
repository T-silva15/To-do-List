using UTAD.ToDoList.WPF.Models.Shared;

namespace UTAD.ToDoList.WPF.Models
{
    public class Tarefa : BaseModel
    {
        public string Titulo { get; set; }
        public DateOnly DataInicio {  get; set; }
        public DateOnly DataTermino { get; set; }

        public enum NivelImportancia { Pouco_Importante, Normal, Important, Prioritaria}
        public bool Estado { get; set; }

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
            DataInicio = DateOnly.MinValue;
            DataTermino = DateOnly.MaxValue;
            Estado = false;
            Periodicidade = new Periodicidade();
            AlertaAntecipacao = new Alerta();
            AlertaExecucao = new Alerta();
        }
        // construtor com parâmetros
        public Tarefa(string _Titulo, string _Descricao, DateOnly _DataInicio, DateOnly _DataTermino, int _NivelImportancia, bool _Estado, 
            Periodicidade _Periodicidade, Alerta _AlertaAntecipacao, Alerta _AlertaExecucao)
        {
            Titulo = _Titulo;
            Descricao = _Descricao;
            DataInicio = _DataInicio;
            DataTermino = _DataTermino;
            Estado = _Estado;
            Periodicidade = _Periodicidade;
            AlertaAntecipacao = _AlertaAntecipacao;
            AlertaExecucao = _AlertaExecucao;
        }
    }
}

