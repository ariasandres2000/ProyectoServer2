using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/[controller]")]
    public class OpenAIController : ControllerBase
    {
        private readonly IndicacionServices _indicacionServices;  
        private readonly AIServices service;

        public OpenAIController(DBContext context)
        {
            _indicacionServices = new IndicacionServices(context);
            service = new AIServices();
        }

        [HttpGet("{idIndicacion}")]
        public ActionResult<EntIndicacion> Get(long idIndicacion)
        {
            try
            {
                EntIndicacion lIndicacion = _indicacionServices.ObtenerIndicacion(idIndicacion);

                if (lIndicacion == null)
                {
                    var respuesta = new
                    {
                        error = true,
                        mensajeError = "No existe el prompt.",
                        data = ""
                    };

                    return Ok(respuesta);
                }
                else
                {
                    if (lIndicacion.tipo.Equals("I"))
                    {
                        int cantidad = string.IsNullOrEmpty(lIndicacion.cantidad.ToString()) ? 1 : int.Parse(lIndicacion.cantidad.ToString());

                        var result = service.Imagen(lIndicacion.instruccion, cantidad, lIndicacion.valor);

                        var respuesta = new
                        {
                            error = false,
                            mensajeError = "",
                            data = result
                        };

                        return Ok(respuesta);
                    }
                    else if (lIndicacion.tipo.Equals("E"))
                    {
                        var result = service.Editar(lIndicacion.valor, lIndicacion.instruccion);

                        var respuesta = new
                        {
                            error = false,
                            mensajeError = "",
                            data = result
                        };

                        return Ok(respuesta);
                    }
                    else if (lIndicacion.tipo.Equals("C"))
                    {
                        var result = service.Terminacion(lIndicacion.instruccion);

                        var respuesta = new
                        {
                            error = false,
                            mensajeError = "",
                            data = result
                        };

                        return Ok(respuesta);
                    }                    
                }

                var respuestaq = new
                {
                    error = true,
                    mensajeError = "Error interno",
                    data = ""
                };

                return Ok(respuestaq);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
