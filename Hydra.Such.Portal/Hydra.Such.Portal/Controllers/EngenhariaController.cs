﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hydra.Such.Data.ViewModel;
using Hydra.Such.Data.Logic;
using Hydra.Such.Data.Database;
using Hydra.Such.Data.Logic.Project;
using Microsoft.AspNetCore.Authorization;
using Hydra.Such.Data.NAV;
using Hydra.Such.Portal.Configurations;
using Microsoft.Extensions.Options;
using Hydra.Such.Data.Logic.Contracts;
using Hydra.Such.Data;

namespace Hydra.Such.Portal.Controllers
{
    //[Authorize]
    //public class EngenhariaController : Controller
    //{
    //    private readonly NAVConfigurations _config;
    //    private readonly NAVWSConfigurations _configws;
    //    private readonly GeneralConfigurations _configup;

    //    public EngenhariaController(IOptions<NAVConfigurations> appSettings, IOptions<NAVWSConfigurations> NAVWSConfigs, IOptions<GeneralConfigurations> appUploadSettings)
    //    {
    //        _config = appSettings.Value;
    //        _configws = NAVWSConfigs.Value;
    //        _configup = appUploadSettings.Value;
    //    }

    //    public IActionResult Index()
    //    {
    //        return View();
    //    }

    //    #region Contratos
    //    public IActionResult Contratos(int? archived, string contractNo)
    //    {
    //        UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.Engenharia, Enumerations.Features.Contratos);

    //        if (UPerm != null && UPerm.Read.Value)
    //        {
    //            ViewBag.Archived = archived == null ? 0 : 1;
    //            ViewBag.ContractNo = contractNo ?? "";
    //            ViewBag.UPermissions = UPerm;
    //            return View();
    //        }
    //        else
    //        {
    //            return RedirectToAction("AccessDenied", "Error");
    //        }
    //    }

    //    public IActionResult DetalhesContrato(string id, string version = "")
    //    {
    //        UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.Engenharia, Enumerations.Features.Contratos);
    //        if (UPerm != null && UPerm.Read.Value)
    //        {
    //            Contratos cContract = null;
    //            if (version != "")
    //                cContract = DBContracts.GetByIdAndVersion(id, int.Parse(version));
    //            else
    //                cContract = DBContracts.GetByIdLastVersion(id);

    //            //if (cContract != null && (cContract.Arquivado == true || cContract.EstadoAlteração == 2))
    //            if (cContract != null && cContract.Arquivado == true)
    //            {
    //                UPerm.Update = false;
    //            }

    //            ViewBag.ContractNo = id ?? "";
    //            ViewBag.VersionNo = version ?? "";
    //            ViewBag.UPermissions = UPerm;
    //            return View();
    //        }
    //        else
    //        {
    //            return RedirectToAction("AccessDenied", "Error");
    //        }
    //    }
    //    #endregion
        
    //    #region Oportunidades
    //    public IActionResult Oportunidades(int? archived, string contractNo)
    //    {
    //        UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.Engenharia, Enumerations.Features.Oportunidades);

    //        if (UPerm != null && UPerm.Read.Value)
    //        {
                
    //            ViewBag.Archived = archived == null ? 0 : 1;
    //            ViewBag.ContractNo = contractNo ?? "";
    //            ViewBag.UPermissions = UPerm;
    //            return View();
    //        }
    //        else
    //        {
    //            return RedirectToAction("AccessDenied", "Error");
    //        }
    //    }

    //    public IActionResult DetalhesOportunidade(string id, string version = "")
    //    {
    //        UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.Engenharia, Enumerations.Features.Oportunidades);
    //        if (UPerm != null && UPerm.Read.Value)
    //        {
    //            Contratos cContract = null;
    //            if (version != "")
    //                cContract = DBContracts.GetByIdAndVersion(id, int.Parse(version));
    //            else
    //                cContract = DBContracts.GetByIdLastVersion(id);

    //            if (cContract != null && cContract.Arquivado == true)
    //            {
    //                UPerm.Update = false;
    //            }

