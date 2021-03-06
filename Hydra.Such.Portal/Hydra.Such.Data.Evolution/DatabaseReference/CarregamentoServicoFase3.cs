﻿using System;
using System.Collections.Generic;

namespace Hydra.Such.Data.Evolution.DatabaseReference
{
    public partial class CarregamentoServicoFase3
    {
        public int Id { get; set; }
        public string NumClienteNav2009 { get; set; }
        public string NumClienteNav2017 { get; set; }
        public string NomeCliente { get; set; }
        public string LocalizacaoFuncional { get; set; }
        public string DescricaoEvolution { get; set; }
        public string InstituicaoSuperior { get; set; }
        public int? IdCliente { get; set; }
        public int? IdInstituicaoSuperior { get; set; }
        public int? IdInstServ { get; set; }
    }
}
