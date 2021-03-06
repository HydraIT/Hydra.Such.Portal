﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hydra.Such.Data.Database;
using Hydra.Such.Data.ViewModel.Compras;
using Microsoft.EntityFrameworkCore;
using static Hydra.Such.Data.Enumerations;

namespace Hydra.Such.Data.Logic.Request
{
    public static class DBRequestTemplates
    {
        #region CRUD

        public static Requisição Create(Requisição item)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    item.ModeloDeRequisição = true;
                    item.DataHoraCriação = DateTime.Now;
                    ctx.Requisição.Add(item);
                    ctx.SaveChanges();
                }
                return item;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static Requisição Update(Requisição item)//, bool updateLines = false)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    //if (updateLines && item.LinhasRequisição != null)
                    //    DBRequestLine.Update(item.LinhasRequisição.ToList(), ctx);

                    item.ModeloDeRequisição = true;
                    item.DataHoraModificação = DateTime.Now;
                    ctx.Requisição.Update(item);
                    ctx.SaveChanges();
                }
                return item;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool Delete(Requisição item)//, bool updateLines = false)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    var itemToDelete = ctx.Requisição
                        .Include(x => x.LinhasRequisição)
                        .FirstOrDefault(x => x.NºRequisição == item.NºRequisição);
                    ctx.LinhasRequisição.RemoveRange(itemToDelete.LinhasRequisição);
                    ctx.Requisição.Remove(itemToDelete);
                    ctx.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static List<Requisição> GetAll()
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    return ctx.Requisição
                        .Where(x => x.ModeloDeRequisição.Value)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static Requisição GetById(string requestId)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    return ctx.Requisição
                        .Where(x => x.ModeloDeRequisição.Value)
                        .Include("LinhasRequisição")
                        .SingleOrDefault(x => x.NºRequisição == requestId);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //public static List<Requisição> GetByState(RequisitionStates state)
        //{
        //    try
        //    {
        //        int stateValue = (int)state;

        //        using (var ctx = new SuchDBContext())
        //        {
        //            return ctx.Requisição
        //                .Include("LinhasRequisição")
        //                .Where(x => x.Estado == stateValue)
        //                .ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}





        //public static Requisição UpdateHeaderAndLines(Requisição item)
        //{
        //    return Update(item, true);
        //}

        //public static bool Delete(Requisição ObjectToDelete)
        //{
        //    try
        //    {
        //        using (var ctx = new SuchDBContext())
        //        {
        //            ctx.Requisição.Remove(ObjectToDelete);
        //            ctx.SaveChanges();
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {

        //        return false;
        //    }
        //}

        //public static List<Requisição> GetAllModelRequest()
        //{
        //    try
        //    {
        //        using (var ctx = new SuchDBContext())
        //        {
        //            return ctx.Requisição.Where(x=> x.ModeloDeRequisição ==true).ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
        #endregion

        //public static List<Requisição> GetReqModel()
        //{
        //    try
        //    {
        //        using (var ctx = new SuchDBContext())
        //        {
        //            return ctx.Requisição.Where(x => x.ModeloDeRequisição == true).ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        //public static List<Requisição> GetReqByUserAreaStatus(string UserName, int AreaId, int Status)
        //{
        //    try
        //    {
        //        using (var ctx = new SuchDBContext())
        //        {
        //            return ctx.Requisição.Where(x => x.UtilizadorCriação == UserName && x.Área == AreaId && x.Estado == Status).ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
        #region Parse Utilities
        public static RequisitionTemplateViewModel ParseToTemplateViewModel(this Requisição item)
        {
            if (item != null)
            {
                return new RequisitionTemplateViewModel()
                {
                    RequisitionNo = item.NºRequisição,
                    NoDocumento = item.NoDocumento,
                    Area = item.Área,
                    //State = item.Estado,
                    State = item.Estado.HasValue && Enum.IsDefined(typeof(RequisitionStates), item.Estado.Value) ? (RequisitionStates)item.Estado.Value : (RequisitionStates?)null,
                    ProjectNo = item.NºProjeto,
                    RegionCode = item.CódigoRegião,
                    FunctionalAreaCode = item.CódigoÁreaFuncional,
                    CenterResponsibilityCode = item.CódigoCentroResponsabilidade,
                    LocalCode = item.CódigoLocalização,
                    EmployeeNo = item.NºFuncionário,
                    Vehicle = item.Viatura,
                    ReceivedDate = !item.DataReceção.HasValue ? "" : item.DataReceção.Value.ToString("yyyy-MM-dd"),
                    Urgent = item.Urgente,
                    Sample = item.Amostra,
                    Attachment = item.Anexo,
                    Immobilized = item.Imobilizado,
                    BuyCash = item.CompraADinheiro,
                    LocalCollectionCode = item.CódigoLocalRecolha,
                    LocalDeliveryCode = item.CódigoLocalEntrega,
                    Comments = item.Observações,
                    RequestModel = item.ModeloDeRequisição,
                    CreateUser = item.UtilizadorCriação,
                    CreateDate = !item.DataHoraCriação.HasValue ? "" : item.DataHoraCriação.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                    UpdateUser = item.UtilizadorModificação,
                    UpdateDate = item.DataHoraModificação,
                    RelatedSearches = item.CabimentoOrçamental,
                    Exclusive = item.Exclusivo,
                    AlreadyPerformed = item.JáExecutado,
                    Equipment = item.Equipamento,
                    StockReplacement = item.ReposiçãoDeStock,
                    Reclamation = item.Reclamação,
                    RequestReclaimNo = item.NºRequisiçãoReclamada,
                    ResponsibleCreation = item.ResponsávelCriação,
                    ResponsibleApproval = item.ResponsávelAprovação,
                    ResponsibleValidation = item.ResponsávelValidação,
                    ResponsibleReception = item.ResponsávelReceção,
                    ApprovalDate = item.DataAprovação,
                    ValidationDate = item.DataValidação,
                    UnitFoodProduction = item.UnidadeProdutivaAlimentação,
                    RequestNutrition = item.RequisiçãoNutrição,
                    RequestforDetergents = item.RequisiçãoDetergentes,
                    ProcedureCcpNo = item.NºProcedimentoCcp,
                    Approvers = item.Aprovadores,
                    LocalMarket = item.MercadoLocal,
                    LocalMarketRegion = item.RegiãoMercadoLocal,
                    RepairWithWarranty = item.ReparaçãoComGarantia,
                    Emm = item.Emm,
                    WarehouseDeliveryDate = !item.DataEntregaArmazém.HasValue ? "" : item.DataEntregaArmazém.Value.ToString("yyyy-MM-dd"),
                    LocalCollection = item.LocalDeRecolha,
                    CollectionAddress = item.MoradaRecolha,
                    Collection2Address = item.Morada2Recolha,
                    CollectionPostalCode = item.CódigoPostalRecolha,
                    CollectionLocality = item.LocalidadeRecolha,
                    CollectionContact = item.ContatoRecolha,
                    CollectionResponsibleReception = item.ResponsávelReceçãoRecolha,
                    LocalDelivery = item.LocalEntrega,
                    DeliveryAddress = item.MoradaEntrega,
                    Delivery2Address = item.Morada2Entrega,
                    DeliveryPostalCode = item.CódigoPostalEntrega,
                    LocalityDelivery = item.LocalidadeEntrega,
                    DeliveryContact = item.ContatoEntrega,
                    ResponsibleReceptionReception = item.ResponsávelReceçãoReceção,
                    InvoiceNo = item.NºFatura,
                    LocalMarketDate = item.DataMercadoLocal,
                    // EstimatedValue = item.,
                    MarketInquiryNo = item.NºConsultaMercado,
                    OrderNo = item.NºEncomenda,
                    RequisitionDate = !item.DataRequisição.HasValue ? "" : item.DataRequisição.Value.ToString("yyyy-MM-dd"),
                    //dimension = item.,
                    //Budget = item.,
                    Lines = item.LinhasRequisição.ToList().ParseToTemplateViewModel(),
                };
            }
            return null;
        }

        public static List<RequisitionTemplateViewModel> ParseToTemplateViewModel(this List<Requisição> items)
        {
            List<RequisitionTemplateViewModel> parsedItems = new List<RequisitionTemplateViewModel>();
            if (items != null)
                items.ForEach(x =>
                    parsedItems.Add(x.ParseToTemplateViewModel()));
            return parsedItems;
        }

        public static Requisição ParseToDB(this RequisitionTemplateViewModel item)
        {
            if (item != null)
            {
                return new Requisição()
                {
                    NºRequisição = item.RequisitionNo,
                    NoDocumento = item.NoDocumento,
                    Área = item.Area,
                    //Estado = item.State.HasValue ? (int)item.State.Value : (int?)null,
                    Estado = item.State.HasValue ? (int)item.State.Value : (int?)null,
                    NºProjeto = item.ProjectNo,
                    CódigoRegião = item.RegionCode,
                    CódigoÁreaFuncional = item.FunctionalAreaCode,
                    CódigoCentroResponsabilidade = item.CenterResponsibilityCode,
                    CódigoLocalização = item.LocalCode,
                    NºFuncionário = item.EmployeeNo,
                    Viatura = item.Vehicle,
                    DataReceção = string.IsNullOrEmpty(item.ReceivedDate) ? (DateTime?)null : DateTime.Parse(item.ReceivedDate),
                    Urgente = item.Urgent,
                    Amostra = item.Sample,
                    Anexo = item.Attachment,
                    Imobilizado = item.Immobilized,
                    CompraADinheiro = item.BuyCash,
                    CódigoLocalRecolha = item.LocalCollectionCode,
                    CódigoLocalEntrega = item.LocalDeliveryCode,
                    Observações = item.Comments,
                    ModeloDeRequisição = item.RequestModel,
                    UtilizadorCriação = item.CreateUser,
                    DataHoraCriação = string.IsNullOrEmpty(item.CreateDate) ? (DateTime?)null : DateTime.Parse(item.CreateDate),
                    UtilizadorModificação = item.UpdateUser,
                    DataHoraModificação = item.UpdateDate,
                    CabimentoOrçamental = item.RelatedSearches,
                    Exclusivo = item.Exclusive,
                    JáExecutado = item.AlreadyPerformed,
                    Equipamento = item.Equipment,
                    ReposiçãoDeStock = item.StockReplacement,
                    Reclamação = item.Reclamation,
                    NºRequisiçãoReclamada = item.RequestReclaimNo,
                    ResponsávelCriação = item.ResponsibleCreation,
                    ResponsávelAprovação = item.ResponsibleApproval,
                    ResponsávelValidação = item.ResponsibleValidation,
                    ResponsávelReceção = item.ResponsibleReception,
                    DataAprovação = item.ApprovalDate,
                    DataValidação = item.ValidationDate,
                    UnidadeProdutivaAlimentação = item.UnitFoodProduction,
                    RequisiçãoNutrição = item.RequestNutrition,
                    RequisiçãoDetergentes = item.RequestforDetergents,
                    NºProcedimentoCcp = item.ProcedureCcpNo,
                    Aprovadores = item.Approvers,
                    MercadoLocal = item.LocalMarket,
                    RegiãoMercadoLocal = item.LocalMarketRegion,
                    ReparaçãoComGarantia = item.RepairWithWarranty,
                    Emm = item.Emm,
                    DataEntregaArmazém = string.IsNullOrEmpty(item.WarehouseDeliveryDate) ? (DateTime?)null : DateTime.Parse(item.WarehouseDeliveryDate),
                    LocalDeRecolha = item.LocalCollection,
                    MoradaRecolha = item.CollectionAddress,
                    Morada2Recolha = item.Collection2Address,
                    CódigoPostalRecolha = item.CollectionPostalCode,
                    LocalidadeRecolha = item.CollectionLocality,
                    ContatoRecolha = item.CollectionContact,
                    ResponsávelReceçãoRecolha = item.CollectionResponsibleReception,
                    LocalEntrega = item.LocalDelivery,
                    MoradaEntrega = item.DeliveryAddress,
                    Morada2Entrega = item.Delivery2Address,
                    CódigoPostalEntrega = item.DeliveryPostalCode,
                    LocalidadeEntrega = item.LocalityDelivery,
                    ContatoEntrega = item.DeliveryContact,
                    ResponsávelReceçãoReceção = item.ResponsibleReceptionReception,
                    NºFatura = item.InvoiceNo,
                    DataMercadoLocal = item.LocalMarketDate,
                    // EstimatedValue = item.,
                    NºConsultaMercado = item.MarketInquiryNo,
                    NºEncomenda = item.OrderNo,
                    DataRequisição = item.RequisitionDate != null && item.RequisitionDate != "" ? DateTime.Parse(item.RequisitionDate) : (DateTime?)null,
                    //dimension = item.,
                    //Budget = item.,
                    LinhasRequisição = item.Lines.ParseToDB(),
                };
            }
            return null;
        }

        public static List<Requisição> ParseToDB(this List<RequisitionTemplateViewModel> items)
        {
            List<Requisição> parsedItems = new List<Requisição>();
            if (items != null)
                items.ForEach(x =>
                    parsedItems.Add(x.ParseToDB()));
            return parsedItems;
        }
        #endregion
    }
}
