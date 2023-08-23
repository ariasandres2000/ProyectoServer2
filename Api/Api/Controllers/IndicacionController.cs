using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/[controller]")]
    public class IndicacionController : ControllerBase
    {
        private readonly IndicacionServices _indicacionServices;

        public IndicacionController(DBContext context)
        {
            _indicacionServices = new IndicacionServices(context);
        }

        [HttpGet("GetIndicacion")]
        public ActionResult<List<EntIndicacion>> GetIndicacion(long idUsuario)
        {
            bool esError = false;
            string msgError = "";

            try
            {
                List<EntIndicacion> indicacion = _indicacionServices.ObtenerIndicacionUsuario(idUsuario);

                var respuesta = new
                {
                    error = esError,
                    mensajeError = msgError,
                    data = indicacion
                };

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{idIndicacion}")]
        public ActionResult<EntIndicacion> Get(long idIndicacion)
        {
            bool esError = false;
            string msgError = "";

            try
            {
                EntIndicacion lIndicacion = _indicacionServices.ObtenerIndicacion(idIndicacion);

                if (lIndicacion == null)
                {
                    var respuesta = new
                    {
                        error = esError,
                        mensajeError = msgError,
                        data = ""
                    };

                    return Ok(respuesta);
                } 
                else
                {
                    var respuesta = new
                    {
                        error = esError,
                        mensajeError = msgError,
                        data = lIndicacion
                    };

                    return Ok(respuesta);
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Post(EntIndicacion indicacion)
        {
            bool esError = false;
            string msgError = "";

            try
            {
                _indicacionServices.Registrar(indicacion);

                var respuesta = new
                {
                    error = esError,
                    mensajeError = msgError
                };

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch]
        public ActionResult Patch(EntIndicacion indicacion)
        {
            bool esError = false;
            string msgError = "";

            try
            {
                _indicacionServices.Actualizar(indicacion);

                var respuesta = new
                {
                    error = esError,
                    mensajeError = msgError
                };

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{idIndicacion}")]
        public ActionResult Delete(long idIndicacion)
        {
            bool esError = false;
            string msgError = "";

            try
            {
                _indicacionServices.Eliminar(idIndicacion);

                var respuesta = new
                {
                    error = esError,
                    mensajeError = msgError
                };

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
