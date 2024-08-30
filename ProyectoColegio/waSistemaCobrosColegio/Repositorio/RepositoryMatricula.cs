using Microsoft.Data.SqlClient;
using waSistemaCobrosColegio.Interfaces;
using waSistemaCobrosColegio.Models;
using System.Data;
using waSistemaCobrosColegio.Database;

namespace waSistemaCobrosColegio.Repositorys
{
    public class RepositoryMatricula : IMatricula
    {
        private string? _error { get; set; }
        private readonly SqlConnection conexion;
        private readonly SqlCommand comando;
        public RepositoryMatricula()
        {
            conexion = Conexion.GetInstancia().CrearConexion();
            comando = new SqlCommand();
        }

        public bool Actualizar(Matricula m)
        {
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "SP_MATRICULA_ACTUALIZAR";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@id", m.Id);
                comando.Parameters.AddWithValue("@id_estudiante", m.Id_Estudiante);
                comando.Parameters.AddWithValue("@id_apoderado", m.Id_Apoderado);
                comando.Parameters.AddWithValue("@periodo", m.Periodo);
                comando.Parameters.AddWithValue("@nivel", m.Nivel);
                comando.Parameters.AddWithValue("@grado", m.Grado);
                comando.Parameters.AddWithValue("@seccion", m.Seccion);
                comando.Parameters.AddWithValue("@situacion", m.Situacion);
                comando.Parameters.AddWithValue("@descripcion", m.Descripcion);
                comando.Parameters.AddWithValue("@fecha_registro", m.Fecha_Registro);
                comando.Parameters.AddWithValue("@fecha_actualizacion", m.Fecha_Actualizacion);
                comando.Parameters.AddWithValue("@estado", m.Estado);
                int r = comando.ExecuteNonQuery();
                if (r > 0) return true;
            }
            catch (Exception ex) { _error = ex.Message; }
            finally { conexion.Close(); }
            return false;
        }

        public bool Crear(Matricula m)
        {
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "SP_MATRICULA_CREAR";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@id_estudiante", m.Id_Estudiante);
                comando.Parameters.AddWithValue("@id_apoderado", m.Id_Apoderado);
                comando.Parameters.AddWithValue("@periodo", m.Periodo);
                comando.Parameters.AddWithValue("@nivel", m.Nivel);
                comando.Parameters.AddWithValue("@grado", m.Grado);
                comando.Parameters.AddWithValue("@seccion", m.Seccion);
                comando.Parameters.AddWithValue("@situacion", m.Situacion);
                comando.Parameters.AddWithValue("@descripcion", m.Descripcion);
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
                comando.Connection = conexion;
                comando.CommandText = "SP_MATRICULA_ELIMINAR_PORID";
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

        public Matricula LeerPorId(int id)
        {
            Matricula matricula = null!;
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "SP_MATRICULA_LEER_PORID";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = comando.ExecuteReader();
                if (dr.Read())
                {
                    matricula = new Matricula()
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Id_Estudiante = Convert.ToInt32(dr["id_estudiante"]),
                        Id_Apoderado = Convert.ToInt32(dr["id_apoderado"]),
                        Periodo = Convert.ToString(dr["periodo"]),
                        Nivel = Convert.ToString(dr["nivel"]),
                        Grado = Convert.ToString(dr["grado"]),
                        Seccion = Convert.ToString(dr["seccion"]),
                        Situacion = Convert.ToString(dr["situacion"]),
                        Descripcion = Convert.ToString(dr["descripcion"]),
                        Fecha_Registro = Convert.ToDateTime(dr["fecha_registro"]),
                        Fecha_Actualizacion = Convert.ToDateTime(dr["fecha_actualizacion"]),
                        Estado = Convert.ToString(dr["estado"])
                    };
                }
                dr.Close();
            }
            catch (Exception ex) { _error = ex.Message; }
            finally { conexion.Close(); }
            return matricula;
        }

        public IEnumerable<Matricula> Listar()
        {
            List<Matricula> listaMatriculas = new List<Matricula>();
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "SP_MATRICULA_LISTAR";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                SqlDataReader dr = comando.ExecuteReader();
                while (dr.Read())
                {
                    listaMatriculas.Add(new Matricula()
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Id_Estudiante = Convert.ToInt32(dr["id_estudiante"]),
                        Id_Apoderado = Convert.ToInt32(dr["id_apoderado"]),
                        Periodo = Convert.ToString(dr["periodo"]),
                        Nivel = Convert.ToString(dr["nivel"]),
                        Grado = Convert.ToString(dr["grado"]),
                        Seccion = Convert.ToString(dr["seccion"]),
                        Situacion = Convert.ToString(dr["situacion"]),
                        Descripcion = Convert.ToString(dr["descripcion"]),
                        Fecha_Registro = Convert.ToDateTime(dr["fecha_registro"]),
                        Fecha_Actualizacion = Convert.ToDateTime(dr["fecha_actualizacion"]),
                        Estado = Convert.ToString(dr["estado"])
                    });
                }
                dr.Close();
            }
            catch (Exception ex) { _error = ex.Message; }
            finally { conexion.Close(); }
            return listaMatriculas;
        }

        public bool ValidarExistencia(int id_estudiante, string periodo)
        {
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "SP_MATRICULA_VALIDAEXISTENCIA";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@id_estudiante", id_estudiante);
                comando.Parameters.AddWithValue("@periodo", periodo);
                int r = Convert.ToInt32(comando.ExecuteScalar());
                if (r > 0) return true;
            }
            catch (Exception ex) { _error = ex.Message; }
            finally { conexion.Close(); }
            return false;
        }

        public Matricula LeerPorIdEstudiante(int id)
        {
            Matricula matricula = null!;
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "SP_MATRICULA_LEER_PORIDESTUDIANTE";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@id_estudiante", id);
                SqlDataReader dr = comando.ExecuteReader();
                if (dr.Read())
                {
                    matricula = new Matricula()
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Id_Estudiante = Convert.ToInt32(dr["id_estudiante"]),
                        Id_Apoderado = Convert.ToInt32(dr["id_apoderado"]),
                        Periodo = Convert.ToString(dr["periodo"]),
                        Nivel = Convert.ToString(dr["nivel"]),
                        Grado = Convert.ToString(dr["grado"]),
                        Seccion = Convert.ToString(dr["seccion"]),
                        Situacion = Convert.ToString(dr["situacion"]),
                        Descripcion = Convert.ToString(dr["descripcion"]),
                        Fecha_Registro = Convert.ToDateTime(dr["fecha_registro"]),
                        Fecha_Actualizacion = Convert.ToDateTime(dr["fecha_actualizacion"]),
                        Estado = Convert.ToString(dr["estado"])
                    };
                }
                dr.Close();
            }
            catch (Exception ex) { _error = ex.Message; }
            finally { conexion.Close(); }
            return matricula;
        }

        string IMatricula.Error()
        {
            return _error!;
        }
    }
}
