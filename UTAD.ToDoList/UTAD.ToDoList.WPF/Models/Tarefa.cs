using UTAD.ToDoList.WPF.Models.Shared;

namespace UTAD.ToDoList.WPF.Models
{
    public enum NivelImportancia { Pouco_Importante, Normal, Importante, Prioritaria }
    public enum Estado { Por_Iniciar, Em_Execucao, Terminada }

    public class Tarefa : BaseModel
    {
        public string Titulo { get; set; }
        public DateTime DataInicio {  get; set; }
        public DateTime DataTermino { get; set; }
        public NivelImportancia NivelImportancia { get; set; }
        public Estado Estado { get; set; }
        public bool DiaInteiro { get; set; }
        

        // atributos opcionais
        public string? Descricao { get; set; }
        public Periodicidade? Periodicidade { get; set; }
        public List<Alerta> ListaAlertaAnt { get; set; }
        public List<Alerta> ListaAlertaNaoExec { get; set; }

        public Tarefa()
        {
            Titulo = "";
            DataInicio = DateTime.Now;
            DataTermino = DateTime.Now;
            NivelImportancia = NivelImportancia.Normal;
            Estado = Estado.Por_Iniciar;
            Descricao = "";
            Periodicidade = null;
            ListaAlertaAnt = new List<Alerta>();
            ListaAlertaNaoExec = new List<Alerta>();
        }

        // construtor com parâmetros
        public Tarefa(string _Titulo, string _Descricao, DateTime _DataInicio, DateTime _DataTermino, NivelImportancia _NivelImportancia, Estado _Estado, bool diaInteiro,
            Periodicidade _Periodicidade, List<Alerta> _AlertaAntecipacao, List<Alerta> _AlertaExecucao)
        {
            Titulo = _Titulo;
            Descricao = _Descricao;
            DataInicio = _DataInicio;
            DataTermino = _DataTermino;
            NivelImportancia = _NivelImportancia;
            Estado = _Estado;
            DiaInteiro = diaInteiro;
            Periodicidade = _Periodicidade;
            ListaAlertaAnt = _AlertaAntecipacao;
            ListaAlertaNaoExec = _AlertaExecucao;
        }
    }
}

