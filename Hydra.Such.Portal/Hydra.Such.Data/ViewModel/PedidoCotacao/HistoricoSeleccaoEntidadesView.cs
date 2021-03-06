﻿using System;
using System.Collections.Generic;
using System.Text;
using Hydra.Such.Data.Database;

namespace Hydra.Such.Data.ViewModel.PedidoCotacao
{
    public class HistoricoSeleccaoEntidadesView : ErrorHandler
    {
        public int IdSeleccaoEntidades { get; set; }
        public string NumConsultaMercado { get; set; }
        public int NumVersao { get; set; }
        public string CodFornecedor { get; set; }
        public string NomeFornecedor { get; set; }
        public string CodActividade { get; set; }
        public string CidadeFornecedor { get; set; }
        public string CodTermosPagamento { get; set; }
        public string CodFormaPagamento { get; set; }
        public bool? Selecionado { get; set; }
        public bool? Preferencial { get; set; }
    }
}
