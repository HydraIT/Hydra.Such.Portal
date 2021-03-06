﻿using System;
using System.Collections.Generic;

namespace Hydra.Such.Data.Database
{
    public partial class ServiçosCliente
    {
        public string NºCliente { get; set; }
        public string CódServiço { get; set; }
        public bool? GrupoServiços { get; set; }
        public string CodGrupoServico { get; set; }
        public DateTime? DataHoraCriação { get; set; }
        public string UtilizadorCriação { get; set; }
        public DateTime? DataHoraModificação { get; set; }
        public string UtilizadorModificação { get; set; }

        public Serviços CódServiçoNavigation { get; set; }
    }
}
