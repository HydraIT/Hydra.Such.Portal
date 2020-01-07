﻿using System;
using System.Collections.Generic;

namespace Hydra.Such.Data.Evolution.DatabaseReference
{
    public partial class FichaManutencaoRelatorioRelatorio
    {
        public int Id { get; set; }
        public string Om { get; set; }
        public int IdEquipamento { get; set; }
        public string Codigo { get; set; }
        public string Versao { get; set; }
        public DateTime Data { get; set; }
        public string Relatorio { get; set; }
        public int? EstadoFinal { get; set; }
        public string Observacao { get; set; }
        public string AssinaturaTecnico { get; set; }
        public int? IdAssinaturaTecnico { get; set; }
        public string AssinaturaCliente { get; set; }
        public string AssinaturaSie { get; set; }
        public int? CriadoPor { get; set; }
        public DateTime? CriadoEm { get; set; }
        public int? ActualizadoPor { get; set; }
        public DateTime? ActualizadoEm { get; set; }
    }
}