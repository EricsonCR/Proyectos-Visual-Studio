using Microsoft.Data.SqlClient;
using System.Data;
using waSistemaCobrosColegio.Database;
using waSistemaCobrosColegio.Interfaces;
using waSistemaCobrosColegio.Models;

namespace waSistemaCobrosColegio.Repositorys
{
    public class RepositoryCategoriaDetalle : ICategoriaDetalle
    {
        public string? Error { get; set; }
        private readonly SqlConnection conexion;
        private readonly SqlCommand comando;
        public RepositoryCategoriaDetalle()
        {
            comando = new SqlCommand();
            conexion = Conexion.GetInstancia().CrearConexion();
        }
        public bool Actualizar(CategoriaDetalle m)
        {
            try
            {
                conexion.Open();
                comando.CommandText = "SP_CATEGORIA_DETALLE_ACTUALIZAR";
                comando.Connection = conexion;
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@id", m.Id);
                comando.Parameters.AddWithValue("@id_categoria", m.Id_Categoria);
                comando.Parameters.AddWithValue("@nombre", m.Nombre);
                comando.Parameters.AddWithValue("@estado", m.Estado);
                int r = comando.ExecuteNonQuery();
                if (r > 0) return true;
            }
            catch (Exception ex) { Error = ex.Message; }
            finally { conexion.Close(); }
            return false;
        }

        public bool Crear(CategoriaDetalle m)
        {
            try
            {
                conexion.Open();
                comando.CommandText = "SP_CATEGORIA_DETALLE_CREAR";
                comando.Connection = conexion;
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@id_categoria", m.Id_Categoria);
                comando.Parameters.AddWithValue("@nombre", m.Nombre);
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
                comando.CommandText = "SP_CATEGORIA_DETALLE_ELIMINAR_PORID";
                comando.Connection = conexion;
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@id", id);
                int r = comando.ExecuteNonQuery();
                if (r > 0) return true;
            }
            catch (Exception ex) { Error = ex.Message; }
            finally { conexion.Close(); }
            return false;
        }

        public CategoriaDetalle LeerPorId()
        {
            CategoriaDetalle categoriaDetalle = null!;
            try
            {
                conexion.Open();
                comando.CommandText = "SP_CATEGORIA_DETALLE_LEER_PORID";
                comando.Connection = conexion;
                comando.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = comando.ExecuteReader();
                if (dr.Read())
                {
                    categoriaDetalle = new CategoriaDetalle()
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Id_Categoria = Convert.ToInt32(dr["id_categoria"]),
                        Nombre = Convert.ToString(dr["nombre"]),
                        Estado = Convert.ToBoolean(dr["estado"]),
                    };
                }
                dr.Close();
            }
            catch (Exception ex) { Error = ex.Message; }
            finally { conexion.Close(); }
            return categoriaDetalle;
        }

        public IEnumerable<CategoriaDetalle> Listar()
        {
            List<CategoriaDetalle> listaCategoriaDetalle = new List<CategoriaDetalle>();
            try
            {
                listaCategoriaDetalle.Clear();
                conexion.Open();
                comando.CommandText = "SP_CATEGORIA_DETALLE_LISTAR";
                comando.Connection = conexion;
                comando.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = comando.ExecuteReader();
                while (dr.Read())
                {
                    listaCategoriaDetalle.Add(new CategoriaDetalle()
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Id_Categoria = Convert.ToInt32(dr["id_categoria"]),
                        Nombre = Convert.ToString(dr["nombre"]),
                        Estado = Convert.ToBoolean(dr["estado"])
                    });
                }
                dr.Close();
            }
            catch (Exception ex) { Error = ex.Message; }
            finally { conexion.Close(); }
            return listaCategoriaDetalle;
        }
    }
}
