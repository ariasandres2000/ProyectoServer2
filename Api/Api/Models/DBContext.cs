using Microsoft.EntityFrameworkCore;

namespace Api.Models
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

        }

        //Tablas
        public DbSet<EntUsuario> EntUsuarios { get; set; }
        public DbSet<EntIndicacion> EntIndicacions { get; set; }
    }
}
