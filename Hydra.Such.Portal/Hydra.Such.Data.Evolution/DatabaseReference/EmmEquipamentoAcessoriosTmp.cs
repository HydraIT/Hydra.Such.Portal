﻿using System;
using System.Collections.Generic;

namespace Hydra.Such.Data.Evolution.DatabaseReference
{
    public partial class EmmEquipamentoAcessoriosTmp
    {
        public int Id { get; set; }
        public string Timestamp { get; set; }
        public int? EquipamentoNumEmm { get; set; }
        public int AcessorioNumEmm { get; set; }
        public bool? Activo { get; set; }
    }
}
