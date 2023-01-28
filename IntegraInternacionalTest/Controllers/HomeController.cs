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
using Services.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

                return Error();
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

                return Error();
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

                return Error();
            }
        }

       

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit([FromForm] EmpleadoEditModel model)
        {
            try
            {
                ModelState.RemoveValidationFor("File");
                if (!ModelState.IsValid)
                {
                    return Ok(ModelState.MVCBadRequest(model));
                }

                var validations = await _empleadoServices.ValidateEmpleadoPostModel(model);
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