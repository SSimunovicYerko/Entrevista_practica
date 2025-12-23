using Entrevista.CORE.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entrevista.CORE.Interfaces
{
    public interface IElementoRepositorio
    {
        Task<IReadOnlyList<Elemento>> ObtenerTodosAsync(CancellationToken ct);
        Task<long> CrearAsync(string nombre,string apellido,string observacion, CancellationToken ct);
    }
}
