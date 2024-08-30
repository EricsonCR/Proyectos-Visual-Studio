using Microsoft.Data.SqlClient;
using System.Data;
using waSistemaCobrosColegio.Database;
using waSistemaCobrosColegio.Interfaces;
using waSistemaCobrosColegio.Models;

namespace waSistemaCobrosColegio.Repositorys
{
    public class RepositoryEstudiante : IEstudiante
    {
        private string? _error;
        private readonly SqlConnection conexion;
        private readonly SqlCommand comando;

        public RepositoryEstudiante()
        {
            conexion = Conexion.GetInstancia().CrearConexion();
            comando = new SqlCommand();
        }
        public bool Actualizar(Estudiante m)
        {
            try
            {
                conexion.Open();
                comando.CommandText = "SP_ESTUDIANTE_ACTUALIZAR";
                comando.Connection = conexion;
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
                comando.Parameters.AddWithValue("@genero", m.Genero);
                comando.Parameters.AddWithValue("@fecha_nacimiento", m.Fecha_Nacimiento);
                comando.Parameters.AddWithValue("@estado", m.Estado);
                int r = comando.ExecuteNonQuery();
                if (r > 0) return true;
            }
            catch (Exception ex) { _error = ex.Message; }
            finally { conexion.Close(); }
            return false;
        }

        public bool Crear(Estudiante m)
        {
            try
            {
                conexion.Open();
                comando.CommandText = "SP_ESTUDIANTE_CREAR";
                comando.Connection = conexion;
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@tipo_documento", m.Tipo_Documento);
                comando.Parameters.AddWithValue("@numero_documento", m.Numero_Documento);
                comando.Parameters.AddWithValue("@nombre", m.Nombre);
                comando.Parameters.AddWithValue("@apellido", m.Apellido);
                comando.Parameters.AddWithValue("@direccion", m.Direccion);
                comando.Parameters.AddWithValue("@telefono", m.Telefono);
                comando.Parameters.AddWithValue("@email", m.Email);
                comando.Parameters.AddWithValue("@genero", m.Genero);
                comando.Parameters.AddWithValue("@fecha_nacimiento", m.Fecha_Nacimiento);
                int r = comando.ExecuteNonQuery();
                if (r > 0) return true;
            }
            catch (Exception ex) { _error = ex.Message; }
            finally { conexion.Close(); }
            return false;
        }

        public bool EliminarPorId(int id)
        {
            try
            {
                conexion.Open();
                comando.CommandText = "SP_ESTUDIANTE_ELIMINAR_PORID";
                comando.Connection = conexion;
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@id", id);
                int r = comando.ExecuteNonQuery();
                if (r > 0) return true;
            }
            catch (Exception ex) { _error = ex.Message; }
            finally { conexion.Close(); }
            return false;
        }

        public Estudiante LeerPorId(int id)
        {
            Estudiante estudiante = null!;
            try
            {
                conexion.Open();
                comando.CommandText = "SP_ESTUDIANTE_LEER_PORID";
                comando.Connection = conexion;
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = comando.ExecuteReader();
                while (dr.Read())
                {
                    estudiante = new Estudiante()
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Tipo_Documento = Convert.ToString(dr["tipo_documento"]),
                        Numero_Documento = Convert.ToString(dr["numero_documento"]),
                        Nombre = Convert.ToString(dr["nombre"]),
                        Apellido = Convert.ToString(dr["apellido"]),
                        Direccion = Convert.ToString(dr["direccion"]),
                        Telefono = Convert.ToString(dr["telefono"]),
                        Email = Convert.ToString(dr["email"]),
                        Genero = Convert.ToChar(dr["genero"]),
                        Fecha_Nacimiento = Convert.ToDateTime(dr["fecha_nacimiento"]),
                        Fecha_Registro = Convert.ToDateTime(dr["fecha_registro"]),
                        Fecha_Actualizacion = Convert.ToDateTime(dr["fecha_actualizacion"]),
                        Estado = Convert.ToBoolean(dr["estado"]),
                    };
                }
                dr.Close();
            }
            catch (Exception ex) { _error = ex.Message; }
            finally { conexion.Close(); }
            return estudiante;
        }

