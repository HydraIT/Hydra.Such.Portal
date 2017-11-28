﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Hydra.Such.Data.ViewModel.Nutrition
{
    public class CoffeeShopViewModel : ErrorHandler
    {
        public int ProductivityUnitNo { get; set; } = 0;
        public int Type { get; set; } = 0;
        public int Code { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
        public string CodeRegion { get; set; } = string.Empty;
        public string CodeFunctionalArea { get; set; } = string.Empty;
        public string CodeResponsabilityCenter { get; set; } = string.Empty;
        public string CodeResponsible { get; set; } = string.Empty;
        public string StartDateExploration { get; set; } = string.Empty;
        public string EndDateExploration { get; set; } = string.Empty;
        public string Warehouse { get; set; } = string.Empty;
        public string WarehouseSupplier { get; set; } = string.Empty;
        public string ProjectNo { get; set; }
        public bool? Active { get; set; } = false;
        public string UpdateUser { get; set; } = string.Empty;
        public DateTime? CreateDate { get; set; }
        public string CreateUser { get; set; } = string.Empty;
        public DateTime? UpdateDate { get; set; }

        public decimal? TotalRevenues { get; set; }
        public decimal? TotalConsumption { get; set; }
        public decimal? TotalMeals { get; set; }
    }
}
