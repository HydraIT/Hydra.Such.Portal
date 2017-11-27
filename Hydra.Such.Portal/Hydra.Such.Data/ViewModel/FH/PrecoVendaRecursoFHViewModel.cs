﻿using System;
using System.Collections.Generic;

namespace Hydra.Such.Data.ViewModel.FH
{
    public partial class PrecoVendaRecursoFHViewModel
    {
        public string Code { get; set; }
        public string Descricao { get; set; }
        public string CodTipoTrabalho { get; set; }
        public decimal? PrecoUnitario { get; set; }
        public DateTime? StartingDate { get; set; }
        public DateTime? EndingDate { get; set; }
        public string FamiliaRecurso { get; set; }
        public string UtilizadorCriacao { get; set; }
        public DateTime? DataHoraCriacao { get; set; }
        public string UtilizadorModificacao { get; set; }
        public DateTime? DataHoraModificacao { get; set; }
    }
}
