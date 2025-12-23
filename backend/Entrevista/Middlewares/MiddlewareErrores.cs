using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Entrevista.Middlewares
{
    public sealed class MiddlewareErrores
    {
        private readonly RequestDelegate _next;

        public MiddlewareErrores(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ArgumentException ex)
            {
                Log.Error(ex, "Error de validación/negocio. TraceId: {TraceId}", context.TraceIdentifier);

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(new
                {
                    codigo = "400",
                    mensaje = ex.Message,
                    traceId = context.TraceIdentifier
                });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error no controlado. TraceId: {TraceId}", context.TraceIdentifier);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(new
                {
                    codigo = "500",
                    mensaje = "Ocurrió un error inesperado.",
                    traceId = context.TraceIdentifier
                });
            }
        }
    }
}
