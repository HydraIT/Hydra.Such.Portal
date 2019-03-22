﻿using System;
using System.Collections.Generic;

namespace Hydra.Such.Data.Evolution.Database
{
    public partial class OrcamentoLine
    {
        public byte[] Timestamp { get; set; }
        public int DocumentType { get; set; }
        public string MoNo { get; set; }
        public int LineNo { get; set; }
        public int? OrderStatus { get; set; }
        public string SortField { get; set; }
        public int? ObjectRefType { get; set; }
        public string ObjectRefNo { get; set; }
        public int? ObjectType { get; set; }
        public string ObjectNo { get; set; }
        public string ObjectDescription { get; set; }
        public string ObjectDescription2 { get; set; }
        public string FunctionalLocationNo { get; set; }
        public string TaskListNo { get; set; }
        public int? Priority { get; set; }
        public string AdditionalData { get; set; }
        public byte? Warranty { get; set; }
        public string BomNo { get; set; }
        public DateTime? WarrantyDate { get; set; }
        public string ComponentOf { get; set; }
        public int? CriticalLevel { get; set; }
        public decimal? ResponseTimeHours { get; set; }
        public decimal? MaintenanceTimeHours { get; set; }
        public DateTime? StartingDate { get; set; }
        public DateTime? StartingTime { get; set; }
        public DateTime? FinishingDate { get; set; }
        public DateTime? FinishingTime { get; set; }
        public int? NotificationType { get; set; }
        public string NotificationNo { get; set; }
        public string ShortcutDimension1Code { get; set; }
        public string ShortcutDimension2Code { get; set; }
        public string JobNo { get; set; }
        public int? LineStatus { get; set; }
        public string CustomerNo { get; set; }
        public string ResponsibilityCenter { get; set; }
        public string PlannerGroupNo { get; set; }
        public string ResourceNo { get; set; }
        public DateTime? PostingDate { get; set; }
        public DateTime? DocumentDate { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? OrderTime { get; set; }
        public string OrderType { get; set; }
        public byte? ResourceFilterYesNo { get; set; }
        public int? FinalState { get; set; }
        public byte? UsedDmmFilterYesNo { get; set; }
        public string ShortcutDimension3Code { get; set; }
        public string ShortcutDimension4Code { get; set; }
        public byte? LinhaOrçamento { get; set; }
        public string InventoryNo { get; set; }
        public int? EstadoLinhasOrçamento { get; set; }
        public int? FaultReasonCode { get; set; }
        public int OrcAlternativo { get; set; }
        public int? IdTipoEquipamento { get; set; }
        public int? IdCategoriaEquipamento { get; set; }
        public int? IdEquipamento { get; set; }
        public int? IdMarcaEquipamento { get; set; }
        public int? IdModeloEquipamento { get; set; }
        public string NumSerieEquipamento { get; set; }
        public string NumInventarioEquipamento { get; set; }
        public string NumEquipamento { get; set; }
    }
}
