using waSistemaCobrosColegio.Models;

namespace waSistemaCobrosColegio.Interfaces
{
    public interface ICategoriaDetalle
    {
        IEnumerable<CategoriaDetalle> Listar();
        CategoriaDetalle LeerPorId();
        bool Crear(CategoriaDetalle m);
        bool Actualizar(CategoriaDetalle m);

        bool EliminarPorId(int id);
    }
}
