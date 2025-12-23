using Entrevista.CORE.Interfaces;
using Entrevista.CORE.Modelos;
using Entrevista.DAL.Conexion;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entrevista.DAL.Repositorios
{
    public sealed class ElementoRepositorio : IElementoRepositorio
    {
        private readonly FabricaConexionPostgres _fabricaConexion;

        public ElementoRepositorio(FabricaConexionPostgres fabricaConexion)
        {
            _fabricaConexion = fabricaConexion;
        }

        public async Task<IReadOnlyList<Elemento>> ObtenerTodosAsync(CancellationToken ct)
        {
            List<Elemento> lista = new List<Elemento>();

            await using NpgsqlConnection conn = (NpgsqlConnection)_fabricaConexion.Crear();
            await conn.OpenAsync(ct);

            await using NpgsqlTransaction tx = await conn.BeginTransactionAsync(ct);

            await using NpgsqlCommand cmd = new NpgsqlCommand("aplicacion.elementos_listar_cursor", conn, tx);
            cmd.CommandType = CommandType.StoredProcedure;

            NpgsqlParameter pCursor = new NpgsqlParameter("p_cursor", NpgsqlDbType.Refcursor);
            pCursor.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(pCursor);

            await cmd.ExecuteNonQueryAsync(ct);

            string? cursorNombre = pCursor.Value as string;
            if (string.IsNullOrWhiteSpace(cursorNombre))
            {
                throw new InvalidOperationException("No se recibió el nombre del cursor desde Postgres.");
            }

            await using NpgsqlCommand fetch = new NpgsqlCommand($"FETCH ALL IN \"{cursorNombre}\";", conn, tx);

            await using (NpgsqlDataReader reader = await fetch.ExecuteReaderAsync(ct))
            {
                int ordId = reader.GetOrdinal("id");
                int ordNombre = reader.GetOrdinal("nombre");
                int ordApellido = reader.GetOrdinal("apellido");
                int ordObservacion = reader.GetOrdinal("observacion");
                int ordCreadoEn = reader.GetOrdinal("creado_en");

                while (await reader.ReadAsync(ct))
                {
                    Elemento e = new Elemento();
                    e.Id = reader.GetInt64(ordId);
                    e.Nombre = reader.GetString(ordNombre);
                    e.Apellido = reader.GetString(ordApellido);
                    e.Apellido = reader.GetString(ordApellido);
                    e.Observacion = reader.GetString(ordObservacion);
                    e.CreadoEn = reader.GetFieldValue<DateTimeOffset>(ordCreadoEn);
                    lista.Add(e);
                }
            }

            await tx.CommitAsync(ct);

            return lista;
        }

        public async Task<long> CrearAsync(string nombre, string apellido,string observacion, CancellationToken ct)
        {
            await using NpgsqlConnection conn = (NpgsqlConnection)_fabricaConexion.Crear();
            await conn.OpenAsync(ct);

            await using NpgsqlCommand cmd = new NpgsqlCommand("aplicacion.elementos_crear", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("p_nombre", nombre);
            cmd.Parameters.AddWithValue("p_apellido", apellido);
            cmd.Parameters.AddWithValue("p_observacion", observacion);

            NpgsqlParameter pId = new NpgsqlParameter("p_id", NpgsqlDbType.Bigint);
            pId.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(pId);

            await cmd.ExecuteNonQueryAsync(ct);

            if (pId.Value == null || pId.Value == DBNull.Value)
            {
                throw new InvalidOperationException("No se recibió el id generado desde Postgres.");
            }

            return Convert.ToInt64(pId.Value);
        }
    }
}
