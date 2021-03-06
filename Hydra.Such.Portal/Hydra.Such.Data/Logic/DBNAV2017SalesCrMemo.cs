﻿using Hydra.Such.Data.Database;
using Hydra.Such.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Hydra.Such.Data.Logic
{
    public class DBNAV2017SalesCrMemo
    {
        public static List<NAVSalesCrMemoLinesViewModel> GetSalesCrMemoLines(string NAVDatabaseName, string NAVCompanyName, string NAVContractNo, DateTime InitialDate, DateTime ExpirationDate)
        {
            try
            {
                List<NAVSalesCrMemoLinesViewModel> result = new List<NAVSalesCrMemoLinesViewModel>();
                using (var ctx = new SuchDBContextExtention())
                {
                    var parameters = new[]{
                        new SqlParameter("@DBName", NAVDatabaseName),
                        new SqlParameter("@CompanyName", NAVCompanyName),
                        new SqlParameter("@ContractNoPortal", NAVContractNo),
                        new SqlParameter("@InitialDate", InitialDate.ToString("yyyy/MM/dd h:mm tt")),
                        new SqlParameter("@ExpirationDate", ExpirationDate.ToString("yyyy/MM/dd h:mm tt"))
                    };

                    IEnumerable<dynamic> data = ctx.execStoredProcedure("exec NAV2017LinhaCRMemo @DBName, @CompanyName, @ContractNoPortal, @InitialDate, @ExpirationDate", parameters);

                    foreach (dynamic temp in data)
                    {
                        result.Add(new NAVSalesCrMemoLinesViewModel()
                        {
                            ContractNo = (string)temp.ContractNoPortal,
                            PostingDate = (string)temp.PostingDate,
                            Amount = (decimal)temp.Amount
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
