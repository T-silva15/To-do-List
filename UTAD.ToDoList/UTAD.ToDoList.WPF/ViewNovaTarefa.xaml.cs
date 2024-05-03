﻿using Syncfusion.UI.Xaml.Scheduler;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UTAD.ToDoList.WPF.Models;
using UTAD.ToDoList.WPF.Models.Shared;


namespace UTAD.ToDoList.WPF
{
    /// <summary>
    /// Interaction logic for ViewNovaTarefa.xaml
    /// </summary>
    public partial class ViewNovaTarefa : Window
    {
        public App App;

        public ViewNovaTarefa()
        {
            App = (App)Application.Current;
            InitializeComponent();
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("PT-pt");
        }

        private void ButtonForward_Click(object sender, RoutedEventArgs e)
        {
            sfCalendario.Forward();
        }

        private void ButtonBackward_Click(object sender, RoutedEventArgs e)
        {
            sfCalendario.Backward();
        }

        private void BtnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            // cria lista de tarefas se não existir
            if (App.Perfil.ListaTarefas == null)
            {
                App.Perfil.ListaTarefas = [];
            }
            if (App.Perfil.TarefasScheduler == null)
            {
                App.Perfil.TarefasScheduler = [];
            }
            // tarefa no modelo
            Tarefa tarefa = new Tarefa();
            tarefa.Titulo = tbNome.Text;

            if (tbHoraInicio.Text == "")
                tbHoraInicio.Text = "0";
            if (tbHoraFim.Text == "")
                tbHoraFim.Text = "0";
            if (tbMinutosInicio.Text == "")
                tbMinutosInicio.Text = "0";
            if (tbMinutosFim.Text == "")
                tbMinutosFim.Text = "0";
            // lê do date picker a data e as horas e minutos das textboxes
            tarefa.DataInicio = dpInicio.SelectedDate.Value.AddHours(Convert.ToInt32(tbHoraInicio.Text)).AddMinutes(Convert.ToInt32(tbMinutosInicio.Text));
            tarefa.DataTermino = dpTermino.SelectedDate.Value.AddHours(Convert.ToInt32(tbHoraFim.Text)).AddMinutes(Convert.ToInt32(tbMinutosFim.Text));
            tarefa.Descricao = tbDescricao.Text;
            
            // estado
            if (rbPorIniciar.IsChecked == true)
            {
                tarefa.Estado = Estado.Por_Iniciar;
            }
            else if (rbEmExecucao.IsChecked == true)
            {
                tarefa.Estado = Estado.Em_Execucao;
            }
            else if (rbTerminada.IsChecked == true)
            {
                tarefa.Estado = Estado.Terminada;
            }

            // tarefa no syncfusion scheduler
            ScheduleAppointment appointment = new ScheduleAppointment();
            appointment.Id = tarefa.Id;
            appointment.StartTime = tarefa.DataInicio;
            appointment.EndTime = tarefa.DataTermino;
            appointment.Subject = tarefa.Titulo;
            appointment.Notes = tarefa.Descricao;

            // prioridade 
            if (rbPoucoImportante.IsChecked == true)
            {
                tarefa.NivelImportancia = NivelImportancia.Pouco_Importante;
                appointment.AppointmentBackground = Brushes.LightGreen;
            }
            else if (rbNormal.IsChecked == true)
            {
                tarefa.NivelImportancia = NivelImportancia.Normal;
                appointment.AppointmentBackground = Brushes.LightBlue;

            }
            else if (rbImportante.IsChecked == true)
            {
                tarefa.NivelImportancia = NivelImportancia.Importante;
                appointment.AppointmentBackground = Brushes.Orange;

            }
            else if (rbNormal.IsChecked == true)
            {
                tarefa.NivelImportancia = NivelImportancia.Prioritaria;
                appointment.AppointmentBackground = Brushes.Red;

            }

            // alerta antecipação
            if (rbAlAnt15Min.IsChecked == true)
            {
                tarefa.AlertaAntecipacao = new Alerta();
                tarefa.AlertaAntecipacao.Data = dpInicio.SelectedDate.Value.AddMinutes(-15);
                tarefa.AlertaAntecipacao.Mensagem = "A tarefa " + tarefa.Titulo + " vai começar dentro de 15 minutos!";
                tarefa.AlertaAntecipacao.Tipo = TipoA.popup;
                tarefa.AlertaAntecipacao.Estado = false;
            }
            else if (rbAlAnt30Min.IsChecked == true)
            {
                tarefa.AlertaAntecipacao = new Alerta();
                tarefa.AlertaAntecipacao.Data = dpInicio.SelectedDate.Value.AddMinutes(-30);
                tarefa.AlertaAntecipacao.Mensagem = "A tarefa " + tarefa.Titulo + " vai começar dentro de 30 minutos!";
                tarefa.AlertaAntecipacao.Tipo = TipoA.popup;
                tarefa.AlertaAntecipacao.Estado = false;
            }
            else if (rbAlAnt60Min.IsChecked == true)
            {
                tarefa.AlertaAntecipacao = new Alerta();
                tarefa.AlertaAntecipacao.Data = dpInicio.SelectedDate.Value.AddMinutes(-60);
                tarefa.AlertaAntecipacao.Mensagem = "A tarefa " + tarefa.Titulo + " vai começar dentro de 60 minutos!";
                tarefa.AlertaAntecipacao.Tipo = TipoA.popup;
                tarefa.AlertaAntecipacao.Estado = false;
            }

