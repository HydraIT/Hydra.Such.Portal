﻿using System;
using System.Collections.Generic;

namespace Hydra.Such.Data.Database
{
    public partial class ConsultaMercado
    {
        public ConsultaMercado()
        {
            CondicoesPropostasFornecedores = new HashSet<CondicoesPropostasFornecedores>();
            LinhasCondicoesPropostasFornecedores = new HashSet<LinhasCondicoesPropostasFornecedores>();
            LinhasConsultaMercado = new HashSet<LinhasConsultaMercado>();
            RegistoDePropostas = new HashSet<RegistoDePropostas>();
            SeleccaoEntidades = new HashSet<SeleccaoEntidades>();
        }

        public string NumConsultaMercado { get; set; }
        public string CodProjecto { get; set; }
        public string Descricao { get; set; }
        public string CodRegiao { get; set; }
        public string CodAreaFuncional { get; set; }
        public string CodCentroResponsabilidade { get; set; }
        public string CodActividade { get; set; }
        public DateTime? DataPedidoCotacao { get; set; }
        public string FornecedorSelecionado { get; set; }
        public string NumDocumentoCompra { get; set; }
        public string CodLocalizacao { get; set; }
        public string FiltroActividade { get; set; }
        public decimal? ValorPedidoCotacao { get; set; }
        public int? Destino { get; set; }
        public int? Estado { get; set; }
        public string UtilizadorRequisicao { get; set; }
        public DateTime? DataLimite { get; set; }
        public bool? EspecificacaoTecnica { get; set; }
        public int? Fase { get; set; }
        public int? Modalidade { get; set; }
        public DateTime? PedidoCotacaoCriadoEm { get; set; }
        public string PedidoCotacaoCriadoPor { get; set; }
        public DateTime? ConsultaEm { get; set; }
        public string ConsultaPor { get; set; }
        public DateTime? NegociacaoContratacaoEm { get; set; }
        public string NegociacaoContratacaoPor { get; set; }
        public DateTime? AdjudicacaoEm { get; set; }
        public string AdjudicacaoPor { get; set; }
        public string NumRequisicao { get; set; }
        public string PedidoCotacaoOrigem { get; set; }
        public decimal? ValorAdjudicado { get; set; }
        public string CodFormaPagamento { get; set; }
        public bool? SeleccaoEfectuada { get; set; }

        public Projetos CodProjectoNavigation { get; set; }
        public ICollection<CondicoesPropostasFornecedores> CondicoesPropostasFornecedores { get; set; }
        public ICollection<LinhasCondicoesPropostasFornecedores> LinhasCondicoesPropostasFornecedores { get; set; }
        public ICollection<LinhasConsultaMercado> LinhasConsultaMercado { get; set; }
        public ICollection<RegistoDePropostas> RegistoDePropostas { get; set; }
        public ICollection<SeleccaoEntidades> SeleccaoEntidades { get; set; }
    }
}
