﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Hydra.Such.Data.Database;
using Hydra.Such.Data.Logic;
using Hydra.Such.Data.Logic.ComprasML;
using Hydra.Such.Data.Logic.PedidoCotacao;
using Hydra.Such.Data.Logic.Request;
using Hydra.Such.Data.NAV;
using Hydra.Such.Data.ViewModel;
using Hydra.Such.Data.ViewModel.Compras;
using Hydra.Such.Data.ViewModel.PedidoCotacao;
using Hydra.Such.Portal.Configurations;
using Hydra.Such.Portal.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using static Hydra.Such.Data.Enumerations;


namespace Hydra.Such.Portal.Controllers
{
    public class ConsultaMercadoController : Controller
    {
        private readonly NAVConfigurations _config;
        private readonly NAVWSConfigurations configws;
        private readonly GeneralConfigurations _generalConfig;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ConsultaMercadoController(IOptions<NAVConfigurations> appSettings, IOptions<NAVWSConfigurations> NAVWSConfigs, IOptions<GeneralConfigurations> appSettingsGeneral, IHostingEnvironment _hostingEnvironment)
        {
            _config = appSettings.Value;
            configws = NAVWSConfigs.Value;
            _generalConfig = appSettingsGeneral.Value;
            this._hostingEnvironment = _hostingEnvironment;
            
        }

        public IActionResult ConsultaMercado()
        {
            UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Features.PedidoCotacao);
            ConfigUtilizadores CUser = DBUserConfigurations.GetById(User.Identity.Name);

            if (CUser != null && CUser.CMHistoricoToActivo.HasValue && CUser.CMHistoricoToActivo == true)
                ViewBag.CMHistoricoToActivo = 1;
            else
                ViewBag.CMHistoricoToActivo = 0;

