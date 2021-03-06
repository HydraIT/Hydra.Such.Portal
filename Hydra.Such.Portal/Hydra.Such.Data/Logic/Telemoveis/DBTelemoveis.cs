﻿using Hydra.Such.Data.Database;
using Hydra.Such.Data.ViewModel.Telemoveis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Hydra.Such.Data.Logic.Telemoveis
{
    public class DBTelemoveis
    {
        #region TELEMOVEIS EQUIPAMENTOS
        #region CRUD
        /// <summary>
        /// Lista de todos os registos (Telemóveis e placas de rede)
        /// </summary>
        /// <returns></returns>
        public static List<TelemoveisEquipamentos> GetAllTelemoveisEquipamentosToList()
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    return ctx.TelemoveisEquipamentos.ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }


        /// <summary>
        /// Lista de todos os registos por tipo, Telemóveis [0] ou placas de rede [1]
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public static List<TelemoveisEquipamentos> GetAllTelemoveisEquipamentosTypeToList(int tipo)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    return ctx.TelemoveisEquipamentos.Where(p => p.Tipo == tipo).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Devolve os dados de um registo da tabela Telemoveis_Equipamentos
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="imei"></param>
        /// <returns></returns>
        public static TelemoveisEquipamentos GetTelemoveisEquipamentos(int tipo, string imei)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    return ctx.TelemoveisEquipamentos.Where(p => p.Tipo == tipo).Where(p => p.Imei == imei).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Criação de registo
        /// </summary>
        /// <param name="ObjectToCreate"></param>
        /// <returns></returns>
        public static TelemoveisEquipamentos Create (TelemoveisEquipamentos ObjectToCreate)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    ObjectToCreate.DataHoraCriacao = DateTime.Now;
                    ctx.TelemoveisEquipamentos.Add(ObjectToCreate);
                    ctx.SaveChanges();
                }

                return ObjectToCreate;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Actualização de registo
        /// </summary>
        /// <param name="ObjectToUpdate"></param>
        /// <returns></returns>
        public static TelemoveisEquipamentos Update(TelemoveisEquipamentos ObjectToUpdate)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    ObjectToUpdate.DataHoraModificacao = DateTime.Now;
                    ctx.TelemoveisEquipamentos.Update(ObjectToUpdate);
                    ctx.SaveChanges();
                }

                return ObjectToUpdate;
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        /// <summary>
        /// Eliminação do registo
        /// </summary>
        /// <param name="ObjectToDelete"></param>
        /// <returns></returns>
        public static TelemoveisEquipamentos Delete(TelemoveisEquipamentos ObjectToDelete)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    ctx.TelemoveisEquipamentos.Remove(ObjectToDelete);
                    ctx.SaveChanges();
                }

                return ObjectToDelete;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        #endregion

        public static List<TelemoveisEquipamentosView> GetAllTelemoveisEquipamentosViewToList()
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    return ctx.TelemoveisEquipamentos.Select(TelemoveisEquipamentos => new TelemoveisEquipamentosView()
                    {
                        Tipo = TelemoveisEquipamentos.Tipo,
                        Imei = TelemoveisEquipamentos.Imei,
                        Marca = TelemoveisEquipamentos.Marca,
                        Modelo = TelemoveisEquipamentos.Modelo,
                        Estado = TelemoveisEquipamentos.Estado,
                        Cor = TelemoveisEquipamentos.Cor,
                        Observacoes = TelemoveisEquipamentos.Observacoes,
                        DataRecepcao = TelemoveisEquipamentos.DataRecepcao,
                        Documento = TelemoveisEquipamentos.Documento,
                        DocumentoRecepcao = TelemoveisEquipamentos.DocumentoRecepcao,
                        Utilizador = TelemoveisEquipamentos.Utilizador,
                        DataAlteracao = TelemoveisEquipamentos.DataAlteracao,
                        DevolvidoBk = TelemoveisEquipamentos.DevolvidoBk,
                        NumEmpregadoComprador = TelemoveisEquipamentos.NumEmpregadoComprador,
                        NomeComprador = TelemoveisEquipamentos.NomeComprador,
                        Devolvido = TelemoveisEquipamentos.Devolvido,
                        UtilizadorCriacao = TelemoveisEquipamentos.UtilizadorCriacao,
                        DataHoraCriacao = TelemoveisEquipamentos.DataHoraCriacao,
                        UtilizadorModificacao = TelemoveisEquipamentos.UtilizadorModificacao,
                        DataHoraModificacao = TelemoveisEquipamentos.DataHoraModificacao,
                        Tipo_Show = TelemoveisEquipamentos.Tipo == 0 ? "Equipamento" : "Placa de Rede",
                        Estado_Show = TelemoveisEquipamentos.Estado == 0 ? "Novo" : "Usado",
                        Devolvido_Show = TelemoveisEquipamentos.Devolvido == 0 ? "" : TelemoveisEquipamentos.Devolvido == 1 ? "Devolvido" : TelemoveisEquipamentos.Devolvido == 2 ? "Abate TMN" : TelemoveisEquipamentos.Devolvido == 3 ? "Vendido" : TelemoveisEquipamentos.Devolvido == 4 ? "Perdido" : TelemoveisEquipamentos.Devolvido == 5 ? "Roubado" : TelemoveisEquipamentos.Devolvido == 6 ? "Empréstimo" : TelemoveisEquipamentos.Devolvido == 7 ? "Não Devolvido" : "",
                        DataRecepcao_Show = TelemoveisEquipamentos.DataRecepcao == null ? "" : TelemoveisEquipamentos.DataRecepcao.Value.ToString("yyyy-MM-dd"),
                        DataAlteracao_Show = TelemoveisEquipamentos.DataAlteracao == null ? "" : TelemoveisEquipamentos.DataAlteracao.Value.ToString("yyyy-MM-dd")
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static TelemoveisEquipamentosView CastTelemoveisEquipamentosToView(TelemoveisEquipamentos ObjectToTransform)
        {
            TelemoveisCartoes telemoveisCartoes = new TelemoveisCartoes();
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    telemoveisCartoes = ctx.TelemoveisCartoes.Where(p => p.Imei == ObjectToTransform.Imei).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                
            }

            
            TelemoveisEquipamentosView view = new TelemoveisEquipamentosView()
            {
                Tipo = ObjectToTransform.Tipo,
                Imei = ObjectToTransform.Imei,
                Marca = ObjectToTransform.Marca,
                Modelo = ObjectToTransform.Modelo,
                Estado = ObjectToTransform.Estado,
                Cor = ObjectToTransform.Cor,
                Observacoes = ObjectToTransform.Observacoes,
                DataRecepcao = ObjectToTransform.DataRecepcao,
                Documento = ObjectToTransform.Documento,
                DocumentoRecepcao = ObjectToTransform.DocumentoRecepcao,
                Utilizador = ObjectToTransform.Utilizador,
                DataAlteracao = ObjectToTransform.DataAlteracao,
                DevolvidoBk = ObjectToTransform.DevolvidoBk,
                NumEmpregadoComprador = ObjectToTransform.NumEmpregadoComprador,
                NomeComprador = ObjectToTransform.NomeComprador,
                Devolvido = ObjectToTransform.Devolvido,
                UtilizadorCriacao = ObjectToTransform.UtilizadorCriacao,
                DataHoraCriacao = ObjectToTransform.DataHoraCriacao,
                UtilizadorModificacao = ObjectToTransform.UtilizadorModificacao,
                DataHoraModificacao = ObjectToTransform.DataHoraModificacao,
                Tipo_Show = ObjectToTransform.Tipo == 0 ? "Equipamento" : "Placa de Rede",
                Estado_Show = ObjectToTransform.Estado == 0 ? "Novo" : "Usado",
                Devolvido_Show = ObjectToTransform.Devolvido == 0 ? "" : ObjectToTransform.Devolvido == 1 ? "Devolvido" : ObjectToTransform.Devolvido == 2 ? "Abate TMN" : ObjectToTransform.Devolvido == 3 ? "Vendido" : ObjectToTransform.Devolvido == 4 ? "Perdido" : ObjectToTransform.Devolvido == 5 ? "Roubado" : ObjectToTransform.Devolvido == 6 ? "Empréstimo" : ObjectToTransform.Devolvido == 7 ? "Não Devolvido" : "",
                DataRecepcao_Show = ObjectToTransform.DataRecepcao == null ? "" : ObjectToTransform.DataRecepcao.Value.ToString("yyyy-MM-dd"),
                DataAlteracao_Show = ObjectToTransform.DataAlteracao == null ? "" : ObjectToTransform.DataAlteracao.Value.ToString("yyyy-MM-dd"),
                NomeUtilizadorCartao_Show = telemoveisCartoes != null ? telemoveisCartoes.Nome : string.Empty,
                DataAtribuicaoUtilizadorCartao_Show = telemoveisCartoes != null ? telemoveisCartoes.DataAtribuicao.Value.ToString("yyyy-MM-dd") : string.Empty
            };

            return view;
        }
        #endregion

        #region TELEMOVEIS CARTÕES
        #region CRUD
        /// <summary>
        /// Lista de todos os registos (Cartões)
        /// </summary>
        /// <returns></returns>
        public static List<TelemoveisCartoes> GetAllTelemoveisCartoesToList()
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    return ctx.TelemoveisCartoes.ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Devolve os dados de um registo da tabela Telemoveis_Cartoes
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="imei"></param>
        /// <returns></returns>
        public static TelemoveisCartoes GetTelemoveisCartoes(string numCartao)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    return ctx.TelemoveisCartoes.Where(p => p.NumCartao == numCartao).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Criação de registo
        /// </summary>
        /// <param name="ObjectToCreate"></param>
        /// <returns></returns>
        public static TelemoveisCartoes Create(TelemoveisCartoes ObjectToCreate)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    ObjectToCreate.DataEstado = DateTime.Now;
                    ctx.TelemoveisCartoes.Add(ObjectToCreate);
                    ctx.SaveChanges();
                }

                return ObjectToCreate;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Actualização de registo
        /// </summary>
        /// <param name="ObjectToUpdate"></param>
        /// <returns></returns>
        public static TelemoveisCartoes Update(TelemoveisCartoes ObjectToUpdate)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    ObjectToUpdate.DataAlteracao = DateTime.Now;
                    ctx.TelemoveisCartoes.Update(ObjectToUpdate);
                    ctx.SaveChanges();
                }

                return ObjectToUpdate;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        /// <summary>
        /// Eliminação do registo
        /// </summary>
        /// <param name="ObjectToDelete"></param>
        /// <returns></returns>
        public static TelemoveisCartoes Delete(TelemoveisCartoes ObjectToDelete)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    ctx.TelemoveisCartoes.Remove(ObjectToDelete);
                    ctx.SaveChanges();
                }

                return ObjectToDelete;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        public static TelemoveisCartoesView CastTelemoveisCartoesToView(TelemoveisCartoes ObjectToTransform)
        {
            decimal _Consumo = 0;
            string _Marca = string.Empty;
            List<TelemoveisMovimentosView> Objecto_Lista_Movimentos = new List<TelemoveisMovimentosView>();


            if (ObjectToTransform != null)
            {
                SuchDBContext _context = new SuchDBContext();
                try
                {
                    List<TelemoveisMovimentos> list = _context.TelemoveisMovimentos.Where(m => m.NumCartao == ObjectToTransform.NumCartao).ToList();

                    foreach (var mov in list)
                    {
                        _Consumo += mov.ValorComIva;
                        Objecto_Lista_Movimentos.Add(CastTelemoveisMovimentosToView(mov));
                    }

                    _Marca = _context.TelemoveisEquipamentos.Where(e => e.Imei == ObjectToTransform.Imei).FirstOrDefault().Marca.ToString();
                }
                catch
                {

                }
            }

            TelemoveisCartoesView view = new TelemoveisCartoesView()
            {
                NumCartao = ObjectToTransform.NumCartao,
                TipoServico = ObjectToTransform.TipoServico,
                ContaSuch = ObjectToTransform.ContaSuch,
                ContaUtilizador = ObjectToTransform.ContaUtilizador,
                Barramentos = ObjectToTransform.Barramentos,
                TarifarioVoz = ObjectToTransform.TarifarioVoz,
                TarifarioDados = ObjectToTransform.TarifarioDados,
                ExtensaoVpn = ObjectToTransform.ExtensaoVpn,
                PlafondFr = ObjectToTransform.PlafondFr,
                PlafondExtra = ObjectToTransform.PlafondExtra,
                FimFidelizacao = ObjectToTransform.FimFidelizacao,
                Gprs = ObjectToTransform.Gprs,
                Estado = ObjectToTransform.Estado,
                DataEstado = ObjectToTransform.DataEstado,
                Observacoes = ObjectToTransform.Observacoes,
                NumFuncionario = ObjectToTransform.NumFuncionario,
                Nome = ObjectToTransform.Nome,
                CodRegiao = ObjectToTransform.CodRegiao,
                CodAreaFuncional = ObjectToTransform.CodAreaFuncional,
                CodCentroResponsabilidade = ObjectToTransform.CodCentroResponsabilidade,
                Grupo = ObjectToTransform.Grupo,
                Imei = ObjectToTransform.Imei,
                DataAtribuicao = ObjectToTransform.DataAtribuicao,
                ChamadasInternacionais = ObjectToTransform.ChamadasInternacionais,
                Roaming = ObjectToTransform.Roaming,
                Internet = ObjectToTransform.Internet,
                Declaracao = ObjectToTransform.Declaracao,
                Utilizador = ObjectToTransform.Utilizador,
                DataAlteracao = ObjectToTransform.DataAlteracao,
                Plafond100percUtilizador = ObjectToTransform.Plafond100percUtilizador,
                WhiteList = ObjectToTransform.WhiteList,
                ValorMensalidadeDados = ObjectToTransform.ValorMensalidadeDados,
                PlafondDados = ObjectToTransform.PlafondDados,
                EquipamentoNaoDevolvido = ObjectToTransform.EquipamentoNaoDevolvido,

                TipoServico_Show = ObjectToTransform.TipoServico == 0 ? "Voz" : "Dados",
                Estado_Show = ObjectToTransform.Estado == 0 ? "Activo" : ObjectToTransform.Estado == 1 ? "Bloqueado" : ObjectToTransform.Estado == 2 ? "Cancelado" : ObjectToTransform.Estado == 3 ? "Alteração de Titular" : ObjectToTransform.Estado == 4 ? "Por Activar" : string.Empty,
                DataEstado_Show = ObjectToTransform.DataEstado.ToString("yyyy-MM-dd"),
                FimFidelizacao_Show = ObjectToTransform.FimFidelizacao == null ? "" : ObjectToTransform.FimFidelizacao.Value.ToString("yyyy-MM-dd"),
                Plafond100percUtilizador_Show = ObjectToTransform.Plafond100percUtilizador == 0 ? false : true,
                ChamadasInternacionais_Show = ObjectToTransform.ChamadasInternacionais == 0 ? false : true,
                Roaming_Show = ObjectToTransform.Roaming == 0 ? false : true,
                Consumos_Show = _Consumo.ToString(),
                Consumos = _Consumo,
                DataAtribuicao_Show = ObjectToTransform.DataAtribuicao == null ? "" : ObjectToTransform.DataAtribuicao.Value.ToString("yyyy-MM-dd"),
                EquipamentoNaoDevolvido_Show = ObjectToTransform.EquipamentoNaoDevolvido == 0 ? false : true,
                Marca_Show = _Marca,
                DataAlteracao_Show = ObjectToTransform.DataAlteracao == null ? "" : ObjectToTransform.DataAlteracao.Value.ToString("yyyy-MM-dd"),
                TelemoveisMovimentos_List = Objecto_Lista_Movimentos
            };

            return view;
        }

        #endregion

        #region TELEMOVEIS MOVIMENTOS
        #region CRUD
        /// <summary>
        /// Lista de todos os registos (Movimentos de um cartão)
        /// </summary>
        /// <param name="numCartao"></param>
        /// <returns></returns>
        public static List<TelemoveisMovimentos> GetTelemoveisMovimentos(string numCartao)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    return ctx.TelemoveisMovimentos.Where(p => p.NumCartao == numCartao).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion


        public static TelemoveisMovimentosView CastTelemoveisMovimentosToView(TelemoveisMovimentos ObjectToTransform)
        {
            TelemoveisMovimentosView view = new TelemoveisMovimentosView()
            {
                NumMovimento = ObjectToTransform.NumMovimento,
                NumCartao = ObjectToTransform.NumCartao,
                Data = ObjectToTransform.Data,
                NumFactura = ObjectToTransform.NumFactura,
                ValorComIva = ObjectToTransform.ValorComIva,
                DataFactura = ObjectToTransform.DataFactura,
                CodRegiao = ObjectToTransform.CodRegiao,
                CodAreaFuncional = ObjectToTransform.CodAreaFuncional,
                CodCentroResponsabilidade = ObjectToTransform.CodCentroResponsabilidade,
                Utilizador = ObjectToTransform.Utilizador,
                DataRegisto = ObjectToTransform.DataRegisto,
                NumDocumento = ObjectToTransform.NumDocumento,
                Valor = ObjectToTransform.Valor,
                DataFactura_Show = ObjectToTransform.DataFactura == null ? "" : ObjectToTransform.DataFactura.ToString("yyyy-MM-dd"),
                DataRegisto_Show = ObjectToTransform.DataRegisto == null ? "" : ObjectToTransform.DataRegisto.ToString("yyyy-MM-dd")
            };

            return view;
        }

        #endregion
    }
}
