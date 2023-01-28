using Data;
using Data.Model;
using IntegraInternacionalTest.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System.Diagnostics;
using DataAccess;
using static DataAccess.Helpers.Factory;
using Shared;
using DataAccess.Helpers;
using System.Collections.Generic;

namespace IntegraInternacionalTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly RootHelper _rootHelper;
        private readonly IEmpleadoServices _empleadoServices;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IEmpleadoServices empleadoServices, RootHelper rootHelper, ILogger<HomeController> logger)
        {
            _rootHelper = rootHelper;
            _empleadoServices = empleadoServices;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _empleadoServices.GetAll());
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details([FromRoute]int id)
        {
            try
            {
                var empleado =await _empleadoServices.Get(id);
                return View(empleado);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            try
            {
                var result = await _empleadoServices.Remove(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit([FromRoute]int id)
        {
            try
            {
                var empleado = await _empleadoServices.Get(id);
                
                return View(new EmpleadoEditModel(empleado));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit([FromForm] EmpleadoEditModel model)
        {
            try
            {
                List<KeyValuePair<string, List<string>>> validations = new List<KeyValuePair<string, List<string>>>();
                if(!await _empleadoServices.Exist(x=>x.Id == model.Id))
                {
                    validations.Add(GetValidationKVEntry("Id", new List<string>() { "No Existe el usuario" }));
                    
                }

                if((await _empleadoServices.GetAll(x=>x.Correo.Trim().ToLower()==model.Correo.Trim().ToLower() && x.Id !=model.Id)).Count>0)
                {
                    validations.Add(GetValidationKVEntry("Correo", new List<string>() { "ya existe este Correo" }));
                }

                if ((await _empleadoServices.GetAll(x => x.Telefono.Trim() == model.Telefono.Trim() && x.Id != model.Id)).Count > 0)
                {
                    validations.Add(GetValidationKVEntry("Telefono", new List<string>() { "ya existe este Telefono" }));
                }

                if (validations.Count>0)
                {
                    Ok(MVCBadRequest(validations, model));
                }



                var empleado = await _empleadoServices.Update(model);

                return Ok(GetResponse<Response>(empleado));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GetResponse<ServerErrorResponse>(null, 500, "INTERNAL SERVER ERROR", false, null));
            }
        }






        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]EmpleadoModel model )
        {
            
            if (!ModelState.IsValid)
            {
                 return Ok(ModelState.MVCBadRequest(model));
                    
            }


            try
            {


                if(await _empleadoServices.Exist(x=>x.Correo.ToLower() == model.Correo.ToLower()))
                {
                    return Ok(MVCBadRequest(GetValidationMessage(GetValidationKVEntry("Correo", new List<string>() { "Ya existe el correo"})), model));
                }

                var re = await _empleadoServices.Add(new(model),model.File);

                
                return Ok(GetResponse<Response>(re));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,GetResponse<ServerErrorResponse>(null,500,"INTERNAL SERVER ERROR",false,null));
            }


        }

        
      
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}