﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hydra.Such.Data.ViewModel
{
    public class PedidosDEVViewModel : ErrorHandler
    {
        public int ID { get; set; }
        public string Processo { get; set; }
        public string Descricao { get; set; }
        public string Acao { get; set; }
        public string URL { get; set; }
        public int? Estado { get; set; }
        public string EstadoText { get; set; }
        public DateTime? DataEstado { get; set; }
        public string DataEstadoText { get; set; }
        public DateTime? DataPedido { get; set; }
        public string DataPedidoText { get; set; }
        public string PedidoPor { get; set; }
        public DateTime? DataConclusao { get; set; }
        public string DataConclusaoText { get; set; }
        public string Intervenientes { get; set; }
        public int? NoHorasPrevistas { get; set; }
        public int? NoHorasRealizadas { get; set; }
        public string CriadoPor { get; set; }
        public DateTime? DataCriacao { get; set; }
        public string DataCriacaoText { get; set; }
        public string AlteradoPor { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public string DataAlteracaoText { get; set; }

        //EXPORTAR PARA EXCEL
        public Object ColunasEXCEL { get; set; }
    }
}
