using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace UTAD.ToDoList.WPF.Models
{
    public class Perfil
    {


        public string nome { get; set; }
        public string email { get; set; }

        // fotografia do perfil (bitmap)
        public BitmapImage fotografia { get; set; }

        
        // construtor por defeito
        public Perfil() 
        {   
            nome = string.Empty;
            email = string.Empty;
            fotografia = new BitmapImage();
        }

        // construtor com parâmetros
        public Perfil(string _name, string _email, BitmapImage _fotografia)
        {
            nome = _name;
            email = _email;
            fotografia= _fotografia;
        }
    }
}
