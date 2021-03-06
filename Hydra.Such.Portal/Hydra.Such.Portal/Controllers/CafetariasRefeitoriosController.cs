﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hydra.Such.Data.Logic.Nutrition;
using Hydra.Such.Data.ViewModel.Nutrition;
using Hydra.Such.Data.Database;
using Hydra.Such.Data.Logic;
using Hydra.Such.Data.ViewModel;
using Hydra.Such.Portal.Configurations;
using Microsoft.Extensions.Options;
using static Hydra.Such.Data.Enumerations;
using Hydra.Such.Data;
using Newtonsoft.Json.Linq;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Hydra.Such.Portal.Areas.Nutricao.Controllers
{
    public class CafetariasRefeitoriosController : Controller
    {
        private readonly NAVConfigurations config;
        private readonly GeneralConfigurations _generalConfig;

        public CafetariasRefeitoriosController(IOptions<NAVConfigurations> appSettings, IOptions<GeneralConfigurations> appSettingsGeneral)
        {
            config = appSettings.Value;
            _generalConfig = appSettingsGeneral.Value;
        }


        public IActionResult Index()
        {
            UserAccessesViewModel userPermissions = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Features.Cafetarias_Refeitórios);

            if (userPermissions != null && userPermissions.Read.Value)
            {
                ViewBag.UserPermissions = userPermissions;
                return View();
            }
            else
            {
                return Redirect(Url.Content("~/Error/AccessDenied"));
            }
        }
      

        [HttpPost]
       
        public JsonResult GetCoffeeShops()
        {
            var items = DBCoffeeShops.GetAll().ParseToViewModel(config.NAVDatabaseName, config.NAVCompanyName);
            if (items != null)
            {
                //Apply User Dimensions Validations
                List<AcessosDimensões> userDimensions = DBUserDimensions.GetByUserId(User.Identity.Name);
                if (userDimensions.Where(y => y.Dimensão == (int)Dimensions.Region).Count() > 0)
                    items.RemoveAll(x => !userDimensions.Any(y => y.Dimensão == (int)Dimensions.Region && (y.ValorDimensão == x.CodeRegion || string.IsNullOrEmpty(x.CodeRegion))));
                if (userDimensions.Where(y => y.Dimensão == (int)Dimensions.FunctionalArea).Count() > 0)
                    items.RemoveAll(x => !userDimensions.Any(y => y.Dimensão == (int)Dimensions.FunctionalArea && (y.ValorDimensão == x.CodeFunctionalArea || string.IsNullOrEmpty(x.CodeFunctionalArea))));
                if (userDimensions.Where(y => y.Dimensão == (int)Dimensions.ResponsabilityCenter).Count() > 0)
                    items.RemoveAll(x => !userDimensions.Any(y => y.Dimensão == (int)Dimensions.ResponsabilityCenter && (y.ValorDimensão == x.CodeResponsabilityCenter || string.IsNullOrEmpty(x.CodeResponsabilityCenter))));
            }
            return Json(items);
        }

       
        public IActionResult Detalhes(int productivityUnitNo, int type, int code, string explorationStartDate)
        {
            UserAccessesViewModel userPermissions = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Features.Cafetarias_Refeitórios);

            if (userPermissions != null && userPermissions.Read.Value)
            {
                ViewBag.UPermissions = userPermissions;

                ViewBag.ProductivityUnityNo = productivityUnitNo;
                ViewBag.Type = type;
                ViewBag.Code = code;
                ViewBag.ExplorationStartDate = explorationStartDate;
                ViewBag.ReportServerURL = config.ReportServerURL;

                return View();
            }
            else
            {
                return Redirect(Url.Content("~/Error/AccessDenied"));
            }
        }

        [HttpPost]
      
        public JsonResult GetCoffeeShop([FromBody] Newtonsoft.Json.Linq.JObject requestParams)
        {
            CoffeeShopViewModel item;
            if (requestParams != null)
            {
                int productivityUnitNo = int.Parse(requestParams["productivityUnitNo"].ToString());
                int type = int.Parse(requestParams["type"].ToString());
                int code = int.Parse(requestParams["code"].ToString());
                string explorationStartDate = requestParams["explorationStartDate"].ToString();

                DateTime date;
                if (DateTime.TryParse(explorationStartDate, out date))
                {
                    var coffeeShop = DBCoffeeShops.GetById(productivityUnitNo, type, code, date);
                    item = DBCoffeeShops.ParseToViewModel(coffeeShop, config.NAVDatabaseName, config.NAVCompanyName);
                }
                else
                {
                    item = new CoffeeShopViewModel();
                    item.ProductivityUnitNo = productivityUnitNo;
                }
            }
            else
            {
                item = new CoffeeShopViewModel();
            }
            return Json(item);
        }

        [HttpPost]
       
        public JsonResult CreateCoffeeShop([FromBody] CoffeeShopViewModel data)
        {
            try
            {
                if (data != null)
                {
                    data.CreateUser = User.Identity.Name;
                    CafetariasRefeitórios itemToCreate = DBCoffeeShops.ParseToDB(data);

                    itemToCreate = DBCoffeeShops.Create(itemToCreate);

                    if (itemToCreate != null)
                    {
                        data = DBCoffeeShops.ParseToViewModel(itemToCreate, config.NAVDatabaseName, config.NAVCompanyName);
                        data.eReasonCode = 1;
                        data.eMessage = "Registo criado com sucesso.";
                    }
                    else
                    {
                        data.eReasonCode = 3;
                        data.eMessage = "Ocorreu um erro ao inserir os dados na base de dados.";
                    }
                }
            }
            catch (Exception)
            {
                data.eReasonCode = 2;
                data.eMessage = "Ocorreu um erro ao criar a cafetaria / refeitório.";
            }
            return Json(data);
        }

        [HttpPost]
      
        public JsonResult UpdateCoffeeShop([FromBody] CoffeeShopViewModel item)
        {
            if (item != null)
            {
                item.UpdateUser = User.Identity.Name;
                CafetariasRefeitórios updatedItem = DBCoffeeShops.ParseToDB(item);
                updatedItem = DBCoffeeShops.Update(updatedItem);

                if (updatedItem != null)
                {
                    item = DBCoffeeShops.ParseToViewModel(updatedItem, config.NAVDatabaseName, config.NAVCompanyName);
                    item.eReasonCode = 1;
                    item.eMessage = "Cafetaria / refeitório atualizado com sucesso.";
                }
                else
                {
                    item.eReasonCode = 2;
                    item.eMessage = "Ocorreu um erro ao atualizar a cafetaria / refeitório.";
                }
            }
            else
            {
                item = new CoffeeShopViewModel()
                {
                    eReasonCode = 3,
                    eMessage = "Ocorreu um erro ao atualizar. A cafetaria / refeitório não pode ser nulo."
                };
            }
            return Json(item);
        }

        [HttpPost]
      
        public JsonResult DeleteCoffeeShop([FromBody] CoffeeShopViewModel item)
        {
            ErrorHandler errorHandler = new ErrorHandler()
            {
                eReasonCode = 2,
                eMessage = "Ocorreu um erro ao eliminar o registo."
            };

            try
            {
                if (item != null)
                {
                    bool sucess = DBCoffeeShops.Delete(DBCoffeeShops.ParseToDB(item));
                    if (sucess)
                    {
                        item.eReasonCode = 1;
                        item.eMessage = "Registo eliminado com sucesso.";
                    }
                }
                else
                {
                    item = new CoffeeShopViewModel();
                }
            }
            catch
            {
                item.eReasonCode = errorHandler.eReasonCode;
                item.eMessage = errorHandler.eMessage;
            }
            return Json(item);
        }

        #region DiárioCafeterias
        public IActionResult RegistoNoRefeicoes(int NºUnidadeProdutiva, int CódigoCafetaria)
        {
            UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Features.Diário_Cafetarias_Refeitórios);
            if (UPerm != null && UPerm.Read.Value)
            {
                ViewBag.UPermissions = UPerm;
                ViewBag.CoffeeShopNo = CódigoCafetaria;
                ViewBag.ProdutiveUnityNo = NºUnidadeProdutiva;
                return View();
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        public IActionResult DiarioCafeterias(int NºUnidadeProdutiva, int CódigoCafetaria)
        {
            UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Features.Diário_Cafetarias_Refeitórios);
            if (UPerm != null && UPerm.Read.Value)
            {
                ViewBag.UPermissions = UPerm;
                ViewBag.CoffeeShopNo = CódigoCafetaria;
                ViewBag.ProdutiveUnityNo = NºUnidadeProdutiva;
                return View();
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        public JsonResult GetCoffeShopDiary([FromBody] CoffeeShopDiaryViewModel data)
        {
            try
            {
                List<DiárioCafetariaRefeitório> CoffeeShopDiaryList;

                if (data != null)
                {
                    CoffeeShopDiaryList = DBCoffeeShopsDiary.GetByIdsList((int)data.ProdutiveUnityNo, (int)data.CoffeShopCode, User.Identity.Name);

                    List<CoffeeShopDiaryViewModel> result = new List<CoffeeShopDiaryViewModel>();
                    CoffeeShopDiaryList.ForEach(x => result.Add(DBCoffeeShopsDiary.ParseToViewModel(x)));
                    foreach (var res in result)
                    {
                        if (res.MealType > 0)
                        {
                            res.DescriptionTypeMeal = DBMealTypes.GetById((int)res.MealType).Descrição;
                        }

                    }

                    return Json(result);
                }

                return Json(false);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public JsonResult GetCoffeShopDiaryDetails([FromBody] CoffeeShopDiaryViewModel data)
        {
            try
            {
                CoffeeShopDiaryViewModel coffeShopPar = new CoffeeShopDiaryViewModel();
                if (data != null)
                {
                    coffeShopPar.DateToday = DateTime.Today.ToString("yyyy-MM-dd");
                    coffeShopPar.CoffeShopCode = data.CoffeShopCode;
                    coffeShopPar.ProdutiveUnityNo = data.ProdutiveUnityNo;
                }
                else
                {
                    coffeShopPar.DateToday = DateTime.Today.ToString("yyyy-MM-dd");
                }

                return Json(coffeShopPar);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public JsonResult CreateCoffeeShopsDiary([FromBody] CoffeeShopDiaryViewModel data)
        {
            try
            {
                if (data != null && data.ProdutiveUnityNo != null && data.CoffeShopCode != null)
                {
                    if (data.RegistryDate == "")
                        data.RegistryDate = null;
                    if (data.Description == "")
                        data.Description = null;

                    List<DiárioCafetariaRefeitório> AllDiary = DBCoffeeShopsDiary.GetByIdsList((int)data.ProdutiveUnityNo, (int)data.CoffeShopCode, User.Identity.Name)
                        .Where(x => Convert.ToDateTime(x.DataRegisto) == Convert.ToDateTime(data.RegistryDate) && x.TipoMovimento == data.MovementType && x.Descrição == data.Description && x.Valor == data.Value).ToList();

                    if (AllDiary == null || AllDiary.Count == 0)
                    {
                        data.User = User.Identity.Name;
                        data.CreateUser = User.Identity.Name;
                        data.CreateDateTime = DateTime.Now.ToString();

                        DiárioCafetariaRefeitório newline = new DiárioCafetariaRefeitório();
                        newline = DBCoffeeShopsDiary.ParseToDB(data);
                        DBCoffeeShopsDiary.Create(newline);

                        if (newline.NºLinha > 0)
                        {
                            return Json(1);
                        }
                        return Json(2);
                    }
                    else
                        return Json(3);
                }

                return Json(4);
            }
            catch (Exception ex)
            {
                return Json(99);
            }
        }

        public JsonResult DeleteCoffeeShopsDiaryLine([FromBody] CoffeeShopDiaryViewModel data)
        {
            try
            {
                if (data != null)
                {
                    DiárioCafetariaRefeitório lineToRemove = new DiárioCafetariaRefeitório();
                    lineToRemove = DBCoffeeShopsDiary.GetById(data.LineNo);

                    DBCoffeeShopsDiary.Delete(lineToRemove);
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception ex)
            {

                return Json(false);
            }
        }

        public JsonResult UpdateCoffeeShopsDiaryLine([FromBody] List<CoffeeShopDiaryViewModel> data)
        {
            try
            {
                if (data != null)
                {
                    List<DiárioCafetariaRefeitório> linesToUpdate = new List<DiárioCafetariaRefeitório>();
                    linesToUpdate = DBCoffeeShopsDiary.ParseToDBList(data);
                    linesToUpdate.ForEach(x => DBCoffeeShopsDiary.Update(x));
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception ex)
            {

                return Json(false);
            }
        }

        public JsonResult MealRegistryLineRegister([FromBody] List<CoffeeShopDiaryViewModel> data)
        {
            try
            {
                if (data != null)
                {
                    int? id = data.Find(x => x.User == User.Identity.Name).CoffeShopCode;
                    CafetariasRefeitórios CoffeeShop = DBCoffeeShops.GetByCode((int)id);

                    foreach (var linesToRegist in data)
                    {
                        MovimentosCafetariaRefeitório MovementsToCreate = new MovimentosCafetariaRefeitório();
                        MovementsToCreate.CódigoCafetariaRefeitório = linesToRegist.CoffeShopCode;
                        MovementsToCreate.NºUnidadeProdutiva = linesToRegist.ProdutiveUnityNo;
                        MovementsToCreate.DataRegisto = linesToRegist.RegistryDate != "" ? DateTime.Parse(linesToRegist.RegistryDate) : (DateTime?)null;
                        MovementsToCreate.NºRecurso = linesToRegist.ResourceNo;
                        MovementsToCreate.Descrição = linesToRegist.Description;
                        MovementsToCreate.Tipo = CoffeeShop.Tipo;
                        if (linesToRegist.MovementType == 2 || linesToRegist.MovementType == 3)
                        {
                            MovementsToCreate.Valor = linesToRegist.Value * (-1);
                        }
                        else
                        {
                            MovementsToCreate.Valor = linesToRegist.Value;
                        }

                        MovementsToCreate.TipoMovimento = linesToRegist.MovementType;
                        MovementsToCreate.Quantidade = linesToRegist.Quantity;
                        MovementsToCreate.TipoRefeição = linesToRegist.MealType;
                        MovementsToCreate.DescriçãoTipoRefeição = linesToRegist.DescriptionTypeMeal;
                        MovementsToCreate.CódigoRegião = CoffeeShop.CódigoRegião ?? "";
                        MovementsToCreate.CódigoÁreaFuncional = CoffeeShop.CódigoÁreaFuncional ?? "";
                        MovementsToCreate.CódigoCentroResponsabilidade = CoffeeShop.CódigoCentroResponsabilidade ?? "";
                        MovementsToCreate.Utilizador = User.Identity.Name;
                        MovementsToCreate.DataHoraSistemaRegisto = DateTime.Now;
                        MovementsToCreate.DataHoraCriação = DateTime.Now;
                        MovementsToCreate.UtilizadorCriação = User.Identity.Name;

                        DBCoffeeShopMovements.Create(MovementsToCreate);
                        if (MovementsToCreate.NºMovimento > 0)
                        {
                            DiárioCafetariaRefeitório lineToRemove = new DiárioCafetariaRefeitório();
                            lineToRemove = DBCoffeeShopsDiary.GetById(linesToRegist.LineNo);
                            DBCoffeeShopsDiary.Delete(lineToRemove);
                        }
                    }

                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception ex)
            {

                return Json(false);
            }
        }

        public JsonResult CoffeeShopsDiaryLineRegister([FromBody] List<CoffeeShopDiaryViewModel> data)
        {
            try
            {
                if (data != null)
                {
                    int? id = data.Find(x => x.User == User.Identity.Name).CoffeShopCode;
                    CafetariasRefeitórios CoffeeShop = DBCoffeeShops.GetByCode((int)id);

                    foreach (var linesToRegist in data)
                    {
                        MovimentosCafetariaRefeitório MovementsToCreate = new MovimentosCafetariaRefeitório();
                        MovementsToCreate.CódigoCafetariaRefeitório = linesToRegist.CoffeShopCode;
                        MovementsToCreate.NºUnidadeProdutiva = linesToRegist.ProdutiveUnityNo;
                        MovementsToCreate.DataRegisto = linesToRegist.RegistryDate != "" ? DateTime.Parse(linesToRegist.RegistryDate) : (DateTime?)null;
                        MovementsToCreate.NºRecurso = linesToRegist.ResourceNo;
                        MovementsToCreate.Descrição = linesToRegist.Description;
                        MovementsToCreate.Tipo = CoffeeShop.Tipo;
                        if (linesToRegist.MovementType == 2 || linesToRegist.MovementType == 3)
                        {
                            MovementsToCreate.Valor = linesToRegist.Value * (-1);
                        }
                        else
                        {
                            MovementsToCreate.Valor = linesToRegist.Value;
                        }

                        MovementsToCreate.TipoMovimento = linesToRegist.MovementType;
                        MovementsToCreate.CódigoRegião = CoffeeShop.CódigoRegião ?? "";
                        MovementsToCreate.CódigoÁreaFuncional = CoffeeShop.CódigoÁreaFuncional ?? "";
                        MovementsToCreate.CódigoCentroResponsabilidade = CoffeeShop.CódigoCentroResponsabilidade ?? "";
                        MovementsToCreate.Utilizador = User.Identity.Name;
                        MovementsToCreate.DataHoraSistemaRegisto = DateTime.Now;
                        MovementsToCreate.DataHoraCriação = DateTime.Now;
                        MovementsToCreate.UtilizadorCriação = User.Identity.Name;

                        DBCoffeeShopMovements.Create(MovementsToCreate);
                        if (MovementsToCreate.NºMovimento > 0)
                        {
                            DiárioCafetariaRefeitório lineToRemove = new DiárioCafetariaRefeitório();
                            lineToRemove = DBCoffeeShopsDiary.GetById(linesToRegist.LineNo);
                            DBCoffeeShopsDiary.Delete(lineToRemove);
                        }
                    }

                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception ex)
            {

                return Json(false);
            }
        }

        public JsonResult ValidateDataRegisto([FromBody] CoffeeShopDiaryViewModel data)
        {
            int DiaLimiteRegistoReceita = 6;

            try
            {
                ConfigUtilizadores user = DBUserConfigurations.GetById(User.Identity.Name);

                if (user != null && user.RegistoDataDiarioCafetaria == true)
                    return Json(true);
                else
                {
                    if (data != null && !string.IsNullOrEmpty(data.RegistryDate))
                    {
                        DateTime DataRegisto = Convert.ToDateTime(data.RegistryDate);

                        DateTime DataAtual = DateTime.Now;
                        int DiaAtual = DataAtual.Day;

                        if (DataRegisto > DataAtual)
                            return Json(false);
                        else
                        {
                            if (DiaAtual > DiaLimiteRegistoReceita)
                            {
                                DateTime DataLimite = Convert.ToDateTime("01/" + DataAtual.Month + "/" + DataAtual.Year);
                                if (DataRegisto < DataLimite)
                                    return Json(false);
                                else
                                    return Json(true);
                            }
                            else
                            {
                                DateTime DataAtualMenos1Mes = DataAtual.AddMonths(-1);
                                DateTime DataLimite = Convert.ToDateTime("01/" + DataAtualMenos1Mes.Month + "/" + DataAtualMenos1Mes.Year);
                                if (DataRegisto < DataLimite)
                                    return Json(false);
                                else
                                    return Json(true);
                            }
                        }
                    }
                    else
                    {
                        return Json(false);
                    }
                }
            }
            catch (Exception ex)
            {

                return Json(false);
            }
        }
        #endregion

        #region MovimentsList
        public IActionResult MovimentsList(int NºUnidadeProdutiva, int CódigoCafetaria)
        {
            UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Features.Diário_Cafetarias_Refeitórios);
            if (UPerm != null && UPerm.Read.Value)
            {
                ViewBag.UPermissions = UPerm;
                ViewBag.CoffeeShopNo = CódigoCafetaria;
                ViewBag.ProdutiveUnityNo = NºUnidadeProdutiva;
                return View();
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        public JsonResult GetMovimentsList([FromBody] CoffeeShopDiaryViewModel data)
        {
            try
            {
                if (data != null)
                {
                    List<MovimentosCafetariaRefeitório> MovimentsList = DBCoffeeShopMovements.GetByUnitAndCoffeeShop((int)data.ProdutiveUnityNo, (int)data.CoffeShopCode);

                    List<CoffeeShopMovimentsViewModel> result = new List<CoffeeShopMovimentsViewModel>();
                    MovimentsList.ForEach(x => result.Add(DBCoffeeShopMovements.ParseToViewModel(x)));
                    foreach (var mov in result)
                    {
                        mov.MovementTypeText = mov.MovementType.HasValue ? EnumerablesFixed.TipoMovimento.Where(y => y.Id == mov.MovementType).FirstOrDefault() != null ? EnumerablesFixed.TipoMovimento.Where(y => y.Id == mov.MovementType).FirstOrDefault().Value : "" : "";
                        mov.TypeText = mov.Type.HasValue ? mov.Type == 1 ? "Cafeteiria" : "Refeitório" : "";
                    }

                    return Json(result);
                }

                return Json(false);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //1
        [HttpPost]
        [RequestSizeLimit(100_000_000)]
        public async Task<JsonResult> ExportToExcel_MovimentsList([FromBody] List<CoffeeShopMovimentsViewModel> Lista)
        {
            JObject dp = (JObject)Lista[0].ColunasEXCEL;

            string sWebRootFolder = _generalConfig.FileUploadFolder + "UnidadesProdutivas\\" + "tmp\\";
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
                ISheet excelSheet = workbook.CreateSheet("Cafetaria-Refeitório Movimentos");
                IRow row = excelSheet.CreateRow(0);
                int Col = 0;

                if (dp["movimentNo"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Nº Movimento"); Col = Col + 1; }
                if (dp["registryDate"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Data Registo"); Col = Col + 1; }
                //if (dp["resourceNo"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("No. Recurso"); Col = Col + 1; }
                if (dp["description"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Descrição"); Col = Col + 1; }
                if (dp["value"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Valor"); Col = Col + 1; }
                if (dp["movementTypeText"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Tipo Movimento"); Col = Col + 1; }
                if (dp["typeText"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Tipo"); Col = Col + 1; }
                if (dp["regionCode"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Código Região"); Col = Col + 1; }
                if (dp["functionalAreaCode"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Código Área Funcional"); Col = Col + 1; }
                if (dp["responsabilityCenterCode"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Centro Responsabilidade"); Col = Col + 1; }
                if (dp["user"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Utilizador"); Col = Col + 1; }
                if (dp["systemCreateDateTime"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue("Data de Sistema"); Col = Col + 1; }

                if (dp != null)
                {
                    int count = 1;
                    foreach (CoffeeShopMovimentsViewModel item in Lista)
                    {
                        Col = 0;
                        row = excelSheet.CreateRow(count);

                        if (dp["movimentNo"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.MovimentNo); Col = Col + 1; }
                        if (dp["registryDate"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.RegistryDate); Col = Col + 1; }
                        //if (dp["resourceNo"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.ResourceNo); Col = Col + 1; }
                        if (dp["description"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.Description); Col = Col + 1; }
                        if (dp["value"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.Value.ToString()); Col = Col + 1; }
                        if (dp["movementTypeText"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.MovementTypeText); Col = Col + 1; }
                        if (dp["typeText"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.TypeText); Col = Col + 1; }
                        if (dp["regionCode"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.RegionCode); Col = Col + 1; }
                        if (dp["functionalAreaCode"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.FunctionalAreaCode); Col = Col + 1; }
                        if (dp["responsabilityCenterCode"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.ResponsabilityCenterCode); Col = Col + 1; }
                        if (dp["user"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.User); Col = Col + 1; }
                        if (dp["systemCreateDateTime"]["hidden"].ToString() == "False") { row.CreateCell(Col).SetCellValue(item.SystemCreateDateTime); Col = Col + 1; }

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
        public IActionResult ExportToExcelDownload_MovimentsList(string sFileName)
        {
            sFileName = _generalConfig.FileUploadFolder + "UnidadesProdutivas\\" + "tmp\\" + sFileName;
            return new FileStreamResult(new FileStream(sFileName, FileMode.Open), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
        #endregion
    }
}