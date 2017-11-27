﻿using Hydra.Such.Data.Database;
using Hydra.Such.Data.ViewModel.FH;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Hydra.Such.Data.Logic.FolhaDeHora
{
    public class DBTipoTrabalhoFH
    {
        public static List<TipoTrabalhoFh> GetAll()
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    return ctx.TipoTrabalhoFh.ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static TipoTrabalhoFh Create(TipoTrabalhoFh ObjectToCreate)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    ObjectToCreate.DataHoraCriacao = DateTime.Now;
                    ctx.TipoTrabalhoFh.Add(ObjectToCreate);
                    ctx.SaveChanges();
                }

                return ObjectToCreate;
            }
            catch (Exception e)
            {

                return null;
            }
        }

        public static TipoTrabalhoFh Update(TipoTrabalhoFh ObjectToUpdate)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    ObjectToUpdate.DataHoraModificacao = DateTime.Now;
                    ctx.TipoTrabalhoFh.Update(ObjectToUpdate);
                    ctx.SaveChanges();
                }

                return ObjectToUpdate;
            }
            catch (Exception e)
            {

                return null;
            }
        }

        public static bool Delete(TipoTrabalhoFh ObjectToDelete)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    ctx.TipoTrabalhoFh.Remove(ObjectToDelete);
                    ctx.SaveChanges();
                }

                return true;
            }
            catch (Exception e)
            {

                return false;
            }
        }
    }
}