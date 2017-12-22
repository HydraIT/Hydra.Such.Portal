﻿using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;
using Hydra.Such.Data.ViewModel.Compras;

namespace Hydra.Such.Data.NAV
{
    public static class NAVPurchaseHeaderService
    {
        static BasicHttpBinding navWSBinding;

        static NAVPurchaseHeaderService()
        {
            // Configure Basic Binding to have access to NAV
            navWSBinding = new BasicHttpBinding();
            navWSBinding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            navWSBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows; 
        }

        public static async Task<WSPurchaseInvHeader.Create_Result> CreateAsync(PurchFromSupplierDTO purchFromSupplier, NAVWSConfigurations WSConfigurations)
        {
            if (purchFromSupplier == null)
                throw new ArgumentNullException("purchFromSupplier");

            WSPurchaseInvHeader.Create navCreate = new WSPurchaseInvHeader.Create()
            {
                WSPurchInvHeaderInterm = new WSPurchaseInvHeader.WSPurchInvHeaderInterm()
                {
                    Pay_to_Vendor_No = purchFromSupplier.SupplierId,
                    FunctionAreaCode20 = purchFromSupplier.FunctionalAreaCode,
                    RegionCode20 = purchFromSupplier.RegionCode,
                    ResponsabilityCenterCode20 = purchFromSupplier.CenterResponsibilityCode
                }
            };

            //Configure NAV Client
            EndpointAddress ws_URL = new EndpointAddress(WSConfigurations.WS_PurchaseInvHeader_URL.Replace("Company", WSConfigurations.WS_User_Company));
            WSPurchaseInvHeader.WSPurchInvHeaderInterm_PortClient ws_Client = new WSPurchaseInvHeader.WSPurchInvHeaderInterm_PortClient(navWSBinding, ws_URL);
            ws_Client.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Delegation;
            ws_Client.ClientCredentials.Windows.ClientCredential = new NetworkCredential(WSConfigurations.WS_User_Login, WSConfigurations.WS_User_Password, WSConfigurations.WS_User_Domain);

            try
            {
                WSPurchaseInvHeader.Create_Result result = await ws_Client.CreateAsync(navCreate);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
