﻿using System;
using System.Collections.Generic;

namespace Hydra.Such.Data.Evolution.Database
{
    public partial class EquipMarca
    {
        public EquipMarca()
        {
            Acessorio = new HashSet<Acessorio>();
            Equipamento = new HashSet<Equipamento>();
        }

        public int IdMarca { get; set; }
        public string Nome { get; set; }
        public bool? Activo { get; set; }

        public ICollection<Acessorio> Acessorio { get; set; }
        public ICollection<Equipamento> Equipamento { get; set; }
    }
}
