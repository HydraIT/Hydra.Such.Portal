﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hydra.Such.Data.Evolution.DatabaseReference
{
    [ModelMetadataType(typeof(IMaintenanceOrderLine))]
    public partial class MaintenanceOrderLine
    {
        [NotMapped]
        public bool IsToExecute
        {
            get { return !(this.FinishingDate  > new DateTime(1753, 1, 1)); }
        }

        [NotMapped]
        public List<Equipamento> Equipment;

    }

    public interface IMaintenanceOrderLine
    {
        byte[] Timestamp { get; set; }

        [Key, Column(Order = 0)]
        int DocumentType { get; set; }

        [Key, Column(Order = 1)]
        string MoNo { get; set; }

        [Key, Column(Order = 2)]
        int LineNo { get; set; }

         int? OrderStatus { get; set; }
         string SortField { get; set; }
         int? ObjectRefType { get; set; }
         string ObjectRefNo { get; set; }
         int? ObjectType { get; set; }
         string ObjectNo { get; set; }
         string ObjectDescription { get; set; }
         string ObjectDescription2 { get; set; }
         string FunctionalLocationNo { get; set; }
         string TaskListNo { get; set; }
         int? Priority { get; set; }
         string AdditionalData { get; set; }
         byte? Warranty { get; set; }
         string BomNo { get; set; }
         DateTime? WarrantyDate { get; set; }
         string ComponentOf { get; set; }
         int? CriticalLevel { get; set; }
         decimal? ResponseTimeHours { get; set; }
         decimal? MaintenanceTimeHours { get; set; }
         DateTime? StartingDate { get; set; }
         DateTime? StartingTime { get; set; }
         DateTime? FinishingDate { get; set; }
         DateTime? FinishingTime { get; set; }
         int? NotificationType { get; set; }
         string NotificationNo { get; set; }
         string ShortcutDimension1Code { get; set; }
         string ShortcutDimension2Code { get; set; }
         string JobNo { get; set; }
         int? LineStatus { get; set; }
         string CustomerNo { get; set; }
         string ResponsibilityCenter { get; set; }
         string PlannerGroupNo { get; set; }
         string ResourceNo { get; set; }
         DateTime? PostingDate { get; set; }
         DateTime? DocumentDate { get; set; }
         DateTime? OrderDate { get; set; }
         DateTime? OrderTime { get; set; }
         string OrderType { get; set; }
         byte? ResourceFilterYesNo { get; set; }
         int? FinalState { get; set; }
        [NotMapped]
        byte? UsedDmmFilterYesNo { get; set; }
         string ShortcutDimension3Code { get; set; }
         string ShortcutDimension4Code { get; set; }
        [NotMapped]
        byte? LinhaOrçamento { get; set; }
         string InventoryNo { get; set; }
         int? EstadoLinhasOrçamento { get; set; }
         int? FaultReasonCode { get; set; }
         int? IdEquipamento { get; set; }
         int? IdEquipEstado { get; set; }
         int? IdRotina { get; set; }
         int? Tbf { get; set; }
         int? IdInstituicao { get; set; }
         int? IdServico { get; set; }
    }
}
