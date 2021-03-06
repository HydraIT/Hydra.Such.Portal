﻿using Hydra.Such.Data.ViewModel;
using Hydra.Such.Data.ViewModel.GuiaTransporte;
using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;
using WSSuchNav2017;
using static Hydra.Such.Data.Enumerations;

namespace Hydra.Such.Data.NAV
{
    public static class WSGuiasTransporteNAV
    {
        static BasicHttpBinding navWSBinding;

        static WSGuiasTransporteNAV()
        {
            navWSBinding = new BasicHttpBinding();
            navWSBinding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            navWSBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
            navWSBinding.MaxReceivedMessageSize = int.MaxValue;

        }



        public static async Task<WSNovaGuiaTransporte_Result> CreateAsync(string userId, NAVWSConfigurations WSConfigurations)
        {
            if (userId == null || userId == "")
                return null;

            WSNovaGuiaTransporte novaGuia = new WSNovaGuiaTransporte()
            {
                idUtilizador = userId
            };

            EndpointAddress ws_URL = new EndpointAddress(WSConfigurations.Ws_SuchNav2017_URL.Replace("Company", WSConfigurations.WS_User_Company));
            WSNAV2017_PortClient ws_Client = new WSNAV2017_PortClient(navWSBinding, ws_URL);
            ws_Client.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Delegation;
            ws_Client.ClientCredentials.Windows.ClientCredential = new NetworkCredential(WSConfigurations.WS_User_Login, WSConfigurations.WS_User_Password, WSConfigurations.WS_User_Domain);

            try
            {
                WSNovaGuiaTransporte_Result result = await ws_Client.WSNovaGuiaTransporteAsync(userId);
                return result;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public static async Task<WSRegistaGuia_Result> RegisterAsync(string noGuia, NAVWSConfigurations WSConfigurations)
        {
            WSRegistaGuia noGuiaRegistar = new WSRegistaGuia()
            {
                noGuiaTransporte = noGuia
            };

            EndpointAddress ws_URL = new EndpointAddress(WSConfigurations.Ws_SuchNav2017_URL.Replace("Company", WSConfigurations.WS_User_Company));
            WSNAV2017_PortClient ws_Client = new WSNAV2017_PortClient(navWSBinding, ws_URL);
            ws_Client.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Delegation;
            ws_Client.ClientCredentials.Windows.ClientCredential = new NetworkCredential(WSConfigurations.WS_User_Login, WSConfigurations.WS_User_Password, WSConfigurations.WS_User_Domain);

            try
            {
                WSRegistaGuia_Result result = await ws_Client.WSRegistaGuiaAsync(noGuia);
                return result;
            }
            catch (Exception ex)
            {

                return null;
            }

        }

        public static async Task<WSCopiaLinhasGuia_Result> CopyLinesAsync(string guiaOrigem, string guiaDestino, NAVWSConfigurations WSConfigurations)
        {
            WSCopiaLinhasGuia copyObject = new WSCopiaLinhasGuia()
            {
                noGuiaDestino = guiaDestino,
                noGuiaOrigem = guiaOrigem
            };

            EndpointAddress ws_URL = new EndpointAddress(WSConfigurations.Ws_SuchNav2017_URL.Replace("Company", WSConfigurations.WS_User_Company));
            WSNAV2017_PortClient ws_Client = new WSNAV2017_PortClient(navWSBinding, ws_URL);
            ws_Client.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Delegation;
            ws_Client.ClientCredentials.Windows.ClientCredential = new NetworkCredential(WSConfigurations.WS_User_Login, WSConfigurations.WS_User_Password, WSConfigurations.WS_User_Domain);

            try
            {
                WSCopiaLinhasGuia_Result result = await ws_Client.WSCopiaLinhasGuiaAsync(guiaOrigem, guiaDestino);
                return result;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public static async Task<WSForcaRegistoGuia_Result> ForceDocumentRegistry(string noGuia, NAVWSConfigurations WSConfigurations)
        {
            WSForcaRegistoGuia noGuiaRegistar = new WSForcaRegistoGuia()
            {
                noGuiaTransporteHistorico = noGuia
            };

            EndpointAddress ws_URL = new EndpointAddress(WSConfigurations.Ws_SuchNav2017_URL.Replace("Company", WSConfigurations.WS_User_Company));
            WSNAV2017_PortClient ws_Client = new WSNAV2017_PortClient(navWSBinding, ws_URL);
            ws_Client.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Delegation;
            ws_Client.ClientCredentials.Windows.ClientCredential = new NetworkCredential(WSConfigurations.WS_User_Login, WSConfigurations.WS_User_Password, WSConfigurations.WS_User_Domain);

            try
            {
                WSForcaRegistoGuia_Result result = await ws_Client.WSForcaRegistoGuiaAsync(noGuia);
                return result;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

    }
}