        public Estudiante LeerPorDni(string dni)
        {
            Estudiante estudiante = null!;
            try
            {
                conexion.Open();
                comando.CommandText = "SP_ESTUDIANTE_LEER_PORDNI";
                comando.Connection = conexion;
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@dni", dni);
                SqlDataReader dr = comando.ExecuteReader();
                while (dr.Read())
                {
                    estudiante = new Estudiante()
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Tipo_Documento = Convert.ToString(dr["tipo_documento"]),
                        Numero_Documento = Convert.ToString(dr["numero_documento"]),
                        Nombre = Convert.ToString(dr["nombre"]),
                        Apellido = Convert.ToString(dr["apellido"]),
                        Direccion = Convert.ToString(dr["direccion"]),
                        Telefono = Convert.ToString(dr["telefono"]),
                        Email = Convert.ToString(dr["email"]),
                        Genero = Convert.ToChar(dr["genero"]),
                        Fecha_Nacimiento = Convert.ToDateTime(dr["fecha_nacimiento"]),
                        Fecha_Registro = Convert.ToDateTime(dr["fecha_registro"]),
                        Fecha_Actualizacion = Convert.ToDateTime(dr["fecha_actualizacion"]),
                        Estado = Convert.ToBoolean(dr["estado"]),
                    };
                }
                dr.Close();
            }
            catch (Exception ex) { _error = ex.Message; }
            finally { conexion.Close(); }
            return estudiante;
        }

        public IEnumerable<Estudiante> Listar()
        {
            List<Estudiante> listaEstudiantes = new List<Estudiante>();
            try
            {
                conexion.Open();
                comando.CommandText = "SP_ESTUDIANTE_LISTAR";
                comando.Connection = conexion;
                comando.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = comando.ExecuteReader();
                while (dr.Read())
                {
                    listaEstudiantes.Add(new Estudiante()
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Tipo_Documento = Convert.ToString(dr["tipo_documento"]),
                        Numero_Documento = Convert.ToString(dr["numero_documento"]),
                        Nombre = Convert.ToString(dr["nombre"]),
                        Apellido = Convert.ToString(dr["apellido"]),
                        Direccion = Convert.ToString(dr["direccion"]),
                        Telefono = Convert.ToString(dr["telefono"]),
                        Email = Convert.ToString(dr["email"]),
                        Genero = Convert.ToChar(dr["genero"]),
                        Fecha_Nacimiento = Convert.ToDateTime(dr["fecha_nacimiento"]),
                        Fecha_Registro = Convert.ToDateTime(dr["fecha_registro"]),
                        Fecha_Actualizacion = Convert.ToDateTime(dr["fecha_actualizacion"]),
                        Estado = Convert.ToBoolean(dr["estado"]),
                    });
                }
                dr.Close();
            }
            catch (Exception ex) { _error = ex.Message; }
            finally { conexion.Close(); }
            return listaEstudiantes;
        }

        public IEnumerable<Estudiante> ListarMatriculados()
        {
            List<Estudiante> listaEstudiantes = new List<Estudiante>();
            try
            {
                conexion.Open();
                comando.CommandText = "SP_ESTUDIANTE_LISTAR_MATRICULADOS";
                comando.Connection = conexion;
                comando.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = comando.ExecuteReader();
                while (dr.Read())
                {
                    listaEstudiantes.Add(new Estudiante()
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Tipo_Documento = Convert.ToString(dr["tipo_documento"]),
                        Numero_Documento = Convert.ToString(dr["numero_documento"]),
                        Nombre = Convert.ToString(dr["nombre"]),
                        Apellido = Convert.ToString(dr["apellido"]),
                        Direccion = Convert.ToString(dr["direccion"]),
                        Telefono = Convert.ToString(dr["telefono"]),
                        Email = Convert.ToString(dr["email"]),
                        Genero = Convert.ToChar(dr["genero"]),
                        Fecha_Nacimiento = Convert.ToDateTime(dr["fecha_nacimiento"]),
                        Fecha_Registro = Convert.ToDateTime(dr["fecha_registro"]),
                        Fecha_Actualizacion = Convert.ToDateTime(dr["fecha_actualizacion"]),
                        Estado = Convert.ToBoolean(dr["estado"]),
                    });
                }
                dr.Close();
            }
            catch (Exception ex) { _error = ex.Message; }
            finally { conexion.Close(); }
            return listaEstudiantes;
        }

        public bool ValidaExistencia(string dni)
        {
            try
            {
                conexion.Open();
                comando.CommandText = "SP_ESTUDIANTE_VALIDA_EXISTENCIA";
                comando.Connection = conexion;
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@dni", dni);
                int r = (Int32)comando.ExecuteScalar();
                if (r == 1) return true;
            }
            catch (Exception ex) { _error = ex.Message; }
            finally { conexion.Close(); }
            return false;
        }

        public string Error()
        {
            return _error!;
        }
    }
}
