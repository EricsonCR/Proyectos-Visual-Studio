using MessagePack.Internal;
using Microsoft.Data.SqlClient;
using System.Data;
using waSistemaCobrosColegio.Database;
using waSistemaCobrosColegio.Interfaces;
using waSistemaCobrosColegio.Models;

namespace waSistemaCobrosColegio.Repositorys
{
    public class RepositoryApoderado : IApoderado
    {
        public string? Error { get; set; }
        private readonly SqlConnection conexion;
        private readonly SqlCommand comando;
        public RepositoryApoderado()
        {
            comando = new SqlCommand();
            conexion = Conexion.GetInstancia().CrearConexion();
        }
        public bool Actualizar(Apoderado m)
        {
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "SP_APODERADO_ACTUALIZAR";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@id", m.Id);
                comando.Parameters.AddWithValue("@tipo_documento", m.Tipo_Documento);
                comando.Parameters.AddWithValue("@numero_documento", m.Numero_Documento);
                comando.Parameters.AddWithValue("@nombre", m.Nombre);
                comando.Parameters.AddWithValue("@apellido", m.Apellido);
                comando.Parameters.AddWithValue("@direccion", m.Direccion);
                comando.Parameters.AddWithValue("@telefono", m.Telefono);
                comando.Parameters.AddWithValue("@email", m.Email);
                comando.Parameters.AddWithValue("@relacion", m.Relacion);
                comando.Parameters.AddWithValue("@fecha_nacimiento", m.Fecha_Nacimiento);
                comando.Parameters.AddWithValue("@estado", m.Estado);
                int r = comando.ExecuteNonQuery();
                if (r > 0) return true;
            }
            catch (Exception ex) { Error = ex.Message; }
            finally { conexion.Close(); }
            return false;
        }

        public bool Crear(Apoderado m)
        {
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "SP_APODERADO_CREAR";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@tipo_documento", m.Tipo_Documento);
                comando.Parameters.AddWithValue("@numero_documento", m.Numero_Documento);
                comando.Parameters.AddWithValue("@nombre", m.Nombre);
                comando.Parameters.AddWithValue("@apellido", m.Apellido);
                comando.Parameters.AddWithValue("@direccion", m.Direccion);
                comando.Parameters.AddWithValue("@telefono", m.Telefono);
                comando.Parameters.AddWithValue("@email", m.Email);
                comando.Parameters.AddWithValue("@relacion", m.Relacion);
                comando.Parameters.AddWithValue("@fecha_nacimiento", m.Fecha_Nacimiento);
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
                comando.CommandText = "SP_APODERADO_ELIMINAR_PORID";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@id", id);
                int r = comando.ExecuteNonQuery();
                if (r > 0) return true;
            }
            catch (Exception ex) { Error = ex.Message; }
            finally { conexion.Close(); }
            return false;
        }

        public Apoderado LeerPorId(int id)
        {
            Apoderado apoderado = null!;
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "SP_APODERADO_LEER_PORID";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = comando.ExecuteReader();
                while (dr.Read())
                {
                    apoderado = new Apoderado()
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Tipo_Documento = Convert.ToString(dr["tipo_documento"]),
                        Numero_Documento = Convert.ToString(dr["numero_documento"]),
                        Nombre = Convert.ToString(dr["nombre"]),
                        Apellido = Convert.ToString(dr["apellido"]),
                        Direccion = Convert.ToString(dr["direccion"]),
                        Telefono = Convert.ToString(dr["telefono"]),
                        Email = Convert.ToString(dr["email"]),
                        Relacion = Convert.ToString(dr["relacion"]),
                        Fecha_Nacimiento = Convert.ToDateTime(dr["fecha_nacimiento"]),
                        Fecha_Registro = Convert.ToDateTime(dr["fecha_registro"]),
                        Fecha_Actualizacion = Convert.ToDateTime(dr["fecha_actualizacion"]),
                        Estado = Convert.ToBoolean(dr["estado"]),
                    };
                }
                dr.Close();
            }
            catch (Exception ex) { Error = ex.Message; }
            finally { conexion.Close(); }
            return apoderado;
        }

        public Apoderado LeerPorDni(string dni)
        {
            Apoderado apoderado = null!;
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "SP_APODERADO_LEER_PORDNI";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@dni", dni);
                SqlDataReader dr = comando.ExecuteReader();
                while (dr.Read())
                {
                    apoderado = new Apoderado()
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Tipo_Documento = Convert.ToString(dr["tipo_documento"]),
                        Numero_Documento = Convert.ToString(dr["numero_documento"]),
                        Nombre = Convert.ToString(dr["nombre"]),
                        Apellido = Convert.ToString(dr["apellido"]),
                        Direccion = Convert.ToString(dr["direccion"]),
                        Telefono = Convert.ToString(dr["telefono"]),
                        Email = Convert.ToString(dr["email"]),
                        Relacion = Convert.ToString(dr["relacion"]),
                        Fecha_Nacimiento = Convert.ToDateTime(dr["fecha_nacimiento"]),
                        Fecha_Registro = Convert.ToDateTime(dr["fecha_registro"]),
                        Fecha_Actualizacion = Convert.ToDateTime(dr["fecha_actualizacion"]),
                        Estado = Convert.ToBoolean(dr["estado"]),
                    };
                }
                dr.Close();
            }
            catch (Exception ex) { Error = ex.Message; }
            finally { conexion.Close(); }
            return apoderado;
        }

        public IEnumerable<Apoderado> Listar()
        {
            List<Apoderado> listaEstudiantes = new List<Apoderado>();
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "SP_APODERADO_LISTAR";
                comando.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = comando.ExecuteReader();
                while (dr.Read())
                {
                    listaEstudiantes.Add(new Apoderado()
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Tipo_Documento = Convert.ToString(dr["tipo_documento"]),
                        Numero_Documento = Convert.ToString(dr["numero_documento"]),
                        Nombre = Convert.ToString(dr["nombre"]),
                        Apellido = Convert.ToString(dr["apellido"]),
                        Direccion = Convert.ToString(dr["direccion"]),
                        Telefono = Convert.ToString(dr["telefono"]),
                        Email = Convert.ToString(dr["email"]),
                        Relacion = Convert.ToString(dr["relacion"]),
                        Fecha_Nacimiento = Convert.ToDateTime(dr["fecha_nacimiento"]),
                        Fecha_Registro = Convert.ToDateTime(dr["fecha_registro"]),
                        Fecha_Actualizacion = Convert.ToDateTime(dr["fecha_actualizacion"]),
                        Estado = Convert.ToBoolean(dr["estado"]),
                    });
                }
                dr.Close();
            }
            catch (Exception ex) { Error = ex.Message; }
            finally { conexion.Close(); }
            return listaEstudiantes;
        }
    }
}
