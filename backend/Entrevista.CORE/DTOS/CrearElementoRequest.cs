using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entrevista.CORE.DTOS
{
    public sealed class CrearElementoRequest
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El nombre no puede superar 100 caracteres.")]
        public string Nombre { get; set; } = "";

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El apellido no puede superar 100 caracteres.")]
        public string Apellido { get; set; } = "";

        [Required(ErrorMessage = "la observacion es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El observacion no puede superar 100 caracteres.")]
        public string Observacion { get; set; } = "";
    }
}
