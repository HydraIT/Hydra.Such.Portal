﻿using Hydra.Such.Data.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hydra.Such.Data.ViewModel.Viaturas
{
    public class MarcasViewModel : ErrorHandler
    {
        public int CodigoMarca { get; set; }
        public int? Tipo { get; set; }
        public string Descricao { get; set; }
        public DateTime? DataHoraCriacao { get; set; }
        public string UtilizadorCriacao { get; set; }
        public DateTime? DataHoraModificacao { get; set; }
        public string UtilizadorModificacao { get; set; }

        //public ICollection<TelemoveisViewModel> Telemoveis { get; set; }
        public ICollection<ViaturasViewModel> Viaturas { get; set; }
    }
}
