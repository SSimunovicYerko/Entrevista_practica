using Entrevista.BLL.Servicios;
using Entrevista.CORE.Interfaces;
using Entrevista.DAL.Conexion;
using Entrevista.DAL.Repositorios;
using Entrevista.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var mensajes = context.ModelState
            .Where(x => x.Value != null && x.Value.Errors.Count > 0)
            .SelectMany(x => x.Value!.Errors.Select(e => e.ErrorMessage))
            .ToList();

        var mensajeUnico = string.Join(" | ", mensajes);

        Log.Warning("Validación falló. TraceId: {TraceId}. Mensajes: {Mensajes}",
            context.HttpContext.TraceIdentifier,
            mensajeUnico);

        return new BadRequestObjectResult(new
        {
            codigo = "400",
            mensaje = mensajeUnico,
            traceId = context.HttpContext.TraceIdentifier
        });
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? cs = builder.Configuration.GetConnectionString("Postgres");
if (string.IsNullOrWhiteSpace(cs))
{
    throw new InvalidOperationException("Falta ConnectionStrings:Postgres");
}

builder.Services.AddSingleton(new FabricaConexionPostgres(cs));
builder.Services.AddScoped<IElementoRepositorio, ElementoRepositorio>();
builder.Services.AddScoped<ElementoServicio>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("React", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("React");

app.UseMiddleware<MiddlewareErrores>();

app.UseAuthorization();
app.MapControllers();

try
{
    app.Run();
}
finally
{
    Log.CloseAndFlush();
}
