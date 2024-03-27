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
            // imagem por defeito não implementada completamente    
            // fotografia = BitmapImage.Create(BitmapImage.CreateOptions.None, BitmapCacheOption.OnLoad, null, null);
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
