﻿using Hydra.Such.Data.Database;
using Hydra.Such.Data.ViewModel.Compras;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hydra.Such.Data.Logic.ComprasML
{
    public static class DBOcorrencias
    {
        public static List<Ocorrencias> GetAll()
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    return ctx.Ocorrencias.ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static Ocorrencias GetByID(int CodOcorrencia)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    return ctx.Ocorrencias.Where(x => x.CodOcorrencia == CodOcorrencia).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static Ocorrencias Create(Ocorrencias ObjectToCreate)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    ObjectToCreate.DataCriacao = DateTime.Now;
                    ctx.Ocorrencias.Add(ObjectToCreate);
                    ctx.SaveChanges();
                }

                return ObjectToCreate;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public static Ocorrencias Update(Ocorrencias ObjectToUpdate)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    ObjectToUpdate.DataModificacao = DateTime.Now;
                    ctx.Ocorrencias.Update(ObjectToUpdate);
                    ctx.SaveChanges();
                }

                return ObjectToUpdate;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public static bool Delete(Ocorrencias ObjectToDelete)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    ctx.Ocorrencias.Remove(ObjectToDelete);
                    ctx.SaveChanges();
                }

                return true;
            }
            catch (Exception e)
            {

                return false;
            }
        }

        public static Ocorrencias ParseToDB(OcorrenciasViewModel x)
        {
            Ocorrencias ocorrencia = new Ocorrencias()
            {
                CodOcorrencia = x.CodOcorrencia,
                CodEstado = x.CodEstado,
                CodFornecedor = x.CodFornecedor,
                NomeFornecedor = x.NomeFornecedor,
                CodEncomenda = x.CodEncomenda,
                CodRegiao = x.CodRegiao,
                CodAreaFuncional = x.CodAreaFuncional,
                CodCentroResponsabilidade = x.CodCentroResponsabilidade,
                //DataOcorrencia = x.DataOcorrencia,
                LocalEntrega = x.LocalEntrega,
                NoDocExterno = x.NoDocExterno,
                CodArtigo = x.CodArtigo,
                Descricao = x.Descricao,
                UnidMedida = x.UnidMedida,
                Quantidade = x.Quantidade,
                Motivo = x.Motivo,
                GrauGravidade = x.GrauGravidade,
                Observacao = x.Observacao,
                MedidaCorretiva = x.MedidaCorretiva,
                UtilizadorMedidaCorretiva = x.UtilizadorMedidaCorretiva,
                //DataMedidaCorretiva = x.DataMedidaCorretiva,
                UtilizadorCriacao = x.UtilizadorCriacao,
                //DataCriacao = x.DataCriacao,
                UtilizadorModificacao = x.UtilizadorModificacao,
                //DataModificacao = x.DataModificacao
            };

            if (!string.IsNullOrEmpty(x.DataOcorrenciaTexto)) ocorrencia.DataOcorrencia = Convert.ToDateTime(x.DataOcorrenciaTexto);
            if (!string.IsNullOrEmpty(x.DataMedidaCorretivaTexto)) ocorrencia.DataMedidaCorretiva = Convert.ToDateTime(x.DataMedidaCorretivaTexto);
            if (!string.IsNullOrEmpty(x.DataCriacaoTexto)) ocorrencia.DataCriacao = Convert.ToDateTime(x.DataCriacaoTexto);
            if (!string.IsNullOrEmpty(x.DataModificacaoTexto)) ocorrencia.DataModificacao = Convert.ToDateTime(x.DataModificacaoTexto);

            return ocorrencia;
        }

        public static List<Ocorrencias> ParseListToViewModel(List<OcorrenciasViewModel> x)
        {
            List<Ocorrencias> Ocorrencias = new List<Ocorrencias>();

            x.ForEach(y => Ocorrencias.Add(ParseToDB(y)));

            return Ocorrencias;
        }

        public static OcorrenciasViewModel ParseToViewModel(Ocorrencias x)
        {
            OcorrenciasViewModel ocorrencia = new OcorrenciasViewModel()
            {
                CodOcorrencia = x.CodOcorrencia,
                CodEstado = x.CodEstado,
                CodFornecedor = x.CodFornecedor,
                NomeFornecedor = x.NomeFornecedor,
                CodEncomenda = x.CodEncomenda,
                CodRegiao = x.CodRegiao,
                CodAreaFuncional = x.CodAreaFuncional,
                CodCentroResponsabilidade = x.CodCentroResponsabilidade,
                //DataOcorrencia = x.DataOcorrencia,
                LocalEntrega = x.LocalEntrega,
                NoDocExterno = x.NoDocExterno,
                CodArtigo = x.CodArtigo,
                Descricao = x.Descricao,
                UnidMedida = x.UnidMedida,
                Quantidade = x.Quantidade,
                Motivo = x.Motivo,
                GrauGravidade = x.GrauGravidade,
                Observacao = x.Observacao,
                MedidaCorretiva = x.MedidaCorretiva,
                UtilizadorMedidaCorretiva = x.UtilizadorMedidaCorretiva,
                //DataMedidaCorretiva = x.DataMedidaCorretiva,
                UtilizadorCriacao = x.UtilizadorCriacao,
                //DataCriacao = x.DataCriacao,
                UtilizadorModificacao = x.UtilizadorModificacao,
                //DataModificacao = x.DataModificacao
            };

            if (x.DataOcorrencia != null) ocorrencia.DataOcorrenciaTexto = x.DataOcorrencia.Value.ToString("yyyy-MM-dd");
            if (x.DataMedidaCorretiva != null) ocorrencia.DataMedidaCorretivaTexto = x.DataMedidaCorretiva.Value.ToString("yyyy-MM-dd");
            if (x.DataCriacao != null) ocorrencia.DataCriacaoTexto = x.DataCriacao.Value.ToString("yyyy-MM-dd");
            if (x.DataModificacao != null) ocorrencia.DataModificacaoTexto = x.DataModificacao.Value.ToString("yyyy-MM-dd");

            return ocorrencia;
        }

        public static List<OcorrenciasViewModel> ParseListToViewModel(List<Ocorrencias> x)
        {
            List<OcorrenciasViewModel> Ocorrencias = new List<OcorrenciasViewModel>();

            x.ForEach(y => Ocorrencias.Add(ParseToViewModel(y)));

            return Ocorrencias;
        }
    }
}