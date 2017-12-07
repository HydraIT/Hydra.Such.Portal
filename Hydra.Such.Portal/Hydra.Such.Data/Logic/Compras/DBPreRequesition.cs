﻿using Hydra.Such.Data.Database;
using Hydra.Such.Data.ViewModel.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hydra.Such.Data.Logic.Compras
{
    public class DBPreRequesition
    {
        public static List<PréRequisição> GetByNo(string PreRequesitionNo)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    return ctx.PréRequisição.Where(x => x.NºPréRequisição == PreRequesitionNo).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public static List<PréRequisição> GetAll()
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    return ctx.PréRequisição.ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public static PréRequisição Create(PréRequisição ObjectToCreate)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    ctx.PréRequisição.Add(ObjectToCreate);
                    ctx.SaveChanges();
                }

                return ObjectToCreate;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public static bool DeleteByPreRequesitionNo(string PreRequesitionNo)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    ctx.PréRequisição.RemoveRange(ctx.PréRequisição.Where(x => x.NºPréRequisição == PreRequesitionNo));
                    ctx.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public static PréRequisição Update(PréRequisição ObjectToUpdate)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    ObjectToUpdate.DataHoraModificação = DateTime.Now;
                    ctx.PréRequisição.Update(ObjectToUpdate);
                    ctx.SaveChanges();
                }

                return ObjectToUpdate;
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public static PréRequisição ParseToDB(PreRequesitionsViewModel x)
        {
            PréRequisição result = new PréRequisição()
            {   
                 NºPréRequisição = x.PreRequesitionsNo, 
                 Área = x.Area,
                 TipoRequisição = x.RequesitionType,
                 NºProjeto = x.ProjectNo,
                 CódigoRegião = x.RegionCode,
                 CódigoÁreaFuncional = x.FunctionalAreaCode,
                 CódigoCentroResponsabilidade = x.ResponsabilityCenterCode,
                 Urgente = x.Urgent,
                 Amostra = x.Sample,
                 Anexo = x.Attachment,
                 Imobilizado = x.Immobilized,
                 CompraADinheiro = x.MoneyBuy,
                 CódigoLocalRecolha = x.LocalCollectionCode,
                 CódigoLocalEntrega = x.LocalDeliveryCode,
                 Observações = x.Notes,
                 ModeloDePréRequisição = x.PreRequesitionModel,
                 DataHoraCriação = x.CreateDateTime,
                 UtilizadorCriação = x.CreateUser, 
                 DataHoraModificação = x.UpdateDateTime,
                 UtilizadorModificação = x.UpdateUser,
                 Exclusivo = x.Exclusive,
                 JáExecutado = x.AlreadyExecuted,
                 Equipamento = x.Equipment,
                 ReposiçãoDeStock = x.StockReplacement,
                 Reclamação = x.Complaint,
                 CódigoLocalização = x.LocationCode,
                 CabimentoOrçamental = x.FittingBudget,
                 NºFuncionário = x.EmployeeNo,
                 Viatura = x.Vehicle,
                 NºRequesiçãoReclamada = x.ClaimedRequesitionNo,
                 ResponsávelCriação = x.CreateResponsible,
                 ResponsávelAprovação = x.ApprovalResponsible,
                 ResponsávelValidação = x.ValidationResponsible,
                 ResponsávelReceção = x.ReceptionResponsible,
                 DataAprovação = x.ApprovalDate,
                 DataValidação = x.ValidationDate,
                 DataReceção = x.ReceptionDate != null ? DateTime.Parse(x.ReceptionDate) : (DateTime?)null,
                 UnidadeProdutivaAlimentação = x.FoodProductionUnit, 
                 RequisiçãoNutrição = x.NutritionRequesition,
                 RequisiçãoDetergentes = x.DetergentsRequisition,
                 NºProcedimentoCcp = x.CCPProcedureNo,
                 Aprovadores = x.Approvers,
                 MercadoLocal = x.LocalMarket,
                 RegiãoMercadoLocal = x.LocalMarketRegion,
                 ReparaçãoComGarantia = x.WarrantyRepair,
                 Emm = x.EMM,
                 DataEntregaArmazém = x.DeliveryWarehouseDate != null ? DateTime.Parse(x.DeliveryWarehouseDate) : (DateTime?)null,
                 LocalDeRecolha = x.CollectionLocal,
                 MoradaRecolha = x.CollectionAddress,
                 Morada2Recolha = x.CollectionAddress2,
                 CodigoPostalRecolha = x.CollectionPostalCode,
                 LocalidadeRecolha = x.CollectionLocality,
                 ContatoRecolha = x.CollectionContact,
                 ResponsávelReceçãoRecolha = x.CollectionReceptionResponsible,
                 LocalEntrega = x.DeliveryLocal,
                 MoradaEntrega = x.DeliveryAddress,
                 Morada2Entrega = x.DeliveryAddress2,
                 CódigoPostalEntrega = x.DeliveryPostalCode,
                 LocalidadeEntrega = x.DeliveryLocality,
                 ContatoEntrega = x.DeliveryContact,
                 ResponsávelReceçãoReceção = x.ReceptionReceptionResponsible,
                 NºFatura = x.InvoiceNo,
            };
            return result;
        }
        public static PreRequesitionsViewModel ParseToViewModel(PréRequisição x)
        {
            PreRequesitionsViewModel result = new PreRequesitionsViewModel()
            {
                PreRequesitionsNo = x.NºPréRequisição,
                Area = x.Área,
                RequesitionType = x.TipoRequisição,
                ProjectNo = x.NºProjeto,
                RegionCode = x.CódigoRegião,
                FunctionalAreaCode = x.CódigoÁreaFuncional ,
                ResponsabilityCenterCode = x.CódigoCentroResponsabilidade,
                Urgent = x.Urgente,
                Sample = x.Amostra,
                Attachment = x.Anexo,
                Immobilized = x.Imobilizado,
                MoneyBuy = x.CompraADinheiro,
                LocalCollectionCode = x.CódigoLocalRecolha,
                LocalDeliveryCode = x.CódigoLocalEntrega,
                Notes = x.Observações,
                PreRequesitionModel = x.ModeloDePréRequisição,
                CreateDateTime = x.DataHoraCriação,
                CreateUser = x.UtilizadorCriação,
                UpdateDateTime = x.DataHoraModificação,
                UpdateUser = x.UtilizadorModificação,
                Exclusive = x.Exclusivo,
                AlreadyExecuted = x.JáExecutado,
                Equipment = x.Equipamento,
                StockReplacement = x.ReposiçãoDeStock,
                Complaint = x.Reclamação,
                LocationCode = x.CódigoLocalização,
                FittingBudget = x.CabimentoOrçamental,
                EmployeeNo = x.NºFuncionário,
                Vehicle = x.Viatura,
                ClaimedRequesitionNo = x.NºRequesiçãoReclamada,
                CreateResponsible = x.ResponsávelCriação,
                ApprovalResponsible = x.ResponsávelAprovação,
                ValidationResponsible = x.ResponsávelValidação,
                ReceptionResponsible = x.ResponsávelReceção,
                ApprovalDate = x.DataAprovação,
                ValidationDate = x.DataValidação,
                ReceptionDate = x.DataReceção.HasValue ? x.DataReceção.Value.ToString("yyyy-MM-dd") : "",
                FoodProductionUnit = x.UnidadeProdutivaAlimentação,
                NutritionRequesition = x.RequisiçãoNutrição,
                DetergentsRequisition = x.RequisiçãoDetergentes,
                CCPProcedureNo = x.NºProcedimentoCcp,
                Approvers = x.Aprovadores,
                LocalMarket = x.MercadoLocal,
                LocalMarketRegion = x.RegiãoMercadoLocal,
                WarrantyRepair = x.ReparaçãoComGarantia,
                EMM = x.Emm,
                DeliveryWarehouseDate = x.DataEntregaArmazém.HasValue ? x.DataEntregaArmazém.Value.ToString("yyyy-MM-dd") : "",
                CollectionLocal = x.LocalDeRecolha,
                CollectionAddress = x.MoradaRecolha,
                CollectionAddress2 = x.Morada2Recolha,
                CollectionPostalCode = x.CodigoPostalRecolha,
                CollectionLocality = x.LocalidadeRecolha,
                CollectionContact = x.ContatoRecolha,
                CollectionReceptionResponsible = x.ResponsávelReceçãoRecolha,
                DeliveryLocal = x.LocalEntrega,
                DeliveryAddress = x.MoradaEntrega,
                DeliveryAddress2 = x.Morada2Entrega,
                DeliveryPostalCode = x.CódigoPostalEntrega,
                DeliveryLocality = x.LocalidadeEntrega,
                DeliveryContact = x.ContatoEntrega,
                ReceptionReceptionResponsible = x.ResponsávelReceçãoReceção,
                InvoiceNo = x.NºFatura,
            };

            return result;

        }

    }
}
