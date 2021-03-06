﻿using Hydra.Such.Data.Database;
using Hydra.Such.Data.ViewModel;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using WSCustomerNAV;
using Hydra.Such.Data.ViewModel.Clients;

namespace Hydra.Such.Data.Logic
{
    public class DBNAV2017Clients
    {
        public static NAVClientsViewModel GetClientById(string NAVDatabaseName, string NAVCompanyName, string NAVClientNo)
        {
            List<NAVClientsViewModel> result = GetClients(NAVDatabaseName, NAVCompanyName, NAVClientNo);
            if (result != null)
                return result.FirstOrDefault();
            return null;
        }

        public static List<NAVClientsViewModel> GetClients(string NAVDatabaseName, string NAVCompanyName, IEnumerable<string> navCustomerIds)
        {
            string navCustomerIdsFilter = string.Join(",", navCustomerIds);
            List<NAVClientsViewModel> customers = GetClients(NAVDatabaseName, NAVCompanyName, navCustomerIdsFilter);
            return customers;
        }

        public static List<NAVClientsViewModel> GetClients(string NAVDatabaseName, string NAVCompanyName, string NAVClientNo)
        {
            try
            {
                List<NAVClientsViewModel> result = new List<NAVClientsViewModel>();
                using (var ctx = new SuchDBContextExtention())
                {
                    var parameters = new[]{
                        new SqlParameter("@DBName", NAVDatabaseName),
                        new SqlParameter("@CompanyName", NAVCompanyName),
                        new SqlParameter("@NoCliente", NAVClientNo)
                    };

                    IEnumerable<dynamic> data = ctx.execStoredProcedure("exec NAV2017Clientes @DBName, @CompanyName, @NoCliente", parameters);

                    foreach (dynamic temp in data)
                    {
                        result.Add(new NAVClientsViewModel()
                        {
                            No_ = temp.No_.Equals(DBNull.Value) ? "" : (string)temp.No_,
                            Name = temp.Name.Equals(DBNull.Value) ? "" : (string)temp.Name,
                            VATRegistrationNo_ = temp.VATRegistrationNo.Equals(DBNull.Value) ? "" : (string)temp.VATRegistrationNo,
                            Address = temp.Address.Equals(DBNull.Value) ? "" : (string)temp.Address,
                            Address1 = temp.Address1.Equals(DBNull.Value) ? "" : (string)temp.Address1,
                            Address2 = temp.Address2.Equals(DBNull.Value) ? "" : (string)temp.Address2,
                            PostCode = temp.PostCode.Equals(DBNull.Value) ? "" : (string)temp.PostCode,
                            City = temp.City.Equals(DBNull.Value) ? "" : (string)temp.City,
                            CountryRegionCode = temp.CountryRegionCode.Equals(DBNull.Value) ? "" : (string)temp.CountryRegionCode,
                            UnderCompromiseLaw = temp.UnderCompromiseLaw.Equals(DBNull.Value) ? false : ((int)temp.UnderCompromiseLaw) == 0 ? false : true,
                            InternalClient = temp.InternalClient.Equals(DBNull.Value) ? false : ((int)temp.InternalClient) == 0 ? false : true,
                            National = temp.NationalCustomer.Equals(DBNull.Value) ? false : ((int)temp.NationalCustomer) == 0 ? false : true,
                            PaymentTermsCode = temp.PaymentTermsCode.Equals(DBNull.Value) ? "" : (string)temp.PaymentTermsCode,
                            PaymentMethodCode = temp.PaymentMethodCode.Equals(DBNull.Value) ? "" : (string)temp.PaymentMethodCode,
                            RegionCode = temp.RegionCode.Equals(DBNull.Value) ? "" : (string)temp.RegionCode,
                            FunctionalAreaCode = temp.FunctionalAreaCode.Equals(DBNull.Value) ? "" : (string)temp.FunctionalAreaCode,
                            ResponsabilityCenterCode = temp.ResponsabilityCenterCode.Equals(DBNull.Value) ? "" : (string)temp.ResponsabilityCenterCode,
                            RegiaoCliente = (Regiao_Cliente)temp.RegiaoCliente,
                            AbrigoLeiCompromisso = ((int)temp.AbrigoLeiCompromisso > 0 ? true : false),
                            AssociadoAN = temp.AssociadoAN.Equals(DBNull.Value) ? "" : (string)temp.AssociadoAN,
                            Blocked = (WSCustomerNAV.Blocked)temp.Blocked,
                            ClienteAssociado = ((int)temp.ClienteAssociado > 0 ? true : false),
                            ClienteInterno = ((int)temp.ClienteInterno > 0 ? true : false),
                            ClienteNacional = ((int)temp.ClienteNacional > 0 ? true : false),
                            County = temp.County.Equals(DBNull.Value) ? "" : (string)temp.County,
                            EMail = temp.EMail.Equals(DBNull.Value) ? "" : (string)temp.EMail,
                            FaxNo = temp.FaxNo.Equals(DBNull.Value) ? "" : (string)temp.FaxNo,
                            HomePage = temp.HomePage.Equals(DBNull.Value) ? "" : (string)temp.HomePage,
                            NaturezaCliente = (WSCustomerNAV.Natureza_Cliente)temp.NaturezaCliente,
                            NoFornecedorAssoc = temp.NoFornecedorAssoc.Equals(DBNull.Value) ? "" : (string)temp.NoFornecedorAssoc,
                            NoSeries = temp.NoSeries.Equals(DBNull.Value) ? "" : (string)temp.NoSeries,
                            PhoneNo = temp.PhoneNo.Equals(DBNull.Value) ? "" : (string)temp.PhoneNo,
                            TaxaAprovisionamento = (decimal)temp.TaxaAprovisionamento,
                            TipoCliente = (WSCustomerNAV.Tipo_Cliente)temp.TipoCliente,
                            VATBusinessPostingGroup = temp.VATBusinessPostingGroup.Equals(DBNull.Value) ? "" : (string)temp.VATBusinessPostingGroup
                        });
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string GetClientNameByNo(string NoClient, string NAVDatabaseName, string NAVCompanyName)
        {
            try
            {
                string result = "";

                if (!string.IsNullOrEmpty(NoClient))
                {
                    using (var ctx = new SuchDBContextExtention())
                    {
                        var parameters = new[]{
                        new SqlParameter("@DBName", NAVDatabaseName),
                        new SqlParameter("@CompanyName", NAVCompanyName),
                        new SqlParameter("@NoCliente", NoClient)
                    };

                        IEnumerable<dynamic> data = ctx.execStoredProcedure("exec NAV2017Clientes @DBName, @CompanyName, @NoCliente", parameters);

                        foreach (dynamic temp in data)
                        {
                            result = (string)temp.Name;

                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static List<NAVCountry> GetCountry(string NAVDatabaseName, string NAVCompanyName)
        {
            try
            {
                List<NAVCountry> result = new List<NAVCountry>();

                using (var ctx = new SuchDBContextExtention())
                {
                    var parameters = new[]{
                    new SqlParameter("@DBName", NAVDatabaseName),
                    new SqlParameter("@CompanyName", NAVCompanyName)
                };

                    IEnumerable<dynamic> data = ctx.execStoredProcedure("exec NAV2017Country @DBName, @CompanyName", parameters);

                    foreach (dynamic temp in data)
                    {
                        result.Add(new NAVCountry()
                        {
                            Code = (string)temp.Code,
                            Country = (string)temp.Value
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

        public static string GetClientVATByNo(string NoClient, string NAVDatabaseName, string NAVCompanyName)
        {
            try
            {
                string result = "";
                using (var ctx = new SuchDBContextExtention())
                {
                    var parameters = new[]{
                        new SqlParameter("@DBName", NAVDatabaseName),
                        new SqlParameter("@CompanyName", NAVCompanyName),
                        new SqlParameter("@NoCliente", NoClient)
                    };

                    IEnumerable<dynamic> data = ctx.execStoredProcedure("exec NAV2017Clientes @DBName, @CompanyName, @NoCliente", parameters);

                    foreach (dynamic temp in data)
                    {
                        result = (string)temp.VATRegistrationNo;
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string GetClientCurrencyByNo(string NoClient, string NAVDatabaseName, string NAVCompanyName)
        {
            try
            {
                string result = "";
                using (var ctx = new SuchDBContextExtention())
                {
                    var parameters = new[]{
                        new SqlParameter("@DBName", NAVDatabaseName),
                        new SqlParameter("@CompanyName", NAVCompanyName),
                        new SqlParameter("@NoCliente", NoClient)
                    };

                    IEnumerable<dynamic> data = ctx.execStoredProcedure("exec NAV2017Clientes @DBName, @CompanyName, @NoCliente", parameters);

                    foreach (dynamic temp in data)
                    {
                        result = (string)temp.Currency;
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static List<NAVClientesInvoicesViewModel> GetInvoices(string NAVDatabaseName, string NAVCompanyName, string NAVClientNo)
        {
            try
            {
                List<NAVClientesInvoicesViewModel> result = new List<NAVClientesInvoicesViewModel>();
                using (var ctx = new SuchDBContextExtention())
                {
                    var parameters = new[]{
                        new SqlParameter("@DBName", NAVDatabaseName),
                        new SqlParameter("@CompanyName", NAVCompanyName),
                        new SqlParameter("@CustomerNo", NAVClientNo ),
                        //new SqlParameter("@Regions", "''"),
                        //new SqlParameter("@FunctionalAreas", "''"),
                        //new SqlParameter("@RespCenters", "''"),
                    };

                    IEnumerable<dynamic> data = ctx.execStoredProcedure("exec NAV2017ClientesInvoices @DBName, @CompanyName, @CustomerNo", parameters);
                    var minDate = new DateTime(2008, 1, 1);
                    foreach (dynamic temp in data)
                    {
                        result.Add(new NAVClientesInvoicesViewModel()
                        {
                            No_ = temp.No_.Equals(DBNull.Value) ? "" : (string)temp.No_,
                            ProjectNo = temp.ProjectNo.Equals(DBNull.Value) ? "" : (string)temp.ProjectNo,
                            DataServPrestado = temp.DataServPrestado.Equals(DBNull.Value) ? "" : (string)temp.DataServPrestado,
                            AmountIncludingVAT = temp.AmountIncludingVAT.Equals(DBNull.Value) ? "" : (string)temp.AmountIncludingVAT.ToString(),
                            BillToCustomerNo = temp.BillToCustomerNo.Equals(DBNull.Value) ? "" : (string)temp.BillToCustomerNo,
                            CreationDate = (DateTime?)temp.CreationDate,
                            DocumentDate = (DateTime?)temp.DocumentDate,
                            DocumentDateText = temp.DocumentDate != null ? Convert.ToDateTime(temp.DocumentDate).ToString("yyyy-MM-dd") : "",
                            DueDate = (DateTime?)temp.DueDate,
                            FunctionalAreaId = temp.FunctionalAreaId.Equals(DBNull.Value) ? "" : (string)temp.FunctionalAreaId,
                            Paid = (bool)temp.Paid,
                            NoContrato = temp.NoContrato.Equals(DBNull.Value) ? "" : (string)temp.NoContrato,
                            NoCompromisso = temp.NoCompromisso.Equals(DBNull.Value) ? "" : (string)temp.NoCompromisso,
                            NoPedido = temp.NoPedido.Equals(DBNull.Value) ? "" : (string)temp.NoPedido,
                            RegionId = temp.RegionId.Equals(DBNull.Value) ? "" : (string)temp.RegionId,
                            RespCenterId = temp.RespCenterId.Equals(DBNull.Value) ? "" : (string)temp.RespCenterId,
                            SellToCustomerNo = temp.SellToCustomerNo.Equals(DBNull.Value) ? "" : (string)temp.SellToCustomerNo,
                            Tipo = temp.Tipo.Equals(DBNull.Value) ? "" : (string)temp.Tipo,
                            ValorPendente = temp.ValorPendente.Equals(DBNull.Value) ? "" : (string)temp.ValorPendente.ToString()
                        });
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<NAVClientesInvoicesViewModel> GetInvoicesFatura(string NAVDatabaseName, string NAVCompanyName, string NAVClientNo)
        {
            try
            {
                List<NAVClientesInvoicesViewModel> result = new List<NAVClientesInvoicesViewModel>();
                using (var ctx = new SuchDBContextExtention())
                {
                    var parameters = new[]{
                        new SqlParameter("@DBName", NAVDatabaseName),
                        new SqlParameter("@CompanyName", NAVCompanyName),
                        new SqlParameter("@CustomerNo", NAVClientNo ),
                        //new SqlParameter("@Regions", "''"),
                        //new SqlParameter("@FunctionalAreas", "''"),
                        //new SqlParameter("@RespCenters", "''"),
                    };

                    IEnumerable<dynamic> data = ctx.execStoredProcedure("exec NAV2017ClientesInvoicesFatura @DBName, @CompanyName, @CustomerNo", parameters);
                    var minDate = new DateTime(2008, 1, 1);
                    foreach (dynamic temp in data)
                    {
                        result.Add(new NAVClientesInvoicesViewModel()
                        {
                            Tipo = temp.Tipo.Equals(DBNull.Value) ? "" : (string)temp.Tipo,
                            No_ = temp.No_.Equals(DBNull.Value) ? "" : (string)temp.No_,
                            ProjectNo = temp.ProjectNo.Equals(DBNull.Value) ? "" : (string)temp.ProjectNo,
                            DataServPrestado = temp.DataServPrestado.Equals(DBNull.Value) ? "" : (string)temp.DataServPrestado,
                            CreationDate = (DateTime?)temp.CreationDate,
                            DocumentDate = (DateTime?)temp.DocumentDate,
                            DocumentDateText = temp.DocumentDate != null ? Convert.ToDateTime(temp.DocumentDate).ToString("yyyy-MM-dd") : "",
                            DueDate = (DateTime?)temp.DueDate,
                            AmountIncludingVAT = temp.AmountIncludingVAT.Equals(DBNull.Value) ? "" : (string)temp.AmountIncludingVAT.ToString(),
                            ValorPendente = temp.ValorPendente.Equals(DBNull.Value) ? "" : (string)temp.ValorPendente.ToString(),
                            Paid = (bool)temp.Paid,
                            SellToCustomerNo = temp.SellToCustomerNo.Equals(DBNull.Value) ? "" : (string)temp.SellToCustomerNo,
                            BillToCustomerNo = temp.BillToCustomerNo.Equals(DBNull.Value) ? "" : (string)temp.BillToCustomerNo,
                            NoContrato = temp.NoContrato.Equals(DBNull.Value) ? "" : (string)temp.NoContrato,
                            NoCompromisso = temp.NoCompromisso.Equals(DBNull.Value) ? "" : (string)temp.NoCompromisso,
                            NoPedido = temp.NoPedido.Equals(DBNull.Value) ? "" : (string)temp.NoPedido
                        });
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<NAVClientesBalanceControlViewModel> GetBalances(string NAVDatabaseName, string NAVCompanyName, string NAVClientNo)
        {
            try
            {
                var result = new List<NAVClientesBalanceControlViewModel>();
                using (var ctx = new SuchDBContextExtention())
                {
                    var parameters = new[]{
                        new SqlParameter("@DBName", NAVDatabaseName),
                        new SqlParameter("@CompanyName", NAVCompanyName),
                        new SqlParameter("@CustomerNo", NAVClientNo ),
                        //new SqlParameter("@Regions", "''"),
                        //new SqlParameter("@FunctionalAreas", "''"),
                        //new SqlParameter("@RespCenters", "''"),
                    };

                    IEnumerable<dynamic> data = ctx.execStoredProcedure("exec NAV2017ClientesBalances @DBName, @CompanyName, @CustomerNo", parameters);
                    var minDate = new DateTime(2008, 1, 1);
                    foreach (dynamic temp in data)
                    {
                        result.Add(new NAVClientesBalanceControlViewModel()
                        {
                            EntryNo = (int)temp.EntryNo,
                            Amount = (decimal?)temp.Amount,
                            CustomerNo = temp.CustomerNo.Equals(DBNull.Value) ? "" : (string)temp.CustomerNo,
                            DataConcil = (DateTime?)temp.DataConcil != null && (DateTime?)temp.DataConcil > minDate ? (DateTime?)temp.DataConcil : null,
                            DataConcilText = (DateTime?)temp.DataConcil != null && (DateTime?)temp.DataConcil > minDate ? temp.DataConcil.ToString("yyyy-MM-dd") : "",
                            Description = temp.Description.Equals(DBNull.Value) ? "" : (string)temp.Description,
                            DocumentNo = temp.DocumentNo.Equals(DBNull.Value) ? "" : (string)temp.DocumentNo,
                            DocumentType = temp.DocumentType.Equals(DBNull.Value) ? "" : (string)temp.DocumentType.ToString(),
                            DocumentTypeText = temp.DocumentTypeText.Equals(DBNull.Value) ? "" : (string)temp.DocumentTypeText.ToString(),
                            PendenteType = (int?)temp.PendenteType == 0 || (int?)temp.PendenteType == null ? "Não" : "Sim",
                            Obs = temp.Obs.Equals(DBNull.Value) ? "" : (string)temp.Obs,
                            PostingDate = (DateTime?)temp.PostingDate,
                            RemainingAmount = (decimal?)temp.RemainingAmount,
                            SinalizacaoRec = (int?)temp.SinalizacaoRec == 0 || (int?)temp.SinalizacaoRec == null ? false : true,
                            SinalizacaoRecText = (int?)temp.SinalizacaoRec == 0 || (int?)temp.SinalizacaoRec == null ? "Não" : "Sim",
                            DocumentDate = (DateTime?)temp.DocumentDate,
                            DueDate = (DateTime?)temp.DueDate,
                            FunctionalAreaId = temp.FunctionalAreaId.Equals(DBNull.Value) ? "" : (string)temp.FunctionalAreaId,
                            RegionId = temp.RegionId.Equals(DBNull.Value) ? "" : (string)temp.RegionId,
                            RespCenterId = temp.RespCenterId.Equals(DBNull.Value) ? "" : (string)temp.RespCenterId
                        });
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static int? UpdateBalance(string NAVDatabaseName, string NAVCompanyName, string NAVClientNo, string EntryNo, bool SinalizacaoRec, string DateRec, string Obs, string UserUpdate)
        {
            try
            {
                var result = new List<NAVClientesBalanceControlViewModel>();
                using (var ctx = new SuchDBContextExtention())
                {
                    var _parameters = new[]{
                        new SqlParameter("@DBName", NAVDatabaseName),
                        new SqlParameter("@CompanyName", NAVCompanyName),
                        new SqlParameter("@CustomerNo", NAVClientNo ),
                        new SqlParameter("@Obs", Obs ),
                        new SqlParameter("@SinalizacaoRec", SinalizacaoRec == true ? "true" : "false" ),
                        new SqlParameter("@EntryNo", EntryNo ),
                        new SqlParameter("@DateRec", DateRec ),
                        new SqlParameter("@UserUpdate", UserUpdate )
                    };

                    var data = ctx.execStoredProcedureNQ("exec NAV2017ClientesBalancesUpdateV2 @DBName, @CompanyName, @CustomerNo, @Obs, @SinalizacaoRec, @EntryNo, @DateRec, @UserUpdate", _parameters);

                    return data;

                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<NAVClientesInvoicesDetailsViewModel> GetInvoiceDetails(string NAVDatabaseName, string NAVCompanyName, string No)
        {
            try
            {
                List<NAVClientesInvoicesDetailsViewModel> result = new List<NAVClientesInvoicesDetailsViewModel>();
                using (var ctx = new SuchDBContextExtention())
                {
                    var parameters = new[]{
                        new SqlParameter("@DBName", NAVDatabaseName),
                        new SqlParameter("@CompanyName", NAVCompanyName),
                        new SqlParameter("@No", No ),
                        //new SqlParameter("@Regions", "''"),
                        //new SqlParameter("@FunctionalAreas", "''"),
                        //new SqlParameter("@RespCenters", "''"),
                    };

                    IEnumerable<dynamic> data = ctx.execStoredProcedure("exec NAV2017ClientesInvoicesDetails @DBName, @CompanyName, @No", parameters);

                    foreach (dynamic temp in data)
                    {
                        result.Add(new NAVClientesInvoicesDetailsViewModel()
                        {
                            No = temp.No.Equals(DBNull.Value) ? "" : (string)temp.No.ToString(),
                            AmountIncludingVAT = temp.AmountIncludingVAT.Equals(DBNull.Value) ? "" : (string)temp.AmountIncludingVAT.ToString(),
                            FunctionalAreaId = temp.FunctionalAreaId.Equals(DBNull.Value) ? "" : (string)temp.FunctionalAreaId.ToString(),
                            RegionId = temp.RegionId.Equals(DBNull.Value) ? "" : (string)temp.RegionId.ToString(),
                            RespCenterId = temp.RespCenterId.Equals(DBNull.Value) ? "" : (string)temp.RespCenterId.ToString(),
                            SellToCustomerNo = temp.SellToCustomerNo.Equals(DBNull.Value) ? "" : (string)temp.SellToCustomerNo.ToString(),
                            Tipo = temp.Tipo.Equals(DBNull.Value) ? "" : (string)temp.Tipo.ToString(),
                            Amount = temp.Amount.Equals(DBNull.Value) ? "" : (string)temp.Amount.ToString(),
                            Description = temp.Description.Equals(DBNull.Value) ? "" : (string)temp.Description.ToString(),
                            Description2 = temp.Description2.Equals(DBNull.Value) ? "" : (string)temp.Description2.ToString(),
                            DimensionSetID = temp.DimensionSetID.Equals(DBNull.Value) ? "" : (string)temp.DimensionSetID.ToString(),
                            DocumentNo = temp.DocumentNo.Equals(DBNull.Value) ? "" : (string)temp.DocumentNo.ToString(),
                            LineNo = temp.LineNo_.Equals(DBNull.Value) ? "" : (string)temp.LineNo_.ToString(),
                            Quantity = temp.Quantity.Equals(DBNull.Value) ? "" : (string)temp.Quantity.ToString(),
                            ServiceContractNo = temp.ServiceContractNo.Equals(DBNull.Value) ? "" : (string)temp.ServiceContractNo.ToString(),
                            UnitOfMeasure = temp.UnitOfMeasure.Equals(DBNull.Value) ? "" : (string)temp.UnitOfMeasure.ToString(),
                            UnitPrice = temp.UnitPrice.Equals(DBNull.Value) ? "" : (string)temp.UnitPrice.ToString(),
                            VAT = temp.VAT.Equals(DBNull.Value) ? "" : (string)temp.VAT.ToString(),
                            TipoRefeicao = temp.TipoRefeicao.Equals(DBNull.Value) ? "" : (string)temp.TipoRefeicao.ToString(),
                            MealTypeDescription = temp.MealTypeDescription.Equals(DBNull.Value) ? "" : (string)temp.MealTypeDescription.ToString(),
                            GrupoServico = temp.GrupoServico.Equals(DBNull.Value) ? "" : (string)temp.GrupoServico.ToString(),
                            ServiceGroupDescription = temp.ServiceGroupDescription.Equals(DBNull.Value) ? "" : (string)temp.ServiceGroupDescription.ToString(),
                            CodServCliente = temp.CodServCliente.Equals(DBNull.Value) ? "" : (string)temp.CodServCliente.ToString(),
                            DesServCliente = temp.DesServCliente.Equals(DBNull.Value) ? "" : (string)temp.DesServCliente.ToString(),
                            NGuiaResiduosGAR = temp.NGuiaResiduosGAR.Equals(DBNull.Value) ? "" : (string)temp.NGuiaResiduosGAR.ToString(),
                            NGuiaExterna = temp.NGuiaExterna.Equals(DBNull.Value) ? "" : (string)temp.NGuiaExterna.ToString(),
                            DataConsumo = temp.DataConsumo.Equals(DBNull.Value) ? "" : Convert.ToDateTime((string)temp.DataConsumo.ToString()).ToShortDateString().Contains("01/01/1753") ? "" : Convert.ToDateTime((string)temp.DataConsumo.ToString()).ToShortDateString(),
                            NoContrato = temp.NoContrato.Equals(DBNull.Value) ? "" : (string)temp.NoContrato.ToString(),
                            NoCompromisso = temp.NoCompromisso.Equals(DBNull.Value) ? "" : (string)temp.NoCompromisso.ToString(),
                            NoPedido = temp.DocumentNo.Equals(DBNull.Value) ? "" : (string)temp.NoPedido.ToString(),
                        });
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<NAVClientesInvoicesDetailsViewModel> GetCrMemoDetails(string NAVDatabaseName, string NAVCompanyName, string No)
        {
            try
            {
                List<NAVClientesInvoicesDetailsViewModel> result = new List<NAVClientesInvoicesDetailsViewModel>();
                using (var ctx = new SuchDBContextExtention())
                {
                    var parameters = new[]{
                        new SqlParameter("@DBName", NAVDatabaseName),
                        new SqlParameter("@CompanyName", NAVCompanyName),
                        new SqlParameter("@No", No ),
                        //new SqlParameter("@Regions", "''"),
                        //new SqlParameter("@FunctionalAreas", "''"),
                        //new SqlParameter("@RespCenters", "''"),
                    };

                    IEnumerable<dynamic> data = ctx.execStoredProcedure("exec NAV2017ClientesCrMemoDetails @DBName, @CompanyName, @No", parameters);

                    foreach (dynamic temp in data)
                    {
                        result.Add(new NAVClientesInvoicesDetailsViewModel()
                        {
                            No = temp.No.Equals(DBNull.Value) ? "" : (string)temp.No.ToString(),
                            AmountIncludingVAT = temp.AmountIncludingVAT.Equals(DBNull.Value) ? "" : (string)temp.AmountIncludingVAT.ToString(),
                            FunctionalAreaId = temp.FunctionalAreaId.Equals(DBNull.Value) ? "" : (string)temp.FunctionalAreaId.ToString(),
                            RegionId = temp.RegionId.Equals(DBNull.Value) ? "" : (string)temp.RegionId.ToString(),
                            RespCenterId = temp.RespCenterId.Equals(DBNull.Value) ? "" : (string)temp.RespCenterId.ToString(),
                            SellToCustomerNo = temp.SellToCustomerNo.Equals(DBNull.Value) ? "" : (string)temp.SellToCustomerNo.ToString(),
                            Tipo = temp.Tipo.Equals(DBNull.Value) ? "" : (string)temp.Tipo.ToString(),
                            Amount = temp.Amount.Equals(DBNull.Value) ? "" : (string)temp.Amount.ToString(),
                            Description = temp.Description.Equals(DBNull.Value) ? "" : (string)temp.Description.ToString(),
                            Description2 = temp.Description2.Equals(DBNull.Value) ? "" : (string)temp.Description2.ToString(),
                            DimensionSetID = temp.DimensionSetID.Equals(DBNull.Value) ? "" : (string)temp.DimensionSetID.ToString(),
                            DocumentNo = temp.DocumentNo.Equals(DBNull.Value) ? "" : (string)temp.DocumentNo.ToString(),
                            LineNo = temp.LineNo_.Equals(DBNull.Value) ? "" : (string)temp.LineNo_.ToString(),
                            Quantity = temp.Quantity.Equals(DBNull.Value) ? "" : (string)temp.Quantity.ToString(),
                            ServiceContractNo = temp.ServiceContractNo.Equals(DBNull.Value) ? "" : (string)temp.ServiceContractNo.ToString(),
                            UnitOfMeasure = temp.UnitOfMeasure.Equals(DBNull.Value) ? "" : (string)temp.UnitOfMeasure.ToString(),
                            UnitPrice = temp.UnitPrice.Equals(DBNull.Value) ? "" : (string)temp.UnitPrice.ToString(),
                            VAT = temp.VAT.Equals(DBNull.Value) ? "" : (string)temp.VAT.ToString(),
                            TipoRefeicao = temp.TipoRefeicao.Equals(DBNull.Value) ? "" : (string)temp.TipoRefeicao.ToString(),
                            MealTypeDescription = temp.MealTypeDescription.Equals(DBNull.Value) ? "" : (string)temp.MealTypeDescription.ToString(),
                            GrupoServico = temp.GrupoServico.Equals(DBNull.Value) ? "" : (string)temp.GrupoServico.ToString(),
                            ServiceGroupDescription = temp.ServiceGroupDescription.Equals(DBNull.Value) ? "" : (string)temp.ServiceGroupDescription.ToString(),
                            CodServCliente = temp.CodServCliente.Equals(DBNull.Value) ? "" : (string)temp.CodServCliente.ToString(),
                            DesServCliente = temp.DesServCliente.Equals(DBNull.Value) ? "" : (string)temp.DesServCliente.ToString(),
                            NGuiaResiduosGAR = temp.NGuiaResiduosGAR.Equals(DBNull.Value) ? "" : (string)temp.NGuiaResiduosGAR.ToString(),
                            NGuiaExterna = temp.NGuiaExterna.Equals(DBNull.Value) ? "" : (string)temp.NGuiaExterna.ToString(),
                            DataConsumo = temp.DataConsumo.Equals(DBNull.Value) ? "" : Convert.ToDateTime((string)temp.DataConsumo.ToString()).ToShortDateString().Contains("01/01/1753") ? "" : Convert.ToDateTime((string)temp.DataConsumo.ToString()).ToShortDateString(),
                            NoContrato = temp.NoContrato.Equals(DBNull.Value) ? "" : (string)temp.NoContrato.ToString(),
                            NoCompromisso = temp.NoCompromisso.Equals(DBNull.Value) ? "" : (string)temp.NoCompromisso.ToString(),
                            NoPedido = temp.NoPedido.Equals(DBNull.Value) ? "" : (string)temp.NoPedido.ToString(),
                        });
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<ListMovimentosAllClientsViewModel> GetListMovAllClients(string dataFiltro)
        {
            try
            {
                List<ListMovimentosAllClientsViewModel> result = new List<ListMovimentosAllClientsViewModel>();

                using (var ctx = new SuchDBContextExtention())
                {
                    var parameters = new[]
                    {
                    new SqlParameter("@DataFiltro", dataFiltro)
                };

                    IEnumerable<dynamic> data = ctx.execStoredProcedure("exec MovAllClientesIntegrado @DataFiltro", parameters);

                    foreach (dynamic temp in data)
                    {
                        result.Add(new ListMovimentosAllClientsViewModel()
                        {
                            CustomerNo = temp.CustomerNo.Equals(DBNull.Value) ? "" : (string)temp.CustomerNo.ToString(),
                            DateTexto = Convert.ToDateTime(temp.Date).ToShortDateString(),
                            DueDateTexto = Convert.ToDateTime(temp.DueDate).ToShortDateString(),
                            DocumentType = temp.DocumentType.Equals(DBNull.Value) ? "" : (string)temp.DocumentType.ToString(),
                            DocumentNo = temp.DocumentNo.Equals(DBNull.Value) ? "" : (string)temp.DocumentNo.ToString(),
                            DocumentNoExterno = temp.DocumentNoExterno.Equals(DBNull.Value) ? "" : (string)temp.DocumentNoExterno.ToString(),
                            DimensionValue = temp.DimensionValue.Equals(DBNull.Value) ? "" : (string)temp.DimensionValue.ToString(),
                            Value = Convert.ToDecimal(temp.Value),
                            FactoringSemRecurso = temp.FactoringSemRecurso.Equals(DBNull.Value) ? "" : (string)temp.FactoringSemRecurso.ToString()
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

        public static List<ListDividaAllClientsViewModel> GetListDividaAllClients(string RegiaoFiltro)
        {
            try
            {
                List<ListDividaAllClientsViewModel> result = new List<ListDividaAllClientsViewModel>();

                using (var ctx = new SuchDBContextExtention())
                {
                    var parameters = new[]
                    {
                    new SqlParameter("@RegiaoFiltro", RegiaoFiltro)
                };

                    IEnumerable<dynamic> data = ctx.execStoredProcedure("exec DividaClientes @RegiaoFiltro", parameters);

                    foreach (dynamic temp in data)
                    {
                        result.Add(new ListDividaAllClientsViewModel()
                        {
                            CustomerRegionNo = temp.CustomerRegionNo.Equals(DBNull.Value) ? "" : (string)temp.CustomerRegionNo.ToString(),
                            CustomerRegionName = temp.CustomerRegionName.Equals(DBNull.Value) ? "" : (string)temp.CustomerRegionName.ToString(),
                            Associado = temp.Associado.Equals(DBNull.Value) ? "" : (string)temp.Associado.ToString(),
                            CustomerNo = temp.CustomerNo.Equals(DBNull.Value) ? "" : (string)temp.CustomerNo.ToString(),
                            CustomerName = temp.CustomerName.Equals(DBNull.Value) ? "" : (string)temp.CustomerName.ToString(),
                            Value = Convert.ToDecimal(temp.Value),
                            DueValue = Convert.ToDecimal(temp.DueValue)
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
