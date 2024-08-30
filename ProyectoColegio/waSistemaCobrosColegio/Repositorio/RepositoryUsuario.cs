using Microsoft.Data.SqlClient;
using System.Data;
using waSistemaCobrosColegio.Database;
using waSistemaCobrosColegio.Interfaces;
using waSistemaCobrosColegio.Models;

namespace waSistemaCobrosColegio.Repositorys
{
    public class RepositoryUsuario : IUsuario
    {
        private string? Error { get; set; }
        private readonly SqlConnection conexion;
        private readonly SqlCommand comando;
        public RepositoryUsuario()
        {
            comando = new SqlCommand();
            conexion = Conexion.GetInstancia().CrearConexion();
        }
        public bool Actualizar(Usuario m)
        {
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "SP_USUARIO_ACTUALIZAR";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@id", m.Id);
                comando.Parameters.AddWithValue("@tipo_documento", m.Tipo_Documento);
                comando.Parameters.AddWithValue("@numero_documento", m.Numero_Documento);
                comando.Parameters.AddWithValue("@nombre", m.Nombre);
                comando.Parameters.AddWithValue("@apellido", m.Apellido);
                comando.Parameters.AddWithValue("@direccion", m.Direccion);
                comando.Parameters.AddWithValue("@telefono", m.Telefono);
                comando.Parameters.AddWithValue("@email", m.Email);
                comando.Parameters.AddWithValue("@clave", m.Clave);
                comando.Parameters.AddWithValue("@genero", m.Genero);
                comando.Parameters.AddWithValue("@fecha_nacimiento", m.Fecha_Nacimiento);
                comando.Parameters.AddWithValue("@estado", m.Estado);
                int r = comando.ExecuteNonQuery();
                if (r > 0) return true;
            }
            catch (Exception ex) { Error = ex.Message; }
            finally { conexion.Close(); }
            return false;
        }

