﻿using System;

namespace Hydra.Such.Data.Database
{
    public partial class Viaturas2Imobilizados
    {
        public int ID { get; set; }
        public string Matricula { get; set; }

        public string NoImobilizado { get; set; }

        public string UtilizadorCriacao { get; set; }
        public DateTime? DataCriacao { get; set; }
        public string UtilizadorModificacao { get; set; }
        public DateTime? DataModificacao { get; set; }
    }
}
