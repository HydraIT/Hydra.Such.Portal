﻿using System;
using System.Collections.Generic;

namespace Hydra.Such.Data.Evolution.DatabaseReference
{
    public partial class Accao
    {
        public Accao()
        {
            Logs = new HashSet<Logs>();
        }

        public int IdAccao { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<Logs> Logs { get; set; }
    }
}
