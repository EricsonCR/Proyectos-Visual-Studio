using Microsoft.Data.SqlClient;

namespace waSistemaCobrosColegio.Database
{
    public class Conexion
    {
        private SqlConnection? conexion;
        private static Conexion? instancia;

        private Conexion() { }

        public static Conexion GetInstancia()
        {
            if (instancia == null) { instancia = new Conexion(); }
            return instancia;
        }

        public SqlConnection CrearConexion()
        {
            conexion = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("cadenaSQL"));
            return conexion;
        }
    }
}
