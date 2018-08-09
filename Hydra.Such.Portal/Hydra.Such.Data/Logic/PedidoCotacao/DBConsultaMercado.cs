﻿using Hydra.Such.Data.Database;
using Hydra.Such.Data.ViewModel.PedidoCotacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Hydra.Such.Data.Logic.PedidoCotacao
{
    public class DBConsultaMercado
    {
        #region Actividades

        #endregion


        #region Actividades_por_Fornecedor

        #endregion


        #region Actividades_por_Produto

        #endregion


        #region Consulta_Mercado

        public static List<ConsultaMercado> GetAllConsultaMercadoToList()
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    return ctx.ConsultaMercado.ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static ConsultaMercado GetConsultaMercadoToList(string Num_Consulta_Mercado)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    return ctx.ConsultaMercado.Where(p => p.NumConsultaMercado == Num_Consulta_Mercado).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static ConsultaMercado Create(ConsultaMercado ObjectToCreate)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    ObjectToCreate.PedidoCotacaoCriadoEm = DateTime.Now;
                    ctx.ConsultaMercado.Add(ObjectToCreate);
                    ctx.SaveChanges();
                }

                return ObjectToCreate;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static ConsultaMercado Update(ConsultaMercado ObjectToUpdate)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    ctx.ConsultaMercado.Update(ObjectToUpdate);
                    ctx.SaveChanges();
                }

                return ObjectToUpdate;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public static ConsultaMercado Delete(ConsultaMercado ObjectToDelete)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    ctx.ConsultaMercado.Remove(ObjectToDelete);
                    ctx.SaveChanges();
                }

                return ObjectToDelete;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public static List<ConsultaMercadoView> GetAllConsultaMercadoViewToList()
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    return ctx.ConsultaMercado.Select(ConsultaMercado => new ConsultaMercadoView()
                    {
                        NumConsultaMercado = ConsultaMercado.NumConsultaMercado,
                        CodProjecto = ConsultaMercado.CodProjecto,
                        Descricao = ConsultaMercado.Descricao,
                        CodRegiao = ConsultaMercado.CodRegiao,
                        CodAreaFuncional = ConsultaMercado.CodAreaFuncional,
                        CodCentroResponsabilidade = ConsultaMercado.CodCentroResponsabilidade,
                        CodActividade = ConsultaMercado.CodActividade,
                        //DataPedidoCotacao = ConsultaMercado.DataPedidoCotacao == null ? default(DateTime) : Convert.ToDateTime(ConsultaMercado.DataPedidoCotacao.Value.ToString("yyyy-MM-dd")),
                        DataPedidoCotacao = ConsultaMercado.DataPedidoCotacao,
                        FornecedorSelecionado = ConsultaMercado.FornecedorSelecionado,
                        NumDocumentoCompra = ConsultaMercado.NumDocumentoCompra,
                        CodLocalizacao = ConsultaMercado.CodLocalizacao,
                        FiltroActividade = ConsultaMercado.FiltroActividade,
                        ValorPedidoCotacao = ConsultaMercado.ValorPedidoCotacao,
                        Destino = ConsultaMercado.Destino,
                        Estado = ConsultaMercado.Estado,
                        UtilizadorRequisicao = ConsultaMercado.UtilizadorRequisicao,
                        DataLimite = ConsultaMercado.DataLimite,
                        EspecificacaoTecnica = ConsultaMercado.EspecificacaoTecnica,
                        Fase = ConsultaMercado.Fase,
                        Modalidade = ConsultaMercado.Modalidade,
                        PedidoCotacaoCriadoEm = ConsultaMercado.PedidoCotacaoCriadoEm,
                        PedidoCotacaoCriadoPor = ConsultaMercado.PedidoCotacaoCriadoPor,
                        ConsultaEm = ConsultaMercado.ConsultaEm,
                        ConsultaPor = ConsultaMercado.ConsultaPor,
                        NegociacaoContratacaoEm = ConsultaMercado.NegociacaoContratacaoEm,
                        NegociacaoContratacaoPor = ConsultaMercado.NegociacaoContratacaoPor,
                        AdjudicacaoEm = ConsultaMercado.AdjudicacaoEm,
                        AdjudicacaoPor = ConsultaMercado.AdjudicacaoPor,
                        NumRequisicao = ConsultaMercado.NumRequisicao,
                        PedidoCotacaoOrigem = ConsultaMercado.PedidoCotacaoOrigem,
                        ValorAdjudicado = ConsultaMercado.ValorAdjudicado,
                        CodFormaPagamento = ConsultaMercado.CodFormaPagamento,
                        SeleccaoEfectuada = ConsultaMercado.SeleccaoEfectuada
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static ConsultaMercadoView CastConsultaMercadoToView(ConsultaMercado ObjectToTransform)
        {
            ConsultaMercado consultaMercado = new ConsultaMercado();
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    consultaMercado = ctx.ConsultaMercado.Where(p => p.NumConsultaMercado == ObjectToTransform.NumConsultaMercado).FirstOrDefault();
                }
            }
            catch (Exception e)
            {

            }

            ConsultaMercadoView view = new ConsultaMercadoView()
            {
                NumConsultaMercado = ObjectToTransform.NumConsultaMercado,
                CodProjecto = ObjectToTransform.CodProjecto,
                Descricao = ObjectToTransform.Descricao,
                CodRegiao = ObjectToTransform.CodRegiao,
                CodAreaFuncional = ObjectToTransform.CodAreaFuncional,
                CodCentroResponsabilidade = ObjectToTransform.CodCentroResponsabilidade,
                CodActividade = ObjectToTransform.CodActividade,
                //DataPedidoCotacao = ObjectToTransform.DataPedidoCotacao == null ? default(DateTime) : Convert.ToDateTime(ObjectToTransform.DataPedidoCotacao.Value.ToString("yyyy-MM-dd")),
                DataPedidoCotacao = ObjectToTransform.DataPedidoCotacao,
                FornecedorSelecionado = ObjectToTransform.FornecedorSelecionado,
                NumDocumentoCompra = ObjectToTransform.NumDocumentoCompra,
                CodLocalizacao = ObjectToTransform.CodLocalizacao,
                FiltroActividade = ObjectToTransform.FiltroActividade,
                ValorPedidoCotacao = ObjectToTransform.ValorPedidoCotacao,
                Destino = ObjectToTransform.Destino,
                Estado = ObjectToTransform.Estado,
                UtilizadorRequisicao = ObjectToTransform.UtilizadorRequisicao,
                DataLimite = ObjectToTransform.DataLimite,
                EspecificacaoTecnica = ObjectToTransform.EspecificacaoTecnica,
                Fase = ObjectToTransform.Fase,
                Modalidade = ObjectToTransform.Modalidade,
                PedidoCotacaoCriadoEm = ObjectToTransform.PedidoCotacaoCriadoEm,
                PedidoCotacaoCriadoPor = ObjectToTransform.PedidoCotacaoCriadoPor,
                ConsultaEm = ObjectToTransform.ConsultaEm,
                ConsultaPor = ObjectToTransform.ConsultaPor,
                NegociacaoContratacaoEm = ObjectToTransform.NegociacaoContratacaoEm,
                NegociacaoContratacaoPor = ObjectToTransform.NegociacaoContratacaoPor,
                AdjudicacaoEm = ObjectToTransform.AdjudicacaoEm,
                AdjudicacaoPor = ObjectToTransform.AdjudicacaoPor,
                NumRequisicao = ObjectToTransform.NumRequisicao,
                PedidoCotacaoOrigem = ObjectToTransform.PedidoCotacaoOrigem,
                ValorAdjudicado = ObjectToTransform.ValorAdjudicado,
                CodFormaPagamento = ObjectToTransform.CodFormaPagamento,
                SeleccaoEfectuada = ObjectToTransform.SeleccaoEfectuada
            };

            if (consultaMercado.CondicoesPropostasFornecedores != null && consultaMercado.CondicoesPropostasFornecedores.Count > 0)
            {
                List<CondicoesPropostasFornecedoresView> CondicoesPropostasFornecedoresList = new List<CondicoesPropostasFornecedoresView>();
                foreach (var cpf in consultaMercado.CondicoesPropostasFornecedores)
                {
                    CondicoesPropostasFornecedoresList.Add(CastCondicoesPropostasFornecedoresToView(cpf));
                }

                view.CondicoesPropostasFornecedores = CondicoesPropostasFornecedoresList;
            }

            if (consultaMercado.LinhasCondicoesPropostasFornecedores != null && consultaMercado.LinhasCondicoesPropostasFornecedores.Count > 0)
            {
                List<LinhasCondicoesPropostasFornecedoresView> LinhasCondicoesPropostasFornecedoresList = new List<LinhasCondicoesPropostasFornecedoresView>();
                foreach (var lcpf in consultaMercado.LinhasCondicoesPropostasFornecedores)
                {
                    LinhasCondicoesPropostasFornecedoresList.Add(CastLinhasCondicoesPropostasFornecedoresToView(lcpf));
                }

                view.LinhasCondicoesPropostasFornecedores = LinhasCondicoesPropostasFornecedoresList;
            }

            if (consultaMercado.LinhasConsultaMercado != null && consultaMercado.LinhasConsultaMercado.Count > 0)
            {
                List<LinhasConsultaMercadoView> linhasConsultaMercadoList = new List<LinhasConsultaMercadoView>();
                foreach (var lcm in consultaMercado.LinhasConsultaMercado)
                {
                    linhasConsultaMercadoList.Add(CastLinhasConsultaMercadoToView(lcm));
                }

                view.LinhasConsultaMercado = linhasConsultaMercadoList;
            }

            if (consultaMercado.SeleccaoEntidades != null && consultaMercado.SeleccaoEntidades.Count > 0)
            {
                List<SeleccaoEntidadesView> seleccaoEntidadesList = new List<SeleccaoEntidadesView>();
                foreach (var se in consultaMercado.SeleccaoEntidades)
                {
                    seleccaoEntidadesList.Add(CastSeleccaoEntidadesToView(se));
                }

                view.SeleccaoEntidades = seleccaoEntidadesList;
            }

            return view;
        }

        #endregion


        #region Linhas_Consulta_Mercado

        public static LinhasConsultaMercadoView CastLinhasConsultaMercadoToView(LinhasConsultaMercado ObjectToTransform)
        {
            LinhasConsultaMercado linhasConsultaMercado = new LinhasConsultaMercado();
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    linhasConsultaMercado = ctx.LinhasConsultaMercado.Where(p => p.NumLinha == ObjectToTransform.NumLinha).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                
            }

            LinhasConsultaMercadoView view = new LinhasConsultaMercadoView()
            {
                NumLinha = ObjectToTransform.NumLinha,
                NumConsultaMercado = ObjectToTransform.NumConsultaMercado,
                CodProduto = ObjectToTransform.CodProduto,
                Descricao = ObjectToTransform.Descricao,
                NumProjecto = ObjectToTransform.NumProjecto,
                CodRegiao = ObjectToTransform.CodRegiao,
                CodAreaFuncional = ObjectToTransform.CodAreaFuncional,
                CodCentroResponsabilidade = ObjectToTransform.CodCentroResponsabilidade,
                CodActividade = ObjectToTransform.CodActividade,
                CodLocalizacao = ObjectToTransform.CodLocalizacao,
                Quantidade = ObjectToTransform.Quantidade,
                CustoUnitarioPrevisto = ObjectToTransform.CustoUnitarioPrevisto,
                CustoTotalPrevisto = ObjectToTransform.CustoTotalPrevisto,
                CustoUnitarioObjectivo = ObjectToTransform.CustoUnitarioObjectivo,
                CustoTotalObjectivo = ObjectToTransform.CustoTotalObjectivo,
                CodUnidadeMedida = ObjectToTransform.CodUnidadeMedida,
                DataEntregaPrevista = ObjectToTransform.DataEntregaPrevista,
                NumRequisicao = ObjectToTransform.NumRequisicao,
                LinhaRequisicao = ObjectToTransform.LinhaRequisicao,
                CriadoEm = ObjectToTransform.CriadoEm,
                CriadoPor = ObjectToTransform.CriadoPor,
                ModificadoEm = ObjectToTransform.ModificadoEm,
                ModificadoPor = ObjectToTransform.ModificadoPor
            };

            return view;
        }

        #endregion


        #region Condicoes_Propostas_Fornecedores

        public static CondicoesPropostasFornecedoresView CastCondicoesPropostasFornecedoresToView(CondicoesPropostasFornecedores ObjectToTransform)
        {
            CondicoesPropostasFornecedores condicoesPropostasFornecedores = new CondicoesPropostasFornecedores();
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    condicoesPropostasFornecedores = ctx.CondicoesPropostasFornecedores.Where(p => p.IdCondicaoPropostaFornecedores == ObjectToTransform.IdCondicaoPropostaFornecedores).FirstOrDefault();
                }
            }
            catch (Exception e)
            {

            }

            CondicoesPropostasFornecedoresView view = new CondicoesPropostasFornecedoresView()
            {
                IdCondicaoPropostaFornecedores = ObjectToTransform.IdCondicaoPropostaFornecedores,
                NumConsultaMercado = ObjectToTransform.NumConsultaMercado,
                CodFornecedor = ObjectToTransform.CodFornecedor,
                Alternativa = ObjectToTransform.Alternativa,
                CodActividade = ObjectToTransform.CodActividade,
                ValidadeProposta = ObjectToTransform.ValidadeProposta,
                CodTermosPagamento = ObjectToTransform.CodTermosPagamento,
                CodFormaPagamento = ObjectToTransform.CodFormaPagamento,
                FornecedorSelecionado = ObjectToTransform.FornecedorSelecionado,
                DataProposta = ObjectToTransform.DataProposta,
                NumProjecto = ObjectToTransform.NumProjecto,
                Preferencial = ObjectToTransform.Preferencial,
                NomeFornecedor = ObjectToTransform.NomeFornecedor,
                EnviarPedidoProposta = ObjectToTransform.EnviarPedidoProposta,
                Notificado = ObjectToTransform.Notificado
            };

            return view;
        }

        #endregion


        #region Linhas_Condicoes_Propostas_Fornecedores

        public static LinhasCondicoesPropostasFornecedoresView CastLinhasCondicoesPropostasFornecedoresToView(LinhasCondicoesPropostasFornecedores ObjectToTransform)
        {
            LinhasCondicoesPropostasFornecedores linhasCondicoesPropostasFornecedores = new LinhasCondicoesPropostasFornecedores();
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    linhasCondicoesPropostasFornecedores = ctx.LinhasCondicoesPropostasFornecedores.Where(p => p.NumLinha == ObjectToTransform.NumLinha).FirstOrDefault();
                }
            }
            catch (Exception e)
            {

            }

            LinhasCondicoesPropostasFornecedoresView view = new LinhasCondicoesPropostasFornecedoresView()
            {
                NumLinha = ObjectToTransform.NumLinha,
                NumConsultaMercado = ObjectToTransform.NumConsultaMercado,
                CodFornecedor = ObjectToTransform.CodFornecedor,
                Alternativa = ObjectToTransform.Alternativa,
                CodProduto = ObjectToTransform.CodProduto,
                Quantidade = ObjectToTransform.Quantidade,
                NumProjecto = ObjectToTransform.NumProjecto,
                CodLocalizacao = ObjectToTransform.CodLocalizacao,
                PrecoFornecedor = ObjectToTransform.PrecoFornecedor,
                DataEntregaPrevista = ObjectToTransform.DataEntregaPrevista,
                Validade = ObjectToTransform.Validade,
                QuantidadeAAdjudicar = ObjectToTransform.QuantidadeAAdjudicar,
                MotivoRejeicao = ObjectToTransform.MotivoRejeicao,
                QuantidadeAdjudicada = ObjectToTransform.QuantidadeAdjudicada,
                QuantidadeAEncomendar = ObjectToTransform.QuantidadeAEncomendar,
                QuantidadeEncomendada = ObjectToTransform.QuantidadeEncomendada,
                CodUnidadeMedida = ObjectToTransform.CodUnidadeMedida,
                PrazoEntrega = ObjectToTransform.PrazoEntrega,
                CodActividade = ObjectToTransform.CodActividade,
                PercentagemDescontoLinha = ObjectToTransform.PercentagemDescontoLinha,
                ValorAdjudicadoDl = ObjectToTransform.ValorAdjudicadoDl,
                EstadoRespostaFornecedor = ObjectToTransform.EstadoRespostaFornecedor,
                DataEntregaPrometida = ObjectToTransform.DataEntregaPrometida,
                RespostaFornecedor = ObjectToTransform.RespostaFornecedor,
                QuantidadeRespondida = ObjectToTransform.QuantidadeRespondida
            };

            return view;
        }

        #endregion


        #region Seleccao_Entidades

        public static SeleccaoEntidadesView CastSeleccaoEntidadesToView(SeleccaoEntidades ObjectToTransform)
        {
            SeleccaoEntidades seleccaoEntidades = new SeleccaoEntidades();
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    seleccaoEntidades = ctx.SeleccaoEntidades.Where(p => p.IdSeleccaoEntidades == ObjectToTransform.IdSeleccaoEntidades).FirstOrDefault();
                }
            }
            catch (Exception e)
            {

            }

            SeleccaoEntidadesView view = new SeleccaoEntidadesView()
            {
                IdSeleccaoEntidades = ObjectToTransform.IdSeleccaoEntidades,
                NumConsultaMercado = ObjectToTransform.NumConsultaMercado,
                CodFornecedor = ObjectToTransform.CodFornecedor,
                NomeFornecedor = ObjectToTransform.NomeFornecedor,
                CodActividade = ObjectToTransform.CodActividade,
                CidadeFornecedor = ObjectToTransform.CidadeFornecedor,
                CodTermosPagamento = ObjectToTransform.CodTermosPagamento,
                CodFormaPagamento = ObjectToTransform.CodFormaPagamento,
                Selecionado = ObjectToTransform.Selecionado,
                Preferencial = ObjectToTransform.Preferencial
            };

            return view;
        }

        #endregion


        #region Historico_Consulta_Mercado

        #endregion


        #region Historico_Linhas_Consulta_Mercado

        #endregion


        #region Historico_Condicoes_Propostas_Fornecedores

        #endregion


        #region Historico_Linhas_Condicoes_Propostas_Fornecedores

        #endregion


        #region Historico_Seleccao_Entidades

        #endregion

    }
}
