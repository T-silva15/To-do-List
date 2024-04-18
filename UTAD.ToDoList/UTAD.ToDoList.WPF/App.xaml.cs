﻿using System.Configuration;
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
        public ViewNovaTarefa ViewNovaTarefa { get; set; }
        

        public App()
        { 
            // inicialização das views

            // Syncfusion License
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NBaF5cXmZCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXtfeHRSQ2ddUk1/Wkc=");
            MainWindow = new MainWindow();
            ViewLogin = new ViewLogin();
            ViewRegisto = new ViewRegisto();
            ViewPerfil = new ViewPerfil();
            ViewNovaTarefa = new ViewNovaTarefa();
            // inicialização do modelo
            Perfil = new Perfil();
        }
    }

}
