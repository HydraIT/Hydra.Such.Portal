﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hydra.Such.Data.ViewModel.Projects;
using Hydra.Such.Data.Logic;
using Hydra.Such.Portal.Configurations;
using Hydra.Such.Data.Database;
using Hydra.Such.Data.Logic.Contracts;
using Hydra.Such.Data.Logic.Project;
using Microsoft.Extensions.Options;
using Hydra.Such.Data.ViewModel;
using Hydra.Such.Data.NAV;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using Hydra.Such.Data;
using static Hydra.Such.Data.Enumerations;
using System.Net;
using Hydra.Such.Data.ViewModel.Clients;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.ComponentModel;
using Hydra.Such.Portal.Extensions;

namespace Hydra.Such.Portal.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        private readonly NAVConfigurations _config;
        private readonly NAVWSConfigurations _configws;
        private readonly GeneralConfigurations _generalConfig;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ClientesController(IOptions<NAVConfigurations> appSettings, IOptions<NAVWSConfigurations> NAVWSConfigs, IOptions<GeneralConfigurations> appSettingsGeneral, IHostingEnvironment _hostingEnvironment)
        {
            _config = appSettings.Value;
            _configws = NAVWSConfigs.Value;
            _generalConfig = appSettingsGeneral.Value;
            this._hostingEnvironment = _hostingEnvironment;
        }

        #region Clientes
        public IActionResult Index()
        {
            UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Features.Clientes); //4, 47);
            if (UPerm != null && UPerm.Read.Value)
            {
                ViewBag.UPermissions = UPerm;
                return View();
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        public IActionResult DetalhesCliente(string id)
        {
            UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Features.Clientes); //4, 47);
            if (UPerm != null && UPerm.Read.Value)
            {
                ViewBag.No = id ?? "";
                ViewBag.reportServerURL = _config.ReportServerURL;
                ViewBag.userLogin = User.Identity.Name.ToString();
                ViewBag.UPermissions = UPerm;
                return View();
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error");
            }
        }
        #endregion

        #region List
        [HttpPost]
        public JsonResult Get([FromBody] JObject requestParams)
        {
            //int AreaId = int.Parse(requestParams["areaid"].ToString());
            JToken customerNoValue;
            string customerNo = string.Empty;
            if (requestParams != null)
            {
                if (requestParams.TryGetValue("customerNo", out customerNoValue))
                    customerNo = (string)customerNoValue;
            }
            List<ClientDetailsViewModel> result = new List<ClientDetailsViewModel>();
            result = DBNAV2017Clients.GetClients(_config.NAVDatabaseName, _config.NAVCompanyName, customerNo).Select(c =>
                 new ClientDetailsViewModel()
                 {
                     No = c.No_,
                     Name = c.Name,
                     Address = c.Address,
                     Post_Code = c.PostCode,
                     City = c.City,
                     County = c.County,
                     Country_Region_Code = c.CountryRegionCode,
                     Phone_No = c.PhoneNo,
                     E_Mail = c.EMail,
                     Fax_No = c.FaxNo,
                     Home_Page = c.HomePage,
                     VAT_Registration_No = c.VATRegistrationNo_,
                     Taxa_Aprovisionamento = c.TaxaAprovisionamento,
                     Abrigo_Lei_Compromisso = c.AbrigoLeiCompromisso,
                     Payment_Terms_Code = c.PaymentTermsCode,
                     Payment_Method_Code = c.PaymentMethodCode,
                     Blocked = (Blocked)c.Blocked,
                     Blocked_Text = ((Blocked)c.Blocked).GetDescription(),
                     Regiao_Cliente = c.RegiaoCliente,
                     Regiao_Cliente_Text = c.RegiaoCliente.ToString(),
                     Global_Dimension_1_Code = c.FunctionalAreaCode,
                     Centro_Resp_Dimensao = c.ResponsabilityCenterCode,
                     Cliente_Associado = c.ClienteAssociado,
                     Associado_A_N = c.AssociadoAN,
                     Tipo_Cliente = c.TipoCliente,
                     Tipo_Cliente_Text = ((Data.ViewModel.Clients.Tipo_Cliente)c.TipoCliente).GetDescription(),
                     Natureza_Cliente = c.NaturezaCliente,
                     Natureza_Cliente_Text = ((Data.ViewModel.Clients.Natureza_Cliente)c.NaturezaCliente).GetDescription(),
                     Cliente_Nacional = c.ClienteNacional,
                     Cliente_Interno = c.ClienteInterno,
                     No_Fornecedor_Assoc = c.NoFornecedorAssoc

                 }
            ).ToList();
            /*
            //Apply User Dimensions Validations
            List<AcessosDimensões> CUserDimensions = DBUserDimensions.GetByUserId(User.Identity.Name);
            //Regions
            if (CUserDimensions.Where(y => y.Dimensão == (int)Dimensions.Region).Count() > 0)
                result.RemoveAll(x => !CUserDimensions.Any(y => y.Dimensão == (int)Dimensions.Region && y.ValorDimensão == x.RegionCode));
            //FunctionalAreas
            if (CUserDimensions.Where(y => y.Dimensão == (int)Dimensions.FunctionalArea).Count() > 0)
                result.RemoveAll(x => !CUserDimensions.Any(y => y.Dimensão == (int)Dimensions.FunctionalArea && y.ValorDimensão == x.FunctionalAreaCode));
            //ResponsabilityCenter
            if (CUserDimensions.Where(y => y.Dimensão == (int)Dimensions.ResponsabilityCenter).Count() > 0)
                result.RemoveAll(x => !CUserDimensions.Any(y => y.Dimensão == (int)Dimensions.ResponsabilityCenter && y.ValorDimensão == x.ResponsabilityCenterCode));
            */
            return Json(result);
        }

        #endregion

        #region Details

        [HttpPost]
        public JsonResult GetDetails([FromBody] ClientDetailsViewModel data)
        {
            if (data != null && data.No != null)
            {
                var getClientTask = WSCustomerService.GetByNoAsync(data.No, _configws);
                try
                {
                    getClientTask.Wait();
                }
                catch (Exception ex)
                {
                    data.eReasonCode = 3;
                    data.eMessage = "Ocorreu um erro a obter o cliente no NAV.";
                    data.eMessages.Add(new TraceInformation(TraceType.Error, ex.Message));
                    return Json(data);
                }

                ClientDetailsViewModel client = getClientTask.Result;
                if (client != null)
                {
                    return Json(client);
                }

            }
            return Json(false);
        }

        //eReason = 1 -> Sucess
        //eReason = 2 -> Error creating Project on Databse 
        //eReason = 3 -> Error creating Project on NAV 
        //eReason = 4 -> Unknow Error 
        //eReason = 5 -> Error getting Numeration 

        [HttpPost]
        public JsonResult Create([FromBody] ClientDetailsViewModel data)
        {

            if (data != null)
            {
                var createClientTask = WSCustomerService.CreateAsync(data, _configws);
                try
                {
                    createClientTask.Wait();
                }
                catch (Exception ex)
                {
                    data.eReasonCode = 3;
                    data.eMessage = "Ocorreu um erro ao criar o cliente no NAV.";
                    data.eMessages.Add(new TraceInformation(TraceType.Error, ex.Message));
                    return Json(data);
                }

                var result = createClientTask.Result;
                if (result == null)
                {
                    data.eReasonCode = 3;
                    data.eMessage = "Ocorreu um erro ao criar o cliente no NAV.";
                    return Json(data);
                }

                data.eReasonCode = 1;

                var client = WSCustomerService.MapCustomerNAVToCustomerModel(result.WSCustomer);
                if (client != null)
                {
                    client.eReasonCode = 1;
                    return Json(client);
                }

            }
            return Json(data);
        }

        [HttpPost]
        public JsonResult Update([FromBody] ClientDetailsViewModel data)
        {
            if (data != null)
            {
                var updateClientTask = WSCustomerService.UpdateAsync(data, _configws);
                try
                {
                    updateClientTask.Wait();
                }
                catch (Exception ex)
                {
                    data.eReasonCode = 3;
                    data.eMessage = "Ocorreu um erro ao atualizar o cliente no NAV.";
                    data.eMessages.Add(new TraceInformation(TraceType.Error, ex.Message));
                    return Json(data);
                }

                var result = updateClientTask.Result;
                if (result == null)
                {
                    data.eReasonCode = 3;
                    data.eMessage = "Ocorreu um erro ao atualizar o cliente no NAV.";
                    return Json(data);
                }

                var client = WSCustomerService.MapCustomerNAVToCustomerModel(result.WSCustomer);
                if (client != null)
                {
                    client.eReasonCode = 1;
                    return Json(client);
                }

            }
            return Json(false);
        }


        [HttpPost]
        public JsonResult Delete([FromBody] ClientDetailsViewModel data)
        {
            if (data != null && data.No != null)
            {
                var deleteClientTask = WSCustomerService.DeleteAsync(data.No, _configws);
                try
                {
                    deleteClientTask.Wait();
                }
                catch (Exception ex)
                {
                    data.eReasonCode = 3;
                    data.eMessage = "Ocorreu um erro a apagar o cliente no NAV.";
                    data.eMessages.Add(new TraceInformation(TraceType.Error, ex.Message));
                    return Json(data);
                }

                var result = deleteClientTask.Result;
                if (result != null)
                {
                    return Json(new ErrorHandler()
                    {
                        eReasonCode = 0,
                        eMessage = "Projeto removido com sucesso."
                    });
                }

            }
            return Json(false);
        }
        #endregion

        //1
        [HttpPost]
        public async Task<JsonResult> ExportToExcel_Clientes([FromBody] List<ClientDetailsViewModel> Lista)
        {
            JObject dp = (JObject)Lista[0].ColunasEXCEL;

            string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\Upload\\temp";
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
                ISheet excelSheet = workbook.CreateSheet("Clientes");
                IRow row = excelSheet.CreateRow(0);
                int Col = 0;

                if (dp["no"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Nº"); Col = Col + 1; }
                if (dp["name"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Nome"); Col = Col + 1; }
                if (dp["address"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Morada"); Col = Col + 1; }
                if (dp["post_Code"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Cód. Postal"); Col = Col + 1; }
                if (dp["city"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Cidade"); Col = Col + 1; }
                if (dp["phone_No"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Telefone"); Col = Col + 1; }
                if (dp["e_Mail"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Email"); Col = Col + 1; }
                if (dp["fax_No"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Nº Fax"); Col = Col + 1; }
                if (dp["home_Page"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Home Page"); Col = Col + 1; }
                if (dp["vaT_Registration_No"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Nº Contribuinte"); Col = Col + 1; }
                if (dp["taxa_Aprovisionamento"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Taxa Aprovisionamento"); Col = Col + 1; }
                if (dp["abrigo_Lei_Compromisso"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Abrigo Lei dos Compromissos"); Col = Col + 1; }
                if (dp["payment_Terms_Code"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Termos de Pagamento"); Col = Col + 1; }
                if (dp["payment_Method_Code"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Forma Pagamento"); Col = Col + 1; }
                if (dp["blocked_Text"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Bloqueado"); Col = Col + 1; }
                if (dp["country_Region_Code"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Código Pais/Região"); Col = Col + 1; }
                if (dp["regiao_Cliente_Text"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Região"); Col = Col + 1; }
                if (dp["global_Dimension_1_Code"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Area"); Col = Col + 1; }
                if (dp["centro_Resp_Dimensao"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Centro de Responsabilidade"); Col = Col + 1; }
                if (dp["cliente_Associado"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Cliente Associado"); Col = Col + 1; }
                if (dp["associado_A_N"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Associado (A/N)"); Col = Col + 1; }
                if (dp["tipo_Cliente_Text"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Tipo de Cliente"); Col = Col + 1; }
                if (dp["natureza_Cliente_Text"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Natureza do Cliente"); Col = Col + 1; }
                if (dp["cliente_Nacional"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Cliente Nacional"); Col = Col + 1; }
                if (dp["cliente_Interno"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Cliente Interno"); Col = Col + 1; }
                if (dp["no_Fornecedor_Assoc"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Nº Fornecedor Associado"); Col = Col + 1; }

                if (dp != null)
                {
                    int count = 1;
                    foreach (ClientDetailsViewModel item in Lista)
                    {
                        Col = 0;
                        row = excelSheet.CreateRow(count);

                        if (dp["no"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.No); Col = Col + 1; }
                        if (dp["name"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.Name); Col = Col + 1; }
                        if (dp["address"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.Address); Col = Col + 1; }
                        if (dp["post_Code"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.Post_Code); Col = Col + 1; }
                        if (dp["city"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.City); Col = Col + 1; }
                        if (dp["phone_No"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.Phone_No); Col = Col + 1; }
                        if (dp["e_Mail"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.E_Mail); Col = Col + 1; }
                        if (dp["fax_No"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.Fax_No); Col = Col + 1; }
                        if (dp["home_Page"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.Home_Page); Col = Col + 1; }
                        if (dp["vaT_Registration_No"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.VAT_Registration_No); Col = Col + 1; }
                        if (dp["taxa_Aprovisionamento"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.Taxa_Aprovisionamento.ToString()); Col = Col + 1; }
                        if (dp["abrigo_Lei_Compromisso"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.Abrigo_Lei_Compromisso); Col = Col + 1; }
                        if (dp["payment_Terms_Code"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.Payment_Terms_Code); Col = Col + 1; }
                        if (dp["payment_Method_Code"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.Payment_Method_Code); Col = Col + 1; }
                        if (dp["blocked_Text"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.Blocked_Text); Col = Col + 1; }
                        if (dp["country_Region_Code"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.Country_Region_Code); Col = Col + 1; }
                        if (dp["regiao_Cliente_Text"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.Regiao_Cliente_Text); Col = Col + 1; }
                        if (dp["global_Dimension_1_Code"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.Global_Dimension_1_Code); Col = Col + 1; }
                        if (dp["centro_Resp_Dimensao"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.Centro_Resp_Dimensao); Col = Col + 1; }
                        if (dp["cliente_Associado"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.Cliente_Associado); Col = Col + 1; }
                        if (dp["associado_A_N"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.Associado_A_N); Col = Col + 1; }
                        if (dp["tipo_Cliente_Text"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.Tipo_Cliente_Text); Col = Col + 1; }
                        if (dp["natureza_Cliente_Text"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.Natureza_Cliente_Text); Col = Col + 1; }
                        if (dp["cliente_Nacional"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.Cliente_Nacional); Col = Col + 1; }
                        if (dp["cliente_Interno"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.Cliente_Interno); Col = Col + 1; }
                        if (dp["no_Fornecedor_Assoc"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.No_Fornecedor_Assoc); Col = Col + 1; }
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
        public IActionResult ExportToExcelDownload_Clientes(string sFileName)
        {
            sFileName = @"/Upload/temp/" + sFileName;
            return File(sFileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Clientes.xlsx");
        }


        [HttpPost]
        public JsonResult GetInvoices([FromBody] ClientDetailsViewModel data)
        {
            if (data != null && data.No != null)
            {
                var result = DBNAV2017Clients.GetInvoices(_config.NAVDatabaseName, _config.NAVCompanyName, data.No);
                return Json(result);
            }
            return Json(false);
        }

        [HttpPost]
        public JsonResult GetInvoiceDetails([FromBody] NAVClientesInvoicesViewModel data)
        {
            if (data != null && data.No_ != null)
            {
                List<NAVClientesInvoicesDetailsViewModel> result = null;
                if (data.Tipo == "Fatura")
                {
                    result = DBNAV2017Clients.GetInvoiceDetails(_config.NAVDatabaseName, _config.NAVCompanyName, data.No_);
                }
                else
                {
                    result = DBNAV2017Clients.GetCrMemoDetails(_config.NAVDatabaseName, _config.NAVCompanyName, data.No_);
                }
                return Json(result);
            }
            return Json(false);
        }

        [HttpPost]
        public JsonResult GetBalances([FromBody] ClientDetailsViewModel data)
        {
            if (data != null && data.No != null)
            {
                var result = DBNAV2017Clients.GetBalances(_config.NAVDatabaseName, _config.NAVCompanyName, data.No);
                return Json(result);
            }
            return Json(false);
        }

        [HttpPost]
        public JsonResult UpdateBalances([FromBody] List<NAVClientesBalanceControlViewModel> listData, string CustomerNo)
        {
            var updateResult = 0;
            if (CustomerNo != null)
            {
                if (listData != null && listData.Count() > 0)
                {
                    listData.ForEach(line =>
                    {
                        var updated = DBNAV2017Clients.UpdateBalance(_config.NAVDatabaseName, _config.NAVCompanyName, CustomerNo, line.EntryNo.ToString(), line.SinalizacaoRec.ToString(), line.Obs);
                        if (updated != null)
                        {
                            updateResult += (int)updated;
                        };

                    });
                }
                var result = DBNAV2017Clients.GetBalances(_config.NAVDatabaseName, _config.NAVCompanyName, CustomerNo);
                return Json(result);
            }
            return Json(false);
        }


        [HttpPost]
        public async Task<JsonResult> InvoiceDetailsExportToExcel([FromBody] dynamic form)
        {
            JObject dp = (JObject)form.GetValue("columns");

            var list = (dynamic)form.GetValue("list");

            var noFatura = (dynamic)form.GetValue("noFatura");

            var tipo = (dynamic)form.GetValue("tipo");

            string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\Upload\\temp";
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

                string excelSheetName = tipo == "Fatura" ? "Detalhe Fatura" : "Detalhe Nota de Crédito";

                ISheet excelSheet = workbook.CreateSheet("Detalhe Fatura");
                IRow row = excelSheet.CreateRow(0);
                int Col = 0;

                if (dp["documentNo"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("documentNo");
                    Col = Col + 1;
                }
                if (dp["tipo"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Tipo");
                    Col = Col + 1;
                }
                if (dp["no"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Nº");
                    Col = Col + 1;
                }
                if (dp["description"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Descrição");
                    Col = Col + 1;
                }
                if (dp["description2"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Descrição 2");
                    Col = Col + 1;
                }
                if (dp["unitOfMeasure"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Unidade de Medida");
                    Col = Col + 1;
                }
                if (dp["quantity"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Quantidade");
                    Col = Col + 1;
                }
                if (dp["unitPrice"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Preço Unitário");
                    Col = Col + 1;
                }
                if (dp["vat"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Iva");
                    Col = Col + 1;
                }
                if (dp["amount"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Custo");
                    Col = Col + 1;
                }

                if (dp["tipoRefeicao"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Tp Refeição");
                    Col = Col + 1;
                }
                if (dp["mealTypeDescription"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Descrição Tp Refeição");
                    Col = Col + 1;
                }
                if (dp["grupoServico"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Gr Serviço");
                    Col = Col + 1;
                }
                if (dp["serviceGroupDescription"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Descrição Gr Serviço");
                    Col = Col + 1;
                }
                if (dp["codServCliente"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Cod Serv Cliente");
                    Col = Col + 1;
                }
                if (dp["desServCliente"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Descrição Serv Cliente");
                    Col = Col + 1;
                }
                if (dp["nGuiaResiduosGAR"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Nº Guia Resíduos (GAR)");
                    Col = Col + 1;
                }
                if (dp["nGuiaExterna"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Nº Guia Externa");
                    Col = Col + 1;
                }

                if (dp != null)
                {
                    int count = 1;

                    foreach (JObject item in list)
                    {
                        Col = 0;
                        row = excelSheet.CreateRow(count);

                        if (dp["documentNo"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item["documentNo"].ToString());
                            Col = Col + 1;
                        }
                        if (dp["tipo"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item["tipo"].ToString());
                            Col = Col + 1;
                        }
                        if (dp["no"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item["no"].ToString());
                            Col = Col + 1;
                        }
                        if (dp["description"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item["description"].ToString());
                            Col = Col + 1;
                        }
                        if (dp["description2"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item["description2"].ToString());
                            Col = Col + 1;
                        }
                        if (dp["unitOfMeasure"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item["unitOfMeasure"].ToString());
                            Col = Col + 1;
                        }
                        if (dp["quantity"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item["quantity"].ToString());
                            Col = Col + 1;
                        }
                        if (dp["unitPrice"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item["unitPrice"].ToString());
                            Col = Col + 1;
                        }
                        if (dp["vat"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item["vat"].ToString());
                            Col = Col + 1;
                        }
                        if (dp["amount"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item["amount"].ToString());
                            Col = Col + 1;
                        }

                        if (dp["tipoRefeicao"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item["tipoRefeicao"].ToString());
                            Col = Col + 1;
                        }
                        if (dp["mealTypeDescription"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item["mealTypeDescription"].ToString());
                            Col = Col + 1;
                        }
                        if (dp["grupoServico"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item["grupoServico"].ToString());
                            Col = Col + 1;
                        }
                        if (dp["serviceGroupDescription"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item["serviceGroupDescription"].ToString());
                            Col = Col + 1;
                        }
                        if (dp["codServCliente"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item["codServCliente"].ToString());
                            Col = Col + 1;
                        }
                        if (dp["desServCliente"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item["desServCliente"].ToString());
                            Col = Col + 1;
                        }
                        if (dp["nGuiaResiduosGAR"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item["nGuiaResiduosGAR"].ToString());
                            Col = Col + 1;
                        }
                        if (dp["nGuiaExterna"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item["nGuiaExterna"].ToString());
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

        public IActionResult InvoiceDetailsExportToExcel(string fileName, string tipo)
        {
            fileName = @"/Upload/temp/" + fileName;

            string fileExportName = tipo == "Fatura" ? "DetalheFatura" : "DetalheNotaCredito";

            return File(fileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileExportName + ".xlsx");
        }

        [HttpPost]
        public async Task<JsonResult> BalancesExportToExcel([FromBody] dynamic form)
        {
            JObject dp = (JObject)form.GetValue("columns");

            var list = (dynamic)form.GetValue("list");

            string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\Upload\\temp";
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

                string excelSheetName = "Controlo Conciliação de Saldos";

                ISheet excelSheet = workbook.CreateSheet("Controlo Conciliação de Saldos");
                IRow row = excelSheet.CreateRow(0);
                int Col = 0;

                if (dp["customerNo"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Nº Cliente");
                    Col = Col + 1;
                }
                if (dp["postingDate"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Data Registo");
                    Col = Col + 1;
                }
                if (dp["documentType"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Tipo Documento");
                    Col = Col + 1;
                }
                if (dp["documentNo"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Nº Documento");
                    Col = Col + 1;
                }
                if (dp["documentDate"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Data Documento");
                    Col = Col + 1;
                }
                if (dp["dueDate"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Data Venc.");
                    Col = Col + 1;
                }
                if (dp["description"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Descrição");
                    Col = Col + 1;
                }
                if (dp["amount"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Valor");
                    Col = Col + 1;
                }
                if (dp["remainingAmount"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Valor Pendente");
                    Col = Col + 1;
                }
                if (dp["sinalizacaoRec"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Sinalização Reconciliação");
                    Col = Col + 1;
                }
                if (dp["dataConcil"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Data Conciliação");
                    Col = Col + 1;
                }
                if (dp["obs"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Observações");
                    Col = Col + 1;
                }
                if (dp["regionId"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Cód. Região");
                    Col = Col + 1;
                }
                if (dp["functionalAreaId"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Cód. Área Funcional");
                    Col = Col + 1;
                }
                if (dp["respCenterId"]["hidden"].ToString() == "False")
                {
                    row.CreateCell(Col).SetCellValue("Cód. Centro de Responsabilidade");
                    Col = Col + 1;
                }

                if (dp != null)
                {
                    int count = 1;

                    foreach (JObject item in list)
                    {
                        Col = 0;
                        row = excelSheet.CreateRow(count);

                        if (dp["customerNo"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item["customerNo"].ToString());
                            Col = Col + 1;
                        }
                        if (dp["postingDate"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item["postingDate"].ToString());
                            Col = Col + 1;
                        }
                        if (dp["documentType"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item["documentType"].ToString());
                            Col = Col + 1;
                        }
                        if (dp["documentNo"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item["documentNo"].ToString());
                            Col = Col + 1;
                        }
                        if (dp["documentDate"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item["documentDate"].ToString());
                            Col = Col + 1;
                        }
                        if (dp["dueDate"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item["dueDate"].ToString());
                            Col = Col + 1;
                        }
                        if (dp["description"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item["description"].ToString());
                            Col = Col + 1;
                        }
                        if (dp["amount"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item["amount"].ToString());
                            Col = Col + 1;
                        }
                        if (dp["remainingAmount"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item["remainingAmount"].ToString());
                            Col = Col + 1;
                        }
                        if (dp["sinalizacaoRec"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item["sinalizacaoRec"].ToString());
                            Col = Col + 1;
                        }
                        if (dp["dataConcil"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item["dataConcil"].ToString());
                            Col = Col + 1;
                        }
                        if (dp["obs"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item["obs"].ToString());
                            Col = Col + 1;
                        }
                        if (dp["regionId"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item["regionId"].ToString());
                            Col = Col + 1;
                        }
                        if (dp["functionalAreaId"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item["functionalAreaId"].ToString());
                            Col = Col + 1;
                        }
                        if (dp["respCenterId"]["hidden"].ToString() == "False")
                        {
                            row.CreateCell(Col).SetCellValue(item["respCenterId"].ToString());
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

        public IActionResult BalancesExportToExcel(string fileName, string tipo)
        {
            fileName = @"/Upload/temp/" + fileName;

            string fileExportName = "ControloConciliacaoSaldos";

            return File(fileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileExportName + ".xlsx");
        }

    }
}