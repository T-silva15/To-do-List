using Syncfusion.UI.Xaml.Scheduler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace UTAD.ToDoList.WPF.Models
{
    public delegate void ListadeTarefasAlteradas();

    [Serializable]
    public class Perfil
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Tarefa> ListaTarefas { get; set; }

        public event ListadeTarefasAlteradas TarefasAlteradas;

        // Fotografia do perfil (caminho para a foto)
        public string? Fotografia { get; set; }



        // construtor por defeito
        public Perfil() 
        {   
            Nome = string.Empty;
            Email = string.Empty;
            Fotografia = string.Empty;
            ListaTarefas = new List<Tarefa>();
        }

        // construtor com parâmetros
        public Perfil(string _name, string _Email, string _Fotografia, List<Tarefa> _ListaTarefas)
        {
            Nome = _name;
            Email = _Email;
            Fotografia= _Fotografia;
        }

        /// <summary>
        /// Função que guarda o perfil do utilizador num ficheiro json, 
        /// o ficheiro está guardado na pasta do utilizador, caso não exista 
        /// a pasta e os ficheiros são criados e armazenados num ficheiro .json.
        /// </summary>
        public void GuardarPerfil()
        {

            
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "to-do list");
            // Cria a pasta se não existir
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = System.IO.Path.Combine(path, Nome) + ".json";
            string jsonString = JsonSerializer.Serialize(this);
            
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine(jsonString);
            }
        }


        /// <summary>
        /// Função que devolve uma tarefa da lista com base no nome da tarefa
        /// </summary>
        /// <param name="nome" Nome da tarefa a devolver></param>
        /// <returns></returns>
        public Tarefa GetTarefa(string nome) 
        { 
            foreach (Tarefa tarefa in ListaTarefas)
            {
                if (tarefa.Titulo == nome)
                {
                    return tarefa;
                }
            }
            return null;
        }

        /// <summary>
        /// Função que remove uma tarefa da lista de tarefas
        /// </summary>
        /// <param name="tarefa">Tarefa a remover</param>
        public void RemoveTarefa(Tarefa tarefa)
        {
            ListaTarefas.Remove(tarefa);
            TarefasAlteradas?.Invoke();
        }
    }
}
