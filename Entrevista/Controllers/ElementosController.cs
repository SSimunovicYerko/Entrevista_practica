using Entrevista.BLL.Servicios;
using Entrevista.CORE.DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Entrevista.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class ElementosController : ControllerBase
    {
        private readonly ElementoServicio _servicio;
        private readonly ILogger<ElementosController> _logger;

        public ElementosController(ElementoServicio servicio, ILogger<ElementosController> logger)
        {
            _servicio = servicio;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Obtener(CancellationToken ct)
        {
            _logger.LogInformation("GET /api/elementos");
            var data = await _servicio.ListarAsync(ct);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Insertar([FromBody] CrearElementoRequest request, CancellationToken ct)
        {
            _logger.LogInformation("POST /api/elementos");

            if (request == null)
            {
                return BadRequest(new { codigo = "400", mensaje = "Body requerido." });
            }

            long id = await _servicio.CrearAsync(request.Nombre, ct);

            return Created($"/api/elementos/{id}", new { id });
        }
    }
}
