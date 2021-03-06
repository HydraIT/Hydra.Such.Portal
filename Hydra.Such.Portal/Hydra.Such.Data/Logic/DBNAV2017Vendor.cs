﻿using Hydra.Such.Data.Database;
using Hydra.Such.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Hydra.Such.Data.Logic
{
    public class DBNAV2017Vendor
    {
        public static List<NAVVendorViewModel> GetVendor(string NAVDatabaseName, string NAVCompanyName)
        {
            try
            {
                List<NAVVendorViewModel> result = new List<NAVVendorViewModel>();
                using (var ctx = new SuchDBContextExtention())
                {
                    var parameters = new[]{
                        new SqlParameter("@DBName", NAVDatabaseName),
                        new SqlParameter("@CompanyName", NAVCompanyName)
                    };

                    IEnumerable<dynamic> data = ctx.execStoredProcedure("exec NAV2017Vendor @DBName, @CompanyName", parameters);

                    foreach (dynamic temp in data)
                    {
                        result.Add(new NAVVendorViewModel()
                        {
                            No_ = (string)temp.No_,
                            Name = (string)temp.Name,
                            Email=(string)temp.Email,
                            Address = (string)temp.Address,
                            VATRegistrationNo = (string)temp.VATRegistrationNo,
                            PostCode = (string)temp.PostCode,
                            VATBusinessPostingGroup = (string)temp.VATBusinessPostingGroup,
                            City = (string)temp.City,
                            Cod_Termos_Pagamento = (string)temp.Cod_Termos_Pagamento,
                            Cod_Formas_Pagamento = (string)temp.Cod_Formas_Pagamento,
                            Atividade = (string)temp.Atividade,
                            Preferencial = (int)temp.Preferencial
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
