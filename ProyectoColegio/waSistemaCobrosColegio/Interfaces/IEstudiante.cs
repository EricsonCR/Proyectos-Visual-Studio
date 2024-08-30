using waSistemaCobrosColegio.Models;

namespace waSistemaCobrosColegio.Interfaces
{
    public interface IEstudiante
    {
        string Error();
        IEnumerable<Estudiante> Listar();
        IEnumerable<Estudiante> ListarMatriculados();
        Estudiante LeerPorId(int id);
        Estudiante LeerPorDni(string dni);
        bool Crear(Estudiante m);
        bool Actualizar(Estudiante m);
        bool EliminarPorId(int id);
        bool ValidaExistencia(string dni);
    }
}
