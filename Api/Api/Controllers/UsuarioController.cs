using Api.DTO;
using Api.Models;
using Api.Services;
using Api.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioServices _usuarioServices;
        private readonly IConfiguration _config;

        public UsuarioController(DBContext context, IConfiguration config)
        {
            _usuarioServices = new UsuarioServices(context);
            _config = config;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public ActionResult<string> Register(EntUsuario usuario)
        {
            bool esError = false;
            string msgError = "Usuario registrado de forma exitosa.";

            try
            {
                if (!_usuarioServices.ValidarUsuario(usuario.correo))
                {
                    esError = true;
                    msgError = "El correo ya se encuentra registrado.";
                }

                _usuarioServices.Registrar(usuario);

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

        [HttpPost("session")]
        [AllowAnonymous]
        public ActionResult<string> Session(EntLoginDTO login)
        {
            bool esError = false;
            string msgError = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(login.correo) && string.IsNullOrEmpty(login.contrasena))
                {
                    esError = true;
                    msgError = "Datos inválidos.";
                }

                EntUsuario usuario = _usuarioServices.Login(login);

                if (usuario == null)
                {
                    esError = true;
                    msgError = "Usuario y/o contraseña incorrectos.";
                }

                if (!esError)
                {
                    var respuesta = new
                    {
                        error = esError,
                        mensajeError = msgError,
                        token = Herramienta.GenerarToken(_config, usuario.correo.Split('@')[0]),
                        data = usuario
                    };

                    return Ok(respuesta);
                }
                else
                {
                    var respuesta = new
                    {
                        error = esError,
                        mensajeError = msgError,
                        token = "",
                        data = usuario
                    };

                    return Ok(respuesta);
                }
                
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<List<EntUsuario>> Get()
        {
            bool esError = false;
            string msgError = string.Empty;

            try
            {
                List<EntUsuario> usuario = _usuarioServices.ObtenerUsuario();

                var respuesta = new
                {
                    error = esError,
                    mensajeError = msgError,
                    data = usuario
                };

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<EntUsuario> Get(int id)
        {
            try
            {
                EntUsuario lUsuario = _usuarioServices.ObtenerUsuario(id);

                if (lUsuario == null)
                    return BadRequest("El usuario no se encuentra registrado.");

                return Ok(lUsuario);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Post(EntUsuario usuario) {
            bool esError = false;
            string msgError = string.Empty;

            try
            {
                if (!_usuarioServices.ValidarUsuario(usuario.correo))
                {
                    esError = true;
                    msgError = "El correo ya se encuentra registrado.";
                }

                _usuarioServices.Registrar(usuario);
                
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
        public ActionResult Patch(EntUsuario usuario)
        {
            bool esError = false;
            string msgError = string.Empty;

            try
            {
                _usuarioServices.Actualizar(usuario);

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

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            bool esError = false;
            string msgError = string.Empty;

            try
            {
                _usuarioServices.Eliminar(id);

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
