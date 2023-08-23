using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class IndicacionServices
    {
        private readonly DBContext _dbContext;

        public IndicacionServices(DBContext context)
        {
            _dbContext = context;
        }

        public List<EntIndicacion> ObtenerIndicacionUsuario(long idUsuario)
        {
            try
            {
                return _dbContext.EntIndicacions.Where(p => p.id_usuario ==idUsuario).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public EntIndicacion ObtenerIndicacion(long idIndicacion)
        {
            try
            {
                return _dbContext.EntIndicacions.Where(p => p.id_indicacion == idIndicacion).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Registrar(EntIndicacion indicacion)
        {
            try
            {
                _dbContext.EntIndicacions.Add(indicacion);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Actualizar(EntIndicacion indicacion)
        {
            try
            {
                EntIndicacion lIndicacion = _dbContext.EntIndicacions.Where(p => p.id_indicacion == indicacion.id_indicacion).FirstOrDefault();

                if (lIndicacion == null)
                    throw new Exception("La indicación no existe.");

                lIndicacion.nombre = indicacion.nombre;
                lIndicacion.tipo = indicacion.tipo;
                lIndicacion.instruccion = indicacion.instruccion;
                lIndicacion.valor = indicacion.valor;
                lIndicacion.cantidad = indicacion.cantidad;
                lIndicacion.etiqueta = indicacion.etiqueta;

                _dbContext.Entry(lIndicacion).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Eliminar(long idIndicacion)
        {
            try
            {
                EntIndicacion indicacion = _dbContext.EntIndicacions.Where(p => p.id_indicacion == idIndicacion).FirstOrDefault();

                if (indicacion == null)
                    throw new Exception("La indicación no existe.");

                _dbContext.EntIndicacions.Remove(indicacion);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
