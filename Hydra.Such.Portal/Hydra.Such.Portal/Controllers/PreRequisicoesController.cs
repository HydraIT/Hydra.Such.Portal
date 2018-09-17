﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Hydra.Such.Data.ViewModel.Compras;
using Hydra.Such.Data.Logic.ComprasML;
using Hydra.Such.Data.ViewModel;
using Hydra.Such.Data.Logic;
using Hydra.Such.Data.Database;
using Hydra.Such.Data.Logic.Request;
using Newtonsoft.Json.Linq;
using System.IO;
using Hydra.Such.Portal.Configurations;
using Microsoft.Extensions.Options;
using Hydra.Such.Data.ViewModel.ProjectView;
using Hydra.Such.Data.Logic.Project;
using Hydra.Such.Portal.Extensions;
using static Hydra.Such.Data.Enumerations;
using Hydra.Such.Data;
using Hydra.Such.Data.Logic.Approvals;
using Hydra.Such.Data.ViewModel.Approvals;
using Hydra.Such.Data.ViewModel.Projects;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Hydra.Such.Data.Logic.Viatura;

namespace Hydra.Such.Portal.Controllers
{
    public class PreRequisicoesController : Controller
    {
        private readonly GeneralConfigurations _config;
        private readonly NAVConfigurations _configNAV;
        private readonly IHostingEnvironment _hostingEnvironment;


        public PreRequisicoesController(IOptions<GeneralConfigurations> appSettings, IOptions<NAVConfigurations> appSettingsNAV, IHostingEnvironment _hostingEnvironment)
        {
            _config = appSettings.Value;
            _configNAV = appSettingsNAV.Value;
            this._hostingEnvironment = _hostingEnvironment;
        }

