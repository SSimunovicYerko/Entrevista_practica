using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entrevista.DAL.Conexion
{
    public sealed class FabricaConexionPostgres
    {
        private readonly string _cadenaConexion;

        public FabricaConexionPostgres(string cadenaConexion)
        {
            if(string.IsNullOrWhiteSpace(cadenaConexion))
            {
                throw new ArgumentException("La cadena de conexión no puede ser nula o vacía", nameof(cadenaConexion));
            }

            _cadenaConexion = cadenaConexion;
        }

        public IDbConnection Crear()
        {
            return new NpgsqlConnection(_cadenaConexion);
        }

    }
}
