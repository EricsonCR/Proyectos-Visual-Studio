using waSistemaCobrosColegio.Models;

namespace waSistemaCobrosColegio.Interfaces
{
    public interface IPago
    {
        IEnumerable<Pago> Listar();
        Pago LeerPorId(int id);
        bool Crear(Pago p);
        bool Actualizar(Pago p);
        bool EliminarPorId(int id);
    }
}