    //            ViewBag.ContractNo = id ?? "";
    //            ViewBag.VersionNo = version ?? "";
    //            ViewBag.UPermissions = UPerm;
    //            return View();
    //        }
    //        else
    //        {
    //            return RedirectToAction("AccessDenied", "Error");
    //        }
    //    }
    //    #endregion
        
    //    #region Propostas
    //    public IActionResult Propostas(int? archived, string contractNo)
    //    {
    //        UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.Engenharia, Enumerations.Features.Propostas); //1, 21);

    //        if (UPerm != null && UPerm.Read.Value)
    //        {
    //            ViewBag.Archived = archived == null ? 0 : 1;
    //            ViewBag.ContractNo = contractNo ?? "";
    //            ViewBag.UPermissions = UPerm;
    //            return View();
    //        }
    //        else
    //        {
    //            return RedirectToAction("AccessDenied", "Error");
    //        }
    //    }

    //    public IActionResult DetalhesProposta(string id, string version = "")
    //    {
    //        UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.Engenharia, Enumerations.Features.Propostas);
    //        if (UPerm != null && UPerm.Read.Value)
    //        {
    //            Contratos cContract = null;
    //            if (version != "")
    //                cContract = DBContracts.GetByIdAndVersion(id, int.Parse(version));
    //            else
    //                cContract = DBContracts.GetByIdLastVersion(id);

    //            if (cContract != null && cContract.Arquivado == true)
    //            {
    //                UPerm.Update = false;
    //            }

    //            ViewBag.ContractNo = id ?? "";
    //            ViewBag.VersionNo = version ?? "";
    //            ViewBag.UPermissions = UPerm;
    //            return View();
    //        }
    //        else
    //        {
    //            return RedirectToAction("AccessDenied", "Error");
    //        }
    //    }

    //    public IActionResult PropostasContrato(string id)
    //    {
    //        UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.Engenharia, Enumerations.Features.Projetos);
    //        if (UPerm != null && UPerm.Read.Value)
    //        {
    //            ViewBag.UPermissions = UPerm;
    //            ViewBag.ContractNo = string.IsNullOrEmpty(id) ? string.Empty : id;
    //            return View();
    //        }
    //        else
    //        {
    //            return RedirectToAction("AccessDenied", "Error");
    //        }
    //    }
    //    #endregion

    //    #region Projetos
    //    public IActionResult Projetos()
    //    {
    //        UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.Engenharia, Enumerations.Features.Projetos);
    //        if (UPerm != null && UPerm.Read.Value)
    //        {
    //            ViewBag.UPermissions = UPerm;
    //            return View();
    //        }
    //        else
    //        {
    //            return RedirectToAction("AccessDenied", "Error");
    //        }
    //    }

    //    public IActionResult DetalhesProjeto(string id)
    //    {
    //        UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.Engenharia, Enumerations.Features.Projetos);
    //        if (UPerm != null && UPerm.Read.Value)
    //        {
    //            ViewBag.ProjectNo = id == null ? "" : id;
    //            ViewBag.UPermissions = UPerm;
    //            return View();
    //        }
    //        else
    //        {
    //            return RedirectToAction("AccessDenied", "Error");
    //        }
    //    }


    //    public IActionResult ProjetosContrato(string id)
    //    {
    //        UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.Engenharia, Enumerations.Features.Projetos);
    //        if (UPerm != null && UPerm.Read.Value)
    //        {
    //            ViewBag.UPermissions = UPerm;
    //            ViewBag.ContractId = string.IsNullOrEmpty(id) ? string.Empty : id;
    //            return View();
    //        }
    //        else
    //        {
    //            return RedirectToAction("AccessDenied", "Error");
    //        }
    //    }
    //    #endregion

    //    #region DiárioProjetos
    //    public IActionResult DiarioProjeto(string id)
    //    {
    //        UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.Engenharia, Enumerations.Features.DiárioProjeto);
    //        if (UPerm != null && UPerm.Read.Value)
    //        {
    //            // UPerm.Update = false;

    //            ViewBag.ProjectNo = id ?? "";
    //            ViewBag.UPermissions = UPerm;
    //            return View();
    //        }
    //        else
    //        {
    //            return RedirectToAction("AccessDenied", "Error");
    //        }
    //    }