            if (UPerm != null && UPerm.Read.Value)
            {
                ViewBag.UploadURL = _generalConfig.FileUploadFolder;
                ViewBag.UPermissions = UPerm;
                return View();
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        public IActionResult ConsultasPorFornecedor()
        {
            UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Features.PedidoCotacao);

            if (UPerm != null && UPerm.Read.Value)
            {
                ViewBag.UploadURL = _generalConfig.FileUploadFolder;
                ViewBag.UPermissions = UPerm;
                return View();
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        [HttpPost]
        public JsonResult GetAllConsultaMercado()
        {
            List<ConsultaMercado> result = DBConsultaMercado.GetAllConsultaMercadoToList();
            List<ConsultaMercadoView> list = new List<ConsultaMercadoView>();

            foreach (ConsultaMercado cm in result)
            {
                list.Add(DBConsultaMercado.CastConsultaMercadoToView(cm));
            }

            //return Json(result);
            return Json(list.OrderByDescending(x => x.NumConsultaMercado));
        }


        [HttpPost]
        public JsonResult GetAllConsultaMercado_Historico([FromBody] JObject requestParams)
        {
            int Historic = 0;
            if (requestParams["Historic"] != null)
                Historic = int.Parse(requestParams["Historic"].ToString());

            List<ConsultaMercado> result = DBConsultaMercado.GetAllConsultaMercado_Historico_ToList(Historic);

            //List<ConsultaMercadoView> list = new List<ConsultaMercadoView>();
            //foreach (ConsultaMercado cm in result)
            //{
            //    list.Add(DBConsultaMercado.CastConsultaMercadoToView(cm));
            //}
            //return Json(list.OrderByDescending(x => x.NumConsultaMercado));

            return Json(result.OrderByDescending(x => x.NumConsultaMercado));
        }

        [HttpPost]
        public JsonResult GetAllConsultasPorFornecedor([FromBody] JObject requestParams)
        {
            try
            {
                bool Historic = false;
                string HistoricPT = "Não";
                if (requestParams["Historic"] != null && requestParams["Historic"].ToString() != "0")
                {
                    Historic = true;
                    HistoricPT = "Sim";
                }

                string FiltroFornecedor = "";
                if (requestParams["FiltroFornecedor"] != null && requestParams["FiltroFornecedor"].ToString() != "")
                    FiltroFornecedor = requestParams["FiltroFornecedor"].ToString();

                int FiltroAno = 1900;
                if (requestParams["FiltroAno"] != null && requestParams["FiltroAno"].ToString() != "")
                    FiltroAno = (int)requestParams["FiltroAno"];

                List<SeleccaoEntidadesView> result = new List<SeleccaoEntidadesView>();

                result = DBSeleccaoEntidades.GetConsultasPorFornecedor(FiltroAno, FiltroFornecedor, Historic);

                //List<SeleccaoEntidades> List = new List<SeleccaoEntidades>();
                //List<ConsultaMercado> AllConsultaMercado = new List<ConsultaMercado>();
                //List<LinhasConsultaMercado> AllLinhasConsultaMercado = new List<LinhasConsultaMercado>();
                //List<RegistoDePropostas> AllRegistoDePropostas = new List<RegistoDePropostas>();

                //using (var ctx = new SuchDBContext())
                //{
                //List = ctx.SeleccaoEntidades.Where(x => ctx.ConsultaMercado.Where(y => y.NumConsultaMercado == x.NumConsultaMercado).FirstOrDefault().Historico == Historic).ToList();

                //AllConsultaMercado = ctx.ConsultaMercado.ToList();
                //AllLinhasConsultaMercado = ctx.LinhasConsultaMercado.ToList();
                //AllRegistoDePropostas = ctx.RegistoDePropostas.ToList();
                //}

                //if (!string.IsNullOrEmpty(FiltroFornecedor))
                //    result.RemoveAll(x => x.CodFornecedor != FiltroFornecedor);

                //if (FiltroAno != 1900)
                //    result.RemoveAll(x => Convert.ToDateTime(x.DataPedidoEsclarecimento).Year != FiltroAno);

                //List.ForEach(y => result.Add(CastSeleccaoEntidadesToView(y)));

                //using (var ctx = new SuchDBContext())
                //{
                //    List = ctx.SeleccaoEntidades.Where(x => ctx.ConsultaMercado.Where(y => y.NumConsultaMercado == x.NumConsultaMercado).FirstOrDefault().Historico == Historic).ToList();

                //    AllConsultaMercado = ctx.ConsultaMercado.ToList();
                //    //AllLinhasConsultaMercado = ctx.LinhasConsultaMercado.ToList();
                //    AllRegistoDePropostas = ctx.RegistoDePropostas.ToList();
                //}

                //if (!string.IsNullOrEmpty(FiltroFornecedor))
                //    List.RemoveAll(x => x.CodFornecedor != FiltroFornecedor);

                //if (FiltroAno != 1900)
                //    List.RemoveAll(x => Convert.ToDateTime(x.DataPedidoEsclarecimento).Year != FiltroAno);

                //List.ForEach(y => result.Add(CastSeleccaoEntidadesToView(y)));

                result.ForEach(selecao =>
                {
                    selecao.DataEnvioAoFornecedor_Show = selecao.DataEnvioAoFornecedor == Convert.ToDateTime("1900-01-01") ? "" : selecao.DataEnvioAoFornecedor.Value.ToString("yyyy-MM-dd");
                    selecao.DataRecepcaoProposta_Show = selecao.DataRecepcaoProposta == Convert.ToDateTime("1900-01-01") ? "" : selecao.DataRecepcaoProposta.Value.ToString("yyyy-MM-dd");
                    selecao.DataRespostaEsperada_Show = selecao.DataRespostaEsperada == Convert.ToDateTime("1900-01-01") ? "" : selecao.DataRespostaEsperada.Value.ToString("yyyy-MM-dd");
                    selecao.DataPedidoEsclarecimento_Show = selecao.DataPedidoEsclarecimento == Convert.ToDateTime("1900-01-01") ? "" : selecao.DataPedidoEsclarecimento.Value.ToString("yyyy-MM-dd");
                    selecao.DataRespostaEsclarecimento_Show = selecao.DataRespostaEsclarecimento == Convert.ToDateTime("1900-01-01") ? "" : selecao.DataRespostaEsclarecimento.Value.ToString("yyyy-MM-dd");
                    selecao.DataRespostaDoFornecedor_Show = selecao.DataRespostaDoFornecedor == Convert.ToDateTime("1900-01-01") ? "" : selecao.DataRespostaDoFornecedor.Value.ToString("yyyy-MM-dd");
                    selecao.DataEnvioPropostaArea_Show = selecao.DataEnvioPropostaArea == Convert.ToDateTime("1900-01-01") ? "" : selecao.DataEnvioPropostaArea.Value.ToString("yyyy-MM-dd");
                    selecao.DataRespostaArea_Show = selecao.DataRespostaArea == Convert.ToDateTime("1900-01-01") ? "" : selecao.DataRespostaArea.Value.ToString("yyyy-MM-dd");
                    selecao.DataPedidoCotacaoCriadoEm_Show = selecao.PedidoCotacaoCriadoEm == Convert.ToDateTime("1900-01-01") ? "" : selecao.PedidoCotacaoCriadoEm.Value.ToString("yyyy-MM-dd");
                    selecao.Historico_Show = selecao.Historico == false ? "Não" : "Sim";
                    selecao.NaoRespostaDoFornecedor_Show = selecao.NaoRespostaDoFornecedor == true ? "Sim" : "Não";

                    //RegistoDePropostas REG = AllRegistoDePropostas.Where(x => x.NumConsultaMercado == selecao.NumConsultaMercado).FirstOrDefault();
                    //if (REG != null)
                    //{
                    //    if (REG.Fornecedor1Code == selecao.CodFornecedor && REG.Fornecedor1Select == true)
                    //        selecao.NotaEncomenda_Show = "Sim";
                    //    if (REG.Fornecedor2Code == selecao.CodFornecedor && REG.Fornecedor2Select == true)
                    //        selecao.NotaEncomenda_Show = "Sim";
                    //    if (REG.Fornecedor3Code == selecao.CodFornecedor && REG.Fornecedor3Select == true)
                    //        selecao.NotaEncomenda_Show = "Sim";
                    //    if (REG.Fornecedor4Code == selecao.CodFornecedor && REG.Fornecedor4Select == true)
                    //        selecao.NotaEncomenda_Show = "Sim";
                    //    if (REG.Fornecedor5Code == selecao.CodFornecedor && REG.Fornecedor5Select == true)
                    //        selecao.NotaEncomenda_Show = "Sim";
                    //    if (REG.Fornecedor6Code == selecao.CodFornecedor && REG.Fornecedor6Select == true)
                    //        selecao.NotaEncomenda_Show = "Sim";
                    //}

                    //selecao.CustoTotalPrevisto = (decimal)AllLinhasConsultaMercado.Where(x => x.NumConsultaMercado == selecao.NumConsultaMercado).Sum(x => x.CustoTotalPrevisto);

                    //ConsultaMercado consulta = AllConsultaMercado.Where(x => x.NumConsultaMercado == selecao.NumConsultaMercado).FirstOrDefault();
                    //if (consulta != null)
                    //{
                    //    selecao.NoRequisicao = consulta.NumRequisicao;
                    //    selecao.CodRegiao = consulta.CodRegiao;
                    //    selecao.CodArea = consulta.CodAreaFuncional;
                    //    selecao.CodCresp = consulta.CodCentroResponsabilidade;
                    //    selecao.DataPedidoCotacaoCriadoEm_Show = consulta.PedidoCotacaoCriadoEm.Value.ToString("yyyy-MM-dd");
                    //}
                    //else
                    //{
                    //    selecao.NoRequisicao = "";
                    //    selecao.CodRegiao = "";
                    //    selecao.CodArea = "";
                    //    selecao.CodCresp = "";
                    //    selecao.DataPedidoCotacaoCriadoEm_Show = "";
                    //}
                });

                return Json(result.OrderByDescending(x => x.NumConsultaMercado));
            }
            catch (Exception e)
            {
                return Json(null);
            }
        }

        public static SeleccaoEntidadesView CastSeleccaoEntidadesToView(SeleccaoEntidades ObjectToTransform)
        {
            SeleccaoEntidadesView selecao = new SeleccaoEntidadesView()
            {
                IdSeleccaoEntidades = ObjectToTransform.IdSeleccaoEntidades,
                NumConsultaMercado = ObjectToTransform.NumConsultaMercado,
                CodFornecedor = ObjectToTransform.CodFornecedor,
                NomeFornecedor = ObjectToTransform.NomeFornecedor,
                CodActividade = ObjectToTransform.CodActividade,
                CidadeFornecedor = ObjectToTransform.CidadeFornecedor,
                CodTermosPagamento = ObjectToTransform.CodTermosPagamento,
                CodFormaPagamento = ObjectToTransform.CodFormaPagamento,
                Preferencial = ObjectToTransform.Preferencial,
                Selecionado = ObjectToTransform.Selecionado,
                EmailFornecedor = ObjectToTransform.EmailFornecedor,
                DataEnvioAoFornecedor = ObjectToTransform.DataEnvioAoFornecedor,
                DataRecepcaoProposta = ObjectToTransform.DataRecepcaoProposta,
                UtilizadorEnvio = ObjectToTransform.UtilizadorEnvio,
                UtilizadorRecepcaoProposta = ObjectToTransform.UtilizadorRecepcaoProposta,
                Fase = ObjectToTransform.Fase == null ? 0 : (int)ObjectToTransform.Fase,
                PrazoResposta = ObjectToTransform.PrazoResposta,
                DataRespostaEsperada = ObjectToTransform.DataRespostaEsperada,
                DataPedidoEsclarecimento = ObjectToTransform.DataPedidoEsclarecimento,
                DataRespostaEsclarecimento = ObjectToTransform.DataRespostaEsclarecimento,
                DataRespostaDoFornecedor = ObjectToTransform.DataRespostaDoFornecedor,
                NaoRespostaDoFornecedor = ObjectToTransform.NaoRespostaDoFornecedor,
                DataEnvioPropostaArea = ObjectToTransform.DataEnvioPropostaArea,
                DataRespostaArea = ObjectToTransform.DataRespostaArea,
                DataEnvioAoFornecedor_Show = ObjectToTransform.DataEnvioAoFornecedor == null ? "" : ObjectToTransform.DataEnvioAoFornecedor.Value.ToString("yyyy-MM-dd"),
                DataRecepcaoProposta_Show = ObjectToTransform.DataRecepcaoProposta == null ? "" : ObjectToTransform.DataRecepcaoProposta.Value.ToString("yyyy-MM-dd"),
                DataRespostaEsperada_Show = ObjectToTransform.DataRespostaEsperada == null ? "" : ObjectToTransform.DataRespostaEsperada.Value.ToString("yyyy-MM-dd"),
                DataPedidoEsclarecimento_Show = ObjectToTransform.DataPedidoEsclarecimento == null ? "" : ObjectToTransform.DataPedidoEsclarecimento.Value.ToString("yyyy-MM-dd"),
                DataRespostaEsclarecimento_Show = ObjectToTransform.DataRespostaEsclarecimento == null ? "" : ObjectToTransform.DataRespostaEsclarecimento.Value.ToString("yyyy-MM-dd"),
                DataRespostaDoFornecedor_Show = ObjectToTransform.DataRespostaDoFornecedor == null ? "" : ObjectToTransform.DataRespostaDoFornecedor.Value.ToString("yyyy-MM-dd"),
                DataEnvioPropostaArea_Show = ObjectToTransform.DataEnvioPropostaArea == null ? "" : ObjectToTransform.DataEnvioPropostaArea.Value.ToString("yyyy-MM-dd"),
                DataRespostaArea_Show = ObjectToTransform.DataRespostaArea == null ? "" : ObjectToTransform.DataRespostaArea.Value.ToString("yyyy-MM-dd"),

                Selecionado_Show = ObjectToTransform.Selecionado.HasValue ? ObjectToTransform.Selecionado == true ? "Sim" : "Não" : "Não",
                Preferencial_Show = ObjectToTransform.Preferencial.HasValue ? ObjectToTransform.Preferencial == true ? "Sim" : "Não" : "Não",
                NaoRespostaDoFornecedor_Show = ObjectToTransform.NaoRespostaDoFornecedor.HasValue ? ObjectToTransform.NaoRespostaDoFornecedor == true ? "Sim" : "Não" : "Não",

            };

            return selecao;
        }

        public IActionResult DetalheConsultaMercado(string id, bool isHistoric = false)
        {
            UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Features.PedidoCotacao);
            ViewBag.reportServerURL = _config.ReportServerURL;

            if (isHistoric == true)
            {
                ViewBag.Historic = "(Histórico) ";
                ViewBag.ifHistoric = true;
            }
            else
            {
                ViewBag.Historic = "";
                ViewBag.ifHistoric = false;
            }

            if (UPerm != null && UPerm.Read.Value)
            {
                ViewBag.UPermissions = UPerm;
                ViewBag.No = id ?? "";
                return View();
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error");
            }
        }


        [HttpPost]
        public JsonResult GetDetalheConsultaMercado([FromBody] ConsultaMercadoView data)
        {
            if (data != null)
            {
                ConsultaMercado consultaMercado = DBConsultaMercado.GetDetalheConsultaMercado(data.NumConsultaMercado);

                if (consultaMercado != null)
                {
                    ConsultaMercadoView result = DBConsultaMercado.CastConsultaMercadoToView(consultaMercado);

                    return Json(result);
                }

                return Json(new ConsultaMercadoView());
            }
            return Json(false);
        }


        [HttpPost]
        public JsonResult CreateConsultaMercado()
        {
            ConsultaMercado consultaMercado = DBConsultaMercado.Create(User.Identity.Name);
            
            if (consultaMercado == null || consultaMercado.NumConsultaMercado == "")
                return Json("");

            return Json(consultaMercado.NumConsultaMercado);

        }


        [HttpPost]
        public JsonResult UpdateConsultaMercado([FromBody] ConsultaMercadoView data)
        {
            ErrorHandler result = new ErrorHandler();
            try
            {
                if (data != null)
                {
                    data.UtilizadorModificacao = User.Identity.Name;
                    ConsultaMercado consultaMercado = DBConsultaMercado.Update(data);
                    if (consultaMercado == null)
                    {
                        result.eReasonCode = 1;
                        result.eMessage = "Ocorreu um erro";
                        return Json(result);
                    }

                    result.eReasonCode = 0;
                    result.eMessage = "Sucesso";
                    return Json(result);
                }
                else
                {
                    result.eReasonCode = -1;
                    result.eMessage = "Sem dados";
                    return Json(result);
                }
            }
            catch (Exception e)
            {
                result.eReasonCode = 1;
                result.eMessage = "Ocorreu um erro";
                return Json(result);
            }
        }

        [HttpPost]
        public JsonResult DeleteConsultaMercado([FromBody] ConsultaMercadoView data)
        {
            ErrorHandler result = new ErrorHandler();
            if (data != null)
            {
                var ctx = new SuchDBContext();

                //ConsultaMercado consultaMercado = ctx.ConsultaMercado.Where(p => p.NumConsultaMercado == data.NumConsultaMercado).FirstOrDefault();

                List<LinhasConsultaMercado> linhasConsultaMercados = ctx.LinhasConsultaMercado.Where(p => p.NumConsultaMercado == data.NumConsultaMercado).ToList();

                if (DBConsultaMercado.Delete(data.NumConsultaMercado))
                {
                    foreach (LinhasConsultaMercado linhas in linhasConsultaMercados)
                    {
                        LinhasRequisição linhasRequisição = ctx.LinhasRequisição.Where(x => x.NºLinha == linhas.LinhaRequisicao).FirstOrDefault();
                        linhasRequisição.NºDeConsultaMercadoCriada = string.Empty;
                        linhasRequisição.CriarConsultaMercado = false;
                        ctx.LinhasRequisição.Update(linhasRequisição);
                        ctx.SaveChanges();
                    }

                    result = new ErrorHandler()
                    {
                        eReasonCode = 0,
                        eMessage = "Consulta ao Mercado removida com sucesso"
                    };
                }
                else
                {
                    result = new ErrorHandler()
                    {
                        eReasonCode = 4,
                        eMessage = "Não foi possível remover a Consulta ao Mercado"
                    };
                }
                return Json(result);
            }
            else
            {
                result = new ErrorHandler()
                {
                    eReasonCode = -1,
                    eMessage = "Sem dados"
                };
                
                return Json(result);
            }
        }

        [HttpPost]
        public JsonResult HistoricoToAtivo([FromBody] ConsultaMercado data)
        {
            ErrorHandler result = new ErrorHandler();
            result.eReasonCode = 99;
            result.eMessage = "Ocorreu um erro.";

            if (data != null)
            {
                ConfigUtilizadores CUser = DBUserConfigurations.GetById(User.Identity.Name);

                if (CUser != null && CUser.CMHistoricoToActivo.HasValue && CUser.CMHistoricoToActivo == true)
                {
                    if (!string.IsNullOrEmpty(data.NumConsultaMercado))
                    {
                        ConsultaMercado CM = DBConsultaMercado.GetDetalheConsultaMercado(data.NumConsultaMercado);

                        if (CM != null)
                        {
                            CM.UserHistoricoToAtivo = User.Identity.Name;
                            CM.Historico = false;
                            CM.UtilizadorModificacao = User.Identity.Name;

                            if (DBConsultaMercado.Update(CM) != null)
                            {
                                result.eReasonCode = 1;
                                result.eMessage = "A Consulta de Mercado foi reativada com sucesso.";
                            }
                            else
                            {
                                result.eReasonCode = 2;
                                result.eMessage = "Ocorreu um erro na reativação da Consuta de Mercado.";
                            }
                        }
                        else
                        {
                            result.eReasonCode = 3;
                            result.eMessage = "Não foi possivel obter a Consulta de Mercado.";
                        }
                    }
                    else
                    {
                        result.eReasonCode = 4;
                        result.eMessage = "Não foi possivel obter o Número da Consulta de Mercado";
                    }
                }
                else
                {
                    result.eReasonCode = 5;
                    result.eMessage = "Não possui permissões para Reativar a Consulta de Mercado.";
                }
            }
            else
            {
                result.eReasonCode = 6;
                result.eMessage = "Não foi possivel obter os dados da Consulta de Mercado.";
            }

            return Json(result);
        }

        [HttpPost]
        public JsonResult CopiarConsultaMercado([FromBody] ConsultaMercadoView data)
        {
            if (data != null)
            {
                //ConsultaMercado consultaMercado = DBConsultaMercado.GetDetalheConsultaMercado(data.NumConsultaMercado);
                ConsultaMercado consultaMercado = DBConsultaMercado.Create(User.Identity.Name);

                consultaMercado.CodProjecto = data.CodProjecto;
                consultaMercado.Descricao = data.Descricao;
                consultaMercado.CodRegiao = data.CodRegiao;
                consultaMercado.CodAreaFuncional = data.CodAreaFuncional;
                consultaMercado.CodCentroResponsabilidade = data.CodCentroResponsabilidade;
                consultaMercado.CodActividade = data.CodActividade;
                consultaMercado.DataPedidoCotacao = data.DataPedidoCotacao;
                consultaMercado.FornecedorSelecionado = data.FornecedorSelecionado;
                consultaMercado.NumDocumentoCompra = data.NumDocumentoCompra;
                consultaMercado.CodLocalizacao = data.CodLocalizacao;
                consultaMercado.FiltroActividade = data.FiltroActividade;
                consultaMercado.ValorPedidoCotacao = data.ValorPedidoCotacao;
                consultaMercado.Destino = data.Destino;
                consultaMercado.Estado = data.Estado;
                consultaMercado.UtilizadorRequisicao = data.UtilizadorRequisicao;
                consultaMercado.DataLimite = data.DataLimite;
                consultaMercado.EspecificacaoTecnica = data.EspecificacaoTecnica;
                consultaMercado.Fase = data.Fase;
                consultaMercado.Modalidade = data.Modalidade;
                //consultaMercado.PedidoCotacaoCriadoEm = data.PedidoCotacaoCriadoEm;
                //consultaMercado.PedidoCotacaoCriadoPor = data.PedidoCotacaoCriadoPor;
                consultaMercado.ConsultaEm = data.ConsultaEm;
                consultaMercado.ConsultaPor = data.ConsultaPor;
                consultaMercado.NegociacaoContratacaoEm = data.NegociacaoContratacaoEm;
                consultaMercado.NegociacaoContratacaoPor = data.NegociacaoContratacaoPor;
                consultaMercado.AdjudicacaoEm = data.AdjudicacaoEm;
                consultaMercado.AdjudicacaoPor = data.AdjudicacaoPor;
                consultaMercado.NumRequisicao = data.NumRequisicao;
                consultaMercado.PedidoCotacaoOrigem = data.NumConsultaMercado;
                consultaMercado.ValorAdjudicado = data.ValorAdjudicado;
                consultaMercado.CodFormaPagamento = data.CodFormaPagamento;
                consultaMercado.SeleccaoEfectuada = data.SeleccaoEfectuada;
                consultaMercado.NumEncomenda = data.NumEncomenda;
                consultaMercado.EmailEnviado = data.EmailEnviado;
                consultaMercado.RegiaoMercadoLocal = data.RegiaoMercadoLocal;
                consultaMercado.DataEntregaFornecedor = data.DataEntregaFornecedor;
                consultaMercado.DataRecolha = data.DataRecolha;
                consultaMercado.DataEntregaArmazem = data.DataEntregaArmazem;
                consultaMercado.CodComprador = data.CodComprador;
                consultaMercado.LocalEntrega = data.LocalEntrega;
                consultaMercado.Equipamento = data.Equipamento;
                consultaMercado.Amostra = data.Amostra;
                consultaMercado.Urgente = data.Urgente;
                consultaMercado.Historico = data.Historico;
                consultaMercado.Obs = data.Obs;
                consultaMercado.UtilizadorModificacao = User.Identity.Name;

                consultaMercado = DBConsultaMercado.Update(consultaMercado);

                if (data.LinhasConsultaMercado != null)
                {
                    foreach (LinhasConsultaMercadoView cmv in data.LinhasConsultaMercado)
                    {
                        DBConsultaMercado.Create_Copia(cmv, consultaMercado.NumConsultaMercado, User.Identity.Name);
                    }
                }

                if (data.CondicoesPropostasFornecedores != null)
                {
                    foreach (CondicoesPropostasFornecedoresView cpfv in data.CondicoesPropostasFornecedores)
                    {
                        DBConsultaMercado.Create_Copia(cpfv, consultaMercado.NumConsultaMercado, User.Identity.Name);
                    }
                }


                if (data.LinhasCondicoesPropostasFornecedores != null)
                {
                    foreach (LinhasCondicoesPropostasFornecedoresView lcpfv in data.LinhasCondicoesPropostasFornecedores)
                    {
                        DBConsultaMercado.Create_Copia(lcpfv, consultaMercado.NumConsultaMercado, User.Identity.Name);
                    }
                }

                if (data.SeleccaoEntidades != null)
                {
                    foreach (SeleccaoEntidadesView sev in data.SeleccaoEntidades)
                    {
                        DBConsultaMercado.Create_Copia(sev, consultaMercado.NumConsultaMercado, User.Identity.Name);
                    }
                }

                ConsultaMercadoView result = DBConsultaMercado.CastConsultaMercadoToView(consultaMercado);
                result.eReasonCode = 0;
                result.eMessage = "Consulta de Mercado copiada com sucesso!";

                return Json(result);
            }

            data.eReasonCode = -1;
            data.eMessage = "Por uma razão desconhecida, não foi efectuada qualquer cópia";
            return Json(data);
        }


        [HttpPost]
        public JsonResult FecharPedido([FromBody] ConsultaMercadoView data)
        {
            if (data != null)
            {
                ConsultaMercado consultaMercado = DBConsultaMercado.GetDetalheConsultaMercado(data.NumConsultaMercado);

                if (consultaMercado != null)
                {
                    List<EnumData> Fases = EnumerablesFixed.Fase;
                    List<EnumData> Estados = EnumerablesFixed.Estado;

                    consultaMercado.Estado = Estados[1].Id;
                    consultaMercado.Fase = Fases[4].Id;

                    consultaMercado.Historico = true;
                    consultaMercado.UserToHistorico = User.Identity.Name;
                    consultaMercado.UtilizadorModificacao = User.Identity.Name;
                    consultaMercado = DBConsultaMercado.Update(consultaMercado);

                    ////Criar uma versão no histórico, com versão incrementada em 1
                    //HistoricoConsultaMercado historicoconsultaMercado = DBConsultaMercado.Create(consultaMercado);

                    //if (historicoconsultaMercado != null || historicoconsultaMercado.NumConsultaMercado != "")
                    //{
                    //    int _numversao = historicoconsultaMercado.NumVersao;

                    //    //Histórico Linhas Consulta Mercado
                    //    foreach (LinhasConsultaMercado lin in consultaMercado.LinhasConsultaMercado)
                    //    {
                    //        DBConsultaMercado.Create_Hist(lin, _numversao);
                    //    }

                    //    //Histórico Condições Propostas Fornecedores
                    //    foreach (CondicoesPropostasFornecedores lin in consultaMercado.CondicoesPropostasFornecedores)
                    //    {
                    //        DBConsultaMercado.Create_Hist(lin, _numversao);
                    //    }

                    //    //Histórico Linhas Condições Propostas Fornecedores
                    //    foreach (LinhasCondicoesPropostasFornecedores lin in consultaMercado.LinhasCondicoesPropostasFornecedores)
                    //    {
                    //        DBConsultaMercado.Create_Hist(lin, _numversao);
                    //    }

                    //    //Histórico Selecção Entidades
                    //    foreach (SeleccaoEntidades lin in consultaMercado.SeleccaoEntidades)
                    //    {
                    //        DBConsultaMercado.Create_Hist(lin, _numversao);
                    //    }
                    //}

                    ConsultaMercadoView result = DBConsultaMercado.CastConsultaMercadoToView(consultaMercado);
                    result.eReasonCode = 0;
                    result.eMessage = "Pedido Fechado com sucesso!";

                    return Json(result);
                }

                data.eReasonCode = -1;
                data.eMessage = "Aconteceu algo errado e não foi Fechado o Pedido!";
                //return GetDetalheConsultaMercado(data);
                return Json(data);
            }

            data.eReasonCode = -1;
            data.eMessage = "Aconteceu algo errado e não foi Fechado o Pedido!";
            //return GetDetalheConsultaMercado(data);
            return Json(data);
        }


        [HttpPost]
        public JsonResult GerarRegistoPropostas([FromBody] ConsultaMercadoView data)
        {
            /*
             Verificar se para a consulta de mercado e para o fornecedor já existe Alternativa > 0
             Inserir registo na tabela "Condicoes_Propostas_Fornecedores"
             Para cada registo acima, inserir as linhas da consulta de mercado na tabela "Linhas_Condicoes_Propostas_Fornecedores"
             */

            if (data != null)
            {
                ConsultaMercado consultaMercado = DBConsultaMercado.GetDetalheConsultaMercado(data.NumConsultaMercado);

                //string _Alternativa = string.Empty;
                //foreach (SeleccaoEntidades seleccaoEntidades in consultaMercado.SeleccaoEntidades)
                //{
                //    _Alternativa = DBConsultaMercado.Get_MAX_Alternativa_CondicoesPropostasFornecedores(data.NumConsultaMercado, seleccaoEntidades.CodFornecedor);

                //    if (_Alternativa == null)
                //    {
                //        _Alternativa = "0";
                //    }
                //    else
                //    {
                //        _Alternativa = (int.Parse(_Alternativa) + 1).ToString();
                //    }

                //    //Inserir registo na tabela "Condicoes_Propostas_Fornecedores", com o valor Alternativa calculado acima
                //    CondicoesPropostasFornecedores condicoesPropostasFornecedores = DBConsultaMercado.Create(seleccaoEntidades, _Alternativa);

                //    //Para cada registo, inserir as linhas da consulta de mercado na tabela "Linhas_Condicoes_Propostas_Fornecedores"
                //    foreach (LinhasConsultaMercado linhasConsultaMercado in consultaMercado.LinhasConsultaMercado)
                //    {
                //        LinhasCondicoesPropostasFornecedores linhasCondicoesPropostasFornecedores = DBConsultaMercado.Create(linhasConsultaMercado, _Alternativa, seleccaoEntidades.CodFornecedor);
                //    }
                //}

                //NOVO MÉTODO, QUE SUBSTITUI O USO DAS DUAS TABELAS ACIMA, "Condicoes_Propostas_Fornecedores" e "Linhas_Condicoes_Propostas_Fornecedores"
                //GRAVA NA NOVA TABELA "Registo_De_Propostas"
                //Para cada registo, inserir as linhas da consulta de mercado na tabela "Linhas_Condicoes_Propostas_Fornecedores"
                string _Alternativa = "0";
                foreach (LinhasConsultaMercado linhasConsultaMercado in consultaMercado.LinhasConsultaMercado)
                {
                    RegistoDePropostas registoDePropostas = DBConsultaMercado.Create(linhasConsultaMercado, _Alternativa, _config.NAVDatabaseName, _config.NAVCompanyName);
                }

                consultaMercado = DBConsultaMercado.GetDetalheConsultaMercado(data.NumConsultaMercado);

                data = DBConsultaMercado.CastConsultaMercadoToView(consultaMercado);
                data.eReasonCode = 0;
                data.eMessage = "Foi Gerado o Registo de Proposta!";
                return Json(data);
            }

            data.eReasonCode = -1;
            data.eMessage = "Aconteceu algo errado e não foi possível Gerar o Registo de Proposta!";
            return Json(data);
        }

        [HttpPost]
        public JsonResult ValidacaoMercadoLocal([FromBody] ConsultaMercadoView item)
        {
            ErrorHandler result = new ErrorHandler
            {
                eReasonCode = 1,
                eMessage = "Os Registos foram atualizados com sucesso."
            };

            if (item != null)
            {
                try
                {
                    if (!string.IsNullOrEmpty(item.RegiaoMercadoLocal))
                    {
                        List<LinhasConsultaMercadoView> linhasConsultaMercados = item.LinhasConsultaMercado.Where(x => x.MercadoLocal == true && x.Quantidade > 0).ToList();

                        if (linhasConsultaMercados != null && linhasConsultaMercados.Count() > 0)
                        {
                            linhasConsultaMercados.ForEach(Linha =>
                            {
                                string Responsaveis = "";
                                string Responsavel1 = "";
                                string Responsavel2 = "";
                                string Responsavel3 = "";
                                string Responsavel4 = "";

                                ConfigMercadoLocal ConfigMercadoLocal = DBConfigMercadoLocal.GetByID(item.RegiaoMercadoLocal);
                                if (ConfigMercadoLocal != null)
                                {
                                    if (!string.IsNullOrEmpty(ConfigMercadoLocal.Responsavel1))
                                        Responsavel1 = ConfigMercadoLocal.Responsavel1;
                                    if (!string.IsNullOrEmpty(ConfigMercadoLocal.Responsavel2))
                                        Responsavel2 = ConfigMercadoLocal.Responsavel2;
                                    if (!string.IsNullOrEmpty(ConfigMercadoLocal.Responsavel3))
                                        Responsavel3 = ConfigMercadoLocal.Responsavel3;
                                    if (!string.IsNullOrEmpty(ConfigMercadoLocal.Responsavel4))
                                        Responsavel4 = ConfigMercadoLocal.Responsavel4;
                                    Responsaveis = "-" + Responsavel1 + "-" + Responsavel2 + "-" + Responsavel3 + "-" + Responsavel4 + "-";
                                }

                                Compras Compra = new Compras
                                {
                                    CodigoProduto = Linha.CodProduto,
                                    Descricao = Linha.Descricao,
                                    Descricao2 = Linha.Descricao2,
                                    CodigoUnidadeMedida = Linha.CodUnidadeMedida,
                                    Quantidade = Linha.Quantidade,
                                    NoRequisicao = Linha.NumRequisicao,
                                    NoLinhaRequisicao = Linha.LinhaRequisicao,
                                    //Urgente = false,
                                    NoProjeto = Linha.NumProjecto,
                                    RegiaoMercadoLocal = item.RegiaoMercadoLocal,
                                    Estado = 2, //0-"";1-"Aprovado";2-"Validado";3-"Recusado";4-"Tratado";
                                    DataCriacao = DateTime.Now,
                                    UtilizadorCriacao = User.Identity.Name,
                                    DataMercadoLocal = DateTime.Now,
                                    Responsaveis = Responsaveis,
                                    NoConsultaMercado = Linha.NumConsultaMercado,
                                    DataConsultaMercado = item.PedidoCotacaoCriadoEm
                                };

                                Compras CompraReq = DBCompras.Create(Compra);
                                if (CompraReq != null)
                                {
                                    Linha.IdCompra = CompraReq.Id;
                                    Linha.ValidadoCompras = true;

                                    if (DBConsultaMercado.Update(DBConsultaMercado.CastLinhasConsultaMercadoViewToDB(Linha)) == null)
                                    {
                                        result.eReasonCode = 7;
                                        result.eMessage = "Ocorreu um erro ao atualizar a linha da Consulta ao Mercado.";
                                    }
                                }
                                else
                                {
                                    result.eReasonCode = 6;
                                    result.eMessage = "Ocorreu um erro ao criar a Compra.";
                                }

                            });
                        }

                        else
                        {
                            result.eReasonCode = 5;
                            result.eMessage = "Não foram encontradas linhas para Validar.";
                        }
                    }
                    else
                    {
                        result.eReasonCode = 4;
                        result.eMessage = "Preencha o campo Região Mercado Local no Cabeçalho.";
                    }
                }
                catch (Exception ex)
                {
                    result.eReasonCode = 3;
                    result.eMessage = "Ocorreu um erro ao criar encomenda de compra (" + ex.Message + ")";
                }
            }
            else
            {
                result.eReasonCode = 2;
                result.eMessage = "Não é possivel validar o mercado local. A Consulta ao Mercado não pode ser nula.";
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult CriarEncomenda([FromBody] ConsultaMercadoView data, bool Arquivar)
        {
            PurchOrderDTO purchOrderDTO = new PurchOrderDTO();
            List<PurchOrderDTO> purchOrders = new List<PurchOrderDTO>();
            List<RegistoDePropostasView> registoDePropostas = new List<RegistoDePropostasView>();

            try
            {
                for (int i = 1; i <= 6; i++)
                {
                    if (i == 1)
                    {
                        registoDePropostas = data.RegistoDePropostas.Where(x => x.Fornecedor1Select.HasValue && x.Fornecedor1Select.Value == true).ToList();

                        if (registoDePropostas.Count() > 0)
                        {
                            //vamos criar a encomenda com as linhas
                            List<PurchOrderLineDTO> purchOrderLineDTOs = new List<PurchOrderLineDTO>();

                            string VAT_Fornecedor = DBNAV2017Vendor.GetVendor(_config.NAVDatabaseName, _config.NAVCompanyName).Where(x => x.No_ == registoDePropostas[0].Fornecedor1Code).FirstOrDefault().VATBusinessPostingGroup;

                            foreach (RegistoDePropostasView registoDePropostasView in registoDePropostas)
                            {
                                PurchOrderLineDTO purchOrderLineDTO = new PurchOrderLineDTO();

                                purchOrderLineDTO.CenterResponsibilityCode = registoDePropostasView.CodCentroResponsabilidade;
                                purchOrderLineDTO.Code = registoDePropostasView.CodProduto;
                                purchOrderLineDTO.Description = registoDePropostasView.Descricao;
                                purchOrderLineDTO.Description2 = registoDePropostasView.Descricao2 ?? string.Empty;
                                purchOrderLineDTO.DiscountPercentage = 0;
                                purchOrderLineDTO.FunctionalAreaCode = registoDePropostasView.CodAreaFuncional;
                                purchOrderLineDTO.LineId = registoDePropostasView.NumLinha;
                                purchOrderLineDTO.LocationCode = registoDePropostasView.CodLocalizacao;
                                //purchOrderLineDTO.OpenOrderLineNo = registoDePropostasView.NumLinhaConsultaMercado;
                                //purchOrderLineDTO.OpenOrderNo = registoDePropostasView.NumConsultaMercado;
                                purchOrderLineDTO.ProjectNo = registoDePropostasView.NumProjecto;
                                purchOrderLineDTO.QuantityRequired = registoDePropostasView.Quantidade;
                                purchOrderLineDTO.RegionCode = registoDePropostasView.CodRegiao;
                                purchOrderLineDTO.UnitCost = registoDePropostasView.Fornecedor1Preco;
                                purchOrderLineDTO.UnitMeasureCode = data.LinhasConsultaMercado.Where(x => x.NumLinha == registoDePropostasView.NumLinhaConsultaMercado).FirstOrDefault().CodUnidadeMedida;
                                //purchOrderLineDTO.VATBusinessPostingGroup = VAT_Fornecedor;
                                //purchOrderLineDTO.VATProductPostingGroup = DBNAV2017Products.GetAllProducts(_config.NAVDatabaseName, _config.NAVCompanyName, registoDePropostasView.CodProduto).FirstOrDefault().VATProductPostingGroup;

                                purchOrderLineDTO.VATBusinessPostingGroup = registoDePropostasView.VatbusinessPostingGroup1;
                                purchOrderLineDTO.VATProductPostingGroup = registoDePropostasView.VatproductPostingGroup;

                                purchOrderLineDTOs.Add(purchOrderLineDTO);
                            }

                            PurchOrderDTO purchOrderDTO1 = new PurchOrderDTO()
                            {
                                CenterResponsibilityCode = data.CodCentroResponsabilidade,
                                FunctionalAreaCode = data.CodAreaFuncional,
                                InAdvance = false,
                                LocalMarketRegion = string.Empty,
                                PricesIncludingVAT = false,
                                RegionCode = data.CodRegiao,
                                RequisitionId = data.NumRequisicao,
                                SupplierId = registoDePropostas[0].Fornecedor1Code,
                                Lines = purchOrderLineDTOs,
                                NAVPrePurchOrderId = data.NumConsultaMercado
                            };

                            purchOrders.Add(purchOrderDTO1);
                        }
                    }

                    if (i == 2)
                    {
                        registoDePropostas = data.RegistoDePropostas.Where(x => x.Fornecedor2Select.HasValue && x.Fornecedor2Select.Value == true).ToList();

                        if (registoDePropostas.Count() > 0)
                        {
                            //vamos criar a encomenda com as linhas
                            List<PurchOrderLineDTO> purchOrderLineDTOs = new List<PurchOrderLineDTO>();

                            string VAT_Fornecedor = DBNAV2017Vendor.GetVendor(_config.NAVDatabaseName, _config.NAVCompanyName).Where(x => x.No_ == registoDePropostas[0].Fornecedor2Code).FirstOrDefault().VATBusinessPostingGroup;

                            foreach (RegistoDePropostasView registoDePropostasView in registoDePropostas)
                            {
                                PurchOrderLineDTO purchOrderLineDTO = new PurchOrderLineDTO();

                                purchOrderLineDTO.CenterResponsibilityCode = registoDePropostasView.CodCentroResponsabilidade;
                                purchOrderLineDTO.Code = registoDePropostasView.CodProduto;
                                purchOrderLineDTO.Description = registoDePropostasView.Descricao;
                                purchOrderLineDTO.Description2 = registoDePropostasView.Descricao2 ?? string.Empty;
                                purchOrderLineDTO.DiscountPercentage = 0;
                                purchOrderLineDTO.FunctionalAreaCode = registoDePropostasView.CodAreaFuncional;
                                purchOrderLineDTO.LineId = registoDePropostasView.NumLinha;
                                purchOrderLineDTO.LocationCode = registoDePropostasView.CodLocalizacao;
                                //purchOrderLineDTO.OpenOrderLineNo = registoDePropostasView.NumLinhaConsultaMercado;
                                //purchOrderLineDTO.OpenOrderNo = registoDePropostasView.NumConsultaMercado;
                                purchOrderLineDTO.ProjectNo = registoDePropostasView.NumProjecto;
                                purchOrderLineDTO.QuantityRequired = registoDePropostasView.Quantidade;
                                purchOrderLineDTO.RegionCode = registoDePropostasView.CodRegiao;
                                purchOrderLineDTO.UnitCost = registoDePropostasView.Fornecedor2Preco;
                                purchOrderLineDTO.UnitMeasureCode = data.LinhasConsultaMercado.Where(x => x.NumLinha == registoDePropostasView.NumLinhaConsultaMercado).FirstOrDefault().CodUnidadeMedida;

                                //purchOrderLineDTO.VATBusinessPostingGroup = VAT_Fornecedor;
                                //purchOrderLineDTO.VATProductPostingGroup = DBNAV2017Products.GetAllProducts(_config.NAVDatabaseName, _config.NAVCompanyName, registoDePropostasView.CodProduto).FirstOrDefault().VATProductPostingGroup;

                                purchOrderLineDTO.VATBusinessPostingGroup = registoDePropostasView.VatbusinessPostingGroup2;
                                purchOrderLineDTO.VATProductPostingGroup = registoDePropostasView.VatproductPostingGroup;

                                purchOrderLineDTOs.Add(purchOrderLineDTO);
                            }

                            PurchOrderDTO purchOrderDTO2 = new PurchOrderDTO()
                            {
                                CenterResponsibilityCode = data.CodCentroResponsabilidade,
                                FunctionalAreaCode = data.CodAreaFuncional,
                                InAdvance = false,
                                LocalMarketRegion = string.Empty,
                                PricesIncludingVAT = false,
                                RegionCode = data.CodRegiao,
                                RequisitionId = data.NumRequisicao,
                                SupplierId = registoDePropostas[0].Fornecedor2Code,
                                Lines = purchOrderLineDTOs,
                                NAVPrePurchOrderId = data.NumConsultaMercado
                            };

                            purchOrders.Add(purchOrderDTO2);
                        }
                    }

                    if (i == 3)
                    {
                        registoDePropostas = data.RegistoDePropostas.Where(x => x.Fornecedor3Select.HasValue && x.Fornecedor3Select.Value == true).ToList();

                        if (registoDePropostas.Count() > 0)
                        {
                            //vamos criar a encomenda com as linhas
                            List<PurchOrderLineDTO> purchOrderLineDTOs = new List<PurchOrderLineDTO>();

                            string VAT_Fornecedor = DBNAV2017Vendor.GetVendor(_config.NAVDatabaseName, _config.NAVCompanyName).Where(x => x.No_ == registoDePropostas[0].Fornecedor3Code).FirstOrDefault().VATBusinessPostingGroup;

                            foreach (RegistoDePropostasView registoDePropostasView in registoDePropostas)
                            {
                                PurchOrderLineDTO purchOrderLineDTO = new PurchOrderLineDTO();

                                purchOrderLineDTO.CenterResponsibilityCode = registoDePropostasView.CodCentroResponsabilidade;
                                purchOrderLineDTO.Code = registoDePropostasView.CodProduto;
                                purchOrderLineDTO.Description = registoDePropostasView.Descricao;
                                purchOrderLineDTO.Description2 = registoDePropostasView.Descricao2 ?? string.Empty;
                                purchOrderLineDTO.DiscountPercentage = 0;
                                purchOrderLineDTO.FunctionalAreaCode = registoDePropostasView.CodAreaFuncional;
                                purchOrderLineDTO.LineId = registoDePropostasView.NumLinha;
                                purchOrderLineDTO.LocationCode = registoDePropostasView.CodLocalizacao;
                                //purchOrderLineDTO.OpenOrderLineNo = registoDePropostasView.NumLinhaConsultaMercado;
                                //purchOrderLineDTO.OpenOrderNo = registoDePropostasView.NumConsultaMercado;
                                purchOrderLineDTO.ProjectNo = registoDePropostasView.NumProjecto;
                                purchOrderLineDTO.QuantityRequired = registoDePropostasView.Quantidade;
                                purchOrderLineDTO.RegionCode = registoDePropostasView.CodRegiao;
                                purchOrderLineDTO.UnitCost = registoDePropostasView.Fornecedor3Preco;
                                purchOrderLineDTO.UnitMeasureCode = data.LinhasConsultaMercado.Where(x => x.NumLinha == registoDePropostasView.NumLinhaConsultaMercado).FirstOrDefault().CodUnidadeMedida;

                                //purchOrderLineDTO.VATBusinessPostingGroup = VAT_Fornecedor;
                                //purchOrderLineDTO.VATProductPostingGroup = DBNAV2017Products.GetAllProducts(_config.NAVDatabaseName, _config.NAVCompanyName, registoDePropostasView.CodProduto).FirstOrDefault().VATProductPostingGroup;

                                purchOrderLineDTO.VATBusinessPostingGroup = registoDePropostasView.VatbusinessPostingGroup3;
                                purchOrderLineDTO.VATProductPostingGroup = registoDePropostasView.VatproductPostingGroup;

                                purchOrderLineDTOs.Add(purchOrderLineDTO);
                            }

                            PurchOrderDTO purchOrderDTO3 = new PurchOrderDTO()
                            {
                                CenterResponsibilityCode = data.CodCentroResponsabilidade,
                                FunctionalAreaCode = data.CodAreaFuncional,
                                InAdvance = false,
                                LocalMarketRegion = string.Empty,
                                PricesIncludingVAT = false,
                                RegionCode = data.CodRegiao,
                                RequisitionId = data.NumRequisicao,
                                SupplierId = registoDePropostas[0].Fornecedor3Code,
                                Lines = purchOrderLineDTOs,
                                NAVPrePurchOrderId = data.NumConsultaMercado
                            };

                            purchOrders.Add(purchOrderDTO3);
                        }
                    }

                    if (i == 4)
                    {
                        registoDePropostas = data.RegistoDePropostas.Where(x => x.Fornecedor4Select.HasValue && x.Fornecedor4Select.Value == true).ToList();

                        if (registoDePropostas.Count() > 0)
                        {
                            //vamos criar a encomenda com as linhas
                            List<PurchOrderLineDTO> purchOrderLineDTOs = new List<PurchOrderLineDTO>();

                            string VAT_Fornecedor = DBNAV2017Vendor.GetVendor(_config.NAVDatabaseName, _config.NAVCompanyName).Where(x => x.No_ == registoDePropostas[0].Fornecedor4Code).FirstOrDefault().VATBusinessPostingGroup;

                            foreach (RegistoDePropostasView registoDePropostasView in registoDePropostas)
                            {
                                PurchOrderLineDTO purchOrderLineDTO = new PurchOrderLineDTO();

                                purchOrderLineDTO.CenterResponsibilityCode = registoDePropostasView.CodCentroResponsabilidade;
                                purchOrderLineDTO.Code = registoDePropostasView.CodProduto;
                                purchOrderLineDTO.Description = registoDePropostasView.Descricao;
                                purchOrderLineDTO.Description2 = registoDePropostasView.Descricao2 ?? string.Empty;
                                purchOrderLineDTO.DiscountPercentage = 0;
                                purchOrderLineDTO.FunctionalAreaCode = registoDePropostasView.CodAreaFuncional;
                                purchOrderLineDTO.LineId = registoDePropostasView.NumLinha;
                                purchOrderLineDTO.LocationCode = registoDePropostasView.CodLocalizacao;
                                //purchOrderLineDTO.OpenOrderLineNo = registoDePropostasView.NumLinhaConsultaMercado;
                                //purchOrderLineDTO.OpenOrderNo = registoDePropostasView.NumConsultaMercado;
                                purchOrderLineDTO.ProjectNo = registoDePropostasView.NumProjecto;
                                purchOrderLineDTO.QuantityRequired = registoDePropostasView.Quantidade;
                                purchOrderLineDTO.RegionCode = registoDePropostasView.CodRegiao;
                                purchOrderLineDTO.UnitCost = registoDePropostasView.Fornecedor4Preco;
                                purchOrderLineDTO.UnitMeasureCode = data.LinhasConsultaMercado.Where(x => x.NumLinha == registoDePropostasView.NumLinhaConsultaMercado).FirstOrDefault().CodUnidadeMedida;

                                //purchOrderLineDTO.VATBusinessPostingGroup = VAT_Fornecedor;
                                //purchOrderLineDTO.VATProductPostingGroup = DBNAV2017Products.GetAllProducts(_config.NAVDatabaseName, _config.NAVCompanyName, registoDePropostasView.CodProduto).FirstOrDefault().VATProductPostingGroup;

                                purchOrderLineDTO.VATBusinessPostingGroup = registoDePropostasView.VatbusinessPostingGroup4;
                                purchOrderLineDTO.VATProductPostingGroup = registoDePropostasView.VatproductPostingGroup;


                                purchOrderLineDTOs.Add(purchOrderLineDTO);
                            }

                            PurchOrderDTO purchOrderDTO4 = new PurchOrderDTO()
                            {
                                CenterResponsibilityCode = data.CodCentroResponsabilidade,
                                FunctionalAreaCode = data.CodAreaFuncional,
                                InAdvance = false,
                                LocalMarketRegion = string.Empty,
                                PricesIncludingVAT = false,
                                RegionCode = data.CodRegiao,
                                RequisitionId = data.NumRequisicao,
                                SupplierId = registoDePropostas[0].Fornecedor4Code,
                                Lines = purchOrderLineDTOs,
                                NAVPrePurchOrderId = data.NumConsultaMercado
                            };

                            purchOrders.Add(purchOrderDTO4);
                        }
                    }

                    if (i == 5)
                    {
                        registoDePropostas = data.RegistoDePropostas.Where(x => x.Fornecedor5Select.HasValue && x.Fornecedor5Select.Value == true).ToList();

                        if (registoDePropostas.Count() > 0)
                        {
                            //vamos criar a encomenda com as linhas
                            List<PurchOrderLineDTO> purchOrderLineDTOs = new List<PurchOrderLineDTO>();

                            string VAT_Fornecedor = DBNAV2017Vendor.GetVendor(_config.NAVDatabaseName, _config.NAVCompanyName).Where(x => x.No_ == registoDePropostas[0].Fornecedor5Code).FirstOrDefault().VATBusinessPostingGroup;

                            foreach (RegistoDePropostasView registoDePropostasView in registoDePropostas)
                            {
                                PurchOrderLineDTO purchOrderLineDTO = new PurchOrderLineDTO();

                                purchOrderLineDTO.CenterResponsibilityCode = registoDePropostasView.CodCentroResponsabilidade;
                                purchOrderLineDTO.Code = registoDePropostasView.CodProduto;
                                purchOrderLineDTO.Description = registoDePropostasView.Descricao;
                                purchOrderLineDTO.Description2 = registoDePropostasView.Descricao2 ?? string.Empty;
                                purchOrderLineDTO.DiscountPercentage = 0;
                                purchOrderLineDTO.FunctionalAreaCode = registoDePropostasView.CodAreaFuncional;
                                purchOrderLineDTO.LineId = registoDePropostasView.NumLinha;
                                purchOrderLineDTO.LocationCode = registoDePropostasView.CodLocalizacao;
                                //purchOrderLineDTO.OpenOrderLineNo = registoDePropostasView.NumLinhaConsultaMercado;
                                //purchOrderLineDTO.OpenOrderNo = registoDePropostasView.NumConsultaMercado;
                                purchOrderLineDTO.ProjectNo = registoDePropostasView.NumProjecto;
                                purchOrderLineDTO.QuantityRequired = registoDePropostasView.Quantidade;
                                purchOrderLineDTO.RegionCode = registoDePropostasView.CodRegiao;
                                purchOrderLineDTO.UnitCost = registoDePropostasView.Fornecedor5Preco;
                                purchOrderLineDTO.UnitMeasureCode = data.LinhasConsultaMercado.Where(x => x.NumLinha == registoDePropostasView.NumLinhaConsultaMercado).FirstOrDefault().CodUnidadeMedida;

                                //purchOrderLineDTO.VATBusinessPostingGroup = VAT_Fornecedor;
                                //purchOrderLineDTO.VATProductPostingGroup = DBNAV2017Products.GetAllProducts(_config.NAVDatabaseName, _config.NAVCompanyName, registoDePropostasView.CodProduto).FirstOrDefault().VATProductPostingGroup;

                                purchOrderLineDTO.VATBusinessPostingGroup = registoDePropostasView.VatbusinessPostingGroup5;
                                purchOrderLineDTO.VATProductPostingGroup = registoDePropostasView.VatproductPostingGroup;


                                purchOrderLineDTOs.Add(purchOrderLineDTO);
                            }

                            PurchOrderDTO purchOrderDTO5 = new PurchOrderDTO()
                            {
                                CenterResponsibilityCode = data.CodCentroResponsabilidade,
                                FunctionalAreaCode = data.CodAreaFuncional,
                                InAdvance = false,
                                LocalMarketRegion = string.Empty,
                                PricesIncludingVAT = false,
                                RegionCode = data.CodRegiao,
                                RequisitionId = data.NumRequisicao,
                                SupplierId = registoDePropostas[0].Fornecedor5Code,
                                Lines = purchOrderLineDTOs,
                                NAVPrePurchOrderId = data.NumConsultaMercado
                            };

                            purchOrders.Add(purchOrderDTO5);
                        }
                    }

                    if (i == 6)
                    {
                        registoDePropostas = data.RegistoDePropostas.Where(x => x.Fornecedor6Select.HasValue && x.Fornecedor6Select.Value == true).ToList();

                        if (registoDePropostas.Count() > 0)
                        {
                            //vamos criar a encomenda com as linhas
                            List<PurchOrderLineDTO> purchOrderLineDTOs = new List<PurchOrderLineDTO>();

                            string VAT_Fornecedor = DBNAV2017Vendor.GetVendor(_config.NAVDatabaseName, _config.NAVCompanyName).Where(x => x.No_ == registoDePropostas[0].Fornecedor6Code).FirstOrDefault().VATBusinessPostingGroup;

                            foreach (RegistoDePropostasView registoDePropostasView in registoDePropostas)
                            {
                                PurchOrderLineDTO purchOrderLineDTO = new PurchOrderLineDTO();

                                purchOrderLineDTO.CenterResponsibilityCode = registoDePropostasView.CodCentroResponsabilidade;
                                purchOrderLineDTO.Code = registoDePropostasView.CodProduto;
                                purchOrderLineDTO.Description = registoDePropostasView.Descricao;
                                purchOrderLineDTO.Description2 = registoDePropostasView.Descricao2 ?? string.Empty;
                                purchOrderLineDTO.DiscountPercentage = 0;
                                purchOrderLineDTO.FunctionalAreaCode = registoDePropostasView.CodAreaFuncional;
                                purchOrderLineDTO.LineId = registoDePropostasView.NumLinha;
                                purchOrderLineDTO.LocationCode = registoDePropostasView.CodLocalizacao;
                                //purchOrderLineDTO.OpenOrderLineNo = registoDePropostasView.NumLinhaConsultaMercado;
                                //purchOrderLineDTO.OpenOrderNo = registoDePropostasView.NumConsultaMercado;
                                purchOrderLineDTO.ProjectNo = registoDePropostasView.NumProjecto;
                                purchOrderLineDTO.QuantityRequired = registoDePropostasView.Quantidade;
                                purchOrderLineDTO.RegionCode = registoDePropostasView.CodRegiao;
                                purchOrderLineDTO.UnitCost = registoDePropostasView.Fornecedor6Preco;
                                purchOrderLineDTO.UnitMeasureCode = data.LinhasConsultaMercado.Where(x => x.NumLinha == registoDePropostasView.NumLinhaConsultaMercado).FirstOrDefault().CodUnidadeMedida;

                                //purchOrderLineDTO.VATBusinessPostingGroup = VAT_Fornecedor;
                                //purchOrderLineDTO.VATProductPostingGroup = DBNAV2017Products.GetAllProducts(_config.NAVDatabaseName, _config.NAVCompanyName, registoDePropostasView.CodProduto).FirstOrDefault().VATProductPostingGroup;

                                purchOrderLineDTO.VATBusinessPostingGroup = registoDePropostasView.VatbusinessPostingGroup6;
                                purchOrderLineDTO.VATProductPostingGroup = registoDePropostasView.VatproductPostingGroup;
                                
                                purchOrderLineDTOs.Add(purchOrderLineDTO);
                            }

                            PurchOrderDTO purchOrderDTO6 = new PurchOrderDTO()
                            {
                                CenterResponsibilityCode = data.CodCentroResponsabilidade,
                                FunctionalAreaCode = data.CodAreaFuncional,
                                InAdvance = false,
                                LocalMarketRegion = string.Empty,
                                PricesIncludingVAT = false,
                                RegionCode = data.CodRegiao,
                                RequisitionId = data.NumRequisicao,
                                SupplierId = registoDePropostas[0].Fornecedor6Code,
                                Lines = purchOrderLineDTOs,
                                NAVPrePurchOrderId = data.NumConsultaMercado
                            };

                            purchOrders.Add(purchOrderDTO6);
                        }
                    }
                }
            }
            catch
            {
                throw new Exception("Ocorreu um erro ao agrupar as linhas.");
            }

            if (purchOrders.Count() > 0)
            {
                purchOrders.ForEach(purchOrder =>
                {
                    try
                    {
                        //purchOrder.Purchaser_Code = User.Identity.Name;
                        purchOrder.Purchaser_Code = string.IsNullOrEmpty(DBUserConfigurations.GetById(User.Identity.Name).EmployeeNo) ? "" : DBUserConfigurations.GetById(User.Identity.Name).EmployeeNo;

                        var result = CreateNAVPurchaseOrderFor(purchOrder, data.NumConsultaMercado, data.Obs);
                        if (result.CompletedSuccessfully)
                        {
                            //INICIO CODIGO NOVO
                            string NotaEncomenda = result.ResultValue;

                            if (!string.IsNullOrEmpty(NotaEncomenda))
                            {
                                Requisição REQ = DBRequest.GetById(purchOrder.RequisitionId);
                                REQ.NºEncomenda = NotaEncomenda;
                                REQ.UtilizadorModificação = User.Identity.Name;

                                DBRequest.Update(REQ);

                                REQ.LinhasRequisição.ToList().ForEach(linha =>
                                {
                                    if (linha.NºFornecedor == purchOrder.SupplierId)
                                    {
                                        linha.NºEncomendaCriada = NotaEncomenda;
                                        linha.UtilizadorModificação = User.Identity.Name;

                                        DBRequestLine.Update(linha);
                                    }
                                });
                            }
                            //FIM CODIGO NOVO

                            data.eMessages.Add(new TraceInformation(TraceType.Success, "Criada encomenda para o fornecedor núm. " + purchOrder.SupplierId + "; "));
                        }
                    }
                    catch (Exception ex)
                    {
                        data.eMessages.Add(new TraceInformation(TraceType.Error, "Ocorreu um erro ao criar encomenda para o fornecedor núm. " + purchOrder.SupplierId + ": " + ex.Message));
                    }
                });

                if (data.eMessages.Any(x => x.Type == TraceType.Error))
                {
                    data.eReasonCode = 2;
                    data.eMessage = "Ocorreram erros ao criar encomenda de compra." + Environment.NewLine + data.eMessages[data.eMessages.Count() - 1].Message;
                }
                else
                {
                    if (Arquivar)
                    {
                        ConsultaMercado consultaMercado = DBConsultaMercado.CastConsultaMercadoViewToConsultaMercado(data);
                        consultaMercado.Historico = true;
                        consultaMercado.UtilizadorModificacao = User.Identity.Name;
                        DBConsultaMercado.Update(consultaMercado);
                        data.Historico = true;
                    }

                    data.eReasonCode = 1;
                    data.eMessage = "Encomenda de compra criada com sucesso.";
                }
            }
            else
            {
                data.eReasonCode = 3;
                data.eMessage = "Não existem linhas que cumpram os requisitos de validação do mercado local.";
            }

            return Json(data);
        }

        [HttpPost]
        public JsonResult ObterTotalFornecedor([FromBody] ConsultaMercadoView data)
        {
            List<RegistoDePropostasView> registoDePropostas = new List<RegistoDePropostasView>();
            string TextoComTotais = "";
            decimal TotalForn1 = 0;
            decimal TotalForn2 = 0;
            decimal TotalForn3 = 0;
            decimal TotalForn4 = 0;
            decimal TotalForn5 = 0;
            decimal TotalForn6 = 0;

            try
            {
                for (int i = 1; i <= 6; i++)
                {
                    if (i == 1)
                    {
                        registoDePropostas = data.RegistoDePropostas.Where(x => x.Fornecedor1Select.HasValue && x.Fornecedor1Select.Value == true).ToList();

                        if (registoDePropostas.Count() > 0)
                        {
                            foreach (RegistoDePropostasView registoDePropostasView in registoDePropostas)
                            {
                                if (registoDePropostasView.Fornecedor1Preco != null)
                                    TotalForn1 = TotalForn1 + (decimal)registoDePropostasView.Fornecedor1Preco;
                            }
                            TextoComTotais = TextoComTotais + "Total de: " + registoDePropostas.FirstOrDefault().Fornecedor1Nome + " = " + TotalForn1.ToString() + " €" + Environment.NewLine;
                        }
                    }

                    if (i == 2)
                    {
                        registoDePropostas = data.RegistoDePropostas.Where(x => x.Fornecedor2Select.HasValue && x.Fornecedor2Select.Value == true).ToList();

                        if (registoDePropostas.Count() > 0)
                        {
                            foreach (RegistoDePropostasView registoDePropostasView in registoDePropostas)
                            {
                                if (registoDePropostasView.Fornecedor2Preco != null)
                                    TotalForn2 = TotalForn2 + (decimal)registoDePropostasView.Fornecedor2Preco;
                            }
                            TextoComTotais = TextoComTotais + "Total de: " + registoDePropostas.FirstOrDefault().Fornecedor2Nome + " = " + TotalForn2.ToString() + " €" + Environment.NewLine;
                        }
                    }

                    if (i == 3)
                    {
                        registoDePropostas = data.RegistoDePropostas.Where(x => x.Fornecedor3Select.HasValue && x.Fornecedor3Select.Value == true).ToList();

                        if (registoDePropostas.Count() > 0)
                        {
                            foreach (RegistoDePropostasView registoDePropostasView in registoDePropostas)
                            {
                                if (registoDePropostasView.Fornecedor3Preco != null)
                                    TotalForn3 = TotalForn3 + (decimal)registoDePropostasView.Fornecedor3Preco;
                            }
                            TextoComTotais = TextoComTotais + "Total de: " + registoDePropostas.FirstOrDefault().Fornecedor3Nome + " = " + TotalForn3.ToString() + " €" + Environment.NewLine;
                        }
                    }

                    if (i == 4)
                    {
                        registoDePropostas = data.RegistoDePropostas.Where(x => x.Fornecedor4Select.HasValue && x.Fornecedor4Select.Value == true).ToList();

                        if (registoDePropostas.Count() > 0)
                        {
                            foreach (RegistoDePropostasView registoDePropostasView in registoDePropostas)
                            {
                                if (registoDePropostasView.Fornecedor4Preco != null)
                                    TotalForn4 = TotalForn4 + (decimal)registoDePropostasView.Fornecedor4Preco;
                            }
                            TextoComTotais = TextoComTotais + "Total de: " + registoDePropostas.FirstOrDefault().Fornecedor4Nome + " = " + TotalForn4.ToString() + " €" + Environment.NewLine;
                        }
                    }

                    if (i == 5)
                    {
                        registoDePropostas = data.RegistoDePropostas.Where(x => x.Fornecedor5Select.HasValue && x.Fornecedor5Select.Value == true).ToList();

                        if (registoDePropostas.Count() > 0)
                        {
                            foreach (RegistoDePropostasView registoDePropostasView in registoDePropostas)
                            {
                                if (registoDePropostasView.Fornecedor5Preco != null)
                                    TotalForn5 = TotalForn5 + (decimal)registoDePropostasView.Fornecedor5Preco;
                            }
                            TextoComTotais = TextoComTotais + "Total de: " + registoDePropostas.FirstOrDefault().Fornecedor5Nome + " = " + TotalForn5.ToString() + " €" + Environment.NewLine;
                        }
                    }

                    if (i == 6)
                    {
                        registoDePropostas = data.RegistoDePropostas.Where(x => x.Fornecedor6Select.HasValue && x.Fornecedor6Select.Value == true).ToList();

                        if (registoDePropostas.Count() > 0)
                        {
                            foreach (RegistoDePropostasView registoDePropostasView in registoDePropostas)
                            {
                                if (registoDePropostasView.Fornecedor6Preco != null)
                                    TotalForn6 = TotalForn6 + (decimal)registoDePropostasView.Fornecedor6Preco;
                            }
                            TextoComTotais = TextoComTotais + "Total de: " + registoDePropostas.FirstOrDefault().Fornecedor6Nome + " = " + TotalForn6.ToString() + " €" + Environment.NewLine;
                        }
                    }
                }
            }
            catch
            {
                throw new Exception("Ocorreu um erro ao obter os totais.");
            }

            return Json(TextoComTotais);
        }

        [HttpPost]
        public async Task<JsonResult> EnviarEmailATodos([FromBody] ConsultaMercadoView data)
        {
            data.eReasonCode = 0;
            data.eMessage = "Email(s) não enviado(s)!";

            //Para cada Fornecedor, criar o pdf da Consulta de Mercado, guardar em algum lado e anexar ao email e enviar!!!
            foreach (SeleccaoEntidadesView fornecedor in data.SeleccaoEntidades)
            {
                string Consulta = data.NumConsultaMercado;
                string Cod = fornecedor.CodFornecedor;

                string sWebRootFolder = _generalConfig.FileUploadFolder + "ConsultasMercado\\" + "tmp\\";
                string sFileName = @Consulta + "_" + Cod + ".pdf";

                //var theURL = (_config.ReportServerURL + "ConsultaMercado&rs:Command=Render&rs:format=PDF&CM=" + Consulta + "&Fornecedor=" + Cod);
                //var theURL = (_config.ReportServerURL + "ConsultaMercado&CM=" + Consulta + "&Fornecedor=" + Cod + "&rs:Command=Render&rs:format=PDF");

                //var theURL = (_config.ReportServerURL_PDF + "ConsultaMercado&rs:Command=Render&rs:format=PDF&CM=" + Consulta + "&Fornecedor=" + Cod);
                var theURL = (_config.ReportServerURL_PDF + "ConsultaMercado&CM=" + Consulta + "&Fornecedor=" + Cod + "&rs:Command=Render&rs:format=PDF");

                //WebClient Client = new WebClient
                //{
                //    UseDefaultCredentials = true
                //};

                //OBTER CREDENCIAIS PARA O SERVIDOR DE REPORTS
                Configuração config = DBConfigurations.GetById(1);

                WebClient Client = new WebClient
                {
                    Credentials = new NetworkCredential(config.ReportUsername, config.ReportPassword)
                };

                byte[] myDataBuffer = Client.DownloadData(theURL);

                using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
                {
                    await fs.WriteAsync(myDataBuffer, 0, myDataBuffer.Length);
                }

                Stream _my_stream = new MemoryStream(myDataBuffer);

                using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
                {
                    await stream.CopyToAsync(_my_stream);
                }

                SendEmailsPedidoCotacao Email = new SendEmailsPedidoCotacao
                {
                    DisplayName = User.Identity.Name,
                    Subject = "Pedido de Cotação - " + Consulta,
                    From = User.Identity.Name,
                    Anexo = Path.Combine(sWebRootFolder, sFileName)
                };

                if (_generalConfig.Conn == "eSUCH_Prod")
                    Email.To.Add(data.SeleccaoEntidades.Where(x => x.CodFornecedor == Cod).First().EmailFornecedor);

                Email.To.Add(User.Identity.Name);

                Email.Body = MakeEmailBodyContent("Solicitamos Pedido de Cotação", User.Identity.Name);
                Email.IsBodyHtml = true;

                Email.SendEmail();

                string email = data.SeleccaoEntidades.Where(x => x.CodFornecedor == Cod).First().EmailFornecedor ?? "Fornecedor sem Email definido!";

                if (data.eReasonCode == 0)
                {
                    data.eReasonCode = 1;
                    data.eMessage = "Email enviado com sucesso para:" + Environment.NewLine + email;

                    data.EmailEnviado = true;
                    data.UtilizadorModificacao = User.Identity.Name;
                    DBConsultaMercado.Update(data);
                }
                else
                {
                    data.eMessage += Environment.NewLine + email;
                }

                //Actualizar Tabela "Seleccao_Entidades", com Data de Envio Ao Fornecedor e com Utilizador Envio
                fornecedor.DataEnvioAoFornecedor = DateTime.Now;
                fornecedor.UtilizadorEnvio = User.Identity.Name;
                DBConsultaMercado.Update(DBConsultaMercado.CastSeleccaoEntidadesViewToDB(fornecedor));

                //Apaga o anexo criado no disco
                if (System.IO.Directory.Exists(Path.Combine(sWebRootFolder, sFileName)))
                    System.IO.File.Delete(Path.Combine(sWebRootFolder, sFileName));
            }

            return Json(data);
        }

        [HttpPost]
        public async Task<JsonResult> EnviarEmailAUm([FromBody] JObject requestParams)
        {
            //Para o fornecedor selecionado, criar o pdf da Consulta de Mercado, guardar em algum lado e anexar ao email e enviar!!!
            string Consulta = requestParams["Consulta"].ToString();
            string Cod = requestParams["Cod"].ToString();

            ConsultaMercado consultaMercado = DBConsultaMercado.GetDetalheConsultaMercado(Consulta);
            ConsultaMercadoView data = DBConsultaMercado.CastConsultaMercadoToView(consultaMercado);

            data.eReasonCode = 0;
            data.eMessage = "Email não enviado!";

            string sWebRootFolder = _generalConfig.FileUploadFolder + "ConsultasMercado\\" + "tmp\\";
            string sFileName = @Consulta + "_" + Cod + ".pdf";

            //var theURL = (_config.ReportServerURL + "ConsultaMercado&rs:Command=Render&rs:format=PDF&CM=" + Consulta + "&Fornecedor=" + Cod);
            //var theURL = (_config.ReportServerURL + "ConsultaMercado&CM=" + Consulta + "&Fornecedor=" + Cod + "&rs:Command=Render&rs:format=PDF");
            var theURL = (_config.ReportServerURL_PDF + "ConsultaMercado&CM=" + Consulta + "&Fornecedor=" + Cod + "&rs:Command=Render&rs:format=PDF");

            //WebClient Client = new WebClient
            //{
            //    UseDefaultCredentials = true
            //};

            //OBTER CREDENCIAIS PARA O SERVIDOR DE REPORTS
            Configuração config = DBConfigurations.GetById(1);

            WebClient Client = new WebClient
            {
                Credentials = new NetworkCredential(config.ReportUsername, config.ReportPassword)
            };


            byte[] myDataBuffer = Client.DownloadData(theURL);

            using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
            {
                await fs.WriteAsync(myDataBuffer, 0, myDataBuffer.Length);
            }

            Stream _my_stream = new MemoryStream(myDataBuffer);

            using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
            {
                await stream.CopyToAsync(_my_stream);
                stream.Close();
            }

            SendEmailsPedidoCotacao Email = new SendEmailsPedidoCotacao
            {
                DisplayName = User.Identity.Name,
                Subject = "Pedido de Cotação - " + Consulta,
                From = User.Identity.Name,
                Anexo = Path.Combine(sWebRootFolder, sFileName)
            };

            if (_generalConfig.Conn == "eSUCH_Prod")
                Email.To.Add(data.SeleccaoEntidades.Where(x => x.CodFornecedor == Cod).First().EmailFornecedor);

            Email.To.Add(User.Identity.Name);

            Email.Body = MakeEmailBodyContent("Solicitamos Pedido de Cotação", User.Identity.Name);
            Email.IsBodyHtml = true;
            
            Email.SendEmail();

            string email = data.SeleccaoEntidades.Where(x => x.CodFornecedor == Cod).First().EmailFornecedor ?? "Fornecedor sem Email definido!";

            data.eReasonCode = 1;
            data.eMessage = "Email enviado com sucesso para:" + Environment.NewLine + email;

            data.EmailEnviado = true;
            data.UtilizadorModificacao = User.Identity.Name;
            DBConsultaMercado.Update(data);

            //Actualizar Tabela "Seleccao_Entidades", com Data de Envio Ao Fornecedor e com Utilizador Envio
            SeleccaoEntidadesView fornecedor = data.SeleccaoEntidades.Where(x => x.CodFornecedor == Cod).First();
            fornecedor.DataEnvioAoFornecedor = DateTime.Now;
            fornecedor.UtilizadorEnvio = User.Identity.Name;
            DBConsultaMercado.Update(DBConsultaMercado.CastSeleccaoEntidadesViewToDB(fornecedor));

            //Apaga o anexo criado no disco
            if (System.IO.Directory.Exists(Path.Combine(sWebRootFolder, sFileName)))
                System.IO.File.Delete(Path.Combine(sWebRootFolder, sFileName));

            return Json(data);
        }

        public static string MakeEmailBodyContent(string BodyText, string SenderName)
        {
            string Body = @"<html>" +
                                "<head>" +
                                    "<style>" +
                                        "table{border:0;} " +
                                        "td{width:600px; vertical-align: top;}" +
                                    "</style>" +
                                "</head>" +
                                "<body>" +
                                    "<table>" +
                                        "<tr><td>&nbsp;</td></tr>" +
                                        "<tr>" +
                                            "<td>" +
                                                "Exmos (as) Senhores (as)," +
                                            "</td>" +
                                        "</tr>" +
                                        "<tr><td>&nbsp;</td></tr>" +
                                        "<tr>" +
                                            "<td>" +
                                                BodyText +
                                            "</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                            "<td>" +
                                                "&nbsp;" +
                                            "</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                            "<td>" +
                                                "Com os melhores cumprimentos," +
                                            "</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                            "<td>" +
                                                SenderName +
                                            "</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                            "<td>" +
                                                "&nbsp;" +
                                            "</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                            "<td>" +
                                                "<i>SUCH - Serviço de Utilização Comum dos Hospitais</i>" +
                                            "</td>" +
                                        "</tr>" +
                                    "</table>" +
                                "</body>" +
                            "</html>";

            return Body;
        }

        private GenericResult CreateNAVPurchaseOrderFor(PurchOrderDTO purchOrder, string CM, string Obs)
        {
            GenericResult createPrePurchOrderResult = new GenericResult();

            //purchOrder.Purchaser_Code = User.Identity.Name;
            purchOrder.Purchaser_Code = string.IsNullOrEmpty(DBUserConfigurations.GetById(User.Identity.Name).EmployeeNo) ? "" : DBUserConfigurations.GetById(User.Identity.Name).EmployeeNo;

            Task<WSPurchaseInvHeader.Create_Result> createPurchaseHeaderTask = NAVPurchaseHeaderIntermService.CreateAsync_CM(purchOrder, configws, CM, Obs);
            createPurchaseHeaderTask.Wait();
            if (createPurchaseHeaderTask.IsCompletedSuccessfully)
            {
                createPrePurchOrderResult.ResultValue = createPurchaseHeaderTask.Result.WSPurchInvHeaderInterm.No;
                purchOrder.NAVPrePurchOrderId = createPrePurchOrderResult.ResultValue;

                bool createPurchaseLinesTask = NAVPurchaseLineService.CreateAndUpdateMultipleAsync(purchOrder, configws);
                if (createPurchaseLinesTask)
                {
                    try
                    {
                        /*
                         *  Swallow errors at this stage as they will be managed in NAV
                         */
                        //Task<WSGenericCodeUnit.FxCabimento_Result> createPurchOrderTask = WSGeneric.CreatePurchaseOrder(purchOrder.NAVPrePurchOrderId, configws);
                        //createPurchOrderTask.Start();
                        ////if (createPurchOrderTask.IsCompletedSuccessfully)
                        ////{
                        ////    createPrePurchOrderResult.CompletedSuccessfully = true;
                        ////}
                    }
                    catch (Exception ex) { }
                    createPrePurchOrderResult.CompletedSuccessfully = true;
                }
            }
            return createPrePurchOrderResult;
        }

        #region Linhas Consulta Mercado

        [HttpPost]
        public JsonResult CreateLinhaConsultaMercado([FromBody] LinhasConsultaMercadoView data)
        {
            bool result = false;
            try
            {
                LinhasConsultaMercado linhaConsultaMercado = new LinhasConsultaMercado
                {
                    CodActividade = data.CodActividade,
                    CodAreaFuncional = data.CodAreaFuncional,
                    CodCentroResponsabilidade = data.CodCentroResponsabilidade,
                    CodLocalizacao = data.CodLocalizacao,
                    CodProduto = data.CodProduto,
                    CodRegiao = data.CodRegiao,
                    CodUnidadeMedida = data.CodUnidadeMedida,
                    CriadoEm = DateTime.Now,
                    CriadoPor = User.Identity.Name,
                    CustoTotalObjectivo = data.CustoTotalObjectivo,
                    CustoTotalPrevisto = data.CustoTotalPrevisto,
                    CustoUnitarioObjectivo = data.CustoUnitarioObjectivo,
                    CustoUnitarioPrevisto = data.CustoUnitarioPrevisto,
                    DataEntregaPrevista = data.DataEntregaPrevista_Show != string.Empty ? DateTime.Parse(data.DataEntregaPrevista_Show) : (DateTime?)null,
                    Descricao = data.Descricao,
                    Descricao2 = data.Descricao2,
                    LinhaRequisicao = data.LinhaRequisicao,
                    ModificadoEm = data.ModificadoEm,
                    ModificadoPor = data.ModificadoPor,
                    NumConsultaMercado = data.NumConsultaMercado,
                    //linhaConsultaMercado.NumLinha = data.NumLinha;
                    NumProjecto = data.NumProjecto,
                    NumRequisicao = data.NumRequisicao,
                    Quantidade = data.Quantidade,
                    MercadoLocal = data.MercadoLocal
                };

                var dbCreateResult = DBConsultaMercado.Create(linhaConsultaMercado);

                if (dbCreateResult != null)
                    result = true;
                else
                    result = false;
            }
            catch (Exception ex)
            {
                //log
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult UpdateLinhaConsultaMercado([FromBody] LinhasConsultaMercadoView data)
        {
            bool result = false;
            try
            {
                if (!string.IsNullOrEmpty(data.CodProduto))
                {
                    NAVProductsViewModel PROD = DBNAV2017Products.GetAllProducts(_config.NAVDatabaseName, _config.NAVCompanyName, data.CodProduto).FirstOrDefault();

                    if (PROD != null)
                    {
                        data.Descricao = PROD.Name;
                        data.CodUnidadeMedida = PROD.MeasureUnit;
                    }
                }
                else
                {
                    data.Descricao = null;
                    data.CodUnidadeMedida = null;
                }

                if (data.Quantidade != null && data.CustoUnitarioPrevisto != null)
                    data.CustoTotalPrevisto = Math.Round((decimal)data.Quantidade * (decimal)data.CustoUnitarioPrevisto * 100) / 100;
                else
                    data.CustoTotalPrevisto = null;

                if (data.Quantidade != null && data.CustoUnitarioObjectivo != null)
                    data.CustoTotalObjectivo = Math.Round((decimal)data.Quantidade * (decimal)data.CustoUnitarioObjectivo * 100) / 100;
                else
                    data.CustoTotalObjectivo = null;

                LinhasConsultaMercado linhaConsultaMercado = DBConsultaMercado.CastLinhasConsultaMercadoViewToDB(data);
                
                linhaConsultaMercado.ModificadoEm = DateTime.Now;
                linhaConsultaMercado.ModificadoPor = User.Identity.Name;

                var dbUpdateResult = DBConsultaMercado.Update(linhaConsultaMercado);

                if (dbUpdateResult != null)
                    result = true;
                else
                    result = false;
            }
            catch (Exception ex)
            {
                //log
            }
            return Json(result);
        }


        [HttpPost]
        public JsonResult DeleteLinhaConsultaMercado([FromBody] LinhasConsultaMercado data)
        {
            bool result = false;
            try
            {
                var ctx = new SuchDBContext();

                int _Numero = data.NumLinha;
                LinhasConsultaMercado linhasConsultaMercado = ctx.LinhasConsultaMercado.Where(p => p.NumLinha == _Numero).FirstOrDefault();
                int? _NumeroLinhaRequisicao = linhasConsultaMercado.LinhaRequisicao;

                if (DBConsultaMercado.Delete(data) != null)
                {
                    LinhasRequisição linhasRequisição = ctx.LinhasRequisição.Where(x => x.NºLinha == _NumeroLinhaRequisicao).FirstOrDefault();
                    linhasRequisição.NºDeConsultaMercadoCriada = string.Empty;
                    linhasRequisição.CriarConsultaMercado = false;
                    ctx.LinhasRequisição.Update(linhasRequisição);
                    ctx.SaveChanges();

                    result = true;
                }
                else
                    result = false;
                
            }
            catch (Exception ex)
            {
                //log
            }
            return Json(result);
        }

        #endregion

        #region Selecção Entidades

        [HttpPost]
        public JsonResult CreateLinhaSeleccaoEntidade([FromBody] SeleccaoEntidadesView data)
        {
            bool result = false;
            try
            {

                string _Email = string.Empty;
                try
                {
                    _Email = DBNAV2017Vendor.GetVendor(_config.NAVDatabaseName, _config.NAVCompanyName).Where(x => x.No_ == data.CodFornecedor).First().Email;
                }
                catch
                {
                    _Email = string.Empty;
                }


                SeleccaoEntidades seleccaoEntidades = new SeleccaoEntidades
                {
                    CidadeFornecedor = null,
                    CodActividade = data.CodActividade,
                    CodFormaPagamento = data.CodFormaPagamento,
                    CodFornecedor = data.CodFornecedor,
                    CodTermosPagamento = data.CodTermosPagamento,
                    NomeFornecedor = data.NomeFornecedor,
                    NumConsultaMercado = data.NumConsultaMercado,
                    Preferencial = data.Preferencial,
                    Selecionado = true,
                    EmailFornecedor = _Email,
                    PrazoResposta = data.PrazoResposta,
                    Fase = 0
                };

                var dbCreateResult = DBConsultaMercado.Create(seleccaoEntidades);

                if (dbCreateResult != null)
                    result = true;
                else
                    result = false;
            }
            catch (Exception ex)
            {
                //log
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult UpdateLinhaSeleccaoEntidade([FromBody] SeleccaoEntidadesView data)
        {
            bool result = false;
            try
            {
                SeleccaoEntidades seleccaoEntidades = DBConsultaMercado.CastSeleccaoEntidadesViewToDB(data);

                string _Email = string.Empty;
                try
                {
                    _Email = DBNAV2017Vendor.GetVendor(_config.NAVDatabaseName, _config.NAVCompanyName).Where(x => x.No_ == data.CodFornecedor).First().Email;
                }
                catch
                {
                    _Email = string.Empty;
                }

                seleccaoEntidades.EmailFornecedor = _Email;

                //Verificar se a Data de Receção de Proposta é diferente da existente na BD
                SeleccaoEntidades verificacao_Data = DBConsultaMercado.GetSeleccaoEntidadesID(data.IdSeleccaoEntidades);

                if (verificacao_Data.DataRecepcaoProposta != data.DataRecepcaoProposta)
                {
                    seleccaoEntidades.DataRecepcaoProposta = DateTime.Now;
                    seleccaoEntidades.UtilizadorRecepcaoProposta = User.Identity.Name;
                }

                //Actualizar Data Resposta Esperada
                seleccaoEntidades.DataRespostaEsperada = seleccaoEntidades.DataEnvioAoFornecedor == null ? (DateTime?)null : seleccaoEntidades.DataEnvioAoFornecedor.Value.AddDays(seleccaoEntidades.PrazoResposta.HasValue ? seleccaoEntidades.PrazoResposta.Value : 0);

                //Actualizar Fase em que se encontra
                seleccaoEntidades.Fase = 0;
                if (seleccaoEntidades.DataEnvioAoFornecedor.HasValue)
                {
                    seleccaoEntidades.Fase = 1;
                }

                if (seleccaoEntidades.DataPedidoEsclarecimento.HasValue)
                {
                    seleccaoEntidades.Fase = 2;
                }
                if (seleccaoEntidades.DataRespostaEsclarecimento.HasValue)
                {
                    seleccaoEntidades.Fase = 1;
                }

                if (seleccaoEntidades.DataRespostaDoFornecedor.HasValue)
                {
                    seleccaoEntidades.Fase = 3;
                }

                if (seleccaoEntidades.NaoRespostaDoFornecedor.HasValue && seleccaoEntidades.NaoRespostaDoFornecedor.Value == true)
                {
                    seleccaoEntidades.Fase = 3;
                }

                if (seleccaoEntidades.DataEnvioPropostaArea.HasValue)
                {
                    seleccaoEntidades.Fase = 3;
                }

                if (seleccaoEntidades.DataRespostaArea.HasValue)
                {
                    seleccaoEntidades.Fase = 4;
                }

                var dbUpdateResult = DBConsultaMercado.Update(seleccaoEntidades);

                if (dbUpdateResult != null)
                    result = true;
                else
                    result = false;
            }
            catch (Exception ex)
            {
                //log
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult UpdateLinhaSeleccaoEntidade_DataRecepcaoProposta([FromBody] SeleccaoEntidadesView data)
        {
            bool result = false;
            try
            {
                SeleccaoEntidades seleccaoEntidades = DBConsultaMercado.CastSeleccaoEntidadesViewToDB(data);

                string _Email = string.Empty;
                try
                {
                    _Email = DBNAV2017Vendor.GetVendor(_config.NAVDatabaseName, _config.NAVCompanyName).Where(x => x.No_ == data.CodFornecedor).First().Email;
                }
                catch
                {
                    _Email = string.Empty;
                }

                seleccaoEntidades.EmailFornecedor = _Email;
                seleccaoEntidades.DataRecepcaoProposta = data.DataRecepcaoProposta_Show == "" ? (DateTime?)null : DateTime.Parse(data.DataRecepcaoProposta_Show);
                seleccaoEntidades.UtilizadorRecepcaoProposta = User.Identity.Name;

                var dbUpdateResult = DBConsultaMercado.Update(seleccaoEntidades);

                if (dbUpdateResult != null)
                    result = true;
                else
                    result = false;
            }
            catch (Exception ex)
            {
                //log
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult DeleteLinhaSeleccaoEntidade([FromBody] SeleccaoEntidades data)
        {
            bool result = false;
            try
            {
                if (DBConsultaMercado.Delete(data) != null)
                    result = true;
                else
                    result = false;

            }
            catch (Exception ex)
            {
                //log
            }
            return Json(result);
        }

        #endregion

        #region Registo de Proposta

        [HttpPost]
        public JsonResult UpdateLinhaRegistoProposta([FromBody] RegistoDePropostasView data)
        {
            bool result = false;
            try
            {
                RegistoDePropostas registoDePropostas = DBConsultaMercado.CastRegistoDePropostasViewToDB(data);

                var dbUpdateResult = DBConsultaMercado.Update(registoDePropostas);

                if (dbUpdateResult != null)
                    result = true;
                else
                    result = false;
            }
            catch (Exception ex)
            {
                //log
            }
            return Json(result);
        }


        #endregion

        #region ANEXOS

        [HttpPost]
        [Route("ConsultaMercado/FileUpload")]
        [Route("ConsultaMercado/FileUpload/{id}")]
        public JsonResult FileUpload(string id)
        {
            try
            {
                var files = Request.Form.Files;
                string full_filename;
                foreach (var file in files)
                {
                    try
                    {
                        string filename = Path.GetFileName(file.FileName);
                        //full_filename = id + "_" + filename;
                        full_filename = id + "_" + filename;
                        var path = Path.Combine(_generalConfig.FileUploadFolder + "ConsultasMercado\\", full_filename);
                        //var path = Path.Combine("E:\\Data\\eSUCH\\ConsultasMercado\\", full_filename);
                        using (FileStream dd = new FileStream(path, FileMode.CreateNew))
                        {
                            file.CopyTo(dd);
                            dd.Dispose();

                            Anexos newfile = new Anexos();
                            newfile.NºOrigem = id;
                            newfile.UrlAnexo = full_filename;

                            //TipoOrigem: 1-PréRequisição; 2-Requisição; 3-Contratos; 4-Procedimentos;5-ConsultaMercado
                            newfile.TipoOrigem = TipoOrigemAnexos.ConsultaMercado;

                            newfile.DataHoraCriação = DateTime.Now;
                            newfile.UtilizadorCriação = User.Identity.Name;

                            DBAttachments.Create(newfile);
                            if (newfile.NºLinha == 0)
                            {
                                System.IO.File.Delete(path);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Json("");
        }

        [HttpPost]
        public JsonResult LoadAttachments([FromBody] JObject requestParams)
        {
            string id = requestParams["id"].ToString();

            List<Anexos> list = DBAttachments.GetById(id);
            List<AttachmentsViewModel> attach = new List<AttachmentsViewModel>();
            list.ForEach(x => attach.Add(DBAttachments.ParseToViewModel(x)));
            return Json(attach);
        }

        [HttpGet]
        public FileStreamResult DownloadFile(string id)
        {
            return new FileStreamResult(new FileStream(_generalConfig.FileUploadFolder + "ConsultasMercado\\" + id, FileMode.Open), "application/xlsx");
            //return new FileStreamResult(new FileStream("E:\\Data\\eSUCH\\ConsultasMercado\\" + id, FileMode.Open), "application/xlsx");
        }

        [HttpPost]
        public JsonResult DeleteAttachments([FromBody] AttachmentsViewModel requestParams)
        {
            try
            {
                System.IO.File.Delete(_generalConfig.FileUploadFolder + "ConsultasMercado\\" + requestParams.Url);
                //System.IO.File.Delete("E:\\Data\\eSUCH\\ConsultasMercado\\" + requestParams.Url);
                DBAttachments.Delete(DBAttachments.ParseToDB(requestParams));
                requestParams.eReasonCode = 1;

            }
            catch (Exception ex)
            {
                requestParams.eReasonCode = 2;
                return Json(requestParams);
            }
            return Json(requestParams);
        }
        #endregion

        #region EXCEL

        //1
        [HttpPost]
        [RequestSizeLimit(100_000_000)]
        public async Task<JsonResult> ExportToExcel_ConsultaMercado([FromBody] List<ConsultaMercadoView> Lista)
        {
            JObject dp = (JObject)Lista[0].ColunasEXCEL;

            string sWebRootFolder = _generalConfig.FileUploadFolder + "ConsultasMercado\\" + "tmp\\";
            string user = User.Identity.Name;
            user = user.Replace("@", "_");
            user = user.Replace(".", "_");
            string sFileName = @"" + user + "_ExportEXCEL.xlsx";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            var memory = new MemoryStream();
            using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("Pedidos de Cotação");
                IRow row = excelSheet.CreateRow(0);
                int Col = 0;

                if (dp["numConsultaMercado"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Nº Consulta Mercado"); Col = Col + 1; }
                if (dp["codProjecto"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Nº Projecto"); Col = Col + 1; }
                //if (dp["descricao"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Descrição"); Col = Col + 1; }
                if (dp["codRegiao"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Cód. Região"); Col = Col + 1; }
                if (dp["codAreaFuncional"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Cód. Área Funcional"); Col = Col + 1; }
                if (dp["codCentroResponsabilidade"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Cód. Centro Responsabilidade"); Col = Col + 1; }
                //if (dp["codActividade"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Cód. Actividade"); Col = Col + 1; }
                //if (dp["dataPedidoCotacao"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Data Pedido Cotação"); Col = Col + 1; }
                //if (dp["fornecedorSelecionado"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Fornecedor Selecionado"); Col = Col + 1; }
                //if (dp["numDocumentoCompra"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Nº Doc. Compra"); Col = Col + 1; }
                //if (dp["codLocalizacao"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Cód. Localização"); Col = Col + 1; }
                //if (dp["filtroActividade"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Filtro Actividade"); Col = Col + 1; }
                //if (dp["valorPedidoCotacao"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Valor Pedido Cotação"); Col = Col + 1; }
                //if (dp["destino"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Destino"); Col = Col + 1; }
                //if (dp["estado"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Estado"); Col = Col + 1; }
                if (dp["utilizadorRequisicao"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Utilizador Requisição"); Col = Col + 1; }
                //if (dp["dataLimite"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Data Limite"); Col = Col + 1; }
                //if (dp["especificacaoTecnica"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Especificação Técnica"); Col = Col + 1; }
                //if (dp["fase"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Fase"); Col = Col + 1; }
                //if (dp["modalidade"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Modalidade"); Col = Col + 1; }
                //if (dp["pedidoCotacaoCriadoEm"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Pedido Cotação Criado Em"); Col = Col + 1; }
                //if (dp["pedidoCotacaoCriadoPor"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Pedido Cotação Criado Por"); Col = Col + 1; }
                //if (dp["consultaEm"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Consulta Em"); Col = Col + 1; }
                //if (dp["consultaPor"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Consulta Por"); Col = Col + 1; }
                //if (dp["negociacaoContratacaoEm"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Negociação Contratação Em"); Col = Col + 1; }
                //if (dp["negociacaoContratacaoPor"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Negociação Contratação Por"); Col = Col + 1; }
                //if (dp["adjudicacaoEm"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Adjudicação Em"); Col = Col + 1; }
                //if (dp["adjudicacaoPor"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Adjudicação Por"); Col = Col + 1; }
                if (dp["numRequisicao"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Nº Requisição"); Col = Col + 1; }
                //if (dp["pedidoCotacaoOrigem"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Pedido Cotação Origem"); Col = Col + 1; }
                //if (dp["valorAdjudicado"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Valor Adjudicado"); Col = Col + 1; }
                //if (dp["codFormaPagamento"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Cod. Forma Pagamento"); Col = Col + 1; }
                //if (dp["seleccaoEfectuada"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Selecção Efectuada"); Col = Col + 1; }
                //if (dp["numEncomenda"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Nº Encomenda"); Col = Col + 1; }
                if (dp["emailEnviado"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Email(s) Enviado(s)"); Col = Col + 1; }
                if (dp["regiaoMercadoLocal"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Região Mercado Local"); Col = Col + 1; }
                if (dp["dataEntregaFornecedor_Show"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Data Entrega Fornecedor"); Col = Col + 1; }
                if (dp["dataRecolha_Show"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Data Recolha"); Col = Col + 1; }
                if (dp["dataEntregaArmazem_Show"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Data Entrega no Armazém"); Col = Col + 1; }
                if (dp["codComprador"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Cód. Comprador"); Col = Col + 1; }
                if (dp["localEntrega"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Local Entrega"); Col = Col + 1; }
                if (dp["equipamento"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Equipamento"); Col = Col + 1; }
                if (dp["amostra"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Amostra"); Col = Col + 1; }
                if (dp["urgente"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Urgente"); Col = Col + 1; }
                if (dp["historico"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Histórico"); Col = Col + 1; }

                if (dp != null)
                {
                    int count = 1;
                    foreach (ConsultaMercadoView item in Lista)
                    {
                        Col = 0;
                        row = excelSheet.CreateRow(count);

                        if (dp["numConsultaMercado"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.NumConsultaMercado); Col = Col + 1; }
                        if (dp["codProjecto"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.CodProjecto); Col = Col + 1; }
                        //if (dp["descricao"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.Descricao); Col = Col + 1; }
                        if (dp["codRegiao"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.CodRegiao); Col = Col + 1; }
                        if (dp["codAreaFuncional"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.CodAreaFuncional); Col = Col + 1; }
                        if (dp["codCentroResponsabilidade"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.CodCentroResponsabilidade); Col = Col + 1; }
                        //if (dp["codActividade"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.CodActividade); Col = Col + 1; }
                        //if (dp["dataPedidoCotacao"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.DataPedidoCotacao.ToString()); Col = Col + 1; }
                        //if (dp["fornecedorSelecionado"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.FornecedorSelecionado); Col = Col + 1; }
                        //if (dp["numDocumentoCompra"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.NumDocumentoCompra); Col = Col + 1; }
                        //if (dp["codLocalizacao"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.CodLocalizacao); Col = Col + 1; }
                        //if (dp["filtroActividade"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.FiltroActividade); Col = Col + 1; }
                        //if (dp["valorPedidoCotacao"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.ValorPedidoCotacao.ToString()); Col = Col + 1; }
                        //if (dp["destino"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.Destino_Show.ToString()); Col = Col + 1; }
                        //if (dp["estado"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.Estado_Show); Col = Col + 1; }
                        if (dp["utilizadorRequisicao"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.UtilizadorRequisicao); Col = Col + 1; }
                        //if (dp["dataLimite"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.DataLimite.ToString()); Col = Col + 1; }
                        //if (dp["especificacaoTecnica"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.EspecificacaoTecnica.ToString()); Col = Col + 1; }
                        //if (dp["fase"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.Fase_Show); Col = Col + 1; }
                        //if (dp["modalidade"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.Modalidade_Show); Col = Col + 1; }
                        //if (dp["pedidoCotacaoCriadoEm"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.PedidoCotacaoCriadoEm.ToString()); Col = Col + 1; }
                        //if (dp["pedidoCotacaoCriadoPor"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.PedidoCotacaoCriadoPor); Col = Col + 1; }
                        //if (dp["consultaEm"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.ConsultaEm.ToString()); Col = Col + 1; }
                        //if (dp["consultaPor"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.ConsultaPor); Col = Col + 1; }
                        //if (dp["negociacaoContratacaoEm"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.NegociacaoContratacaoEm.ToString()); Col = Col + 1; }
                        //if (dp["negociacaoContratacaoPor"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.NegociacaoContratacaoPor); Col = Col + 1; }
                        //if (dp["adjudicacaoEm"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.AdjudicacaoEm.ToString()); Col = Col + 1; }
                        //if (dp["adjudicacaoPor"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.AdjudicacaoPor); Col = Col + 1; }
                        if (dp["numRequisicao"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.NumRequisicao); Col = Col + 1; }
                        //if (dp["pedidoCotacaoOrigem"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.PedidoCotacaoOrigem); Col = Col + 1; }
                        //if (dp["valorAdjudicado"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.ValorAdjudicado.ToString()); Col = Col + 1; }
                        //if (dp["codFormaPagamento"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.CodFormaPagamento); Col = Col + 1; }
                        //if (dp["seleccaoEfectuada"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.SeleccaoEfectuada.ToString()); Col = Col + 1; }
                        //if (dp["numEncomenda"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.NumEncomenda == null ? string.Empty : item.NumEncomenda.ToString()); Col = Col + 1; }
                        if (dp["emailEnviado"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.EmailEnviado.ToString()); Col = Col + 1; }
                        if (dp["regiaoMercadoLocal"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.RegiaoMercadoLocal == null ? string.Empty : item.RegiaoMercadoLocal.ToString()); Col = Col + 1; }
                        if (dp["dataEntregaFornecedor_Show"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.DataEntregaFornecedor.ToString()); Col = Col + 1; }
                        if (dp["dataRecolha_Show"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.DataRecolha.ToString()); Col = Col + 1; }
                        if (dp["dataEntregaArmazem_Show"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.DataEntregaArmazem.ToString()); Col = Col + 1; }
                        if (dp["codComprador"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.CodComprador == null ? string.Empty : item.CodComprador.ToString()); Col = Col + 1; }
                        if (dp["localEntrega"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.LocalEntrega_Show); Col = Col + 1; }
                        if (dp["equipamento"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.Equipamento.ToString()); Col = Col + 1; }
                        if (dp["amostra"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.Amostra.ToString()); Col = Col + 1; }
                        if (dp["urgente"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.Urgente.ToString()); Col = Col + 1; }
                        if (dp["historico"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.Historico.ToString()); Col = Col + 1; }
                        count++;
                    }
                }
                workbook.Write(fs);
            }
            using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return Json(sFileName);
        }
        //2
        public IActionResult ExportToExcelDownload_ConsultaMercado(string sFileName)
        {
            sFileName = _generalConfig.FileUploadFolder + "ConsultasMercado\\" + "tmp\\" + sFileName;
            //return File(sFileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Pedidos de Cotação.xlsx");
            return new FileStreamResult(new FileStream(sFileName, FileMode.Open), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        //1
        [HttpPost]
        [RequestSizeLimit(100_000_000)]
        public async Task<JsonResult> ExportToExcel_ConsultasPorFornecedor([FromBody] List<SeleccaoEntidadesView> Lista)
        {
            JObject dp = (JObject)Lista[0].ColunasEXCEL;

            string sWebRootFolder = _generalConfig.FileUploadFolder + "ConsultasMercado\\" + "tmp\\";
            string user = User.Identity.Name;
            user = user.Replace("@", "_");
            user = user.Replace(".", "_");
            string sFileName = @"" + user + "_ExportEXCEL.xlsx";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            var memory = new MemoryStream();
            using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("Consultas por Fornecedor");
                IRow row = excelSheet.CreateRow(0);
                int Col = 0;

                if (dp["numConsultaMercado"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Nº Consulta Mercado"); Col = Col + 1; }
                if (dp["codFornecedor"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Código Fornecedor"); Col = Col + 1; }
                if (dp["nomeFornecedor"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Nome Fornecedor"); Col = Col + 1; }
                //if (dp["selecionado_Show"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Selecionado"); Col = Col + 1; }
                //if (dp["preferencial_Show"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Preferencial"); Col = Col + 1; }
                //if (dp["emailFornecedor"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Email Fornecedor"); Col = Col + 1; }
                if (dp["dataEnvioAoFornecedor_Show"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Data Envio ao Fornecedor"); Col = Col + 1; }
                if (dp["dataRecepcaoProposta_Show"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Data Recepção Proposta"); Col = Col + 1; }
                if (dp["utilizadorEnvio"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Utilizador Envio"); Col = Col + 1; }
                //if (dp["utilizadorRecepcaoProposta"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Utilizador Recepção Proposta"); Col = Col + 1; }
                //if (dp["prazoResposta"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Prazo Resposta"); Col = Col + 1; }
                //if (dp["dataRespostaEsperada_Show"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Data Resposta Esperada"); Col = Col + 1; }
                if (dp["dataPedidoEsclarecimento_Show"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Data Pedido Esclarecimento"); Col = Col + 1; }
                if (dp["dataRespostaEsclarecimento_Show"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Data Resposta Esclarecimento"); Col = Col + 1; }
                if (dp["dataRespostaDoFornecedor_Show"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Data Resposta do Fornecedor"); Col = Col + 1; }
                if (dp["naoRespostaDoFornecedor_Show"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Não Resposta do Fornecedor"); Col = Col + 1; }
                if (dp["dataPedidoCotacaoCriadoEm_Show"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Pedido Cotação Criado Em"); Col = Col + 1; }
                //if (dp["custoTotalPrevisto"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Valor c / e / ou s/ IVA da Proposta"); Col = Col + 1; }
                if (dp["noRequisicao"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Nº Requisição"); Col = Col + 1; }
                if (dp["codRegiao"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Cód. Região"); Col = Col + 1; }
                if (dp["codArea"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Cód. Área"); Col = Col + 1; }
                if (dp["codCresp"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Cód. Cresp"); Col = Col + 1; }
                if (dp["dataEnvioPropostaArea_Show"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Data de Envio á Área"); Col = Col + 1; }
                if (dp["notaEncomenda_Show"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Nota de Encomenda"); Col = Col + 1; }
                if (dp["historico_Show"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Histórico"); Col = Col + 1; }

                if (dp != null)
                {
                    int count = 1;
                    foreach (SeleccaoEntidadesView item in Lista)
                    {
                        Col = 0;
                        row = excelSheet.CreateRow(count);

                        if (dp["numConsultaMercado"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.NumConsultaMercado); Col = Col + 1; }
                        if (dp["codFornecedor"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.CodFornecedor); Col = Col + 1; }
                        if (dp["nomeFornecedor"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.NomeFornecedor); Col = Col + 1; }
                        //if (dp["selecionado_Show"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.Selecionado_Show); Col = Col + 1; }
                        //if (dp["preferencial_Show"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.Preferencial_Show); Col = Col + 1; }
                        //if (dp["emailFornecedor"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.EmailFornecedor); Col = Col + 1; }
                        if (dp["dataEnvioAoFornecedor_Show"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.DataEnvioAoFornecedor_Show); Col = Col + 1; }
                        if (dp["dataRecepcaoProposta_Show"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.DataRecepcaoProposta_Show); Col = Col + 1; }
                        if (dp["utilizadorEnvio"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.UtilizadorEnvio); Col = Col + 1; }
                        //if (dp["utilizadorRecepcaoProposta"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.UtilizadorRecepcaoProposta); Col = Col + 1; }
                        //if (dp["prazoResposta"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.PrazoResposta.ToString()); Col = Col + 1; }
                        //if (dp["dataRespostaEsperada_Show"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.DataRespostaEsperada_Show); Col = Col + 1; }
                        if (dp["dataPedidoEsclarecimento_Show"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.DataPedidoEsclarecimento_Show); Col = Col + 1; }
                        if (dp["dataRespostaEsclarecimento_Show"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.DataRespostaEsclarecimento_Show); Col = Col + 1; }
                        if (dp["dataRespostaDoFornecedor_Show"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.DataRespostaDoFornecedor_Show); Col = Col + 1; }
                        if (dp["naoRespostaDoFornecedor_Show"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.NaoRespostaDoFornecedor_Show); Col = Col + 1; }
                        if (dp["dataPedidoCotacaoCriadoEm_Show"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.DataPedidoCotacaoCriadoEm_Show); Col = Col + 1; }
                        //if (dp["custoTotalPrevisto"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.CustoTotalPrevisto.ToString()); Col = Col + 1; }
                        if (dp["noRequisicao"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.NoRequisicao); Col = Col + 1; }
                        if (dp["codRegiao"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.CodRegiao); Col = Col + 1; }
                        if (dp["codArea"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.CodArea); Col = Col + 1; }
                        if (dp["codCresp"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.CodCresp); Col = Col + 1; }
                        if (dp["dataEnvioPropostaArea_Show"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.DataEnvioPropostaArea_Show); Col = Col + 1; }
                        if (dp["notaEncomenda_Show"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.NotaEncomenda_Show); Col = Col + 1; }
                        if (dp["historico_Show"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.Historico_Show); Col = Col + 1; }
                        count++;
                    }
                }
                workbook.Write(fs);
            }
            using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return Json(sFileName);
        }
        //2
        public IActionResult ExportToExcelDownload_ConsultasPorFornecedor(string sFileName)
        {
            sFileName = _generalConfig.FileUploadFolder + "ConsultasMercado\\" + "tmp\\" + sFileName;
            //return File(sFileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Consultas por Fornecedor.xlsx");
            return new FileStreamResult(new FileStream(sFileName, FileMode.Open), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
        #endregion

    }
}