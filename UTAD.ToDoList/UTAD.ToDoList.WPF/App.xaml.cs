using System.Configuration;
using System.Data;
using System.Windows;
using UTAD.ToDoList.WPF.Models;
using UTAD.ToDoList.WPF.Models.Shared;

namespace UTAD.ToDoList.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // propriedades dos modelos
        public Perfil Perfil { get; set; }

        // propriedades das views
        public new MainWindow MainWindow { get; set; }
        public ViewLogin ViewLogin { get; set; }
        public ViewRegisto ViewRegisto { get; set; }
        public ViewPerfil ViewPerfil { get; set; }
        

        public App()
        { 
            // inicialização das views
            MainWindow = new MainWindow();
            ViewLogin = new ViewLogin();
            ViewRegisto = new ViewRegisto();
            ViewPerfil = new ViewPerfil();
            // inicialização do modelo
            Perfil = new Perfil();

        }
    }

}
