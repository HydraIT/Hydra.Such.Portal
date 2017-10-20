﻿using Hydra.Such.Data.Database;

namespace Hydra.Such.Data.ViewModel.CCP
{
    public static class CCPFunctions
    {
        #region functions to map the Entity Framework objects to Json-friendly objects
        // this function receives an ProcedimentosCcp objet and maps it to a ProcedimentosCccpView object
        public static ProcedimentoCCPView CastProcCcpToProcCcpView(ProcedimentosCcp proc)
        {
            return (new ProcedimentoCCPView
            {
                No = proc.Nº,
                Tipo = proc.Tipo,
                Ano = proc.Ano,
                Referencia = proc.Referência,
                CodigoRegiao = proc.CódigoRegião,
                CodigoAreaFuncional = proc.CódigoÁreaFuncional,
                CodigoCentroResponsabilidade = proc.CódigoCentroResponsabilidade,
                Estado = proc.Estado,
                DataCriacao = proc.DataCriação,
                Imobilizado = proc.Imobilizado,
                ComentarioEstado = proc.ComentárioEstado,
                Anexos = proc.Anexos,
                DataHoraEstado = proc.DataHoraEstado,
                UtilizadorEstado = proc.UtilizadorEstado,
                CondicoesDePagamento = proc.CondiçõesDePagamento,
                NomeProcesso = proc.NomeProcesso,
                GestorProcesso = proc.GestorProcesso,
                TipoProcedimento = proc.TipoProcedimento,
                InformacaoTecnica = proc.InformaçãoTécnica,
                FundamentacaoAquisicao = proc.FundamentaçãoAquisição,
                PrecoBase = proc.PreçoBase,
                ValorPrecoBase = proc.ValorPreçoBase,
                Negociacao = proc.Negociação,
                CriteriosAdjudicacao = proc.CritériosAdjudicação,
                Prazo = proc.Prazo,
                PrecoMaisBaixo = proc.PreçoMaisBaixo,
                PropostaEconMaisVantajosa = proc.PropostaEconMaisVantajosa,
                PropostaVariante = proc.PropostaVariante,
                AbertoFechadoAoMercado = proc.AbertoFechadoAoMercado,
                PrazoEntrega = proc.PrazoEntrega,
                LocaisEntrega = proc.LocaisEntrega,
                ObservacoesAdicionais = proc.ObservaçõesAdicionais,
                EstimativaPreco = proc.EstimativaPreço,
                FornecedoresSugeridos = proc.FornecedoresSugeridos,
                FornecedorExclusivo = proc.FornecedorExclusivo,
                Interlocutor = proc.Interlocutor,
                DescPrecoMaisBaixo = proc.DescPreçoMaisBaixo,
                DescPropostaEconMaisVantajosa = proc.DescPropostaEconMaisVantajosa,
                DescPropostaVariante = proc.DescPropostaVariante,
                DescAbertoFechadoAoMercado = proc.DescAbertoFechadoAoMercado,
                DescFornecedorExclusivo = proc.DescFornecedorExclusivo,
                CriterioEscolhaProcedimento = proc.CritérioEscolhaProcedimento,
                DescEscolhaProcedimento = proc.DescEscolhaProcedimento,
                Juri = proc.Júri,
                ObjetoDoContrato = proc.ObjetoDoContrato,
                PreArea = proc.PréÁrea,
                SubmeterPreArea = proc.SubmeterPréÁrea,
                ValorDecisaoContratar = proc.ValorDecisãoContratar,
                ValorAdjudicacaoAnteriro = proc.ValorAdjudicaçãoAnteriro,
                ValorAdjudicacaoAtual = proc.ValorAdjudicaçãoAtual,
                DiferencaEuros = proc.DiferençaEuros,
                DiferencaPercent = proc.DiferençaPercent,
                WorkflowFinanceiros = proc.WorkflowFinanceiros,
                WorkflowJuridicos = proc.WorkflowJurídicos,
                WorkflowFinanceirosConfirm = proc.WorkflowFinanceirosConfirm,
                WorkflowJuridicosConfirm = proc.WorkflowJurídicosConfirm,
                AutorizacaoImobCa = proc.AutorizaçãoImobCa,
                AutorizacaoAberturaCa = proc.AutorizaçãoAberturaCa,
                AutorizacaoAquisicaoCa = proc.AutorizaçãoAquisiçãoCa,
                RejeicaoImobCa = proc.RejeiçãoImobCa,
                RejeicaoAberturaCa = proc.RejeiçãoAberturaCa,
                RejeicaoAquisicaoCa = proc.RejeiçãoAquisiçãoCa,
                DataAutorizacaoImobCa = proc.DataAutorizaçãoImobCa,
                DataAutorizacaoAbertCa = proc.DataAutorizaçãoAbertCa,
                DataAutorizacaoAquisiCa = proc.DataAutorizaçãoAquisiCa,
                RatificarCaAbertura = proc.RatificarCaAbertura,
                RatificarCaAdjudicacao = proc.RatificarCaAdjudicação,
                CaRatificar = proc.CaRatificar,
                CaDataRatificacaoAbert = proc.CaDataRatificaçãoAbert,
                CaDataRatificacaoAdjudic = proc.CaDataRatificaçãoAdjudic,
                No_Ata = proc.NºAta,
                DataAta = proc.DataAta,
                ComentarioPublicacao = proc.ComentárioPublicação,
                DataPublicacao = proc.DataPublicação,
                UtilizadorPublicacao = proc.UtilizadorPublicação,
                DataSistemaPublicacao = proc.DataSistemaPublicação,
                RecolhaComentario = proc.RecolhaComentário,
                DataRecolha = proc.DataRecolha,
                UtilizadorRecolha = proc.UtilizadorRecolha,
                DataSistemaRecolha = proc.DataSistemaRecolha,
                ComentarioRelatorioPreliminar = proc.ComentárioRelatórioPreliminar,
                DataValidRelatorioPreliminar = proc.DataValidRelatórioPreliminar,
                UtilizadorValidRelatorioPreliminar = proc.UtilizadorValidRelatórioPreliminar,
                DataSistemaValidRelatorioPreliminar = proc.DataSistemaValidRelatórioPreliminar,
                ComentarioAudienciaPrevia = proc.ComentárioAudiênciaPrévia,
                DataAudienciaPrevia = proc.DataAudiênciaPrévia,
                UtilizadorAudienciaPrevia = proc.UtilizadorAudiênciaPrévia,
                DataSistemaAudienciaPrevia = proc.DataSistemaAudiênciaPrévia,
                ComentarioRelatorioFinal = proc.ComentárioRelatórioFinal,
                DataRelatorioFinal = proc.DataRelatórioFinal,
                UtilizadorRelatorioFinal = proc.UtilizadorRelatórioFinal,
                DataSistemaRelatorioFinal = proc.DataSistemaRelatórioFinal,
                ComentarioNotificacao = proc.ComentárioNotificação,
                DataNotificacao = proc.DataNotificação,
                UtilizadorNotificacao = proc.UtilizadorNotificação,
                DataSistemaNotificacao = proc.DataSistemaNotificação,
                PrazoNotificacaoDias = proc.PrazoNotificaçãoDias,
                PercentExecucao = proc.PercentExecução,
                Arquivado = proc.Arquivado,
                DataFechoInicial = proc.DataFechoInicial,
                DataFechoPrevista = proc.DataFechoPrevista,
                No_DiasProcesso = proc.NºDiasProcesso,
                No_DiasAtraso = proc.NºDiasAtraso,
                Tratado = proc.Tratado,
                Fornecedor = proc.Fornecedor,
                Comentario = proc.Comentário,
                CaSuspenso = proc.CaSuspenso,
                CriticoAbertura = proc.CríticoAbertura,
                CriticoAdjudicacao = proc.CríticoAdjudicação,
                ObjetoDecisao = proc.ObjetoDecisão,
                RazaoNecessidade = proc.RazãoNecessidade,
                ProtocoloContrato = proc.ProtocoloContrato,
                AutorizacaoAdjudicacao = proc.AutorizaçãoAdjudicação,
                NaoAdjudicacaoEEncerramento = proc.NãoAdjudicaçãoEEncerramento,
                NaoAdjudicacaoESuspensao = proc.NãoAdjudicaçãoESuspensão,
                DataHoraCriacao = proc.DataHoraCriação,
                UtilizadorCriacao = proc.UtilizadorCriação,
                DataHoraModificacao = proc.DataHoraModificação,
                UtilizadorModificacao = proc.UtilizadorModificação
            });
        }

