using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    [Table("usuario", Schema = "web")]
    public class EntUsuario
    {
        [Key]
        public long id_usuario { get; set; }
        [Required(ErrorMessage = "Se requiere el nombre.")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "Se requiere el apellido.")]
        public string apellido { get; set;}
        [Required(ErrorMessage = "Se requiere el correo.")]
        public string correo { get; set; }
        [Required(ErrorMessage = "Se requiere la contrasena.")]
        public string contrasena { get; set; }
        [Required(ErrorMessage = "Se requiere el tipo.")]
        public char tipo { get; set; }
    }
}
