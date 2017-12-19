﻿using System;
using System.Collections.Generic;
using System.Linq;
using Hydra.Such.Data.Database;
using Hydra.Such.Data.Logic;
using Hydra.Such.Data.Logic.Nutrition;
using Hydra.Such.Data.Logic.Request;
using Hydra.Such.Data.ViewModel.Compras;
using Hydra.Such.Data.ViewModel.Nutrition;
using Microsoft.AspNetCore.Mvc;

namespace Hydra.Such.Portal.Areas.Nutricao.Controllers
{
    public class NecessidadeComprasController : Controller
    {
        [Area("Nutricao")]
        public IActionResult Detalhes(int id)
        {
            if (id != null && id >0)
            {
                UnidadesProdutivas ProductivityUnitDB = DBProductivityUnits.GetById(id);
                ViewBag.ProductivityUnitId = ProductivityUnitDB.NºUnidadeProdutiva;
                ViewBag.ProductivityUnitDesc = ProductivityUnitDB.Descrição;
            }
            return View();
        }

        [HttpPost]
        [Area("Nutricao")]
        public JsonResult GetGridValues()
        {
            List<DailyRequisitionProductiveUnitViewModel> result = DBShoppingNecessity.GetAll().ParseToViewModel();
            return Json(result);
        }

       
            [HttpPost]
        [Area("Nutricao")]
        public JsonResult GetModelRequisition()
            {
                List<RequisitionViewModel> result = DBRequest.GetAllModelRequest().ParseToViewModel();
                //Apply User Dimensions Validations
                List<AcessosDimensões> CUserDimensions = DBUserDimensions.GetByUserId(User.Identity.Name);
                //Regions
                if (CUserDimensions.Where(y => y.Dimensão == 1).Count() > 0)
                    result.RemoveAll(x => !CUserDimensions.Any(y => y.Dimensão == 1 && y.ValorDimensão == x.RegionCode));
                //FunctionalAreas
                if (CUserDimensions.Where(y => y.Dimensão == 2).Count() > 0)
                    result.RemoveAll(x => !CUserDimensions.Any(y => y.Dimensão == 2 && y.ValorDimensão == x.FunctionalAreaCode));
                //ResponsabilityCenter
                if (CUserDimensions.Where(y => y.Dimensão == 3).Count() > 0)
                    result.RemoveAll(x => !CUserDimensions.Any(y => y.Dimensão == 3 && y.ValorDimensão == x.CenterResponsibilityCode));
            return Json(result);
        }

