using Syncfusion.SfSkinManager;
using System.Configuration;
using System.Data;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows;
using UTAD.ToDoList.WPF.Models;
using UTAD.ToDoList.WPF.Models.Shared;


namespace UTAD.ToDoList.WPF
{

    public partial class App : Application
    {
        // propriedades dos modelos
        public Perfil Perfil { get; set; }
        public SchedulerModel scheduler { get; set; }

        // propriedades das views
        public new MainWindow MainWindow { get; set; }
        public ViewRegisto ViewRegisto { get; set; }
        public ViewPerfil ViewPerfil { get; set; }
        public ViewNovaTarefa ViewNovaTarefa { get; set; }
        public ViewEditarTarefa ViewEditarTarefa { get; set; }

        public App()
        {

            // Syncfusion License
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NBaF5cXmZCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXtfeHRSQ2ddUk1/Wkc=");
            
            // inicialização das views
            MainWindow = new MainWindow();
            ViewRegisto = new ViewRegisto();
            ViewPerfil = new ViewPerfil();
            ViewNovaTarefa = new ViewNovaTarefa();
            ViewEditarTarefa = new ViewEditarTarefa();
            
            // inicialização do modelo
            Perfil = new Perfil();
            scheduler = new SchedulerModel();
        }

        /// <summary>
        /// Função que converte uma SecureString para string,
        /// usada para converter a password para texto e mostrar ao utilizador
        /// </summary>
        /// <param name="secureString">SecureString lida da textbox</param>
        /// <returns></returns>
        public string ConvertToPlainText(SecureString secureString)
        {
            IntPtr ptr = IntPtr.Zero;
            try
            {
                ptr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(ptr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(ptr);
            }
        }

        /// <summary>
        /// Função que verifica se o email é válido
        /// </summary>
        /// <param name="email">Email a verificar</param>
        /// <returns></returns>
        public bool IsValidEmail(string email)
        {
            // Regex para verificar o email
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // verifica email
            Match match = Regex.Match(email, emailPattern);

            return match.Success;
        }
    }
}
