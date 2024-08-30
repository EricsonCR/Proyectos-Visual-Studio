using waSistemaCobrosColegio.Models;

namespace waSistemaCobrosColegio.Interfaces
{
    public interface IApoderado
    {
        IEnumerable<Apoderado> Listar();
        Apoderado LeerPorId(int id);
        Apoderado LeerPorDni(string dni);
        bool Crear(Apoderado m);
        bool Actualizar(Apoderado m);

        bool EliminarPorId(int id);
    }
}