        public bool Crear(Usuario m)
        {
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "SP_USUARIO_CREAR";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@rol", m.Rol);
                comando.Parameters.AddWithValue("@tipo_documento", m.Tipo_Documento);
                comando.Parameters.AddWithValue("@numero_documento", m.Numero_Documento);
                comando.Parameters.AddWithValue("@nombre", m.Nombre);
                comando.Parameters.AddWithValue("@apellido", m.Apellido);
                comando.Parameters.AddWithValue("@direccion", m.Direccion);
                comando.Parameters.AddWithValue("@telefono", m.Telefono);
                comando.Parameters.AddWithValue("@email", m.Email);
                comando.Parameters.AddWithValue("@clave", m.Clave);
                comando.Parameters.AddWithValue("@genero", m.Genero);
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
                comando.CommandText = "SP_USUARIO_ELIMINAR_PORID";
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

        public Usuario LeerPorId(int id)
        {
            Usuario usuario = null!;
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "SP_USUARIO_LEER_PORID";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = comando.ExecuteReader();
                while (dr.Read())
                {
                    usuario = new Usuario()
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Tipo_Documento = Convert.ToString(dr["tipo_documento"]),
                        Numero_Documento = Convert.ToString(dr["numero_documento"]),
                        Nombre = Convert.ToString(dr["nombre"]),
                        Apellido = Convert.ToString(dr["apellido"]),
                        Direccion = Convert.ToString(dr["direccion"]),
                        Telefono = Convert.ToString(dr["telefono"]),
                        Email = Convert.ToString(dr["email"]),
                        Clave = Convert.ToString(dr["clave"]),
                        Genero = Convert.ToChar(dr["genero"]),
                        Fecha_Nacimiento = Convert.ToDateTime(dr["fecha_nacimiento"]),
                        Fecha_Registro = Convert.ToDateTime(dr["fecha_registro"]),
                        Fecha_Actualizacion = Convert.ToDateTime(dr["fecha_actualizacion"]),
                        Fecha_Acceso = Convert.ToDateTime(dr["fecha_acceso"]),
                        Estado = Convert.ToBoolean(dr["estado"]),
                    };
                }
                dr.Close();
            }
            catch (Exception ex) { Error = ex.Message; }
            finally { conexion.Close(); }
            return usuario;
        }

        public IEnumerable<Usuario> Listar()
        {
            List<Usuario> listaUsuarios = new List<Usuario>();
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "SP_USUARIO_LISTAR";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                SqlDataReader dr = comando.ExecuteReader();
                while (dr.Read())
                {
                    listaUsuarios.Add(new Usuario()
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Rol = Convert.ToString(dr["rol"]),
                        Tipo_Documento = Convert.ToString(dr["tipo_documento"]),
                        Numero_Documento = Convert.ToString(dr["numero_documento"]),
                        Nombre = Convert.ToString(dr["nombre"]),
                        Apellido = Convert.ToString(dr["apellido"]),
                        Direccion = Convert.ToString(dr["direccion"]),
                        Telefono = Convert.ToString(dr["telefono"]),
                        Email = Convert.ToString(dr["email"]),
                        Clave = Convert.ToString(dr["clave"]),
                        Genero = Convert.ToChar(dr["genero"]),
                        Fecha_Nacimiento = Convert.ToDateTime(dr["fecha_nacimiento"]),
                        Fecha_Registro = Convert.ToDateTime(dr["fecha_registro"]),
                        Fecha_Actualizacion = Convert.ToDateTime(dr["fecha_actualizacion"]),
                        Estado = Convert.ToBoolean(dr["estado"]),
                        Fecha_Acceso = Convert.ToDateTime(dr["fecha_acceso"]),
                    });
                }
                dr.Close();
            }
            catch (Exception ex) { Error = ex.Message; }
            finally { conexion.Close(); }
            return listaUsuarios;
        }

        public Usuario Autenticar(Usuario m)
        {
            Usuario usuario = null!;
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "SP_USUARIO_AUTENTICAR";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@email", m.Email);
                comando.Parameters.AddWithValue("@clave", m.Clave);
                SqlDataReader dr = comando.ExecuteReader();
                while (dr.Read())
                {
                    usuario = new Usuario()
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Tipo_Documento = Convert.ToString(dr["tipo_documento"]),
                        Numero_Documento = Convert.ToString(dr["numero_documento"]),
                        Nombre = Convert.ToString(dr["nombre"]),
                        Apellido = Convert.ToString(dr["apellido"]),
                        Direccion = Convert.ToString(dr["direccion"]),
                        Telefono = Convert.ToString(dr["telefono"]),
                        Email = Convert.ToString(dr["email"]),
                        Clave = Convert.ToString(dr["clave"]),
                        Genero = Convert.ToChar(dr["genero"]),
                        Fecha_Nacimiento = Convert.ToDateTime(dr["fecha_nacimiento"]),
                        Fecha_Registro = Convert.ToDateTime(dr["fecha_registro"]),
                        Fecha_Actualizacion = Convert.ToDateTime(dr["fecha_actualizacion"]),
                        Fecha_Acceso = Convert.ToDateTime(dr["fecha_acceso"]),
                        Estado = Convert.ToBoolean(dr["estado"]),
                    };
                }
                dr.Close();
            }
            catch (Exception ex) { Error = ex.Message; }
            finally { conexion.Close(); }
            return usuario;
        }

        public bool Registrar(Usuario m)
        {
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "SP_USUARIO_REGISTRAR";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@nombre", m.Nombre);
                comando.Parameters.AddWithValue("@apellido", m.Apellido);
                comando.Parameters.AddWithValue("@email", m.Email);
                comando.Parameters.AddWithValue("@clave", m.Clave);
                int r = comando.ExecuteNonQuery();
                if (r > 0) return true;
            }
            catch (Exception ex) { Error = ex.Message; }
            finally { conexion.Close(); }
            return false;
        }

        public bool ValidarExistencia(Usuario m)
        {
            throw new NotImplementedException();
        }
    }
}