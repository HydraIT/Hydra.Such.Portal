﻿using System;
using System.Collections.Generic;

namespace Hydra.Such.Data.Database
{
    public partial class LinhasFaturaçãoContrato
    {
        public string NºContrato { get; set; }
        public int GrupoFatura { get; set; }
        public int NºLinha { get; set; }
        public string Tipo { get; set; }
        public string Código { get; set; }
        public string Descrição { get; set; }
        public string Descricao2 { get; set; }
        public decimal? Quantidade { get; set; }
        public string CódUnidadeMedida { get; set; }
        public decimal? PreçoUnitário { get; set; }
        public decimal? ValorVenda { get; set; }
        public string CódigoRegião { get; set; }
        public string CódigoÁreaFuncional { get; set; }
        public string CódigoCentroResponsabilidade { get; set; }
        public int? TipoRecurso { get; set; }
        public string CódigoServiço { get; set; }
        public DateTime? DataHoraCriação { get; set; }
        public string UtilizadorCriação { get; set; }
        public DateTime? DataHoraModificação { get; set; }
        public string UtilizadorModificação { get; set; }
        public string NºProjeto { get; set; }
        public DateTime? Data_Inicio_Serv_Prestado { get; set; }
        public DateTime? Data_Fim_Serv_Prestado { get; set; }
    }
}
