﻿using System;
using System.Collections.Generic;

namespace Hydra.Such.Tester.Database
{
    public partial class AcessosPerfil
    {
        public int IdPerfil { get; set; }
        public int Área { get; set; }
        public int Funcionalidade { get; set; }
        public bool? Leitura { get; set; }
        public bool? Inserção { get; set; }
        public bool? Modificação { get; set; }
        public bool? Eliminação { get; set; }
        public DateTime? DataHoraCriação { get; set; }
        public DateTime? DataHoraModificação { get; set; }
        public string UtilizadorCriação { get; set; }
        public string UtilizadorModificação { get; set; }

        public PerfisModelo IdPerfilNavigation { get; set; }
    }
}
