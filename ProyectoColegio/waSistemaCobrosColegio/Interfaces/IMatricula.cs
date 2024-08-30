using waSistemaCobrosColegio.Models;

namespace waSistemaCobrosColegio.Interfaces
{
    public interface IMatricula
    {
        string Error();
        IEnumerable<Matricula> Listar();
        Matricula LeerPorId(int id);
        Matricula LeerPorIdEstudiante(int id);
        bool Crear(Matricula m);
        bool Actualizar(Matricula m);
        bool EliminarPorId(int id);
        bool ValidarExistencia(int id_estudiante, string periodo);
    }
}
