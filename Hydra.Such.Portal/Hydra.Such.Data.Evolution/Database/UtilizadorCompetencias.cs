﻿using System;
using System.Collections.Generic;

namespace Hydra.Such.Data.Evolution.Database
{
    public partial class UtilizadorCompetencias
    {
        public int IdCompetencia { get; set; }
        public int IdUtilizador { get; set; }
        public string Descricao { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataValidade { get; set; }
        public string NumCarteira { get; set; }
        public string Observacao { get; set; }

        public Utilizador IdUtilizadorNavigation { get; set; }
    }
}
