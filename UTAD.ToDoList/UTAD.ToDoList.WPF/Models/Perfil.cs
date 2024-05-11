using Syncfusion.UI.Xaml.Scheduler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace UTAD.ToDoList.WPF.Models
{
    [Serializable]
    public class Perfil
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Fotografia do perfil (caminho para a foto)
        public string? Fotografia { get; set; }

        public List<Tarefa> ListaTarefas { get; set; }


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

        public void GuardarPerfil()
        {
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "to-do list");
            path = System.IO.Path.Combine(path, Nome) + ".json";
            string jsonString = JsonSerializer.Serialize(this);
            
            using (StreamWriter writer = new StreamWriter(path))
            {
                // Write some text to the file
                writer.WriteLine(jsonString);
            }
        }

    }
}
