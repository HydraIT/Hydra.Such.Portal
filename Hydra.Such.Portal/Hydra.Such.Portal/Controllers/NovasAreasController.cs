﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Hydra.Such.Data.ViewModel;
using Hydra.Such.Data.Logic;
using Hydra.Such.Data.Database;
using Hydra.Such.Data.Logic.Contracts;
using Hydra.Such.Data;

namespace Hydra.Such.Portal.Controllers
{
    [Authorize]
    public class NovasAreasController : Controller
    {
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //#region Projetos
        //public IActionResult Projetos()
        //{
        //    UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.NovasÁreas, Enumerations.Features.Projetos); //7, 1);
        //    if (UPerm != null && UPerm.Read.Value)
        //    {
        //        ViewBag.UPermissions = UPerm;
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("AccessDenied", "Error");
        //    }
        //}

        //public IActionResult DetalhesProjeto(string id)
        //{
        //    UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.NovasÁreas, Enumerations.Features.Projetos); //7, 1);
        //    if (UPerm != null && UPerm.Read.Value)
        //    {
        //        ViewBag.ProjectNo = id == null ? "" : id;
        //        ViewBag.UPermissions = UPerm;
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("AccessDenied", "Error");
        //    }
        //}

        //public IActionResult ProjetosContrato(string id)
        //{
        //    UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.NovasÁreas, Enumerations.Features.Projetos); //7, 1);
        //    if (UPerm != null && UPerm.Read.Value)
        //    {
        //        ViewBag.UPermissions = UPerm;
        //        ViewBag.ContractId = string.IsNullOrEmpty(id) ? string.Empty : id;
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("AccessDenied", "Error");
        //    }
        //}

        //public IActionResult DiarioProjeto(string id)
        //{
        //    UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.NovasÁreas, Enumerations.Features.DiárioProjeto); //7, 19);
        //    if (UPerm != null && UPerm.Read.Value)
        //    {
        //        // UPerm.Update = false;

        //        ViewBag.ProjectNo = id ?? "";
        //        ViewBag.UPermissions = UPerm;
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("AccessDenied", "Error");
        //    }
        //}

        //public IActionResult AutorizacaoFaturacao(string id)
        //{
        //    UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.NovasÁreas, Enumerations.Features.AutorizaçãoFaturação); //7, 22);
        //    if (UPerm != null && UPerm.Read.Value)
        //    {
        //        ViewBag.ProjectNo = id ?? "";
        //        ViewBag.UPermissions = UPerm;
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("AccessDenied", "Error");
        //    }
        //}


        //#endregion

        //#region Contratos
        //public IActionResult Contratos(int? archived, string contractNo)
        //{
        //    UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.NovasÁreas, Enumerations.Features.Contratos); //7, 2);

        //    if (UPerm != null && UPerm.Read.Value)
        //    {
        //        ViewBag.Archived = archived == null ? 0 : 1;
        //        ViewBag.ContractNo = contractNo ?? "";
        //        ViewBag.UPermissions = UPerm;
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("AccessDenied", "Error");
        //    }
        //}

        //public IActionResult DetalhesContrato(string id, string version = "")
        //{
        //    UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.NovasÁreas, Enumerations.Features.Contratos); //7, 2);
        //    if (UPerm != null && UPerm.Read.Value)
        //    {
        //        Contratos cContract = null;
        //        if (version != "")
        //            cContract = DBContracts.GetByIdAndVersion(id, int.Parse(version));
        //        else
        //            cContract = DBContracts.GetByIdLastVersion(id);

        //        if (cContract != null && cContract.Arquivado == true)
        //        {
        //            UPerm.Update = false;
        //        }

        //        ViewBag.ContractNo = id ?? "";
        //        ViewBag.VersionNo = version ?? "";
        //        ViewBag.UPermissions = UPerm;
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("AccessDenied", "Error");
        //    }
        //}
        //#endregion

        //#region Oportunidades
        //public IActionResult Oportunidades(int? archived, string contractNo)
        //{
        //    UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.NovasÁreas, Enumerations.Features.Oportunidades); //7, 20);

        //    if (UPerm != null && UPerm.Read.Value)
        //    {
        //        ViewBag.Archived = archived == null ? 0 : 1;
        //        ViewBag.ContractNo = contractNo ?? "";
        //        ViewBag.UPermissions = UPerm;
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("AccessDenied", "Error");
        //    }
        //}

        //public IActionResult DetalhesOportunidade(string id, string version = "")
        //{
        //    UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.NovasÁreas, Enumerations.Features.Oportunidades); //7, 20);
        //    if (UPerm != null && UPerm.Read.Value)
        //    {
        //        Contratos cContract = null;
        //        if (version != "")
        //            cContract = DBContracts.GetByIdAndVersion(id, int.Parse(version));
        //        else
        //            cContract = DBContracts.GetByIdLastVersion(id);

        //        if (cContract != null && cContract.Arquivado == true)
        //        {
        //            UPerm.Update = false;
        //        }

        //        ViewBag.ContractNo = id ?? "";
        //        ViewBag.VersionNo = version ?? "";
        //        ViewBag.UPermissions = UPerm;
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("AccessDenied", "Error");
        //    }
        //}
        //#endregion

        //#region Propostas
        //public IActionResult Propostas(int? archived, string contractNo)
        //{
        //    UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.NovasÁreas, Enumerations.Features.Propostas); //7, 21);

        //    if (UPerm != null && UPerm.Read.Value)
        //    {
        //        ViewBag.Archived = archived == null ? 0 : 1;
        //        ViewBag.ContractNo = contractNo ?? "";
        //        ViewBag.UPermissions = UPerm;
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("AccessDenied", "Error");
        //    }
        //}