        // this function receives an ProcedimentosCcpView object and maps it to a ProcedimentosCcp object
        public static ProcedimentosCcp CastProcCcpViewToProcCcp(ProcedimentoCCPView proc_view)
        {
            return (new ProcedimentosCcp
            {
                Nº = proc_view.No,
                Tipo = proc_view.Tipo,
                Ano = proc_view.Ano,
                Referência = proc_view.Referencia,
                CódigoRegião = proc_view.CodigoRegiao,
                CódigoÁreaFuncional = proc_view.CodigoAreaFuncional,
                CódigoCentroResponsabilidade = proc_view.CodigoCentroResponsabilidade,
                Estado = proc_view.Estado,
                DataCriação = proc_view.DataCriacao,
                Imobilizado = proc_view.Imobilizado,
                ComentárioEstado = proc_view.ComentarioEstado,
                Anexos = proc_view.Anexos,
                DataHoraEstado = proc_view.DataHoraEstado,
                UtilizadorEstado = proc_view.UtilizadorEstado,
                CondiçõesDePagamento = proc_view.CondicoesDePagamento,
                NomeProcesso = proc_view.NomeProcesso,
                GestorProcesso = proc_view.GestorProcesso,
                TipoProcedimento = proc_view.TipoProcedimento,
                InformaçãoTécnica = proc_view.InformacaoTecnica,
                FundamentaçãoAquisição = proc_view.FundamentacaoAquisicao,
                PreçoBase = proc_view.PrecoBase,
                ValorPreçoBase = proc_view.ValorPrecoBase,
                Negociação = proc_view.Negociacao,
                CritériosAdjudicação = proc_view.CriteriosAdjudicacao,
                Prazo = proc_view.Prazo,
                PreçoMaisBaixo = proc_view.PrecoMaisBaixo,
                PropostaEconMaisVantajosa = proc_view.PropostaEconMaisVantajosa,
                PropostaVariante = proc_view.PropostaVariante,
                AbertoFechadoAoMercado = proc_view.AbertoFechadoAoMercado,
                PrazoEntrega = proc_view.PrazoEntrega,
                LocaisEntrega = proc_view.LocaisEntrega,
                ObservaçõesAdicionais = proc_view.ObservacoesAdicionais,
                EstimativaPreço = proc_view.EstimativaPreco,
                FornecedoresSugeridos = proc_view.FornecedoresSugeridos,
                FornecedorExclusivo = proc_view.FornecedorExclusivo,
                Interlocutor = proc_view.Interlocutor,
                DescPreçoMaisBaixo = proc_view.DescPrecoMaisBaixo,
                DescPropostaEconMaisVantajosa = proc_view.DescPropostaEconMaisVantajosa,
                DescPropostaVariante = proc_view.DescPropostaVariante,
                DescAbertoFechadoAoMercado = proc_view.DescAbertoFechadoAoMercado,
                DescFornecedorExclusivo = proc_view.DescFornecedorExclusivo,
                CritérioEscolhaProcedimento = proc_view.CriterioEscolhaProcedimento,
                DescEscolhaProcedimento = proc_view.DescEscolhaProcedimento,
                Júri = proc_view.Juri,
                ObjetoDoContrato = proc_view.ObjetoDoContrato,
                PréÁrea = proc_view.PreArea,
                SubmeterPréÁrea = proc_view.SubmeterPreArea,
                ValorDecisãoContratar = proc_view.ValorDecisaoContratar,
                ValorAdjudicaçãoAnteriro = proc_view.ValorAdjudicacaoAnteriro,
                ValorAdjudicaçãoAtual = proc_view.ValorAdjudicacaoAtual,
                DiferençaEuros = proc_view.DiferencaEuros,
                DiferençaPercent = proc_view.DiferencaPercent,
                WorkflowFinanceiros = proc_view.WorkflowFinanceiros,
                WorkflowJurídicos = proc_view.WorkflowJuridicos,
                WorkflowFinanceirosConfirm = proc_view.WorkflowFinanceirosConfirm,
                WorkflowJurídicosConfirm = proc_view.WorkflowJuridicosConfirm,
                AutorizaçãoImobCa = proc_view.AutorizacaoImobCa,
                AutorizaçãoAberturaCa = proc_view.AutorizacaoAberturaCa,
                AutorizaçãoAquisiçãoCa = proc_view.AutorizacaoAquisicaoCa,
                RejeiçãoImobCa = proc_view.RejeicaoImobCa,
                RejeiçãoAberturaCa = proc_view.RejeicaoAberturaCa,
                RejeiçãoAquisiçãoCa = proc_view.RejeicaoAquisicaoCa,
                DataAutorizaçãoImobCa = proc_view.DataAutorizacaoImobCa,
                DataAutorizaçãoAbertCa = proc_view.DataAutorizacaoAbertCa,
                DataAutorizaçãoAquisiCa = proc_view.DataAutorizacaoAquisiCa,
                RatificarCaAbertura = proc_view.RatificarCaAbertura,
                RatificarCaAdjudicação = proc_view.RatificarCaAdjudicacao,
                CaRatificar = proc_view.CaRatificar,
                CaDataRatificaçãoAbert = proc_view.CaDataRatificacaoAbert,
                CaDataRatificaçãoAdjudic = proc_view.CaDataRatificacaoAdjudic,
                NºAta = proc_view.No_Ata,
                DataAta = proc_view.DataAta,
                ComentárioPublicação = proc_view.ComentarioPublicacao,
                DataPublicação = proc_view.DataPublicacao,
                UtilizadorPublicação = proc_view.UtilizadorPublicacao,
                DataSistemaPublicação = proc_view.DataSistemaPublicacao,
                RecolhaComentário = proc_view.RecolhaComentario,
                DataRecolha = proc_view.DataRecolha,
                UtilizadorRecolha = proc_view.UtilizadorRecolha,
                DataSistemaRecolha = proc_view.DataSistemaRecolha,
                ComentárioRelatórioPreliminar = proc_view.ComentarioRelatorioPreliminar,
                DataValidRelatórioPreliminar = proc_view.DataValidRelatorioPreliminar,
                UtilizadorValidRelatórioPreliminar = proc_view.UtilizadorValidRelatorioPreliminar,
                DataSistemaValidRelatórioPreliminar = proc_view.DataSistemaValidRelatorioPreliminar,
                ComentárioAudiênciaPrévia = proc_view.ComentarioAudienciaPrevia,
                DataAudiênciaPrévia = proc_view.DataAudienciaPrevia,
                UtilizadorAudiênciaPrévia = proc_view.UtilizadorAudienciaPrevia,
                DataSistemaAudiênciaPrévia = proc_view.DataSistemaAudienciaPrevia,
                ComentárioRelatórioFinal = proc_view.ComentarioRelatorioFinal,
                DataRelatórioFinal = proc_view.DataRelatorioFinal,
                UtilizadorRelatórioFinal = proc_view.UtilizadorRelatorioFinal,
                DataSistemaRelatórioFinal = proc_view.DataSistemaRelatorioFinal,
                ComentárioNotificação = proc_view.ComentarioNotificacao,
                DataNotificação = proc_view.DataNotificacao,
                UtilizadorNotificação = proc_view.UtilizadorNotificacao,
                DataSistemaNotificação = proc_view.DataSistemaNotificacao,
                PrazoNotificaçãoDias = proc_view.PrazoNotificacaoDias,
                PercentExecução = proc_view.PercentExecucao,
                Arquivado = proc_view.Arquivado,
                DataFechoInicial = proc_view.DataFechoInicial,
                DataFechoPrevista = proc_view.DataFechoPrevista,
                NºDiasProcesso = proc_view.No_DiasProcesso,
                NºDiasAtraso = proc_view.No_DiasAtraso,
                Tratado = proc_view.Tratado,
                Fornecedor = proc_view.Fornecedor,
                Comentário = proc_view.Comentario,
                CaSuspenso = proc_view.CaSuspenso,
                CríticoAbertura = proc_view.CriticoAbertura,
                CríticoAdjudicação = proc_view.CriticoAdjudicacao,
                ObjetoDecisão = proc_view.ObjetoDecisao,
                RazãoNecessidade = proc_view.RazaoNecessidade,
                ProtocoloContrato = proc_view.ProtocoloContrato,
                AutorizaçãoAdjudicação = proc_view.AutorizacaoAdjudicacao,
                NãoAdjudicaçãoEEncerramento = proc_view.NaoAdjudicacaoEEncerramento,
                NãoAdjudicaçãoESuspensão = proc_view.NaoAdjudicacaoESuspensao,
                DataHoraCriação = proc_view.DataHoraCriacao,
                UtilizadorCriação = proc_view.UtilizadorCriacao,
                DataHoraModificação = proc_view.DataHoraModificacao,
                UtilizadorModificação = proc_view.UtilizadorModificacao

            });
        }

        public static RegistoActasView CastRegistoActasToRegistoActasView(RegistoDeAtas reg)
        {
            return (new RegistoActasView
            {
                NumProcedimento = reg.NºProcedimento,
                NumActa = reg.NºAta,
                DataDaActa = reg.DataDaAta,
                Observacoes = reg.Observações,
                DataHoraCriacao = reg.DataHoraCriação,
                UtilizadorCriacao = reg.UtilizadorCriação,
                DataHoraModificacao = reg.DataHoraModificação,
                UtilizadorModificacao = reg.UtilizadorModificação
            });
        }

        public static RegistoDeAtas CastRegistoActasViewToRegistoActas(RegistoActasView reg)
        {
            return (new RegistoDeAtas
            {
                NºProcedimento = reg.NumProcedimento,
                NºAta = reg.NumActa,
                DataDaAta = reg.DataDaActa,
                Observações = reg.Observacoes,
                DataHoraCriação = reg.DataHoraCriacao,
                UtilizadorCriação = reg.UtilizadorCriacao,
                DataHoraModificação = reg.DataHoraModificacao,
                UtilizadorModificação = reg.UtilizadorModificacao
            });
        }

        
        #endregion
    }
}
