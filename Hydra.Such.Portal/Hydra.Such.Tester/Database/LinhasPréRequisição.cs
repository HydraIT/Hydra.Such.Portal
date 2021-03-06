﻿using System;
using System.Collections.Generic;

namespace Hydra.Such.Tester.Database
{
    public partial class LinhasPréRequisição
    {
        public string NºPréRequisição { get; set; }
        public int NºLinha { get; set; }
        public int? Tipo { get; set; }
        public string Código { get; set; }
        public string Descrição { get; set; }
        public string CódigoLocalização { get; set; }
        public string CódigoUnidadeMedida { get; set; }
        public decimal? QuantidadeARequerer { get; set; }
        public string CódigoRegião { get; set; }
        public string CódigoÁreaFuncional { get; set; }
        public string CódigoCentroResponsabilidade { get; set; }
        public string NºProjeto { get; set; }
        public DateTime? DataHoraCriação { get; set; }
        public string UtilizadorCriação { get; set; }
        public DateTime? DataHoraModificação { get; set; }
        public string UtilizadorModificação { get; set; }

        public Projetos NºProjetoNavigation { get; set; }
        public PréRequisição NºPréRequisiçãoNavigation { get; set; }
    }
}