        //public IActionResult DetalhesProposta(string id, string version = "")
        //{
        //    UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.NovasÁreas, Enumerations.Features.Propostas); //7, 21);
        //    if (UPerm != null && UPerm.Read.Value)
        //    {
        //        Contratos cContract = null;
        //        if (version != "")
        //            cContract = DBContracts.GetByIdAndVersion(id, int.Parse(version));
        //        else
        //            cContract = DBContracts.GetByIdLastVersion(id);

        //        if (cContract != null && cContract.Arquivado == true)
        //        {
        //            UPerm.Update = false;
        //        }

        //        ViewBag.ContractNo = id ?? "";
        //        ViewBag.VersionNo = version ?? "";
        //        ViewBag.UPermissions = UPerm;
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("AccessDenied", "Error");
        //    }
        //}
        //#endregion

        //public ActionResult Administracao()
        //{
        //    UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.NovasÁreas, Enumerations.Features.Administração); //7, 18);
        //    if (UPerm != null && UPerm.Read.Value)
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("AccessDenied", "Error");
        //    }
        //}

        //#region Folha de Horas
        //public IActionResult FolhaDeHoras(string folhaDeHoraNo)
        //{
        //    UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.NovasÁreas, Enumerations.Features.FolhasHoras); //7, 6);

        //    if (UPerm != null && UPerm.Read.Value)
        //    {
        //        ViewBag.FolhaDeHorasNo = folhaDeHoraNo ?? "";
        //        ViewBag.UPermissions = UPerm;

        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("AccessDenied", "Error");
        //    }
        //}

        //public IActionResult FolhaDeHoras_Index(string folhaDeHoraNo)
        //{
        //    UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.NovasÁreas, Enumerations.Features.FolhasHoras); //7, 6);

        //    if (UPerm != null && UPerm.Read.Value)
        //    {
        //        ViewBag.FolhaDeHorasNo = folhaDeHoraNo ?? "";
        //        ViewBag.UPermissions = UPerm;

        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("AccessDenied", "Error");
        //    }
        //}

        //public IActionResult FolhaDeHoras_IntegracaoAjudaCusto(string folhaDeHoraNo)
        //{
        //    UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.NovasÁreas, Enumerations.Features.FolhasHoras); //7, 6);

        //    if (UPerm != null && UPerm.Read.Value)
        //    {
        //        ViewBag.FolhaDeHorasNo = folhaDeHoraNo ?? "";
        //        ViewBag.UPermissions = UPerm;

        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("AccessDenied", "Error");
        //    }
        //}

        //public IActionResult FolhaDeHoras_IntegracaoKMS(string folhaDeHoraNo)
        //{
        //    UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.NovasÁreas, Enumerations.Features.FolhasHoras); //7, 6);

        //    if (UPerm != null && UPerm.Read.Value)
        //    {
        //        ViewBag.FolhaDeHorasNo = folhaDeHoraNo ?? "";
        //        ViewBag.UPermissions = UPerm;

        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("AccessDenied", "Error");
        //    }
        //}

        //public IActionResult FolhaDeHoras_Validacao(string folhaDeHoraNo)
        //{
        //    UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.NovasÁreas, Enumerations.Features.FolhasHoras); //7, 6);

        //    if (UPerm != null && UPerm.Read.Value)
        //    {
        //        ViewBag.FolhaDeHorasNo = folhaDeHoraNo ?? "";
        //        ViewBag.UPermissions = UPerm;

        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("AccessDenied", "Error");
        //    }
        //}

        //public IActionResult FolhaDeHoras_Historico(string folhaDeHoraNo)
        //{
        //    UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.NovasÁreas, Enumerations.Features.FolhasHoras); //7, 6);

        //    if (UPerm != null && UPerm.Read.Value)
        //    {
        //        ViewBag.FolhaDeHorasNo = folhaDeHoraNo ?? "";
        //        ViewBag.UPermissions = UPerm;

        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("AccessDenied", "Error");
        //    }
        //}
        //#endregion

        //#region Pré-Requisições

        //public IActionResult PreRequisicoesLista()
        //{
        //    UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.NovasÁreas, Enumerations.Features.PréRequisições); //7, 3);
        //    if (UPerm != null && UPerm.Read.Value)
        //    {
        //        ViewBag.Area = 7;
        //        ViewBag.UPermissions = UPerm;
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("AccessDenied", "Error");
        //    }
        //}

        //public IActionResult PreRequisicoesDetalhes(string PreRequesitionNo)
        //{
        //    UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Areas.NovasÁreas, Enumerations.Features.PréRequisições); //7, 3);
        //    if (UPerm != null && UPerm.Read.Value)
        //    {
        //        ViewBag.Area = 7;
        //        ViewBag.PreRequesitionNo = User.Identity.Name;
        //        ViewBag.UPermissions = UPerm;
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("AccessDenied", "Error");
        //    }
        //}
        //#endregion

        //#region Pending Requesitions
        //public IActionResult RequisicoesPendentes()
        //{
        //    UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, (int)Enumerations.Areas.NovasÁreas, 0);
        //    if (UPerm != null && UPerm.Read.Value)
        //    {
        //        ViewBag.Area = 7;
        //        ViewBag.UPermissions = UPerm;
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("AccessDenied", "Error");
        //    }
        //}

        //#endregion

        //#region history Requesitions
        //public IActionResult RequisicoesHistorico()
        //{
        //    UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, (int)Enumerations.Areas.NovasÁreas, 0);
        //    if (UPerm != null && UPerm.Read.Value)
        //    {
        //        ViewBag.Area = 4;
        //        ViewBag.UPermissions = UPerm;
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("AccessDenied", "Error");
        //    }
        //}

        //#endregion

    }
}