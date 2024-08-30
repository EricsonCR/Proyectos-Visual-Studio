using waSistemaCobrosColegio.Models;

namespace waSistemaCobrosColegio.Interfaces
{
    public interface IMatriculaDetalle
    {
        IEnumerable<MatriculaDetalle> Listar();
        IEnumerable<MatriculaDetalle> ListarPorDniEstudiante(string dni);
        IEnumerable<MatriculaDetalle> ListarPorIdMatricula(int id);
        IEnumerable<MatriculaDetalle> ListarPorIdMatriculaPendiente(int id);
        MatriculaDetalle LeerPorId(int id);
        bool Actualizar(MatriculaDetalle m);
    }
}