            // alerta execução
            if (rbAlNR15Min.IsChecked == true)
            {
                tarefa.AlertaExecucao = new Alerta();
                tarefa.AlertaExecucao.Data = dpTermino.SelectedDate.Value.AddMinutes(15);
                tarefa.AlertaExecucao.Mensagem = "A tarefa " + tarefa.Titulo + " começou há 15 minutos atrás!";
                tarefa.AlertaExecucao.Tipo = TipoA.popup;
                tarefa.AlertaExecucao.Estado = false;
            }
            else if (rbAlNR30Min.IsChecked == true)
            {
                tarefa.AlertaExecucao = new Alerta();
                tarefa.AlertaExecucao.Data = dpTermino.SelectedDate.Value.AddMinutes(30);
                tarefa.AlertaExecucao.Mensagem = "A tarefa " + tarefa.Titulo + " começou há 30 minutos atrás!";
                tarefa.AlertaExecucao.Tipo = TipoA.popup;
                tarefa.AlertaExecucao.Estado = false;
            }
            else if (rbAlNR60Min.IsChecked == true)
            {
                tarefa.AlertaExecucao = new Alerta();
                tarefa.AlertaExecucao.Data = dpTermino.SelectedDate.Value.AddMinutes(60);
                tarefa.AlertaExecucao.Mensagem = "A tarefa " + tarefa.Titulo + " começou há 60 minutos atrás!";
                tarefa.AlertaExecucao.Tipo = TipoA.popup;
                tarefa.AlertaExecucao.Estado = false;
            }

            if (PerDiario.IsChecked == true)
            {
                tarefa.Periodicidade = new Periodicidade();
                tarefa.Periodicidade.Tipo = TipoP.diario;
                tarefa.Periodicidade.Intervalo = Convert.ToInt32(tbIntervalo.Text);
                if (tbQuantidade.Text == "")
                    tarefa.Periodicidade.Quantidade = 0;
                tarefa.Periodicidade.Quantidade = Convert.ToInt32(tbQuantidade.Text);
                appointment.RecurrenceRule = "FREQ=DAILY;INTERVAL=" + tarefa.Periodicidade.Intervalo + ";COUNT=" + tarefa.Periodicidade.Quantidade;
            }
            else if (PerSemanal.IsChecked == true)
            {
                tarefa.Periodicidade = new Periodicidade();
                tarefa.Periodicidade.Tipo = TipoP.semanal;
                tarefa.Periodicidade.Intervalo = Convert.ToInt32(tbIntervalo.Text);
                if (tbQuantidade.Text == "")
                    tarefa.Periodicidade.Quantidade = 0;
                tarefa.Periodicidade.Quantidade = Convert.ToInt32(tbQuantidade.Text);
                appointment.RecurrenceRule = "FREQ=WEEKLY;INTERVAL=" + tarefa.Periodicidade.Intervalo + ";COUNT=" + tarefa.Periodicidade.Quantidade;

            }
            else if (PerMensal.IsChecked == true)
            {
                tarefa.Periodicidade = new Periodicidade();
                tarefa.Periodicidade.Tipo = TipoP.mensal;
                tarefa.Periodicidade.Intervalo = Convert.ToInt32(tbIntervalo.Text);
                if (tbQuantidade.Text == "")
                    tarefa.Periodicidade.Quantidade = 0;
                tarefa.Periodicidade.Quantidade = Convert.ToInt32(tbQuantidade.Text);
                appointment.RecurrenceRule = "FREQ=MONTHLY;INTERVAL=" + tarefa.Periodicidade.Intervalo + ";COUNT=" + tarefa.Periodicidade.Quantidade;

            }

            App.Perfil.ListaTarefas.Add(tarefa);
            App.Perfil.TarefasScheduler.Add(appointment);

            this.Close();
        }

        private void BtnSair_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dpInicio.SelectedDate = DateTime.Now;
            dpTermino.SelectedDate = DateTime.Now;

        }

    }
}