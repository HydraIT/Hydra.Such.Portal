﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Hydra.Such.Data.ViewModel.Compras
{
    public class BillingRecWorkflowModel
    {
        public int Id { get; set; }
        public string IdRecFaturacao { get; set; }
        public Enumerations.BillingReceptionStates Estado { get; set; }
        public string AreaWorkflow { get; set; }
        public string Area { get; set; }
        public string Descricao { get; set; }
        public string CodDestino { get; set; }
        public string Destinatario { get; set; }
        public string Comentario { get; set; }
        public DateTime? Data { get; set; }
        public string Utilizador { get; set; }
        public string CodTipoProblema { get; set; }
        public string CodProblema { get; set; }
        public string EnderecoEnvio { get; set; }
        public string EnderecoFornecedor { get; set; }
        public DateTime? DataCriacao { get; set; }
        public string CriadoPor { get; set; }
        public DateTime? DataModificacao { get; set; }
        public string ModificadoPor { get; set; }
        public bool? AttachedIs { get; set; }
        public List<BillingRecWorkflowModelAttached> Attached { get; set; }
    }
}
