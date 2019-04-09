﻿using System;
using System.Collections.Generic;

namespace Hydra.Such.Data.Evolution.Database
{
    public partial class NivelAcessoTipo
    {
        public NivelAcessoTipo()
        {
            Utilizador = new HashSet<Utilizador>();
        }

        public int Id { get; set; }
        public string NivelAcesso { get; set; }

        public virtual PermissoesDefault PermissoesDefault { get; set; }
        public virtual ICollection<Utilizador> Utilizador { get; set; }
    }
}
