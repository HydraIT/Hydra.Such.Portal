﻿using System;

namespace Hydra.Such.Data.ViewModel.Viaturas
{
    public class Viaturas2ModelosViewModel : ErrorHandler
    {
        public int ID { get; set; }
        public int IDMarca { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string UtilizadorCriacao { get; set; }
        public DateTime? DataCriacao { get; set; }
        public string DataCriacaoTexto { get; set; }
        public string UtilizadorModificacao { get; set; }
        public DateTime? DataModificacao { get; set; }
        public string DataModificacaoTexto { get; set; }
    }
}