        [HttpPost]
        [Area("Nutricao")]
        public JsonResult UpdateShoppingNecessity([FromBody] List<DailyRequisitionProductiveUnitViewModel> dp)
        {
            List<DiárioRequisiçãoUnidProdutiva> previousList;
                // Get All
                previousList = DBShoppingNecessity.GetAll();
            foreach (DiárioRequisiçãoUnidProdutiva line in previousList)
            {
                if (!dp.Any(x => x.LineNo == line.NºLinha))
                {
                    DBShoppingNecessity.Delete(line);
                }
            }

            //Update or Create
            try
            {
                dp.ForEach(x =>
                {
                    List<DiárioRequisiçãoUnidProdutiva> dpObject = DBShoppingNecessity.GetByLineNo(x.LineNo);

                    if (dpObject.Count > 0)
                    {
                        DiárioRequisiçãoUnidProdutiva newdp = dpObject.FirstOrDefault();

                        newdp.NºLinha = x.LineNo;
                        newdp.NºUnidadeProdutiva = x.ProductionUnitNo;
                        newdp.NºProduto = x.ProductNo;
                        newdp.Descrição = x.Description;
                        newdp.CódUnidadeMedida = x.UnitMeasureCode;
                        newdp.Quantidade = x.Quantity;
                        newdp.CustoUnitárioDireto = x.DirectUnitCost;
                        newdp.Valor = x.TotalValue;
                        newdp.NºProjeto = x.ProjectNo;
                        newdp.NºFornecedor = x.SupplierNo;
                        newdp.TipoRefeição = x.MealType;
                        newdp.TabelaPreçosFornecedor = x.TableSupplierPrice;
                        newdp.DataHoraCriação = x.CreateDateTime;
                        newdp.DataHoraModificação = DateTime.Now;
                        newdp.UtilizadorCriação = x.CreateUser;
                        newdp.UtilizadorModificação = User.Identity.Name;
                        newdp.DataReceçãoEsperada = string.IsNullOrEmpty(x.ExpectedReceptionDate) ? (DateTime?)null : DateTime.Parse(x.ExpectedReceptionDate);
                        newdp.DataPPreçoFornecedor = string.IsNullOrEmpty(x.DateByPriceSupplier) ? (DateTime?)null : DateTime.Parse(x.DateByPriceSupplier);
                        newdp.CodigoLocalização = x.LocalCode;
                        newdp.QuantidadePorUnidMedida = x.QuantitybyUnitMeasure;
                        newdp.CodigoProdutoFornecedor = x.SupplierProductCode;
                        newdp.DescriçãoProdutoFornecedor = x.SupplierProductDescription;
                        newdp.NomeFornecedor = x.SupplierName;
                        newdp.NºEncomendaAberto = x.OpenOrderNo;
                        newdp.NºLinhaEncomendaAberto = x.OrderLineOpenNo;
                        newdp.DescriçãoUnidadeProduto = x.ProductUnitDescription;
                        newdp.NºDocumento = x.DocumentNo;
                        DBShoppingNecessity.Update(newdp);
                    }
                    else
                    {
                        DiárioRequisiçãoUnidProdutiva newdp = new DiárioRequisiçãoUnidProdutiva()
                        {
                            NºLinha = x.LineNo,
                            NºUnidadeProdutiva = x.ProductionUnitNo,
                            NºProduto = x.ProductNo,
                            Descrição = x.Description,
                            CódUnidadeMedida = x.UnitMeasureCode,
                            Quantidade = x.Quantity,
                            CustoUnitárioDireto = x.DirectUnitCost,
                            Valor = x.TotalValue,
                            NºProjeto = x.ProjectNo,
                            NºFornecedor = x.SupplierNo,
                            TipoRefeição = x.MealType,
                            TabelaPreçosFornecedor = x.TableSupplierPrice,
                            DataHoraModificação = x.UpdateDateTime,
                            UtilizadorModificação = User.Identity.Name,
                            DataReceçãoEsperada = string.IsNullOrEmpty(x.ExpectedReceptionDate) ? (DateTime?)null : DateTime.Parse(x.ExpectedReceptionDate),
                            DataPPreçoFornecedor = string.IsNullOrEmpty(x.DateByPriceSupplier) ? (DateTime?)null : DateTime.Parse(x.DateByPriceSupplier),
                            CodigoLocalização = x.LocalCode,
                            QuantidadePorUnidMedida = x.QuantitybyUnitMeasure,
                            CodigoProdutoFornecedor = x.SupplierProductCode,
                            DescriçãoProdutoFornecedor = x.SupplierProductDescription,
                            NomeFornecedor = x.SupplierName,
                            NºEncomendaAberto = x.OpenOrderNo,
                            NºLinhaEncomendaAberto = x.OrderLineOpenNo,
                            DescriçãoUnidadeProduto = x.ProductUnitDescription,
                            NºDocumento = x.DocumentNo
                    };
                        newdp.UtilizadorCriação = User.Identity.Name;

                        newdp.DataHoraCriação = DateTime.Now;
                        DBShoppingNecessity.Create(newdp);
                    }


                });
            }
            catch (Exception e)
            {
            }


            return Json(dp);
        }
         [HttpPost]
        [Area("Nutricao")]
        public JsonResult GetProductivityUnits()
        {
            List<ProductivityUnitViewModel> result = DBProductivityUnits.ParseListToViewModel(DBProductivityUnits.GetAll());
            return Json(result);
        }

