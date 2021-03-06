﻿using Hydra.Such.Data.Database;
using Hydra.Such.Data.ViewModel;
using Hydra.Such.Data.ViewModel.ProjectView;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Hydra.Such.Data.Logic
{
    public class DBNAV2017Locations
    {
        public static List<NAVLocationsViewModel> GetAllLocations(string NAVDatabaseName, string NAVCompanyName)
        {
            try
            {
                List<NAVLocationsViewModel> result = new List<NAVLocationsViewModel>();
                using (var ctx = new SuchDBContextExtention())
                {
                    var parameters = new[]{
                        new SqlParameter("@DBName", NAVDatabaseName),
                        new SqlParameter("@CompanyName", NAVCompanyName),
                    };

                    IEnumerable<dynamic> data = ctx.execStoredProcedure("exec NAV2017Localizacoes @DBName, @CompanyName", parameters);

                    foreach (dynamic temp in data)
                    {
                        result.Add(new NAVLocationsViewModel()
                        {
                            Code = (string)temp.Code,
                            Name = (string)temp.Name,
                            ArmazemCDireta = (byte)temp.ArmazemCDireta,
                        });
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<NAVLocationsViewModel> GetAllLocationsByProduct(string NAVDatabaseName, string NAVCompanyName, string Product = "")
        {
            try
            {
                List<NAVLocationsViewModel> result = new List<NAVLocationsViewModel>();
                using (var ctx = new SuchDBContextExtention())
                {
                    var parameters = new[]{
                        new SqlParameter("@DBName", NAVDatabaseName),
                        new SqlParameter("@CompanyName", NAVCompanyName),
                        new SqlParameter("@Product", Product),
                    };

                    IEnumerable<dynamic> data = ctx.execStoredProcedure("exec NAV2017LocalizacoesByProduct @DBName, @CompanyName, @Product", parameters);

                    foreach (dynamic temp in data)
                    {
                        result.Add(new NAVLocationsViewModel()
                        {
                            Code = (string)temp.Code,
                            Name = (string)temp.Name,
                            ArmazemCDireta = (byte)temp.ArmazemCDireta,
                        });
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

