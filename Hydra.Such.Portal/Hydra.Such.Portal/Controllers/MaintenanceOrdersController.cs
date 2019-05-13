﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hydra.Such.Data.Database;
using Hydra.Such.Data.ViewModel.Projects;
using Hydra.Such.Data.NAV;
using Hydra.Such.Data.Logic;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Hydra.Such.Data.Evolution;
using Hydra.Such.Data.Evolution.Repositories;
using SharpRepository.Repository;
using Hydra.Such.Data.Evolution.Database;
using SharpRepository.Repository.Queries;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Extensions;
using NJsonSchema;
using NJsonSchema.Generation;
using Manatee.Json.Schema;
using System.Text.RegularExpressions;
using Hydra.Such.Data.ViewModel;
using Hydra.Such.Data;

namespace Hydra.Such.Portal.Controllers
{
    [Authorize]
    [Route("ordens-de-manutencao")]
    public class MaintenanceOrdersController : Controller
    {

        protected MaintnenceOrdersRepository maintnenceOrdersRepository;
        protected EvolutionWEBContext evolutionWEBContext;
        private readonly ISession session;

        public MaintenanceOrdersController(MaintnenceOrdersRepository repository, EvolutionWEBContext evolutionWEBContext, IOptions<NAVWSConfigurations> NAVWSConfigs, IHttpContextAccessor httpContextAccessor)
        {
            session = httpContextAccessor.HttpContext.Session;
            this.maintnenceOrdersRepository = repository;
            this.evolutionWEBContext = evolutionWEBContext;
        }

        [Route("")]
        public IActionResult Index()
        {

            UserAccessesViewModel UPerm = DBUserAccesses.GetByUserAreaFunctionality(User.Identity.Name, Enumerations.Features.MaintenanceOrders);
            UserConfigurationsViewModel userConfig = DBUserConfigurations.GetById(User.Identity.Name).ParseToViewModel();
            if (UPerm != null && UPerm.Read.Value)
            {
                return View();
            }
            return RedirectToAction("AccessDenied", "Error");
        }

        
        [Route("all"), HttpGet]
        public ActionResult GetAll (ODataQueryOptions<MaintenanceOrder> queryOptions, DateTime? from, DateTime? to, int idCliente)
        {
            if(from== null || to == null) {  return NotFound(); }
            
            var pageSize = 30;

            IQueryable results = queryOptions.ApplyTo(maintnenceOrdersRepository.AsQueryable().Where(o => o.OrderDate >= from && o.OrderDate <= to).OrderByDescending(o=>o.OrderDate), new ODataQuerySettings { PageSize = pageSize });
            var list = results.Cast<dynamic>().AsEnumerable();
            var total = Request.ODataFeature().TotalCount;
            var nextLink = Request.GetNextPageLink(pageSize);

            List<MaintenanceOrder> newList;
            try
            {
             newList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MaintenanceOrder>>(Newtonsoft.Json.JsonConvert.SerializeObject(list));
            } catch
            {
                newList = new List<MaintenanceOrder>();
            }

            newList.ForEach((item) =>
            {
                var technicals = GetTechnicals(null, item.No, null);
                if(technicals != null)
                {
                    item.Technicals = technicals.ToList();
                }
            });

            var result = new PageResult<dynamic>(newList, nextLink, total);  

            var query = maintnenceOrdersRepository.AsQueryable().Where(o => o.OrderDate >= from && o.OrderDate <= to)
                .Select(m => new {m.IsToExecute, m.IsPreventive, m.OrderType, m.FinishingDate }).ToList();

            var ordersCounts = new {
                preventive = query.Where(o => o.IsPreventive && !o.IsToExecute).Count(),
                preventiveToExecute = query.Where(o => o.IsPreventive && o.IsToExecute).Count(),
                curative = query.Where(o => !o.IsPreventive && !o.IsToExecute).Count(),
                curativeToExecute = query.Where(o => !o.IsPreventive && o.IsToExecute).Count(),
            };
          
            return Json(new {
                result,
                ordersCounts,
                range = new { from,to}
            }); 
        }

        
        [Route("technicals"), HttpGet]
        public ActionResult HttpGetTecnicalls(string local, string orderId, string technicalid)
        {
            if ((local == null || local == "") && (orderId == null || orderId == "") && (technicalid == null || technicalid == "")) { return NotFound(); }
            return Json(new { technicals = GetTechnicals( local, orderId, technicalid).OrderBy(o=>o.Nome) });
        }


