﻿using System;
using System.Collections.Generic;

namespace Hydra.Such.Data.Evolution.Database
{
    public partial class EmmInspeccoesEstados
    {
        public int Id { get; set; }
        public string Estado { get; set; }
        public bool? Activo { get; set; }
    }
}
