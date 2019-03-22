﻿using System;
using System.Collections.Generic;

namespace Hydra.Such.Data.Evolution.Database
{
    public partial class Rotina
    {
        public Rotina()
        {
            EquipPimp = new HashSet<EquipPimp>();
            InstituicaoPimp = new HashSet<InstituicaoPimp>();
            OrdemManutencaoEquipamentos = new HashSet<OrdemManutencaoEquipamentos>();
            OrdemManutencaoLinha = new HashSet<OrdemManutencaoLinha>();
            SolicitacoesLinha = new HashSet<SolicitacoesLinha>();
        }

        public int IdRotina { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }

        public ICollection<EquipPimp> EquipPimp { get; set; }
        public ICollection<InstituicaoPimp> InstituicaoPimp { get; set; }
        public ICollection<OrdemManutencaoEquipamentos> OrdemManutencaoEquipamentos { get; set; }
        public ICollection<OrdemManutencaoLinha> OrdemManutencaoLinha { get; set; }
        public ICollection<SolicitacoesLinha> SolicitacoesLinha { get; set; }
    }
}
