﻿using Hydra.Such.Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hydra.Such.Data.Logic.Contracts
{
    public static class DBContractLines
    {
        #region CRUD

        public static LinhasContratos Create(LinhasContratos ObjectToCreate)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    ctx.LinhasContratos.Add(ObjectToCreate);
                    ctx.SaveChanges();
                }

                return ObjectToCreate;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        #endregion

        public static List<LinhasContratos> GetAllByActiveContract(string contractNo, int versionNo)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    return ctx.LinhasContratos.Where(x => x.NºContrato == contractNo && x.NºVersão == versionNo).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public static List<LinhasContratos> GetAllByNoTypeVersion(string contractNo, int type, int version)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    return ctx.LinhasContratos.Where(x => x.NºContrato == contractNo && x.Tipo == type && x.NºVersão == version).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
