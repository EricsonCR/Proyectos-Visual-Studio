using Microsoft.Data.SqlClient;
using System.Data;
using waSistemaCobrosColegio.Database;
using waSistemaCobrosColegio.Interfaces;
using waSistemaCobrosColegio.Models;

namespace waSistemaCobrosColegio.Repositorio
{
    public class RepositoryPagoDetalle : IPagoDetalle
    {
        private string? _error { get; set; }
        private readonly SqlConnection conexion;
        private readonly SqlCommand comando;
        public RepositoryPagoDetalle()
        {
            comando = new SqlCommand();
            conexion = Conexion.GetInstancia().CrearConexion();
        }

        public string Error() { return _error!; }

        public IEnumerable<PagoDetalle> Listar()
        {
            List<PagoDetalle> lista = new List<PagoDetalle>();
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "SP_PAGO_DETALLE_LISTAR";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                SqlDataReader dr = comando.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new PagoDetalle()
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Id_Pago = Convert.ToInt32(dr["id_pago"]),
                        Id_Matricula_Detalle = Convert.ToInt32(dr["id_matricula_detalle"]),
                        Concepto = Convert.ToString(dr["concepto"]),
                        Monto = Convert.ToDouble(dr["monto"]),
                        Estado = Convert.ToString(dr["estado"])
                    });
                }
                dr.Close();
            }
            catch (Exception ex) { _error = ex.Message; }
            finally { conexion.Close(); }
            return lista;
        }

        public IEnumerable<PagoDetalle> ListarPorIdPago(int id)
        {
            List<PagoDetalle> lista = new List<PagoDetalle>();
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "SP_PAGO_DETALLE_LISTAR_PORIDPAGO";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@id_pago", id);
                SqlDataReader dr = comando.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new PagoDetalle()
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Id_Pago = Convert.ToInt32(dr["id_pago"]),
                        Id_Matricula_Detalle = Convert.ToInt32(dr["id_matricula_detalle"]),
                        Concepto = Convert.ToString(dr["concepto"]),
                        Monto = Convert.ToDouble(dr["monto"]),
                        Estado = Convert.ToString(dr["estado"])
                    });
                }
                dr.Close();
            }
            catch (Exception ex) { _error = ex.Message; }
            finally { conexion.Close(); }
            return lista;
        }
    }
}
