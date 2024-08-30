using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;
using waSistemaCobrosColegio.Database;
using waSistemaCobrosColegio.Interfaces;
using waSistemaCobrosColegio.Models;

namespace waSistemaCobrosColegio.Repositorys
{
    public class RepositoryMatriculaDetalle : IMatriculaDetalle
    {
        public string? Error { get; set; }
        private readonly SqlConnection conexion;
        private readonly SqlCommand comando;
        public RepositoryMatriculaDetalle()
        {
            comando = new SqlCommand();
            conexion = Conexion.GetInstancia().CrearConexion();
        }
        public bool Actualizar(MatriculaDetalle m)
        {
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "SP_MATRICULA_DETALLE_ACTUALIZAR";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@id", m.Id);
                comando.Parameters.AddWithValue("@estado", m.Estado);
                int r = comando.ExecuteNonQuery();
                if (r > 0) return true;
            }
            catch (Exception ex) { Error = ex.Message; }
            finally { conexion.Close(); }
            return false;
        }

        public MatriculaDetalle LeerPorId(int id)
        {
            MatriculaDetalle matriculaDetalle = null!;
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "SP_MATRICULA_DETALLE_LEER_PORID";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = comando.ExecuteReader();
                while (dr.Read())
                {
                    matriculaDetalle = new MatriculaDetalle()
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Id_Matricula = Convert.ToInt32(dr["id_matricula"]),
                        Concepto = Convert.ToString(dr["concepto"]),
                        Monto = Convert.ToDecimal(dr["monto"]),
                        Estado = Convert.ToString(dr["estado"]),
                    };
                }
                dr.Close();
            }
            catch (Exception ex) { Error = ex.Message; }
            finally { conexion.Close(); }
            return matriculaDetalle;
        }

        public IEnumerable<MatriculaDetalle> Listar()
        {
            List<MatriculaDetalle> listaMatriculaDetalle = new List<MatriculaDetalle>();
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "SP_MATRICULA_DETALLE_LISTAR";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                SqlDataReader dr = comando.ExecuteReader();
                while (dr.Read())
                {
                    listaMatriculaDetalle.Add(new MatriculaDetalle()
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Id_Matricula = Convert.ToInt32(dr["id_matricula"]),
                        Concepto = Convert.ToString(dr["concepto"]),
                        Monto = Convert.ToDecimal(dr["monto"]),
                        Estado = Convert.ToString(dr["estado"]),
                    });
                }
                dr.Close();
            }
            catch (Exception ex) { Error = ex.Message; }
            finally { conexion.Close(); }
            return listaMatriculaDetalle;
        }

        public IEnumerable<MatriculaDetalle> ListarPorDniEstudiante(string dni)
        {
            List<MatriculaDetalle> listaMatriculaDetalle = new List<MatriculaDetalle>();
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "SP_MATRICULA_DETALLE_LISTAR_PORDNIESTUDIANTE";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@numero_documento", dni);
                SqlDataReader dr = comando.ExecuteReader();
                while (dr.Read())
                {
                    listaMatriculaDetalle.Add(new MatriculaDetalle()
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Id_Matricula = Convert.ToInt32(dr["id_matricula"]),
                        Concepto = Convert.ToString(dr["concepto"]),
                        Monto = Convert.ToDecimal(dr["monto"]),
                        Estado = Convert.ToString(dr["estado"]),
                    });
                }
                dr.Close();
            }
            catch (Exception ex) { Error = ex.Message; }
            finally { conexion.Close(); }
            return listaMatriculaDetalle;
        }
        public IEnumerable<MatriculaDetalle> ListarPorIdMatriculaPendiente(int id)
        {
            List<MatriculaDetalle> listaMatriculaDetalle = new List<MatriculaDetalle>();
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "SP_MATRICULA_DETALLE_LISTAR_ID_MATRICULA_PENDIENTE";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@id_matricula", id);
                SqlDataReader dr = comando.ExecuteReader();
                while (dr.Read())
                {
                    listaMatriculaDetalle.Add(new MatriculaDetalle()
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Id_Matricula = Convert.ToInt32(dr["id_matricula"]),
                        Concepto = Convert.ToString(dr["concepto"]),
                        Monto = Convert.ToDecimal(dr["monto"]),
                        Estado = Convert.ToString(dr["estado"]),
                    });
                }
                dr.Close();
            }
            catch (Exception ex) { Error = ex.Message; }
            finally { conexion.Close(); }
            return listaMatriculaDetalle;
        }

        public IEnumerable<MatriculaDetalle> ListarPorIdMatricula(int id)
        {
            List<MatriculaDetalle> listaMatriculaDetalle = new List<MatriculaDetalle>();
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "SP_MATRICULA_DETALLE_LISTAR_ID_MATRICULA";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@id_matricula", id);
                SqlDataReader dr = comando.ExecuteReader();
                while (dr.Read())
                {
                    listaMatriculaDetalle.Add(new MatriculaDetalle()
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Id_Matricula = Convert.ToInt32(dr["id_matricula"]),
                        Concepto = Convert.ToString(dr["concepto"]),
                        Monto = Convert.ToDecimal(dr["monto"]),
                        Estado = Convert.ToString(dr["estado"]),
                    });
                }
                dr.Close();
            }
            catch (Exception ex) { Error = ex.Message; }
            finally { conexion.Close(); }
            return listaMatriculaDetalle;
        }
    }
}
