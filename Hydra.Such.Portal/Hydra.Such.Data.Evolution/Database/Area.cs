﻿using System;
using System.Collections.Generic;

namespace Hydra.Such.Data.Evolution.Database
{
    public partial class Area
    {
        public Area()
        {
            Acessorio = new HashSet<Acessorio>();
            ClientePimp = new HashSet<ClientePimp>();
            Equipa = new HashSet<Equipa>();
            Equipamento = new HashSet<Equipamento>();
        }

        public int IdArea { get; set; }
        public string Nome { get; set; }
        public bool? Activo { get; set; }
        public string Descricao { get; set; }

        public ICollection<Acessorio> Acessorio { get; set; }
        public ICollection<ClientePimp> ClientePimp { get; set; }
        public ICollection<Equipa> Equipa { get; set; }
        public ICollection<Equipamento> Equipamento { get; set; }
    }
}
