using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entrevista.CORE.Modelos
{
    public sealed class Elemento
    {
        public long Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Apellido { get; set; } = "";
        public string Observacion { get; set; } = "";
        public DateTimeOffset CreadoEn { get; set; }
    }
}