        [HttpPost]
        [Area("Nutricao")]
        public JsonResult GenerateRequesition([FromBody] DailyRequisitionProductiveUnitViewModel data)
        {
            Requisição result = new Requisição();

            //Get Contract Numeration
            Configuração Configs = DBConfigurations.GetById(1);
            int ProjectNumerationConfigurationId = 0;
            ProjectNumerationConfigurationId = Configs.NumeraçãoPréRequisições.Value;
            try
            {
                UnidadesProdutivas ProductivityUnitDB = DBProductivityUnits.GetById((int)data.ProductionUnitNo);
                if (data != null && ProductivityUnitDB != null)
                {
                    result.NºRequisição = DBNumerationConfigurations.GetNextNumeration(ProjectNumerationConfigurationId, (result.NºRequisição == "" || result.NºRequisição == null));
                    result.CódigoÁreaFuncional = ProductivityUnitDB.CódigoÁreaFuncional;
                    result.CódigoCentroResponsabilidade = ProductivityUnitDB.CódigoCentroResponsabilidade;
                    result.CódigoRegião = ProductivityUnitDB.CódigoRegião;
                    result.CódigoLocalização = data.LocalCode;
                    result.UnidadeProdutivaAlimentação = Convert.ToString(ProductivityUnitDB.NºUnidadeProdutiva);
                    result.RequisiçãoNutrição = true;
                    result.DataReceção = string.IsNullOrEmpty(data.ExpectedReceptionDate) ? (DateTime?)null : DateTime.Parse(data.ExpectedReceptionDate);
                    result.Aprovadores = "";
                    result.UtilizadorCriação = User.Identity.Name;
                    result.DataHoraCriação = DateTime.Now;
                    result.Estado = (int)RequisitionStates.Pending;
                    DBRequest.Create(result);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(null);
            }
            return Json(result);
        }
        [HttpPost]
        [Area("Nutricao")]
        public JsonResult GenerateRequesitionLines([FromBody] List<DailyRequisitionProductiveUnitViewModel> data)
        {
            LinhasRequisição result = new LinhasRequisição();
            if (data != null && data.Count>0)
            {
                foreach (DailyRequisitionProductiveUnitViewModel rpu in data)
                {
                   
                    Configuração Configs = DBConfigurations.GetById(1);
                    int ProjectNumerationConfigurationId = 0;
                    ProjectNumerationConfigurationId = Configs.NumeraçãoPréRequisições.Value;
                    try
                    {
                        UnidadesProdutivas ProductivityUnitDB = DBProductivityUnits.GetById((int)rpu.ProductionUnitNo);
                        if (ProductivityUnitDB != null)
                        {
                            result.NºRequisição = DBNumerationConfigurations.GetNextNumeration(ProjectNumerationConfigurationId, (result.NºRequisição == "" || result.NºRequisição == null));
                            result.Tipo = 2;
                            result.Código = rpu.ProductNo;
                            result.Descrição = rpu.Description;
                            result.CódigoUnidadeMedida = rpu.UnitMeasureCode;
                            result.QtdPorUnidadeDeMedida = rpu.QuantitybyUnitMeasure;
                            result.CódigoLocalização = rpu.LocalCode;
                            result.QuantidadeARequerer = rpu.Quantity;
                            result.CustoUnitário = rpu.DirectUnitCost;
                            result.NºFornecedor = rpu.SupplierNo;
                            result.DataReceçãoEsperada = string.IsNullOrEmpty(rpu.ExpectedReceptionDate) ? (DateTime?)null : DateTime.Parse(rpu.ExpectedReceptionDate) ;
                            result.CódigoProdutoFornecedor = rpu.SupplierProductCode;
                            result.NºEncomendaAberto = rpu.OpenOrderNo;
                            result.NºLinhaEncomendaAberto =Convert.ToInt32(rpu.OrderLineOpenNo);
                            result.CódigoÁreaFuncional = ProductivityUnitDB.CódigoÁreaFuncional;
                            result.CódigoCentroResponsabilidade = ProductivityUnitDB.CódigoCentroResponsabilidade;
                            result.CódigoRegião = ProductivityUnitDB.CódigoRegião;
                            result.UtilizadorCriação = User.Identity.Name;
                            DBRequestLine.Create(result);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        return Json(null);
                    }
                }
            }
            return Json(result);
        }
        [Area("Nutricao")]
        public JsonResult GenerateByRequesition([FromBody] List<RequisitionViewModel> data)
        {
            if (data != null && data.Count > 0)
            {
                foreach (RequisitionViewModel rpu in data)
                {
                    UnidadesProdutivas ProductivityUnitDB = DBProductivityUnits.GetById(Convert.ToInt32(rpu.UnitFoodProduction));
                    DiárioRequisiçãoUnidProdutiva newdp = new DiárioRequisiçãoUnidProdutiva()
                    {
                        NºUnidadeProdutiva = ProductivityUnitDB.NºUnidadeProdutiva,
                        Quantidade = 0,
                        DataReceçãoEsperada = string.IsNullOrEmpty(rpu.ReceivedDate) ? (DateTime?)null : DateTime.Parse(rpu.ReceivedDate),
                        CodigoLocalização = rpu.LocalCode
                    };
                    newdp.UtilizadorCriação = User.Identity.Name;
                    newdp.DataHoraCriação = DateTime.Now;
                    DBShoppingNecessity.Create(newdp);
                }
            }
            return Json(data);
        }

    }
}