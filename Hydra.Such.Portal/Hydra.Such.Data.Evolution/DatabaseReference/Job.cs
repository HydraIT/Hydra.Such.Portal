﻿using System;
using System.Collections.Generic;

namespace Hydra.Such.Data.Evolution.DatabaseReference
{
    public partial class Job
    {
        public byte[] Timestamp { get; set; }
        public string No { get; set; }
        public string SearchDescription { get; set; }
        public string Description { get; set; }
        public string Description2 { get; set; }
        public string BillToCustomerNo { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public int Status { get; set; }
        public string PersonResponsible { get; set; }
        public string GlobalDimension1Code { get; set; }
        public string GlobalDimension2Code { get; set; }
        public string JobPostingGroup { get; set; }
        public int Blocked { get; set; }
        public DateTime LastDateModified { get; set; }
        public string CustomerDiscGroup { get; set; }
        public string CustomerPriceGroup { get; set; }
        public string LanguageCode { get; set; }
        public byte[] Picture { get; set; }
        public string BillToName { get; set; }
        public string BillToAddress { get; set; }
        public string BillToAddress2 { get; set; }
        public string BillToCity { get; set; }
        public string BillToPostCode { get; set; }
        public string NoSeries { get; set; }
        public string BillToCountryRegionCode { get; set; }
        public int WipMethod { get; set; }
        public string CurrencyCode { get; set; }
        public string BillToContactNo { get; set; }
        public string BillToContact { get; set; }
        public byte WipPostedToGL { get; set; }
        public DateTime WipPostingDate { get; set; }
        public int PostedWipMethodUsed { get; set; }
        public string InvoiceCurrencyCode { get; set; }
        public int ExchCalculationCost { get; set; }
        public int ExchCalculationPrice { get; set; }
        public byte AllowScheduleContractLines { get; set; }
        public byte Complete { get; set; }
        public int CalcWipMethodUsed { get; set; }
        public byte Facturável { get; set; }
        public string ContractNo { get; set; }
        public string Motorista { get; set; }
        public string AreaFilter { get; set; }
        public string ShipToCode { get; set; }
        public string ShipToName { get; set; }
        public string ShipToName2 { get; set; }
        public string ShipToAddress { get; set; }
        public string ShipToAddress2 { get; set; }
        public string ShipToPostCode { get; set; }
        public string ShipToCity { get; set; }
        public string ShipToCounty { get; set; }
        public string ShipToContact { get; set; }
        public int TipoProjecto { get; set; }
        public byte OnlyForMaintInvoicing { get; set; }
        public string Local { get; set; }
        public string Projecto { get; set; }
        public string NossaProposta { get; set; }
        public string ObjectoServiço { get; set; }
        public string ShortcutDimension3Code { get; set; }
        public string ShortcutDimension4Code { get; set; }
        public string Utilizador { get; set; }
        public string AreaIntervencao { get; set; }
        public int FiltroArea { get; set; }
        public string TipoGrupoContabProjecto { get; set; }
        public string TipoGrupoContabOmProjecto { get; set; }
        public string NºAntigoAs400 { get; set; }
        public string PedidoDoCliente { get; set; }
        public int Opção { get; set; }
        public DateTime DataPedido { get; set; }
        public DateTime DataValidade { get; set; }
        public DateTime ValidadePedido { get; set; }
        public decimal ValorProjecto { get; set; }
        public string DescriçãoDetalhada { get; set; }
        public string DeliberaçãoCa { get; set; }
        public byte ServInternosRequisições { get; set; }
        public byte ServInternosFolhasDeObra { get; set; }
        public byte ServInternosDébInternos { get; set; }
        public byte MãoDeObraEDeslocações { get; set; }
        public string ConfigResponsavel { get; set; }
        public DateTime DataUltimoMail { get; set; }
        public string Responsavel { get; set; }
        public string UserChefeProjecto { get; set; }
        public DateTime DataChefeProjecto { get; set; }
        public string UserResponsavel { get; set; }
        public DateTime DataResponsavel { get; set; }
        public string NoCompromisso { get; set; }
        public byte ProjetoMigrado { get; set; }
        public byte ProjetoNaoVisivel { get; set; }
        public byte ProjetoDimensoesFixas { get; set; }
        public string TipoProjetoAec { get; set; }
        public string TipoRequisicao { get; set; }
        public int Contrato { get; set; }
        public byte Integrado { get; set; }
        public DateTime DataIntegracao { get; set; }
        public int CategoriaProjeto { get; set; }
        public string NºContratoOrçamento { get; set; }
        public byte ProjetoInterno { get; set; }
    }
}
