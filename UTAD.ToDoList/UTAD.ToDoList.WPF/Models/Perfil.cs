using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace UTAD.ToDoList.WPF.Models
{
    public class Perfil
    {


        public string Nome { get; set; }
        public string Email { get; set; }

        // Fotografia do perfil (bitmap)
        public BitmapImage Fotografia { get; set; }

        
        // construtor por defeito
        public Perfil() 
        {   
            Nome = string.Empty;
            Email = string.Empty;
            Fotografia = new BitmapImage();
        }

        // construtor com parâmetros
        public Perfil(string _name, string _Email, BitmapImage _Fotografia)
        {
            Nome = _name;
            Email = _Email;
            Fotografia= _Fotografia;
        }
    }
}
