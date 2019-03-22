﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Hydra.Such.Data.Evolution.Database
{
    [ModelMetadataType(typeof(IOrdemManutencaoLinha))]
    public partial class OrdemManutencaoLinha : IOrdemManutencaoLinha
    { }

    public interface IOrdemManutencaoLinha
    {
        int IdOmLinha { get; set; }
        string No { get; set; }
        int NumLinha { get; set; }
        int? IdEquipamento { get; set; }
        int? IdEquipEstado { get; set; }
        int? IdRotina { get; set; }
        int? Tbf { get; set; }

        EquipEstado IdEquipEstadoNavigation { get; set; }
        Equipamento IdEquipamentoNavigation { get; set; }
        Rotina IdRotinaNavigation { get; set; }
    }
}