        private IQueryable<Utilizador> GetTechnicals(string local, string orderId, string technicalid) {
            if ((local == null || local == "") && (orderId == null || orderId == "") && (technicalid == null || technicalid == "")) { return null; }

            IQueryable<Utilizador> technicals;
            if (technicalid != null && technicalid != "")
            {
                technicals = evolutionWEBContext.Utilizador.Where(u => u.NumMec == technicalid);
                return technicals;
            }

            if (orderId != null && orderId != "")
            {
                var order = maintnenceOrdersRepository.AsQueryable().Where(m => m.No == orderId).FirstOrDefault();
                var technicalsId = new List<int>();

                for (int i = 1; i <= 5; i++)
                {
                    var prop = order.GetType().GetProperty("IdTecnico" + i.ToString());
                    int? name = (int?)(prop.GetValue(order, null));
                    if (name != null)
                    {
                        technicalsId.Add((int)name);
                    }
                }

                technicals = evolutionWEBContext.Utilizador.Where(u => technicalsId.Contains(u.Id));
                return technicals;
            }

            technicals = evolutionWEBContext.Utilizador.Where(a => a.Code1 == local);
            return technicals;
        }
        


        [Route("technicals"), HttpPut]
        public ActionResult TecnicallsPut([FromBody] UpdateTechnicalsModel data)
        {
            if (data.orderId == null || data.orderId == "" || data.technicalsId == null) { return NotFound(); }

            var orderToUpdate = maintnenceOrdersRepository.AsQueryable().Where(m => m.No == data.orderId).FirstOrDefault();

            if (orderToUpdate == null) { return NotFound(); }

            var technicalsToUpdate = evolutionWEBContext.Utilizador.Where(u => data.technicalsId.Contains(u.NumMec)).ToArray();

            orderToUpdate.IdTecnico1 = technicalsToUpdate.Count() > 0 ? (int?)technicalsToUpdate[0].Id : null;
            orderToUpdate.IdTecnico2 = technicalsToUpdate.Count() > 1 ? (int?)technicalsToUpdate[1].Id : null;
            orderToUpdate.IdTecnico3 = technicalsToUpdate.Count() > 2 ? (int?)technicalsToUpdate[2].Id : null;
            orderToUpdate.IdTecnico4 = technicalsToUpdate.Count() > 3 ? (int?)technicalsToUpdate[3].Id : null;
            orderToUpdate.IdTecnico5 = technicalsToUpdate.Count() > 4 ? (int?)technicalsToUpdate[4].Id : null;

            try
            {
                evolutionWEBContext.Update(orderToUpdate);
                evolutionWEBContext.SaveChanges();
            }
            catch (Exception)
            {
                return Json(0);
            }
            
            
            return Json(orderToUpdate);
        }


        public class UpdateTechnicalsModel
        {
            public string orderId;
            public string[] technicalsId;
        }



        //[Authorize]
        // Todo adds custom authorize filter (eSuchAuthorizationFilter)  (info: Authentication -> Authorization)
        [Route("getAll")]
        public PageResult<dynamic> GetAll(ODataQueryOptions<MaintenanceOrder> queryOptions)
        {
            var pageSize = 100;
            IQueryable results = queryOptions.ApplyTo(maintnenceOrdersRepository.AsQueryable(), new ODataQuerySettings { PageSize = pageSize });
            var list = results.Cast<dynamic>().AsEnumerable();
            var total = Request.ODataFeature().TotalCount;
            var nextLink = Request.GetNextPageLink(pageSize);

            return new PageResult<dynamic>(list, nextLink, total);
        }

        [Route("getSchema")]
        [ResponseCache(Duration = 600)]
        public async Task<string> GetSchema()
        {
            var _schema = await JsonSchema4.FromTypeAsync<OrdemManutencao>();

            var settings = new JsonSchemaGeneratorSettings();
            //settings.GenerateKnownTypes = false;
            //settings.SchemaType = SchemaType.OpenApi3;

            var generator = new JsonSchemaGenerator(settings);
            var schema = await generator.GenerateAsync(typeof(IOrdemManutencao));

            var json = Regex.Replace(schema.ToJson(), @"\s+", string.Empty);

            return json.Replace(",\"format\":\"decimal\"", string.Empty).Replace(",\"format\":\"byte\"", string.Empty).Replace(",\"format\":\"int32\"", string.Empty).Replace(",\"format\":\"time-span\"", string.Empty);
        }

    }

    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private static string[] Summaries = new[]
       {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}