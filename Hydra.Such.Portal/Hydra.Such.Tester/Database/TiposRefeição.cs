﻿using System;
using System.Collections.Generic;

namespace Hydra.Such.Tester.Database
{
    public partial class TiposRefeição
    {
        public TiposRefeição()
        {
            DiárioCafetariaRefeitório = new HashSet<DiárioCafetariaRefeitório>();
            DiárioDeProjeto = new HashSet<DiárioDeProjeto>();
            DiárioDesperdíciosAlimentares = new HashSet<DiárioDesperdíciosAlimentares>();
            DiárioRequisiçãoUnidProdutiva = new HashSet<DiárioRequisiçãoUnidProdutiva>();
            LinhasRequisiçõesSimplificadas = new HashSet<LinhasRequisiçõesSimplificadas>();
        }

        public int Código { get; set; }
        public string Descrição { get; set; }
        public string GrupoContabProduto { get; set; }
        public DateTime? DataHoraCriação { get; set; }
        public string UtilizadorCriação { get; set; }
        public DateTime? DataHoraModificação { get; set; }
        public string UtilizadorModificação { get; set; }

        public ICollection<DiárioCafetariaRefeitório> DiárioCafetariaRefeitório { get; set; }
        public ICollection<DiárioDeProjeto> DiárioDeProjeto { get; set; }
        public ICollection<DiárioDesperdíciosAlimentares> DiárioDesperdíciosAlimentares { get; set; }
        public ICollection<DiárioRequisiçãoUnidProdutiva> DiárioRequisiçãoUnidProdutiva { get; set; }
        public ICollection<LinhasRequisiçõesSimplificadas> LinhasRequisiçõesSimplificadas { get; set; }
    }
}
