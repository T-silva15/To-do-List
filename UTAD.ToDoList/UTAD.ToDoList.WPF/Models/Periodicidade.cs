﻿using UTAD.ToDoList.WPF.Models.Shared;

namespace UTAD.ToDoList.WPF.Models
{
    public enum TipoP { DAILY, WEEKLY, MONTHLY}

    public class Periodicidade : BaseModel
    {
        public TipoP Tipo { get; set; }
        public int Intervalo { get; set; }
        public int Quantidade { get; set; }


        public Periodicidade()
        {
            Tipo = TipoP.DAILY;
            Intervalo = 1;
            Quantidade = 0;
        }

        public Periodicidade(TipoP _Tipo, int _Intervalo, int _Quantidade)
        {
            Tipo = _Tipo;
            Intervalo = _Intervalo;
            Quantidade = _Quantidade;
        }

    }

    
}
