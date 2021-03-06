﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Hydra.Such.Data.ViewModel.Approvals
{
    public class ApprovalConfigurationsViewModel
    {
        public int Id { get; set; }
        public int? Type { get; set; }
        public int? Area { get; set; }
        public string FunctionalArea { get; set; }
        public string ResponsabilityCenter { get; set; }
        public string Region { get; set; }
        public int? Level { get; set; }
        public decimal? ApprovalValue { get; set; }
        public string ApprovalUser { get; set; }
        public int? ApprovalGroup { get; set; }
        public string ApprovalGroupText { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string CreateUser { get; set; }
        public string UpdateUser { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