    //    public IActionResult AutorizacaoFaturacao(string id)
    //    {
    //        UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.Engenharia, Enumerations.Features.AutorizaçãoFaturação);
    //        if (UPerm != null && UPerm.Read.Value)
    //        {
    //            ViewBag.ProjectNo = id ?? "";
    //            ViewBag.UPermissions = UPerm;
    //            return View();
    //        }
    //        else
    //        {
    //            return RedirectToAction("AccessDenied", "Error");
    //        }
    //    }

    //    #endregion
        
    //    public IActionResult Requisicoes()
    //    {
    //        return View();
    //    }

      
    //    public IActionResult Administracao()
    //    {
    //        UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.Engenharia, Enumerations.Features.Administração);
    //        if (UPerm != null && UPerm.Read.Value)
    //        {
    //            return View();
    //        }
    //        else
    //        {
    //            return RedirectToAction("AccessDenied", "Error");
    //        }
    //    }

    //    #region Folha de Horas
    //    public IActionResult FolhaDeHoras(string folhaDeHoraNo)
    //    {
    //        UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.Engenharia, Enumerations.Features.FolhasHoras);

    //        if (UPerm != null && UPerm.Read.Value)
    //        {
    //            ViewBag.FolhaDeHorasNo = folhaDeHoraNo ?? "";
    //            ViewBag.UPermissions = UPerm;

    //            return View();
    //        }
    //        else
    //        {
    //            return RedirectToAction("AccessDenied", "Error");
    //        }
    //    }

    //    public IActionResult FolhaDeHoras_Index(string folhaDeHoraNo)
    //    {
    //        UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.Engenharia, Enumerations.Features.FolhasHoras);

    //        if (UPerm != null && UPerm.Read.Value)
    //        {
    //            ViewBag.FolhaDeHorasNo = folhaDeHoraNo ?? "";
    //            ViewBag.UPermissions = UPerm;

    //            return View();
    //        }
    //        else
    //        {
    //            return RedirectToAction("AccessDenied", "Error");
    //        }
    //    }

    //    #endregion

        
    //    #region Pré-Requisições

    //    public IActionResult PreRequisicoesLista()
    //    {
    //        UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.Engenharia, Enumerations.Features.PréRequisições);
    //        if (UPerm != null && UPerm.Read.Value)
    //        {
    //            ViewBag.Area = 1;
    //            ViewBag.UPermissions = UPerm;
    //            return View();
    //        }
    //        else
    //        {
    //            return RedirectToAction("AccessDenied", "Error");
    //        }
    //    }

    //    public IActionResult PreRequisicoesDetalhes(string PreRequesitionNo)
    //    {
    //        UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.Engenharia, Enumerations.Features.PréRequisições);
    //        if (UPerm != null && UPerm.Read.Value)
    //        {
    //            ViewBag.UploadURL = _configup.FileUploadFolder;
    //            ViewBag.Area = 1;
    //            ViewBag.PreRequesitionNo = User.Identity.Name;
    //            ViewBag.UPermissions = UPerm;
    //            return View();
    //        }
    //        else
    //        {
    //            return RedirectToAction("AccessDenied", "Error");
    //        }
    //    }
    //    #endregion

    //    #region Pending Requesitions
    //    public IActionResult RequisicoesPendentes()
    //    {
    //        UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.Engenharia, Enumerations.Features.Requisições);
            
    //        if (UPerm != null && UPerm.Read.Value)
    //        {
    //            ViewBag.Area = 1;
    //            ViewBag.UPermissions = UPerm;
    //            return View();
    //        }
    //        else
    //        {
    //            return RedirectToAction("AccessDenied", "Error");
    //        }
    //    }

    //    #endregion
      
    //    #region history Requesitions
    //    public IActionResult RequisicoesHistorico()
    //    {
    //        UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, (int)Enumerations.Areas.Engenharia, 0);
    //        if (UPerm != null && UPerm.Read.Value)
    //        {
    //            ViewBag.Area = 4;
    //            ViewBag.UPermissions = UPerm;
    //            return View();
    //        }
    //        else
    //        {
    //            return RedirectToAction("AccessDenied", "Error");
    //        }
    //    }

    //    #endregion
    //}
}