        public IActionResult Index()
        {
            UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Features.PréRequisições);
            if (UPerm != null && UPerm.Read.Value)
            {
                ViewBag.UploadURL = _config.FileUploadFolder;
                ViewBag.Area = 1;
                ViewBag.PreRequesitionNo = User.Identity.Name;
                ViewBag.UPermissions = UPerm;
                return View();
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        public IActionResult RequisicoesPendentes()
        {
            UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Features.Requisições);

            if (UPerm != null && UPerm.Read.Value)
            {
                ViewBag.Area = 1;
                ViewBag.UPermissions = UPerm;
                return View();
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        public JsonResult GetProjectDim([FromBody] string ProjectNo)
        {

            ProjectListItemViewModel result = new ProjectListItemViewModel();

            List<NAVProjectsViewModel> navList = DBNAV2017Projects.GetAll(_configNAV.NAVDatabaseName, _configNAV.NAVCompanyName, ProjectNo).ToList();
            NAVProjectsViewModel Project = navList.Where(x => x.No == ProjectNo).FirstOrDefault();

            if (Project != null)
            {
                result.RegionCode = Project.RegionCode != null ? Project.RegionCode : "";
                result.FunctionalAreaCode = Project.AreaCode != null ? Project.AreaCode : "";
                result.ResponsabilityCenterCode = Project.CenterResponsibilityCode != null ? Project.CenterResponsibilityCode : "";
            }
            else
            {
                result.RegionCode = "";
                result.FunctionalAreaCode = "";
                result.ResponsabilityCenterCode = "";
            }

            return Json(result);
        }

        public JsonResult GetProjetoNo([FromBody] string Matricula)
        {
            Viaturas viatura = new Viaturas();
            string ProjetoNo = "";

            if (!string.IsNullOrEmpty(Matricula))
            {
                viatura = DBViatura.GetByMatricula(Matricula);
                ProjetoNo = viatura.NoProjeto ?? "";
            }

            return Json(ProjetoNo);
        }

        public JsonResult GetPreReqList([FromBody] int Area)
        {
            List<PréRequisição> PreRequisition = null;
            PreRequisition = DBPreRequesition.GetAll(User.Identity.Name, Area);

            List<PreRequesitionsViewModel> result = new List<PreRequesitionsViewModel>();


            PreRequisition.ForEach(x => result.Add(DBPreRequesition.ParseToViewModel(x)));
            return Json(result);
        }
        [HttpPost]
        public JsonResult CleanPreRequesition([FromBody] JObject requestParams)
        {
            try
            {
                string PreRequesitionsNo = requestParams["PreRequesitionsNo"].ToString();
                PréRequisição PR = DBPreRequesition.GetByNo(PreRequesitionsNo);
                if (PR != null && !String.IsNullOrEmpty(PreRequesitionsNo))
                {
                    ConfigUtilizadores CU = DBUserConfigurations.GetById(User.Identity.Name);

                    PR.Área = PR.Área;
                    PR.CódigoRegião = CU.RegiãoPorDefeito ?? null;
                    PR.CódigoÁreaFuncional = CU.AreaPorDefeito ?? null;
                    PR.CódigoCentroResponsabilidade = CU.CentroRespPorDefeito ?? null;
                    PR.TipoRequisição = null;
                    PR.NºProjeto = null;
                    PR.Urgente = false;
                    PR.Amostra = false;
                    PR.Anexo = null;
                    PR.Imobilizado = false;
                    PR.CompraADinheiro = false;
                    PR.CódigoLocalRecolha = null;
                    PR.CódigoLocalEntrega = null;
                    PR.Observações = null;
                    PR.ModeloDePréRequisição = null;
                    PR.DataHoraModificação = DateTime.Now;
                    PR.UtilizadorModificação = User.Identity.Name;
                    PR.Exclusivo = false;
                    PR.JáExecutado = false;
                    PR.Equipamento = false;
                    PR.ReposiçãoDeStock = false;
                    PR.Reclamação = false;
                    PR.CódigoLocalização = null;
                    PR.CabimentoOrçamental = false;
                    PR.NºFuncionário = null;
                    PR.Viatura = null;
                    PR.NºRequesiçãoReclamada = null;
                    PR.ResponsávelCriação = null;
                    PR.ResponsávelAprovação = null;
                    PR.ResponsávelValidação = null;
                    PR.ResponsávelReceção = null;
                    PR.DataAprovação = null;
                    PR.DataValidação = null;
                    PR.DataReceção = null;
                    PR.UnidadeProdutivaAlimentação = null;
                    PR.RequisiçãoNutrição = null;
                    PR.RequisiçãoDetergentes = null;
                    PR.NºProcedimentoCcp = null;
                    PR.Aprovadores = null;
                    PR.MercadoLocal = null;
                    PR.RegiãoMercadoLocal = null;
                    PR.ReparaçãoComGarantia = null;
                    PR.Emm = null;
                    PR.DataEntregaArmazém = null;
                    PR.LocalDeRecolha = null;
                    PR.MoradaRecolha = null;
                    PR.Morada2Recolha = null;
                    PR.CodigoPostalRecolha = null;
                    PR.LocalidadeRecolha = null;
                    PR.ContatoRecolha = null;
                    PR.ResponsávelReceçãoRecolha = null;
                    PR.LocalEntrega = null;
                    PR.MoradaEntrega = null;
                    PR.Morada2Entrega = null;
                    PR.CódigoPostalEntrega = null;
                    PR.LocalidadeEntrega = null;
                    PR.ContatoEntrega = null;
                    PR.ResponsávelReceçãoReceção = null;
                    PR.NºFatura = null;
                    DBPreRequesition.Update(PR);


                    List<Anexos> AllAttachments = DBAttachments.GetById(PreRequesitionsNo);
                    if (AllAttachments.Count > 0)
                    {
                        foreach (Anexos Attach in AllAttachments)
                        {
                            DBAttachments.Delete(Attach);
                        }
                    }
                    return Json(PR);
                }
                else
                {
                    ErrorHandler data = new ErrorHandler();
                    data.eReasonCode = 2;
                    data.eMessage = "Ocorreu um erro inesperado ao limpar os campos.";
                    return Json(data);
                }


            }
            catch (Exception e)
            {
                ErrorHandler data = new ErrorHandler();
                data.eReasonCode = 2;
                data.eMessage = "Ocorreu um erro inesperado ao limpar os campos.";
                return Json(data);
            }


        }

        #region Pre Requesition Details
        [HttpPost]
        public JsonResult GetPreRequesitionDetails([FromBody] PreRequesitionsViewModel data)
        {
            if (data != null)
            {
                PreRequesitionsViewModel result = new PreRequesitionsViewModel();
                if (data.PreRequesitionsNo != "")
                {
                    PréRequisição PreRequisition = DBPreRequesition.GetByNo(data.PreRequesitionsNo);
                    result = DBPreRequesition.ParseToViewModel(PreRequisition);

                }
                return Json(result);

            }
            return Json(false);
        }

        public IActionResult PréRequisiçõesDetalhes(string PreRequesitionNo)
        {
            UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Features.PréRequisições);
            if (UPerm != null && UPerm.Read.Value)
            {

                ViewBag.PreRequesitionNo = PreRequesitionNo ?? "";
                ViewBag.UPermissions = UPerm;
                return View();
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error");
            }
        }
        #endregion

        #region Pre Requesition Lines
        [HttpPost]
        public JsonResult GetPreReqLines([FromBody] PreRequesitionsViewModel data)
        {
            if (data != null)
            {
                List<LinhasPréRequisição> PreRequesitionLines = DBPreRequesitionLines.GetAllByNo(data.PreRequesitionsNo);

                PreRequesitionLineHelperViewModel result = new PreRequesitionLineHelperViewModel
                {
                    PreRequisitionNo = data.PreRequesitionsNo,
                    Lines = new List<PreRequisitionLineViewModel>()
                };

                if (PreRequesitionLines != null)
                {
                    PreRequesitionLines.ForEach(x => result.Lines.Add(DBPreRequesitionLines.ParseToViewModel(x)));
                }
                return Json(result);
            }
            return Json(false);
        }

        [HttpPost]
        public JsonResult UpdateContractLines([FromBody] PreRequesitionLineHelperViewModel data)
        {
            try
            {
                if (data != null && data.Lines != null)
                {
                    List<LinhasPréRequisição> PreRequesitionLines = DBPreRequesitionLines.GetAllByNo(data.PreRequisitionNo);
                    List<LinhasPréRequisição> CLToDelete = PreRequesitionLines.Where(y => !data.Lines.Any(x => x.PreRequisitionLineNo == y.NºPréRequisição && x.LineNo == y.NºLinha)).ToList();

                    CLToDelete.ForEach(x => DBPreRequesitionLines.Delete(x));

                    data.Lines.ForEach(x =>
                    {
                        LinhasPréRequisição CLine = PreRequesitionLines.Where(y => x.PreRequisitionLineNo == y.NºPréRequisição && x.LineNo == y.NºLinha).FirstOrDefault();

                        if (CLine != null)
                        {
                            CLine.NºPréRequisição = x.PreRequisitionLineNo;
                            CLine.NºLinha = x.LineNo;
                            CLine.Tipo = x.Type;
                            CLine.Código = x.Code;
                            CLine.Descrição = x.Description;
                            CLine.Descrição2 = x.Description2;
                            CLine.CódigoLocalização = x.LocalCode;
                            CLine.CódigoUnidadeMedida = x.UnitMeasureCode;
                            CLine.QuantidadeARequerer = x.QuantityToRequire;
                            CLine.CódigoRegião = x.RegionCode;
                            CLine.CódigoÁreaFuncional = x.FunctionalAreaCode;
                            CLine.CódigoCentroResponsabilidade = x.CenterResponsibilityCode;
                            CLine.NºProjeto = x.ProjectNo;
                            CLine.DataHoraCriação = x.CreateDateTime != null && x.CreateDateTime != "" ? DateTime.Parse(x.CreateDateTime) : (DateTime?)null;
                            CLine.UtilizadorCriação = x.CreateUser;
                            CLine.DataHoraModificação = DateTime.Now;
                            CLine.UtilizadorModificação = User.Identity.Name;
                            CLine.QtdPorUnidadeMedida = x.QtyByUnitOfMeasure;
                            CLine.QuantidadeRequerida = x.QuantityRequired;
                            CLine.QuantidadePendente = x.QuantityPending;
                            CLine.CustoUnitário = x.UnitCost;
                            CLine.PreçoUnitárioVenda = x.SellUnityPrice;
                            CLine.ValorOrçamento = x.BudgetValue;
                            CLine.DataReceçãoEsperada = x.ExpectedReceivingDate != null && x.ExpectedReceivingDate != "" ? DateTime.Parse(x.ExpectedReceivingDate) : (DateTime?)null;
                            CLine.Faturável = x.Billable;
                            CLine.NºLinhaOrdemManutenção = x.MaintenanceOrderLineNo;
                            CLine.NºFuncionário = x.EmployeeNo;
                            CLine.Viatura = x.Vehicle;
                            CLine.NºFornecedor = x.SupplierNo;
                            CLine.CódigoProdutoFornecedor = x.SupplierProductCode;
                            CLine.LocalCompraDireta = x.ArmazemCDireta;
                            CLine.UnidadeProdutivaNutrição = x.UnitNutritionProduction;
                            CLine.NºCliente = x.CustomerNo;
                            CLine.NºEncomendaAberto = x.OpenOrderNo;
                            CLine.NºLinhaEncomendaAberto = x.OpenOrderLineNo;

                            DBPreRequesitionLines.Update(CLine);
                        }
                        else
                        {
                            x = DBPreRequesitionLines.ParseToViewModel(DBPreRequesitionLines.Create(DBPreRequesitionLines.ParseToDB(x)));
                        }
                    });

                    data.eReasonCode = 1;
                    data.eMessage = "Linhas de Pré-Requisição atualizadas com sucesso.";
                }
            }
            catch (Exception ex)
            {
                data.eReasonCode = 2;
                data.eMessage = "Ocorreu um erro ao atualizar as linhas de Pré-Requisição.";
                data.eMessages.Add(new TraceInformation(TraceType.Error, ex.ToString()));
            }
            return Json(data);
        }

        [HttpPost]
        public JsonResult DuplicarContractLines([FromBody] PreRequisitionLineViewModel linha)
        {
            ErrorHandler result = new ErrorHandler();
            result.eReasonCode = 0;
            result.eMessage = "Ocorreu um erro ao duplicar a linha.";

            try
            {
                if (linha != null && !string.IsNullOrEmpty(linha.PreRequisitionLineNo) && linha.LineNo > 0)
                {
                    LinhasPréRequisição LinhaOriginal = DBPreRequesitionLines.GetById(linha.PreRequisitionLineNo, linha.LineNo);
                    LinhasPréRequisição LinhaDuplicada = new LinhasPréRequisição();

                    LinhaDuplicada.NºPréRequisição = LinhaOriginal.NºPréRequisição;
                    LinhaDuplicada.Tipo = LinhaOriginal.Tipo;
                    LinhaDuplicada.Código = LinhaOriginal.Código;
                    LinhaDuplicada.Descrição = LinhaOriginal.Descrição;
                    LinhaDuplicada.CódigoLocalização = LinhaOriginal.CódigoLocalização;
                    LinhaDuplicada.CódigoUnidadeMedida = LinhaOriginal.CódigoUnidadeMedida;
                    LinhaDuplicada.QuantidadeARequerer = LinhaOriginal.QuantidadeARequerer;
                    LinhaDuplicada.CódigoRegião = LinhaOriginal.CódigoRegião;
                    LinhaDuplicada.CódigoÁreaFuncional = LinhaOriginal.CódigoÁreaFuncional;
                    LinhaDuplicada.CódigoCentroResponsabilidade = LinhaOriginal.CódigoCentroResponsabilidade;
                    LinhaDuplicada.NºProjeto = LinhaOriginal.NºProjeto;
                    LinhaDuplicada.DataHoraCriação = DateTime.Now;
                    LinhaDuplicada.UtilizadorCriação = User.Identity.Name;
                    LinhaDuplicada.DataHoraModificação = (DateTime?)null;
                    LinhaDuplicada.UtilizadorModificação = "";
                    LinhaDuplicada.Descrição2 = LinhaOriginal.Descrição2;
                    LinhaDuplicada.QtdPorUnidadeMedida = LinhaOriginal.QtdPorUnidadeMedida;
                    LinhaDuplicada.QuantidadeRequerida = LinhaOriginal.QuantidadeRequerida;
                    LinhaDuplicada.QuantidadePendente = LinhaOriginal.QuantidadePendente;
                    LinhaDuplicada.CustoUnitário = LinhaOriginal.CustoUnitário;
                    LinhaDuplicada.PreçoUnitárioVenda = LinhaOriginal.PreçoUnitárioVenda;
                    LinhaDuplicada.ValorOrçamento = LinhaOriginal.ValorOrçamento;
                    LinhaDuplicada.DataReceçãoEsperada = LinhaOriginal.DataReceçãoEsperada;
                    LinhaDuplicada.Faturável = LinhaOriginal.Faturável;
                    LinhaDuplicada.NºLinhaOrdemManutenção = LinhaOriginal.NºLinhaOrdemManutenção;
                    LinhaDuplicada.NºFuncionário = LinhaOriginal.NºFuncionário;
                    LinhaDuplicada.Viatura = LinhaOriginal.Viatura;
                    LinhaDuplicada.NºFornecedor = LinhaOriginal.NºFornecedor;
                    LinhaDuplicada.CódigoProdutoFornecedor = LinhaOriginal.CódigoProdutoFornecedor;
                    LinhaDuplicada.UnidadeProdutivaNutrição = LinhaOriginal.UnidadeProdutivaNutrição;
                    LinhaDuplicada.NºCliente = LinhaOriginal.NºCliente;
                    LinhaDuplicada.NºEncomendaAberto = LinhaOriginal.NºEncomendaAberto;
                    LinhaDuplicada.NºLinhaEncomendaAberto = LinhaOriginal.NºLinhaEncomendaAberto;
                    LinhaDuplicada.LocalCompraDireta = LinhaOriginal.LocalCompraDireta;

                    if (DBPreRequesitionLines.Create(LinhaDuplicada) != null)
                    {
                        result.eReasonCode = 1;
                        result.eMessage = "A duplicação da Linha com sucesso.";
                    }
                    else
                    {
                        result.eReasonCode = 2;
                        result.eMessage = "Ocorreu um erro ao criar a linha duplicada.";
                    }
                }
                else
                {
                    result.eReasonCode = 3;
                    result.eMessage = "Falta informação para duplicar a linha.";
                }
            }
            catch (Exception ex)
            {
                result.eReasonCode = 99;
                result.eMessage = "Ocorreu um erro.";

                return Json(result);
            }

            return Json(result);
        }

        [HttpPost]
        public JsonResult UpdateLinhaPreRequisicao([FromBody] PreRequisitionLineViewModel linha)
        {
            ErrorHandler result = new ErrorHandler();
            result.eReasonCode = 0;
            result.eMessage = "Ocorreu um erro ao atualizar a linha.";

            try
            {
                if (!string.IsNullOrEmpty(linha.LocalCode))
                {
                    if (!string.IsNullOrEmpty(linha.Code))
                    {
                        if (linha.QuantityToRequire > 0)
                        {
                            if (DBPreRequesitionLines.Update(DBPreRequesitionLines.ParseToDB(linha)) != null)
                            {
                                result.eReasonCode = 1;
                                result.eMessage = "Linha Atualizada com Sucesso.";

                            }
                            else
                            {
                                result.eReasonCode = 2;
                                result.eMessage = "Ocorreu um erro ao atualizar a linha.";
                            }
                        }
                        else
                        {
                            result.eReasonCode = 3;
                            result.eMessage = "A Qt. a Requerer tem que ser superior a 0.";
                        }
                    }
                    else
                    {
                        result.eReasonCode = 4;
                        result.eMessage = "É obrigatório preencher o Cód. Produto.";
                    }
                }
                else
                {
                    result.eReasonCode = 5;
                    result.eMessage = "É obrigatório preencher o Código Localização.";
                }
            }
            catch (Exception ex)
            {
                result.eReasonCode = 99;
                result.eMessage = "Ocorreu um erro.";

                return Json(result);
            }

            return Json(result);
        }

        [HttpPost]
        public JsonResult UpdateLinhaPreRequisicaoProduto([FromBody] PreRequisitionLineViewModel linha)
        {
            ErrorHandler result = new ErrorHandler();
            result.eReasonCode = 0;
            result.eMessage = "Ocorreu um erro ao atualizar a linha.";

            try
            {
                if (!string.IsNullOrEmpty(linha.LocalCode))
                {
                    if (!string.IsNullOrEmpty(linha.Code))
                    {
                        if (linha.QuantityToRequire > 0)
                        {
                            NAVProductsViewModel PRODUTO = DBNAV2017Products.GetAllProducts(_configNAV.NAVDatabaseName, _configNAV.NAVCompanyName, linha.Code).FirstOrDefault();

                            linha.Description = PRODUTO.Name;
                            linha.Description2 = PRODUTO.Name2;
                            linha.UnitMeasureCode = PRODUTO.MeasureUnit;

                            if (DBPreRequesitionLines.Update(DBPreRequesitionLines.ParseToDB(linha)) != null)
                            {
                                result.eReasonCode = 1;
                                result.eMessage = "Linha Atualizada com Sucesso.";

                            }
                            else
                            {
                                result.eReasonCode = 2;
                                result.eMessage = "Ocorreu um erro ao atualizar a linha.";
                            }
                        }
                        else
                        {
                            result.eReasonCode = 3;
                            result.eMessage = "A Qt. a Requerer tem que ser superior a 0.";
                        }
                    }
                    else
                    {
                        result.eReasonCode = 4;
                        result.eMessage = "É obrigatório preencher o Cód. Produto.";
                    }
                }
                else
                {
                    result.eReasonCode = 5;
                    result.eMessage = "É obrigatório preencher o Código Localização.";
                }
            }
            catch (Exception ex)
            {
                result.eReasonCode = 99;
                result.eMessage = "Ocorreu um erro.";

                return Json(result);
            }

            return Json(result);
        }
        #endregion

        #region CRUD

        [HttpPost]
        public JsonResult CreateNewForThisUser([FromBody] JObject requestParams)

        {
            int AreaNo = int.Parse(requestParams["areaid"].ToString());
            string pPreRequisicao = DBPreRequesition.GetByNoAndArea(User.Identity.Name, AreaNo);
            ConfigUtilizadores CU = DBUserConfigurations.GetById(User.Identity.Name);

            if (pPreRequisicao != null)
            {
                PreRequesitionsViewModel reqID = new PreRequesitionsViewModel();
                reqID.PreRequesitionsNo = pPreRequisicao;
                reqID.RegionCode = CU.RegiãoPorDefeito;
                reqID.FunctionalAreaCode = CU.AreaPorDefeito;
                reqID.ResponsabilityCenterCode = CU.CentroRespPorDefeito;

                return Json(reqID);
            }
            else
            {


                PréRequisição createNew = new PréRequisição();
                createNew.CódigoCentroResponsabilidade = CU.CentroRespPorDefeito;
                createNew.CódigoRegião = CU.RegiãoPorDefeito;
                createNew.CódigoÁreaFuncional = CU.AreaPorDefeito;
                createNew.NºPréRequisição = User.Identity.Name;
                createNew.Área = AreaNo;
                createNew.UtilizadorCriação = User.Identity.Name;
                createNew.DataHoraCriação = DateTime.Now;
                DBPreRequesition.Create(createNew);

                PreRequesitionsViewModel reqID = new PreRequesitionsViewModel();
                reqID.PreRequesitionsNo = createNew.NºPréRequisição;
                reqID.RegionCode = createNew.CódigoRegião;
                reqID.FunctionalAreaCode = createNew.CódigoÁreaFuncional;
                reqID.ResponsabilityCenterCode = createNew.CódigoCentroResponsabilidade;
                return Json(reqID);
            }


        }

        [HttpPost]
        public JsonResult CreatePreRequesition([FromBody] PreRequesitionsViewModel data)
        {
            try
            {
                if (data != null)
                {
                    //Get Numeration
                    bool autoGenId = false;
                    Configuração conf = DBConfigurations.GetById(1);
                    int entityNumerationConfId = conf.NumeraçãoPréRequisições.Value;

                    if (data.PreRequesitionsNo == "" || data.PreRequesitionsNo == null)
                    {
                        autoGenId = true;
                        data.PreRequesitionsNo = DBNumerationConfigurations.GetNextNumeration(entityNumerationConfId, autoGenId, false);
                    }
                    if (data.PreRequesitionsNo != null)
                    {
                        PréRequisição pPreRequisicao = DBPreRequesition.ParseToDB(data);
                        pPreRequisicao.UtilizadorCriação = User.Identity.Name;
                        pPreRequisicao.DataHoraCriação = DateTime.Now;

                        //Create Contract On Database

                        pPreRequisicao = DBPreRequesition.Create(pPreRequisicao);

                        if (pPreRequisicao == null)
                        {
                            data.eReasonCode = 3;
                            data.eMessage = "Ocorreu um erro ao criar o contrato.";
                        }
                        else
                        {
                            ConfiguraçãoNumerações configNumerations = DBNumerationConfigurations.GetById(entityNumerationConfId);
                            if (configNumerations != null && autoGenId)
                            {
                                configNumerations.ÚltimoNºUsado = data.PreRequesitionsNo;
                                configNumerations.UtilizadorModificação = User.Identity.Name;
                                DBNumerationConfigurations.Update(configNumerations);
                            }
                            data.eReasonCode = 1;
                        }
                    }
                    else
                    {
                        data.eReasonCode = 5;
                        data.eMessage = "A numeração configurada não é compativel com a inserida.";
                    }
                }
            }
            catch (Exception ex)
            {
                data.eReasonCode = 4;
                data.eMessage = "Ocorreu um erro ao criar o contrato";
            }
            return Json(data);

        }

        [HttpPost]
        public JsonResult UpdatePreRequesition([FromBody] PreRequesitionsViewModel data)
        {
            try
            {
                if (data != null)
                {
                    if (data.PreRequesitionsNo != null)
                    {
                        //Contratos cContract = DBContracts.ParseToDB(data);
                        PréRequisição PreRequisicaoDB = DBPreRequesition.GetByNo(data.PreRequesitionsNo);


                        if (PreRequisicaoDB != null)
                        {
                            PreRequisicaoDB.NºPréRequisição = data.PreRequesitionsNo;
                            PreRequisicaoDB.Área = data.Area;
                            PreRequisicaoDB.TipoRequisição = data.RequesitionType;
                            PreRequisicaoDB.NºProjeto = data.ProjectNo;
                            PreRequisicaoDB.CódigoRegião = data.RegionCode;
                            PreRequisicaoDB.CódigoÁreaFuncional = data.FunctionalAreaCode;
                            PreRequisicaoDB.CódigoCentroResponsabilidade = data.ResponsabilityCenterCode;
                            PreRequisicaoDB.Urgente = data.Urgent;
                            PreRequisicaoDB.Amostra = data.Sample;
                            PreRequisicaoDB.Anexo = data.Attachment;
                            PreRequisicaoDB.Imobilizado = data.Immobilized;
                            PreRequisicaoDB.CompraADinheiro = data.MoneyBuy;
                            PreRequisicaoDB.CódigoLocalRecolha = data.LocalCollectionCode;
                            PreRequisicaoDB.CódigoLocalEntrega = data.LocalDeliveryCode;
                            PreRequisicaoDB.Observações = data.Notes;
                            PreRequisicaoDB.ModeloDePréRequisição = data.PreRequesitionModel;
                            PreRequisicaoDB.DataHoraCriação = data.CreateDateTime;
                            PreRequisicaoDB.UtilizadorCriação = data.CreateUser;
                            PreRequisicaoDB.DataHoraModificação = data.UpdateDateTime;
                            PreRequisicaoDB.UtilizadorModificação = data.UpdateUser;
                            PreRequisicaoDB.Exclusivo = data.Exclusive;
                            PreRequisicaoDB.JáExecutado = data.AlreadyExecuted;
                            PreRequisicaoDB.Equipamento = data.Equipment;
                            PreRequisicaoDB.ReposiçãoDeStock = data.StockReplacement;
                            PreRequisicaoDB.Reclamação = data.Complaint;
                            PreRequisicaoDB.CódigoLocalização = data.LocationCode;
                            PreRequisicaoDB.CabimentoOrçamental = data.FittingBudget;
                            PreRequisicaoDB.NºFuncionário = data.EmployeeNo;
                            PreRequisicaoDB.Viatura = data.Vehicle;
                            PreRequisicaoDB.NºRequesiçãoReclamada = data.ClaimedRequesitionNo;
                            PreRequisicaoDB.ResponsávelCriação = data.CreateResponsible;
                            PreRequisicaoDB.ResponsávelAprovação = data.ApprovalResponsible;
                            PreRequisicaoDB.ResponsávelValidação = data.ValidationResponsible;
                            PreRequisicaoDB.ResponsávelReceção = data.ReceptionResponsible;
                            PreRequisicaoDB.DataAprovação = data.ApprovalDate;
                            PreRequisicaoDB.DataValidação = data.ValidationDate;
                            PreRequisicaoDB.DataReceção = data.ReceptionDate != "" && data.ReceptionDate != null ? DateTime.Parse(data.ReceptionDate) : (DateTime?)null;
                            PreRequisicaoDB.UnidadeProdutivaAlimentação = data.FoodProductionUnit;
                            PreRequisicaoDB.RequisiçãoNutrição = data.NutritionRequesition;
                            PreRequisicaoDB.RequisiçãoDetergentes = data.DetergentsRequisition;
                            PreRequisicaoDB.NºProcedimentoCcp = data.CCPProcedureNo;
                            PreRequisicaoDB.Aprovadores = data.Approvers;
                            PreRequisicaoDB.MercadoLocal = data.LocalMarket;
                            PreRequisicaoDB.RegiãoMercadoLocal = data.LocalMarketRegion;
                            PreRequisicaoDB.ReparaçãoComGarantia = data.WarrantyRepair;
                            PreRequisicaoDB.Emm = data.EMM;
                            PreRequisicaoDB.DataEntregaArmazém = data.DeliveryWarehouseDate != "" && data.DeliveryWarehouseDate != null ? DateTime.Parse(data.DeliveryWarehouseDate) : (DateTime?)null;
                            PreRequisicaoDB.LocalDeRecolha = data.CollectionLocal;
                            PreRequisicaoDB.MoradaRecolha = data.CollectionAddress;
                            PreRequisicaoDB.Morada2Recolha = data.CollectionAddress2;
                            PreRequisicaoDB.CodigoPostalRecolha = data.CollectionPostalCode;
                            PreRequisicaoDB.LocalidadeRecolha = data.CollectionLocality;
                            PreRequisicaoDB.ContatoRecolha = data.CollectionContact;
                            PreRequisicaoDB.ResponsávelReceçãoRecolha = data.CollectionReceptionResponsible;
                            PreRequisicaoDB.LocalEntrega = data.DeliveryLocal;
                            PreRequisicaoDB.MoradaEntrega = data.DeliveryAddress;
                            PreRequisicaoDB.Morada2Entrega = data.DeliveryAddress2;
                            PreRequisicaoDB.CódigoPostalEntrega = data.DeliveryPostalCode;
                            PreRequisicaoDB.LocalidadeEntrega = data.DeliveryLocality;
                            PreRequisicaoDB.ContatoEntrega = data.DeliveryContact;
                            PreRequisicaoDB.ResponsávelReceçãoReceção = data.ReceptionReceptionResponsible;
                            PreRequisicaoDB.NºFatura = data.InvoiceNo;
                            PreRequisicaoDB.PedirOrcamento = data.PedirOrcamento;

                            PreRequisicaoDB = DBPreRequesition.Update(PreRequisicaoDB);
                        }
                        data.eReasonCode = 1;
                        data.eMessage = "Contrato atualizado com sucesso.";
                    }
                }
            }
            catch (Exception ex)
            {
                data.eReasonCode = 2;
                data.eMessage = "Ocorreu um erro ao atualizar o contrato.";
            }
            return Json(data);

        }

        [HttpPost]
        public JsonResult DeletePreRequesition([FromBody] PreRequesitionsViewModel data)
        {
            ErrorHandler result = new ErrorHandler();
            try
            {
                if (data != null)
                {
                    //Verify if contract have Invoices Or Projects
                    List<LinhasPréRequisição> lines = DBPreRequesitionLines.GetAllByNo(data.PreRequesitionsNo);
                    foreach (var linestodelete in lines)
                    {
                        DBPreRequesitionLines.Delete(linestodelete);
                    }
                    // Delete Contract 
                    DBPreRequesition.DeleteByPreRequesitionNo(data.PreRequesitionsNo);

                    result.eReasonCode = 1;
                    result.eMessage = "Pré-Requisição eliminada com sucesso.";
                    //}

                }
            }
            catch (Exception ex)
            {
                result.eReasonCode = 2;
                result.eMessage = "Ocorreu um erro ao eliminar a Pré-Requisição.";
            }
            return Json(result);

        }
        #endregion

        #region Populate CB
        public JsonResult CBVehicleFleetBool([FromBody] int id)
        {
            //bool FleetBool = false;
            //var item = DBRequesitionType.GetById(id);
            //if (item != null)
            //    FleetBool = item.Frota.HasValue ? item.Frota.Value : false;
            return Json(true);
        }

        public JsonResult GetPlaceData([FromBody] int placeId)
        {
            PlacesViewModel PlacesData = DBPlaces.ParseToViewModel(DBPlaces.GetById(placeId));
            return Json(PlacesData);
        }

        public JsonResult GetPurchaseHeader([FromBody] string respcenter)
        {
            int DimValueID = DBNAV2017DimensionValues.GetById(_configNAV.NAVDatabaseName, _configNAV.NAVCompanyName, 3, User.Identity.Name, respcenter).FirstOrDefault().DimValueID;
            List<DDMessageString> result = DBNAV2017EncomendaAberto.GetByDimValue(_configNAV.NAVDatabaseName, _configNAV.NAVCompanyName, DimValueID).Select(x => new DDMessageString()
            {
                id = x.Code
            }).GroupBy(x => new
            {
                x.id
            }).Select(x => new DDMessageString
            {
                id = x.Key.id
            }).ToList();

            return Json(result);
        }

        #endregion

        #region Numeração Pré-Requisição
        [HttpPost]
        public JsonResult ValidateNumeration([FromBody] PreRequesitionsViewModel data)
        {
            //Get Project Numeration
            Configuração Cfg = DBConfigurations.GetById(1);
            int ProjectNumerationConfigurationId = 0;
            ProjectNumerationConfigurationId = Cfg.NumeraçãoPréRequisições.Value;

            ConfiguraçãoNumerações CfgNumeration = DBNumerationConfigurations.GetById(ProjectNumerationConfigurationId);

            //Validate if ProjectNo is valid
            if (!(data.PreRequesitionsNo == "" || data.PreRequesitionsNo == null) && !CfgNumeration.Manual.Value)
            {
                return Json("A numeração configurada para pré-requisições não permite inserção manual.");
            }
            else if (data.PreRequesitionsNo == "" && !CfgNumeration.Automático.Value)
            {
                return Json("É obrigatório inserir o Nº Pré-Requisição.");
            }

            return Json("");
        }
        #endregion

        #region Requesition Model Lines

        public IActionResult RequisiçõesModeloLista(string id)
        {
            UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Features.PréRequisições);
            if (UPerm != null && UPerm.Read.Value)
            {
                ViewBag.PreReqNo = id;
                ViewBag.UPermissions = UPerm;
                return View();
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        public JsonResult GetReqModelList()
        {
            List<Requisição> RequisitionModel = null;
            RequisitionModel = DBRequestTemplates.GetAll();


            List<RequisitionViewModel> result = new List<RequisitionViewModel>();

            RequisitionModel.ForEach(x => result.Add(DBRequest.ParseToViewModel(x)));
            return Json(result);
        }

        public JsonResult CopyReqModelLines([FromBody] RequisitionViewModel req, string id)
        {
            ErrorHandler result = new ErrorHandler()
            {
                eMessage = "Ocorreu um erro ao copiar as linhas",
                eReasonCode = 2,
            };
            Projetos project = null;
            if (!string.IsNullOrEmpty(req.ProjectNo))
            {
                project = DBProjects.GetById(req.ProjectNo);
            }

            List<RequisitionLineViewModel> reqLines = DBRequestLine.GetByRequisitionId(req.RequisitionNo).ParseToViewModel();

            if (reqLines != null)
            {
                if (reqLines.Count > 0)
                {
                    reqLines.UpdateAgreedPrices();

                    List<LinhasPréRequisição> preReqLines = new List<LinhasPréRequisição>();
                    reqLines.ForEach(x =>
                    {
                        LinhasPréRequisição newline = new LinhasPréRequisição();

                        newline.NºPréRequisição = req.PreRequisitionNo;
                        newline.CódigoLocalização = x.LocalCode;
                        newline.CódigoProdutoFornecedor = x.SupplierProductCode;
                        newline.Código = x.Code;
                        newline.Descrição = x.Description;
                        newline.Descrição2 = x.Description2;
                        newline.CódigoUnidadeMedida = x.UnitMeasureCode;
                        newline.QuantidadeARequerer = x.QuantityToRequire;
                        newline.CustoUnitário = x.UnitCost;
                        newline.NºLinhaOrdemManutenção = x.MaintenanceOrderLineNo;
                        newline.Viatura = x.Vehicle;
                        newline.NºFornecedor = x.SupplierNo;
                        newline.NºEncomendaAberto = x.OpenOrderNo;
                        newline.NºLinhaEncomendaAberto = x.OpenOrderLineNo;
                        newline.NºProjeto = x.ProjectNo;
                        if (project != null)
                        {
                            newline.CódigoRegião = project.CódigoRegião;
                            newline.CódigoÁreaFuncional = project.CódigoÁreaFuncional;
                            newline.CódigoCentroResponsabilidade = project.CódigoCentroResponsabilidade;
                        }
                        else
                        {
                            newline.CódigoRegião = x.RegionCode;
                            newline.CódigoÁreaFuncional = x.FunctionalAreaCode;
                            newline.CódigoCentroResponsabilidade = x.CenterResponsibilityCode;
                        }
                        preReqLines.Add(newline);
                    });

                    if (DBPreRequesitionLines.CreateMultiple(preReqLines))
                    {
                        result.eReasonCode = 1;
                        result.eMessage = "Linhas copiadas com sucesso.";
                    }
                    else
                    {
                        result.eReasonCode = 2;
                        result.eMessage = "Ocorreu um erro ao copiar as linhas";
                    }
                }
                else
                {
                    result.eReasonCode = 2;
                    result.eMessage = "O modelo de requisição não tem linhas";
                }
            }
            else
            {
                result.eReasonCode = 2;
                result.eMessage = "Ocorreu um erro ao obter as linhas do modelo.";
            }
            return Json(result);
        }

        public JsonResult GetPendingReq()
        {
            List<Requisição> requisition = null;
            //List<RequisitionStates> states = new List<RequisitionStates>()
            //{
            //    RequisitionStates.Pending,
            //    RequisitionStates.Rejected
            //};
            //requisition = DBRequest.GetReqByUserAreaStatus(User.Identity.Name, states);
            requisition = DBRequest.GetReqByUser(User.Identity.Name);
            List<RequisitionViewModel> result = new List<RequisitionViewModel>();
            List<ApprovalMovementsViewModel> AproveList = DBApprovalMovements.ParseToViewModel(DBApprovalMovements.GetAllAssignedToUserFilteredByStatus(User.Identity.Name, 1));
            if (requisition != null)
            {
                requisition.ForEach(x => result.Add(x.ParseToViewModel()));
                if (result.Count > 0)
                {
                    foreach (RequisitionViewModel item in result)
                    {
                        if (item.State == RequisitionStates.Pending || item.State == RequisitionStates.Rejected)
                        {
                            item.SentReqToAprove = true;
                        }
                        else
                        {
                            item.SentReqToAprove = false;
                        }

                        if(item.ApprovalDate != null)
                        {
                            item.ApprovalDateString = item.ApprovalDate.Value.ToString("yyyy-MM-dd");
                        }
                        
                        item.LocalCode = DBRequestLine.GetByRequisitionId(item.RequisitionNo).FirstOrDefault()?.CódigoLocalização;
                    }
                    if (AproveList != null && AproveList.Count > 0)
                    {
                        foreach (ApprovalMovementsViewModel apmov in AproveList)
                        {
                            foreach (RequisitionViewModel req in result)
                            {
                                if (apmov.Number == req.RequisitionNo)
                                {
                                    req.SentReqToAprove = false;
                                }
                            }
                        }
                    }
                }

            }
            return Json(result.OrderByDescending(x => x.RequisitionNo));
        }

        public JsonResult GetPendingReqLines([FromBody] JObject requestParams)
        {
            string ReqNo = requestParams["ReqNo"].ToString();

            List<LinhasRequisição> RequisitionLines = null;
            RequisitionLines = DBRequestLine.GetByRequisitionId(ReqNo);

            List<RequisitionLineViewModel> result = new List<RequisitionLineViewModel>();

            RequisitionLines.ForEach(x => result.Add(DBRequestLine.ParseToViewModel(x)));
            return Json(result);

        }

        public JsonResult GetHistoryReq()
        {
            List<RequisiçãoHist> requisition = null;
            List<RequisitionStates> states = new List<RequisitionStates>()
            {
                RequisitionStates.Archived,
            };
            requisition = DBRequesitionHist.GetReqByUserAreaStatus(User.Identity.Name, states);

            List<RequisitionHistViewModel> result = new List<RequisitionHistViewModel>();

            requisition.ForEach(x => result.Add(DBRequesitionHist.ParseToViewModel(x)));

            return Json(result);
        }

        public JsonResult GetHistoryReqLines([FromBody] JObject requestParams)
        {
            string ReqNo = requestParams["ReqNo"].ToString();

            List<LinhasRequisiçãoHist> RequisitionLines = null;
            RequisitionLines = DBRequesitionLinesHist.GetByRequisitionId(ReqNo);

            List<RequisitionLineHistViewModel> result = new List<RequisitionLineHistViewModel>();

            RequisitionLines.ForEach(x => result.Add(DBRequesitionLinesHist.ParseToViewModel(x)));
            return Json(result);

        }
        #endregion

        #region Requisition

        [HttpPost]
        public JsonResult CreateRequesition([FromBody] PreRequesitionsViewModel data)
        {
            try
            {
                List<string> AllRequesitionIds = new List<string>();
                if (data != null)
                {
                    List<LinhasPréRequisição> PreRequesitionLines = DBPreRequesitionLines.GetAllByNo(data.PreRequesitionsNo);
                    data.eMessage = "";
                    if (PreRequesitionLines.Count > 0)
                    {

                        if (data.Complaint == true && (data.ClaimedRequesitionNo == "" || data.ClaimedRequesitionNo == null))
                        {
                            data.eReasonCode = 2;
                            data.eMessage = "O campo Nº Requisição Reclamada deve ser preenchido.";
                            return Json(data);
                        }

                        if (data.MoneyBuy == true)
                        {
                            if (PreRequesitionLines != null)
                            {
                                foreach (var lines in PreRequesitionLines)
                                {
                                    if (lines.CustoUnitário == null || (lines.NºFornecedor == null || lines.NºFornecedor == ""))
                                    {
                                        data.eReasonCode = 3;
                                        data.eMessage = "Os campos Custo Unitário e Nº Fornecedor das linhas devem ser todos preenchidos.";
                                        return Json(data);
                                    }
                                }
                            }
                        }

                        if (data.Equipment == true)
                        {
                            if (data.CollectionLocal == null || String.IsNullOrEmpty(data.CollectionAddress) || String.IsNullOrEmpty(data.CollectionPostalCode) || String.IsNullOrEmpty(data.CollectionLocality) || String.IsNullOrEmpty(data.CollectionContact) || String.IsNullOrEmpty(data.CollectionReceptionResponsible))
                            {
                                data.eReasonCode = 4;
                                data.eMessage = "Os campos de Recolha devem ser todos preenchidos.";
                                return Json(data);
                            }
                            else if (data.DeliveryLocal == null || String.IsNullOrEmpty(data.DeliveryAddress) || String.IsNullOrEmpty(data.DeliveryPostalCode) || String.IsNullOrEmpty(data.DeliveryLocality) || String.IsNullOrEmpty(data.CollectionReceptionResponsible) || String.IsNullOrEmpty(data.InvoiceNo))
                            {
                                data.eReasonCode = 4;
                                data.eMessage = "Os campos de Entrega (Fornecedor) devem ser todos preenchidos.";
                                return Json(data);
                            }
                        }

                        //Get VATPostingGroup Info
                        List<string> productsInRequisitionIds = PreRequesitionLines.Select(y => y.Código).Distinct().ToList();
                        var productsInRequisition = DBNAV2017Products.GetProductsById(_configNAV.NAVDatabaseName, _configNAV.NAVCompanyName, productsInRequisitionIds);
                        var vendors = DBNAV2017Vendor.GetVendor(_configNAV.NAVDatabaseName, _configNAV.NAVCompanyName);

                        List<PreRequisitionLineViewModel> GroupedListOpenOrderLine = new List<PreRequisitionLineViewModel>();
                        PreRequesitionLines.Where(x => x.NºLinhaEncomendaAberto.HasValue).ToList().ForEach(x => GroupedListOpenOrderLine.Add(DBPreRequesitionLines.ParseToViewModel(x)));

                        List<RequisitionViewModel> newlistOpenOrder = GroupedListOpenOrderLine.GroupBy(
                            x => x.OpenOrderNo,
                            x => x,
                            (key, items) => new RequisitionViewModel
                            {
                                RequestReclaimNo = data.ClaimedRequesitionNo,
                                Urgent = data.Urgent,
                                Area = data.Area,
                                Immobilized = data.Immobilized,
                                Exclusive = data.Exclusive,
                                AlreadyPerformed = data.AlreadyExecuted,
                                Sample = data.Sample,
                                Equipment = data.Equipment,
                                BuyCash = data.MoneyBuy,
                                StockReplacement = data.StockReplacement,
                                Reclamation = data.Complaint,
                                RegionCode = data.RegionCode,
                                FunctionalAreaCode = data.FunctionalAreaCode,
                                CenterResponsibilityCode = data.ResponsabilityCenterCode,
                                Vehicle = data.Vehicle,
                                ProjectNo = data.ProjectNo,
                                ReceivedDate = data.ReceptionDate,
                                Comments = data.Notes,
                                RepairWithWarranty = data.WarrantyRepair,
                                Emm = data.EMM,
                                WarehouseDeliveryDate = data.DeliveryWarehouseDate,
                                LocalCollection = data.CollectionLocal,
                                CollectionAddress = data.CollectionAddress,
                                CollectionPostalCode = data.CollectionPostalCode,
                                CollectionLocality = data.CollectionLocality,
                                CollectionContact = data.CollectionContact,
                                CollectionResponsibleReception = data.CollectionReceptionResponsible,
                                LocalDelivery = data.DeliveryLocal,
                                DeliveryAddress = data.DeliveryAddress,
                                DeliveryPostalCode = data.DeliveryPostalCode,
                                LocalityDelivery = data.DeliveryLocality,
                                ResponsibleReceptionReception = data.ReceptionReceptionResponsible,
                                InvoiceNo = data.InvoiceNo,
                                State = RequisitionStates.Pending,
                                RequisitionDate = DateTime.Now.ToString("dd-MM-yyyy"),
                                CreateUser = User.Identity.Name,

                                Lines = items.Select(line => new RequisitionLineViewModel()
                                {

                                    LocalCode = line.LocalCode,
                                    Code = line.Code,
                                    Description = line.Description,
                                    Description2 = line.Description2,
                                    UnitMeasureCode = line.UnitMeasureCode,
                                    QuantityToRequire = line.QuantityToRequire,
                                    UnitCost = line.UnitCost,
                                    ProjectNo = line.ProjectNo,
                                    MaintenanceOrderLineNo = line.MaintenanceOrderLineNo,
                                    Vehicle = line.Vehicle,
                                    SupplierNo = line.SupplierNo,
                                    RegionCode = line.RegionCode,
                                    FunctionalAreaCode = line.FunctionalAreaCode,
                                    CenterResponsibilityCode = line.CenterResponsibilityCode,
                                    OpenOrderNo = line.OpenOrderNo,
                                    OpenOrderLineNo = line.OpenOrderLineNo,
                                }).ToList()
                            }).ToList();

                        //Set VATPostingGroup Info
                        newlistOpenOrder.ForEach(header =>
                        {
                            header.Lines.ForEach(line =>
                            {
                                line.VATBusinessPostingGroup = vendors.FirstOrDefault(x => x.No_ == line.SupplierNo)?.VATBusinessPostingGroup;
                                line.VATProductPostingGroup = productsInRequisition.FirstOrDefault(x => x.Code == line.Code)?.VATProductPostingGroup;
                            });

                            header.LocalMarketRegion = header.Lines.FirstOrDefault().MarketLocalRegion;
                        });

                        data = CreateRequesition(newlistOpenOrder, data);

                        List<PreRequisitionLineViewModel> GroupedList = new List<PreRequisitionLineViewModel>();
                        PreRequesitionLines.Where(x => x.NºLinhaEncomendaAberto == 0 || x.NºLinhaEncomendaAberto == null).ToList().ForEach(x => GroupedList.Add(DBPreRequesitionLines.ParseToViewModel(x)));

                        List<RequisitionViewModel> newlist = GroupedList.GroupBy(
                            x => x.ArmazemCDireta,
                            x => x,
                            (key, items) => new RequisitionViewModel
                            {
                                RequestReclaimNo = data.ClaimedRequesitionNo,
                                Urgent = data.Urgent,
                                Area = data.Area,
                                Immobilized = data.Immobilized,
                                Exclusive = data.Exclusive,
                                AlreadyPerformed = data.AlreadyExecuted,
                                Sample = data.Sample,
                                Equipment = data.Equipment,
                                BuyCash = data.MoneyBuy,
                                StockReplacement = data.StockReplacement,
                                Reclamation = data.Complaint,
                                RegionCode = data.RegionCode,
                                FunctionalAreaCode = data.FunctionalAreaCode,
                                CenterResponsibilityCode = data.ResponsabilityCenterCode,
                                Vehicle = data.Vehicle,
                                ProjectNo = data.ProjectNo,
                                ReceivedDate = data.ReceptionDate,
                                Comments = data.Notes,
                                RepairWithWarranty = data.WarrantyRepair,
                                Emm = data.EMM,
                                WarehouseDeliveryDate = data.DeliveryWarehouseDate,
                                LocalCollection = data.CollectionLocal,
                                CollectionAddress = data.CollectionAddress,
                                CollectionPostalCode = data.CollectionPostalCode,
                                CollectionLocality = data.CollectionLocality,
                                CollectionContact = data.CollectionContact,
                                CollectionResponsibleReception = data.CollectionReceptionResponsible,
                                LocalDelivery = data.DeliveryLocal,
                                DeliveryAddress = data.DeliveryAddress,
                                DeliveryPostalCode = data.DeliveryPostalCode,
                                LocalityDelivery = data.DeliveryLocality,
                                ResponsibleReceptionReception = data.ReceptionReceptionResponsible,
                                InvoiceNo = data.InvoiceNo,
                                State = RequisitionStates.Pending,
                                RequisitionDate = DateTime.Now.ToString("dd-MM-yyyy"),
                                CreateUser = User.Identity.Name,

                                Lines = items.Select(line => new RequisitionLineViewModel()
                                {

                                    LocalCode = line.LocalCode,
                                    Code = line.Code,
                                    Description = line.Description,
                                    Description2 = line.Description2,
                                    UnitMeasureCode = line.UnitMeasureCode,
                                    QuantityToRequire = line.QuantityToRequire,
                                    UnitCost = line.UnitCost,
                                    ProjectNo = line.ProjectNo,
                                    MaintenanceOrderLineNo = line.MaintenanceOrderLineNo,
                                    Vehicle = line.Vehicle,
                                    SupplierNo = line.SupplierNo,
                                    RegionCode = line.RegionCode,
                                    FunctionalAreaCode = line.FunctionalAreaCode,
                                    CenterResponsibilityCode = line.CenterResponsibilityCode,
                                    OpenOrderNo = line.OpenOrderNo,
                                    OpenOrderLineNo = line.OpenOrderLineNo,
                                }).ToList()
                            }).ToList();

                        //Set VATPostingGroup Info
                        newlist.ForEach(header =>
                        {
                            header.Lines.ForEach(line =>
                            {
                                line.VATBusinessPostingGroup = vendors.FirstOrDefault(x => x.No_ == line.SupplierNo)?.VATBusinessPostingGroup;
                                line.VATProductPostingGroup = productsInRequisition.FirstOrDefault(x => x.Code == line.Code)?.VATProductPostingGroup;
                            });

                            header.LocalMarketRegion = header.Lines.FirstOrDefault().MarketLocalRegion;
                        });
                        data = CreateRequesition(newlist, data);

                        if (data.eReasonCode == 1 && newlist.Count > 0 || newlistOpenOrder.Count > 0)
                        {
                            //if all items have been created delete pre-requisition lines
                            DBPreRequesitionLines.DeleteAllFromPreReqNo(data.PreRequesitionsNo);

                            var successMessages = data.eMessages.Where(x => x.Type == TraceType.Success).Select(x => x.Message).ToArray();
                            if (successMessages.Length > 0)
                            {
                                data.eMessage += " " + string.Join(";", successMessages);
                            }
                        }
                        else
                        {
                            data.eReasonCode = 0;
                            data.eMessage = "Ocorreu um erro ao criar a requisição.";
                        }
                    }
                    else
                    {
                        data.eReasonCode = 0;
                        data.eMessage = "Pré-Requisição não contém linhas.";
                    }
                }
            }
            catch (Exception ex)
            {
                data.eReasonCode = 0;
                data.eMessage = "Ocorreu um erro ao criar a requisição.";
            }
            return Json(data);
        }

        public PreRequesitionsViewModel CreateRequesition(List<RequisitionViewModel> newlist, PreRequesitionsViewModel data)
        {
            int totalItems = 0;
            string createdReqIds = ": ";

            foreach (var req in newlist)
            {
                //Get Contract Numeration
                Configuração Configs = DBConfigurations.GetById(1);
                int ProjectNumerationConfigurationId = 0;
                ProjectNumerationConfigurationId = Configs.NumeraçãoRequisições.Value;

                string RequisitionNo = DBNumerationConfigurations.GetNextNumeration(ProjectNumerationConfigurationId, true, false);
                if (!string.IsNullOrEmpty(RequisitionNo))
                {
                    //Update Last Numeration Used
                    ConfiguraçãoNumerações ConfigNumerations = DBNumerationConfigurations.GetById(ProjectNumerationConfigurationId);
                    ConfigNumerations.ÚltimoNºUsado = RequisitionNo;
                    ConfigNumerations.UtilizadorModificação = User.Identity.Name;
                    DBNumerationConfigurations.Update(ConfigNumerations);

                    req.RequisitionNo = RequisitionNo;
                    Requisição createReq = DBRequest.ParseToDB(req);

                    createReq = DBRequest.Create(createReq);
                    if (createReq != null)
                    {
                        //Create Workflow
                        var ctx = new SuchDBContext();
                        var logEntry = new RequisicoesRegAlteracoes();
                        logEntry.NºRequisição = createReq.NºRequisição;
                        logEntry.Estado = (int)RequisitionStates.Pending; //PENDENTE = 0
                        logEntry.ModificadoEm = DateTime.Now;
                        logEntry.ModificadoPor = User.Identity.Name;
                        ctx.RequisicoesRegAlteracoes.Add(logEntry);
                        ctx.SaveChanges();

                        //copy files
                        var preReq = data.PreRequesitionsNo;
                        List<Anexos> FilesLoaded = DBAttachments.GetById(preReq);
                        foreach (var file in FilesLoaded)
                        {
                            try
                            {
                                string FileName = file.UrlAnexo;
                                string NewFileName = createReq.NºRequisição + FileName.Substring(FileName.IndexOf('_'));
                                try
                                {
                                    System.IO.File.Copy(_config.FileUploadFolder + FileName, _config.FileUploadFolder + NewFileName);
                                }
                                catch (Exception ex)
                                {
                                    data.eMessages.Add(new TraceInformation(TraceType.Exception, "Erro ao copiar anexo " + FileName + ": " + ex.Message));
                                }

                                AttachmentsViewModel CopyFile = new AttachmentsViewModel();
                                CopyFile.DocNumber = createReq.NºRequisição;
                                CopyFile.CreateUser = User.Identity.Name;
                                CopyFile.DocType = 2;
                                CopyFile.Url = NewFileName;
                                Anexos newFile = DBAttachments.Create(DBAttachments.ParseToDB(CopyFile));
                                if (newFile != null)
                                {
                                    System.IO.File.Delete(_config.FileUploadFolder + file.UrlAnexo);
                                    DBAttachments.Delete(file);
                                }

                            }
                            catch (Exception ex)
                            {
                                data.eReasonCode = 0;
                                data.eMessage = "Ocorreu um erro ao copiar os anexos.";
                                data.eMessages.Add(new TraceInformation(TraceType.Exception, "Erro ao guardar anexo: " + ex.Message));
                                //throw;
                            }

                        }

                        //count successful items for later validation
                        totalItems++;
                        //createdReqIds += RequisitionNo + "; ";
                        var totalValue = req.GetTotalValue();
                        //Start Approval
                        ErrorHandler result = ApprovalMovementsManager.StartApprovalMovement(1, createReq.CódigoÁreaFuncional, createReq.CódigoCentroResponsabilidade, createReq.CódigoRegião, totalValue, createReq.NºRequisição, User.Identity.Name, "");
                        if (result.eReasonCode != 100)
                        {
                            data.eMessages.Add(new TraceInformation(TraceType.Error, result.eMessage));
                        }

                        data.eReasonCode = 1;
                        data.eMessage = "Requisições criadas com sucesso";
                        data.eMessages.Add(new TraceInformation(TraceType.Success, RequisitionNo));
                    }
                    else
                    {
                        data.eReasonCode = 0;
                        data.eMessage = "Ocorreu um erro ao criar a requisição.";
                    }
                }
                else
                {
                    data.eReasonCode = 0;
                    data.eMessage = "A numeração configurada não é compativel com a inserida.";
                }

            }
            if (newlist.Count > 0 && totalItems == newlist.Count)
            {
                //if all items have been created delete pre-requisition lines


                DBPreRequesitionLines.DeleteAllFromPreReqNo(data.PreRequesitionsNo);
                //data.eMessage += createdReqIds;
                //if (data.eMessages.Count > 0)
                //{
                //    data.eMessages.Insert(0, new TraceInformation(TraceType.Error, "Não foi possivel iniciar o processo de aprovação para as seguintes requisições: "));
                //}
            }
            else
            {
                data.eReasonCode = 0;
                data.eMessage = "Ocorreu um erro ao criar a requisição.";
            }

            return data;
        }


        [HttpPost]
        public JsonResult SendReqForApproval([FromBody] JObject requestParams)
        {
            string ReqNo = requestParams["requisitionNo"].ToString();
            Requisição createReq = DBRequest.GetById(ReqNo);
            ErrorHandler ApprovalMovResult = new ErrorHandler();
            string Error = "";

            List<ConfiguraçãoAprovações> approv = DBApprovalConfigurations.GetAll();

            List<ApprovalMovementsViewModel> result = DBApprovalMovements.ParseToViewModel(DBApprovalMovements.GetAllAssignedToUserFilteredByStatus(User.Identity.Name, 1));

            if (result != null && result.Count > 0)
            {
                foreach (ApprovalMovementsViewModel req in result)
                {
                    if (req.Number == ReqNo)
                    {
                        Error = "Esta Requisição já está à espera de Aprovação.";
                    }
                }

                if (String.IsNullOrEmpty(Error))
                {
                    ApprovalMovResult = ApprovalMovementsManager.StartApprovalMovement(1, createReq.CódigoÁreaFuncional, createReq.CódigoCentroResponsabilidade, createReq.CódigoRegião, 0, createReq.NºRequisição, User.Identity.Name, "");
                    if (ApprovalMovResult.eReasonCode != 100)
                    {
                        ApprovalMovResult.eReasonCode = 2;
                        //ApprovalMovResult.eMessage = "Não foi possivel iniciar o processo de aprovação para esta requisição: " + ReqNo;
                    }
                }
                else
                {
                    ApprovalMovResult.eReasonCode = 3;
                    ApprovalMovResult.eMessage = Error;
                }
            }
            else
            {
                ApprovalMovResult = ApprovalMovementsManager.StartApprovalMovement(1, createReq.CódigoÁreaFuncional, createReq.CódigoCentroResponsabilidade, createReq.CódigoRegião, 0, createReq.NºRequisição, User.Identity.Name, "");
                if (ApprovalMovResult.eReasonCode != 100)
                {
                    ApprovalMovResult.eReasonCode = 2;
                    //ApprovalMovResult.eMessage = "Não foi possivel iniciar o processo de aprovação para esta requisição: " + ReqNo;
                }
            }

            if (ApprovalMovResult.eReasonCode != 3 && ApprovalMovResult.eReasonCode != 2)
            {
                ApprovalMovResult.eReasonCode = 1;
                ApprovalMovResult.eMessage = "Foi iniciado o processo de aprovação para esta requisição";
            }
            return Json(ApprovalMovResult);
        }

        #endregion

        #region Numeração Requisição
        [HttpPost]
        public JsonResult ValidateNumerationReq([FromBody] PreRequesitionsViewModel data)
        {
            //Get Project Numeration
            Configuração Cfg = DBConfigurations.GetById(1);
            int ProjectNumerationConfigurationId = 0;
            ProjectNumerationConfigurationId = Cfg.NumeraçãoRequisições.Value;

            ConfiguraçãoNumerações CfgNumeration = DBNumerationConfigurations.GetById(ProjectNumerationConfigurationId);

            //Validate if ProjectNo is valid
            if (!CfgNumeration.Automático.Value)
            {
                return Json("É obrigatório inserir o Nº Requisição.");
            }

            return Json("");
        }


        #endregion

        #region Attachments
        [HttpPost]
        [Route("PreRequisicoes/FileUpload")]
        [Route("PreRequisicoes/FileUpload/{id}/{linha}")]
        public JsonResult FileUpload(string id, int linha)
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
                        full_filename = id + "_" + filename;
                        var path = Path.Combine(_config.FileUploadFolder, full_filename);
                        using (FileStream dd = new FileStream(path, FileMode.CreateNew))
                        {
                            file.CopyTo(dd);
                            dd.Dispose();

                            Anexos newfile = new Anexos();
                            newfile.NºOrigem = id;
                            newfile.UrlAnexo = full_filename;
                            newfile.TipoOrigem = 1;
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
            catch (Exception ex)
            {
                throw;
            }
            return Json("");
        }

        [HttpPost]
        public JsonResult LoadAttachments([FromBody] JObject requestParams)
        {
            string id = requestParams["id"].ToString();
            //string line = requestParams["linha"].ToString();
            //int lineNo = Int32.Parse(line);

            List<Anexos> list = DBAttachments.GetById(id);
            List<AttachmentsViewModel> attach = new List<AttachmentsViewModel>();
            list.ForEach(x => attach.Add(DBAttachments.ParseToViewModel(x)));
            return Json(attach);
        }

        [HttpGet]
        public FileStreamResult DownloadFile(string id)
        {
            return new FileStreamResult(new FileStream(_config.FileUploadFolder + id, FileMode.Open), "application/xlsx");
        }


        [HttpPost]
        public JsonResult DeleteAttachments([FromBody] AttachmentsViewModel requestParams)
        {
            try
            {
                System.IO.File.Delete(_config.FileUploadFolder + requestParams.Url);
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

        //1
        [HttpPost]
        public async Task<JsonResult> ExportToExcel_RequisicoesArquivadas([FromBody] List<RequisitionViewModel> Lista)
        {
            JObject dp = (JObject)Lista[0].ColunasEXCEL;

            string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\Upload\\temp";
            string user = User.Identity.Name;
            user = user.Replace("@", "_");
            user = user.Replace(".", "_");
            string sFileName = @"" + user + ".xlsx";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            var memory = new MemoryStream();
            using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("Requisições Arquivadas");
                IRow row = excelSheet.CreateRow(0);
                int Col = 0;

                if (dp["requisitionNo"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Nº Requisição");
                    Col = Col + 1;
                }
                if (dp["state"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Estado");
                    Col = Col + 1;
                }
                if (dp["requisitionDate"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Data Requisição");
                    Col = Col + 1;
                }
                if (dp["localCode"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Código Localização");
                    Col = Col + 1;
                }
                if (dp["responsibleApproval"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Responsável Aprovação");
                    Col = Col + 1;
                }
                if (dp["approvalDate"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Data/Hora Aprovação");
                    Col = Col + 1;
                }
                if (dp["comments"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Observações");
                    Col = Col + 1;
                }
                if (dp["employeeNo"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Nº Funcionário");
                    Col = Col + 1;
                }

                if (dp != null)
                {
                    int count = 1;
                    foreach (RequisitionViewModel item in Lista)
                    {
                        Col = 0;
                        row = excelSheet.CreateRow(count);

                        if (dp["requisitionNo"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item.RequisitionNo);
                            Col = Col + 1;
                        }
                        if (dp["state"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item.State.ToString());
                            Col = Col + 1;
                        }
                        if (dp["requisitionDate"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item.RequisitionDate);
                            Col = Col + 1;
                        }
                        if (dp["localCode"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item.LocalCode);
                            Col = Col + 1;
                        }
                        if (dp["responsibleApproval"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item.ResponsibleApproval);
                            Col = Col + 1;
                        }
                        if (dp["approvalDate"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item.ApprovalDate.ToString());
                            Col = Col + 1;
                        }
                        if (dp["comments"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item.Comments);
                            Col = Col + 1;
                        }
                        if (dp["employeeNo"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item.EmployeeNo);
                            Col = Col + 1;
                        }
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
        public IActionResult ExportToExcelDownload_RequisicoesArquivadas(string sFileName)
        {
            sFileName = @"/Upload/temp/" + sFileName;
            return File(sFileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Requisições Arquivadas.xlsx");
        }
    }
}