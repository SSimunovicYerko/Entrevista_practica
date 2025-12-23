using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Entrevista.CORE.Interfaces;
using Entrevista.CORE.Modelos;

namespace Entrevista.BLL.Servicios
{
    public sealed class ElementoServicio
    {
        private readonly IElementoRepositorio _repositorio;

        public ElementoServicio(IElementoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public Task<IReadOnlyList<Elemento>> ListarAsync(CancellationToken ct)
        {
            return _repositorio.ObtenerTodosAsync(ct);
        }

        public Task<long> CrearAsync(string nombre, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("El nombre es obligatorio.", nameof(nombre));
            }

            string limpio = nombre.Trim();

            if (limpio.Length > 100)
            {
                throw new ArgumentException("El nombre no puede superar 100 caracteres.", nameof(nombre));
            }

            return _repositorio.CrearAsync(limpio, ct);
        }
    }
}
