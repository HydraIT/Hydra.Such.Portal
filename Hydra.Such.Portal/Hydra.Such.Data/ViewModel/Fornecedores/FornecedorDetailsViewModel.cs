﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Hydra.Such.Data.ViewModel.Fornecedores
{
    public class FornecedorDetailsViewModel : ErrorHandler
    {
        public string No { get; set; }
        public string Name { get; set; }
        public string FullAddress { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string HomePage { get; set; }
        public string VATRegistrationNo { get; set; }
        public string PaymentTermsCode { get; set; }
        public string PaymentMethodCode { get; set; }
        public string NoClienteAssociado { get; set; }
        public int? Blocked { get; set; }
        public string BlockedText { get; set; }
        public string Address { get; set; }
        public string Address_2 { get; set; }
        public string Distrito { get; set; }
        public int? Criticidade { get; set; }
        public string CriticidadeText { get; set; }
        public string Observacoes { get; set; }
        public string Utilizador_Alteracao_eSUCH { get; set; }
        public string NomeAnexo { get; set; }



        //EXPORTAR PARA EXCEL
        public Object ColunasEXCEL { get; set; }
    }
}
