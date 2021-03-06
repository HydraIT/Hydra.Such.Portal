﻿using System;
using System.Collections.Generic;

namespace Hydra.Such.Data.Evolution.DatabaseReference
{
    public partial class Equipamento
    {
        public Equipamento()
        {
            EquipDadosTecnicos = new HashSet<EquipDadosTecnicos>();
            EquipDependenteIdEquipPrincipalNavigation = new HashSet<EquipDependente>();
            EquipDependenteIdEquipSecundarioNavigation = new HashSet<EquipDependente>();
            EquipPimp = new HashSet<EquipPimp>();
            EquipamentoAcessorio = new HashSet<EquipamentoAcessorio>();
            OrdemManutencaoEquipamentos = new HashSet<OrdemManutencaoEquipamentos>();
            OrdemManutencaoLinha = new HashSet<OrdemManutencaoLinha>();
            SolicitacoesLinha = new HashSet<SolicitacoesLinha>();
        }

        public int IdEquipamento { get; set; }
        public string Nome { get; set; }
        public int Marca { get; set; }
        public int Modelo { get; set; }
        public int Categoria { get; set; }
        public string NumSerie { get; set; }
        public string NumInventario { get; set; }
        public string NumEquipamento { get; set; }
        public string Observacao { get; set; }
        public string IdFornecedor { get; set; }
        public string NomeFornecedor { get; set; }
        public int IdCliente { get; set; }
        public int IdServico { get; set; }
        public bool? Activo { get; set; }
        public DateTime? DataAquisicao { get; set; }
        public DateTime? DataInstalacao { get; set; }
        public int? AnoFabrico { get; set; }
        public int? IdRegiao { get; set; }
        public int? IdArea { get; set; }
        public int? IdEquipa { get; set; }
        public int? IdAreaOp { get; set; }
        public string IdContrato { get; set; }
        public byte[] CodigoBarras { get; set; }
        public byte[] CodigoBarrasCliente { get; set; }
        public decimal? PrecoAquisicao { get; set; }
        public string Localizacao { get; set; }
        public int? IdPeriodicidade { get; set; }
        public bool AssociadoContrato { get; set; }
        public int InseridoPor { get; set; }
        public DateTime DataInsercao { get; set; }
        public bool? PorValidar { get; set; }
        public int? ValidadoPor { get; set; }
        public DateTime? DataValidacao { get; set; }
        public DateTime? DataEntradaContrato { get; set; }
        public DateTime? DataSaidaContrato { get; set; }
        public string DesignacaoComplementar { get; set; }
        public byte[] Foto { get; set; }
        public int? AlteradoPor { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int ToleranciaEquipamento { get; set; }
        public bool? Garantia { get; set; }
        public DateTime? DataFimGarantia { get; set; }
        public bool? IncluiMc { get; set; }
        public int? MpPlaneadas { get; set; }
        public DateTime? DataInactivacao { get; set; }
        public bool Instalacao { get; set; }
        public int? Criticidade { get; set; }
        public bool? Abatido { get; set; }
        public string Sala { get; set; }
        public string DesignacaoComplementar2 { get; set; }
        public bool? SubContratar { get; set; }

        public virtual EquipCategoria CategoriaNavigation { get; set; }
        public virtual Area IdAreaNavigation { get; set; }
        public virtual AreaOp IdAreaOpNavigation { get; set; }
        public virtual Cliente IdClienteNavigation { get; set; }
        public virtual Contrato IdContratoNavigation { get; set; }
        public virtual Equipa IdEquipaNavigation { get; set; }
        public virtual Fornecedor IdFornecedorNavigation { get; set; }
        public virtual Regiao IdRegiaoNavigation { get; set; }
        public virtual Servico IdServicoNavigation { get; set; }
        public virtual EquipMarca MarcaNavigation { get; set; }
        public virtual EquipModelo ModeloNavigation { get; set; }
        public virtual ICollection<EquipDadosTecnicos> EquipDadosTecnicos { get; set; }
        public virtual ICollection<EquipDependente> EquipDependenteIdEquipPrincipalNavigation { get; set; }
        public virtual ICollection<EquipDependente> EquipDependenteIdEquipSecundarioNavigation { get; set; }
        public virtual ICollection<EquipPimp> EquipPimp { get; set; }
        public virtual ICollection<EquipamentoAcessorio> EquipamentoAcessorio { get; set; }
        public virtual ICollection<OrdemManutencaoEquipamentos> OrdemManutencaoEquipamentos { get; set; }
        public virtual ICollection<OrdemManutencaoLinha> OrdemManutencaoLinha { get; set; }
        public virtual ICollection<SolicitacoesLinha> SolicitacoesLinha { get; set; }
    }
}
