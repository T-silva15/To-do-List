using UTAD.ToDoList.WPF.Models.Shared;

namespace UTAD.ToDoList.WPF.Models
{
    public class Tarefa : BaseModel
    {
        public string titulo { get; set; }
        public DateOnly dataInicio {  get; set; }
        public DateOnly dataTermino { get; set; }

        // nível de importância (0 - Sem Nível | 1 - Pouco Importante | 2 - Normal | 3 - Importante | 4 - Prioritária)
        public int nivelImportancia { get; set; }
        public bool estado { get; set; }

        // atributos opcionais
        public string? descricao { get; set; }
        public Periodicidade? periodicidade { get; set; }
        public Alerta? alertaAntecipacao { get; set; }
        public Alerta? alertaExecucao { get; set; }

        // construtor por defeito
        public Tarefa()
        {
            titulo = string.Empty;
            descricao = string.Empty;
            dataInicio = DateOnly.MinValue;
            dataTermino = DateOnly.MaxValue;
            nivelImportancia = 0;
            estado = false;
            periodicidade = new Periodicidade();
            alertaAntecipacao = new Alerta();
            alertaExecucao = new Alerta();
        }
        // construtor com parâmetros
        public Tarefa(string _titulo, string _descricao, DateOnly _dataInicio, DateOnly _dataTermino, int _nivelImportancia, bool _estado, 
            Periodicidade _periodicidade, Alerta _alertaAntecipacao, Alerta _alertaExecucao)
        {
            titulo = _titulo;
            descricao = _descricao;
            dataInicio = _dataInicio;
            dataTermino = _dataTermino;
            nivelImportancia = _nivelImportancia;
            estado = _estado;
            periodicidade = _periodicidade;
            alertaAntecipacao = _alertaAntecipacao;
            alertaExecucao = _alertaExecucao;
        }
    }
}

