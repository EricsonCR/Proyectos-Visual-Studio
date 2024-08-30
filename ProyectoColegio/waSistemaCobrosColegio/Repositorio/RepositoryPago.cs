using Microsoft.Data.SqlClient;
using System.Data;
using waSistemaCobrosColegio.Database;
using waSistemaCobrosColegio.Interfaces;
using waSistemaCobrosColegio.Models;

namespace waSistemaCobrosColegio.Repositorys
{
    public class RepositoryPago : IPago
    {
        private string? Error { get; set; }
        private readonly SqlConnection conexion;
        private readonly SqlCommand comando;
        public RepositoryPago()
        {
            comando = new SqlCommand();
            conexion = Conexion.GetInstancia().CrearConexion();
        }
        public bool Actualizar(Pago p)
        {
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "SP_PAGO_ACTUALIZAR";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@id", p.Id);
                comando.Parameters.AddWithValue("@id", p.Id_Matricula);
                comando.Parameters.AddWithValue("@id", p.Monto_Total);
                comando.Parameters.AddWithValue("@id", p.Metodo_Pago);
                comando.Parameters.AddWithValue("@id", p.Numero_Op);
                comando.Parameters.AddWithValue("@id", p.Url_Voucher);
                comando.Parameters.AddWithValue("@id", p.Fecha_Actualizacion);
                comando.Parameters.AddWithValue("@id", p.Estado);
                int r = comando.ExecuteNonQuery();
                if (r > 0) return true;
            }
            catch (Exception ex) { Error = ex.Message; }
            finally { conexion.Close(); }
            return false;
        }

        public bool Crear(Pago p)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Id_Matricula_Detalle", System.Type.GetType("System.Int32")!);
                dt.Columns.Add("Concepto", System.Type.GetType("System.String")!);
                dt.Columns.Add("Monto", System.Type.GetType("System.Double")!);

                foreach (PagoDetalle item in p.Pago_Detalle!) { dt.Rows.Add(item.Id_Matricula_Detalle, item.Concepto, item.Monto); }

                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "SP_PAGO_CREAR";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@id_matricula", p.Id_Matricula);
                comando.Parameters.AddWithValue("@monto_total", p.Monto_Total);
                comando.Parameters.AddWithValue("@metodo_pago", p.Metodo_Pago);
                comando.Parameters.AddWithValue("@numero_op", p.Numero_Op);
                comando.Parameters.AddWithValue("@tDetalle", dt).SqlDbType = SqlDbType.Structured;
                int r = comando.ExecuteNonQuery();
                if (r > 0) return true;
            }
            catch (Exception ex) { Error = ex.Message; }
            finally { conexion.Close(); }
            return false;
        }

        public bool EliminarPorId(int id)
        {
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "SP_PAGO_ELIMINAR_PORID";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@id", id);
                int r = comando.ExecuteNonQuery();
                if (r > 0) return true;
            }
            catch (Exception ex) { Error = ex.Message; }
            finally { conexion.Close(); }
            return false;
        }

        public Pago LeerPorId(int id)
        {
            Pago pago = null!;
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "SP_PAGO_LEER_PORID";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = comando.ExecuteReader();
                while (dr.Read())
                {
                    pago = new Pago()
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Id_Matricula = Convert.ToInt32(dr["id_matricula"]),
                        Monto_Total = Convert.ToDecimal(dr["monto_total"]),
                        Metodo_Pago = Convert.ToString(dr["metodo_pago"]),
                        Numero_Op = Convert.ToString(dr["numero_op"]),
                        Url_Voucher = Convert.ToString(dr["url_voucher"]),
                        Fecha_Registro = Convert.ToDateTime(dr["fecha_registro"]),
                        Fecha_Actualizacion = Convert.ToDateTime(dr["fecha_actualizacion"]),
                        Estado = Convert.ToString(dr["estado"])
                    };
                }
                dr.Close();
            }
            catch (Exception ex) { Error = ex.Message; }
            finally { conexion.Close(); }
            return pago;
        }

        public IEnumerable<Pago> Listar()
        {
            List<Pago> listaPagos = new List<Pago>();
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "SP_PAGO_LISTAR";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                SqlDataReader dr = comando.ExecuteReader();
                while (dr.Read())
                {
                    listaPagos.Add(new Pago()
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Id_Matricula = Convert.ToInt32(dr["id_matricula"]),
                        Monto_Total = Convert.ToDecimal(dr["monto_total"]),
                        Metodo_Pago = Convert.ToString(dr["metodo_pago"]),
                        Numero_Op = Convert.ToString(dr["numero_op"]),
                        Url_Voucher = Convert.ToString(dr["url_voucher"]),
                        Fecha_Registro = Convert.ToDateTime(dr["fecha_registro"]),
                        Fecha_Actualizacion = Convert.ToDateTime(dr["fecha_actualizacion"]),
                        Estado = Convert.ToString(dr["estado"])
                    });
                }
                dr.Close();
            }
            catch (Exception ex) { Error = ex.Message; }
            finally { conexion.Close(); }
            return listaPagos;
        }
    }
}
