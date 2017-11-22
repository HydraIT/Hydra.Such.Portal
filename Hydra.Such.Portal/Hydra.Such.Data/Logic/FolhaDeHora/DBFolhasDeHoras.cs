﻿using Hydra.Such.Data.Database;
using Hydra.Such.Data.ViewModel.FH;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Hydra.Such.Data.Logic.FolhaDeHora
{
    public class DBFolhasDeHoras
    {
        #region CRUD
        public static FolhasDeHoras GetById(string NFolhaDeHora)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    return ctx.FolhasDeHoras.Where(x => x.NºFolhaDeHoras == NFolhaDeHora).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<FolhasDeHoras> GetAll()
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    return ctx.FolhasDeHoras.ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static FolhasDeHoras Create(FolhasDeHoras ObjectToCreate)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    ObjectToCreate.DataHoraCriação = DateTime.Now;
                    ctx.FolhasDeHoras.Add(ObjectToCreate);
                    ctx.SaveChanges();
                }

                return ObjectToCreate;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static FolhasDeHoras Update(FolhasDeHoras ObjectToUpdate)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    ObjectToUpdate.DataHoraModificação = DateTime.Now;
                    ctx.FolhasDeHoras.Update(ObjectToUpdate);
                    ctx.SaveChanges();
                }

                return ObjectToUpdate;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool Delete(string FolhaDeHoraNo)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    ctx.FolhasDeHoras.RemoveRange(ctx.FolhasDeHoras.Where(x => x.NºFolhaDeHoras == FolhaDeHoraNo));
                    ctx.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static List<FolhasDeHoras> GetAllByArea(int AreaId)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    return ctx.FolhasDeHoras.Where(x => x.Área == AreaId).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        #endregion

        public static List<FolhaDeHorasViewModel> GetAllByAreaToList(int AreaId)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    return ctx.FolhasDeHoras.Where(FH => FH.Área == AreaId).Select(FH => new FolhaDeHorasViewModel()
                    {
                        FolhaDeHorasNo = FH.NºFolhaDeHoras,
                        Area = FH.Área,
                        AreaTexto = FH.Área == null ? "" : FH.Área.ToString(),
                        ProjetoNo = FH.NºProjeto,
                        ProjetoDescricao = FH.ProjetoDescricao,
                        EmpregadoNo = FH.NºEmpregado,
                        EmpregadoNome = FH.NomeEmpregado,
                        DataHoraPartida = FH.DataHoraPartida,
                        DataPartidaTexto = FH.DataHoraPartida == null ? "" : FH.DataHoraPartida.Value.ToString("yyyy-MM-dd"),
                        HoraPartidaTexto = FH.DataHoraPartida == null ? "" : FH.DataHoraPartida.Value.ToString("HH:mm:ss"),
                        DataHoraChegada = FH.DataHoraChegada,
                        DataChegadaTexto = FH.DataHoraChegada == null ? "" : FH.DataHoraChegada.Value.ToString("yyyy-MM-dd"),
                        HoraChegadaTexto = FH.DataHoraChegada == null ? "" : FH.DataHoraChegada.Value.ToString("HH:mm:ss"),
                        TipoDeslocacao = FH.TipoDeslocação,
                        TipoDeslocacaoTexto = FH.TipoDeslocação == null ? "" : FH.TipoDeslocação == null ? "" : FH.TipoDeslocação.ToString(),
                        CodigoTipoKms = FH.CódigoTipoKmS,
                        Matricula = FH.Matrícula,
                        DeslocacaoForaConcelho = FH.DeslocaçãoForaConcelho,
                        DeslocacaoForaConcelhoTexto = FH.DeslocaçãoForaConcelho == null ? "" : FH.DeslocaçãoForaConcelho.ToString(),
                        DeslocacaoPlaneada = FH.DeslocaçãoPlaneada,
                        DeslocacaoPlaneadaTexto = FH.DeslocaçãoPlaneada == null ? "" : FH.DeslocaçãoPlaneada.ToString(),
                        Terminada = FH.Terminada,
                        TerminadaTexto = FH.Terminada == null ? "" : FH.Terminada.ToString(),
                        Estado = FH.Estado,
                        Estadotexto = FH.Estado == null ? "" : FH.Estado.ToString(),
                        CriadoPor = FH.CriadoPor,
                        DataHoraCriacao = FH.DataHoraCriação,
                        DataCriacaoTexto = FH.DataHoraCriação == null ? "" : FH.DataHoraCriação.Value.ToString("yyyy-MM-dd"),
                        HoraCriacaoTexto = FH.DataHoraCriação == null ? "" : FH.DataHoraCriação.Value.ToString("HH:mm:ss"),
                        CodigoRegiao = FH.CódigoRegião,
                        CodigoAreaFuncional = FH.CódigoÁreaFuncional,
                        CodigoCentroResponsabilidade = FH.CódigoCentroResponsabilidade,
                        TerminadoPor = FH.TerminadoPor,
                        DataHoraTerminado = FH.DataHoraTerminado,
                        DataTerminadoTexto = FH.DataHoraTerminado == null ? "" : FH.DataHoraTerminado.Value.ToString("yyyy-MM-dd"),
                        HoraTerminadoTexto = FH.DataHoraTerminado == null ? "" : FH.DataHoraTerminado.Value.ToString("HH:mm:ss"),
                        Validado = FH.Validado,
                        ValidadoTexto = FH.Validado == null ? "" : FH.Validado.ToString(),
                        Validadores = FH.Validadores == null ? "" : FH.Validadores,
                        Validador = FH.Validador,
                        DataHoraValidacao = FH.DataHoraValidação,
                        DataValidacaoTexto = FH.DataHoraValidação == null ? "" : FH.DataHoraValidação.Value.ToString("yyyy-MM-dd"),
                        HoraValidacaoTexto = FH.DataHoraValidação == null ? "" : FH.DataHoraValidação.Value.ToString("HH:mm:ss"),
                        IntegradorEmRH = FH.IntegradorEmRh,
                        IntegradoresEmRH = FH.IntegradoresEmRh,
                        DataIntegracaoEmRH = FH.DataIntegraçãoEmRh,
                        DataIntegracaoEmRHTexto = FH.DataIntegraçãoEmRh == null ? "" : FH.DataIntegraçãoEmRh.Value.ToString("yyyy-MM-dd"),
                        HoraIntegracaoEmRHTexto = FH.DataIntegraçãoEmRh == null ? "" : FH.DataIntegraçãoEmRh.Value.ToString("HH:mm:ss"),
                        IntegradorEmRHKM = FH.IntegradorEmRhKm,
                        IntegradoresEmRHKM = FH.IntegradoresEmRhkm,
                        DataIntegracaoEmRHKM = FH.DataIntegraçãoEmRhKm,
                        DataIntegracaoEmRHKMTexto = FH.DataIntegraçãoEmRhKm == null ? "" : FH.DataIntegraçãoEmRhKm.Value.ToString("yyyy-MM-dd"),
                        HoraIntegracaoEmRHKMTexto = FH.DataIntegraçãoEmRhKm == null ? "" : FH.DataIntegraçãoEmRhKm.Value.ToString("HH:mm:ss"),
                        CustoTotalAjudaCusto = Convert.ToDecimal(FH.CustoTotalAjudaCusto),
                        CustoTotalHoras = Convert.ToDecimal(FH.CustoTotalHoras),
                        CustoTotalKM = Convert.ToDecimal(FH.CustoTotalKm),
                        NumTotalKM = Convert.ToDecimal(FH.NumTotalKm),
                        Observacoes = FH.Observações,
                        Responsavel1No = FH.NºResponsável1,
                        Responsavel2No = FH.NºResponsável2,
                        Responsavel3No = FH.NºResponsável3,
                        ValidadoresRHKM = FH.ValidadoresRhKm,
                        DataHoraUltimoEstado = FH.DataHoraÚltimoEstado,
                        DataUltimoEstadoTexto = FH.DataHoraÚltimoEstado == null ? "" : FH.DataHoraÚltimoEstado.Value.ToString("yyyy-MM-dd"),
                        HoraUltimoEstadoTexto = FH.DataHoraÚltimoEstado == null ? "" : FH.DataHoraÚltimoEstado.Value.ToString("HH:mm:ss"),
                        UtilizadorModificacao = FH.UtilizadorModificação,
                        DataHoraModificacao = FH.DataHoraModificação,
                        DataModificacaoTexto = FH.DataHoraModificação == null ? "" : FH.DataHoraModificação.Value.ToString("yyyy-MM-dd"),
                        HoraModificacaoTexto = FH.DataHoraModificação == null ? "" : FH.DataHoraModificação.Value.ToString("HH:mm:ss")
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
