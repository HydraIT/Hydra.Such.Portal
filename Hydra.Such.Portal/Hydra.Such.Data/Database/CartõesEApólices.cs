﻿using System;
using System.Collections.Generic;

namespace Hydra.Such.Data.Database
{
    public partial class CartõesEApólices
    {
        public int Tipo { get; set; }
        public string Número { get; set; }
        public DateTime? DataInício { get; set; }
        public DateTime? DataFim { get; set; }
        public string Descrição { get; set; }
        public string Fornecedor { get; set; }
        public DateTime? DataHoraCriação { get; set; }
        public DateTime? DataHoraModificação { get; set; }
        public string UtilizadorCriação { get; set; }
        public string UtilizadorModificação { get; set; }
    }
}
