using Api.DTO;
using Api.Models;
using Api.Utils;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class UsuarioServices
    {
        private readonly DBContext _dbContext;

        public UsuarioServices(DBContext context)
        {
            _dbContext = context;
        }

        public EntUsuario Login(EntLoginDTO login)
        {
            try
            {
                string lContrasena = Herramienta.EncriptarContrasena(login.contrasena);

                return _dbContext.EntUsuarios.Where(p => p.correo.Equals(login.correo) && p.contrasena.Equals(lContrasena)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<EntUsuario> ObtenerUsuario()
        {
            try
            {
                return _dbContext.EntUsuarios.OrderBy(p => p.id_usuario).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public EntUsuario ObtenerUsuario(int idUsuario)
        {
            try
            {
                return _dbContext.EntUsuarios.Where(p => p.id_usuario.Equals(idUsuario)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Registrar(EntUsuario usuario)
        {
            try
            {
                usuario.contrasena = Herramienta.EncriptarContrasena(usuario.contrasena);

                _dbContext.EntUsuarios.Add(usuario);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Actualizar(EntUsuario usuario)
        {
            try
            {                
                EntUsuario lUsuario = _dbContext.EntUsuarios.Where(p => p.id_usuario.Equals(usuario.id_usuario)).First();
                lUsuario.nombre = usuario.nombre;
                lUsuario.apellido = usuario.apellido;
                if (usuario.contrasena != lUsuario.contrasena)
                {
                    lUsuario.contrasena = Herramienta.EncriptarContrasena(usuario.contrasena);
                }
                lUsuario.tipo = usuario.tipo;

                _dbContext.Entry(lUsuario).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Eliminar(int idUsuario)
        {
            try
            {
                EntUsuario lUsuario = _dbContext.EntUsuarios.Where(p => p.id_usuario.Equals(idUsuario)).First();

                if (lUsuario.correo.Equals("admin@proyecto.com"))
                    throw new Exception("El usuario Administrador no se puede eliminar.");

                _dbContext.EntUsuarios.Remove(lUsuario);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ValidarUsuario(string correo)
        {
            bool valido = false;

            try
            {
                EntUsuario lUsuario = _dbContext.EntUsuarios.Where(p => p.correo.Equals(correo)).FirstOrDefault();

                if (lUsuario == null)
                    valido = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return valido;
        }
    }
}
