﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Hydra.Such.Data.ViewModel.Compras
{
    public class RequisitionStateOfPlayViewModel
    {
        public string RequisitionNo { get; set; }
        public List<StateOfPlayViewModel> StatesOfPlay { get; set; }

        public RequisitionStateOfPlayViewModel()
        {
            this.StatesOfPlay = new List<StateOfPlayViewModel>();
        }
    }
    public class StateOfPlayViewModel : ErrorHandler
    {
        public string RequisitionNo { get; set; }
        public int StateOfPlayId { get; set; }

        public string Question { get; set; }
        public DateTime QuestionDate { get; set; }
        public string QuestionedBy { get; set; }

        public string Answer { get; set; }
        public DateTime? AnswerDate { get; set; }
        public string AnsweredBy { get; set; }
        
        /// <summary>
        /// Indicates whether the question has been read
        /// </summary>
        public bool Read { get; set; }
        /// <summary>
        /// For filter purposes
        /// </summary>

        //EXPORTAR PARA EXCEL
        public Object ColunasEXCEL { get; set; }

        public string ReadStringValue
        {
            get
            {
                return this.Read ? "Sim" : "Não";
            }
        }
    }
}
