using System;
using System.Collections.Generic;

namespace Hydra.Such.Data.Database
{
    public partial class LinhasRequisi��oHist
    {
        public string N�Requisi��o { get; set; }
        public int N�Linha { get; set; }
        public int? Tipo { get; set; }
        public string C�digo { get; set; }
        public string Descri��o { get; set; }
        public string C�digoUnidadeMedida { get; set; }
        public string C�digoLocaliza��o { get; set; }
        public bool? MercadoLocal { get; set; }
        public decimal? QuantidadeARequerer { get; set; }
        public decimal? QuantidadeRequerida { get; set; }
        public decimal? QuantidadeADisponibilizar { get; set; }
        public decimal? QuantidadeDisponibilizada { get; set; }
        public decimal? QuantidadeAReceber { get; set; }
        public decimal? QuantidadeRecebida { get; set; }
        public decimal? QuantidadePendente { get; set; }
        public decimal? CustoUnit�rio { get; set; }
        public DateTime? DataRece��oEsperada { get; set; }
        public bool? Fatur�vel { get; set; }
        public string N�Projeto { get; set; }
        public string C�digoRegi�o { get; set; }
        public string C�digo�reaFuncional { get; set; }
        public string C�digoCentroResponsabilidade { get; set; }
        public string N�Funcion�rio { get; set; }
        public string Viatura { get; set; }
        public DateTime? DataHoraCria��o { get; set; }
        public string UtilizadorCria��o { get; set; }
        public DateTime? DataHoraModifica��o { get; set; }
        public string UtilizadorModifica��o { get; set; }
        public decimal? QtdPorUnidadeDeMedida { get; set; }
        public decimal? Pre�oUnit�rioVenda { get; set; }
        public decimal? ValorOr�amento { get; set; }
        public int? N�LinhaOrdemManuten��o { get; set; }
        public bool? CriarConsultaMercado { get; set; }
        public bool? EnviarPr�Compra { get; set; }
        public bool? EnviadoPr�Compra { get; set; }
        public DateTime? DataMercadoLocal { get; set; }
        public string UserMercadoLocal { get; set; }
        public bool? EnviadoParaCompras { get; set; }
        public DateTime? DataEnvioParaCompras { get; set; }
        public bool? ValidadoCompras { get; set; }
        public bool? RecusadoCompras { get; set; }
        public string MotivoRecusaMercLocal { get; set; }
        public DateTime? DataRecusaMercLocal { get; set; }
        public int? IdCompra { get; set; }
        public string N�Fornecedor { get; set; }
        public string N�EncomendaAberto { get; set; }
        public int? N�LinhaEncomendaAberto { get; set; }
        public string N�DeConsultaMercadoCriada { get; set; }
        public string N�EncomendaCriada { get; set; }
        public string C�digoProdutoFornecedor { get; set; }
        public string UnidadeProdutivaNutri��o { get; set; }
        public string Regi�oMercadoLocal { get; set; }
        public string N�Cliente { get; set; }
        public string Aprovadores { get; set; }
        public bool? Urgente { get; set; }
        public string GrupoRegistoIvanegocio { get; set; }
        public string GrupoRegistoIvaproduto { get; set; }
    }
}
