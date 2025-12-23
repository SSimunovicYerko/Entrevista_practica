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

        public Task<long> CrearAsync(string nombre, string apellido, string observacion, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("El nombre es obligatorio.", nameof(nombre));
            }

            if (string.IsNullOrWhiteSpace(apellido))
            {
                throw new ArgumentException("El apellido es obligatorio.", nameof(apellido));
            }

            if (string.IsNullOrWhiteSpace(observacion))
            {
                throw new ArgumentException("La observación es obligatoria.", nameof(observacion));
            }

            string nombreLimpio = nombre.Trim();
            string apellidoLimpio = apellido.Trim();
            string observacionLimpia = observacion.Trim();

            if (nombreLimpio.Length > 100)
            {
                throw new ArgumentException("El nombre no puede superar 100 caracteres.", nameof(nombre));
            }

            if (apellidoLimpio.Length > 100)
            {
                throw new ArgumentException("El apellido no puede superar 100 caracteres.", nameof(apellido));
            }

            if (observacionLimpia.Length > 500)
            {
                throw new ArgumentException("La observación no puede superar 500 caracteres.", nameof(observacion));
            }

            return _repositorio.CrearAsync(nombreLimpio, apellidoLimpio, observacionLimpia, ct);
        }

    }
}
