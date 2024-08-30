using waSistemaCobrosColegio.Models;

namespace waSistemaCobrosColegio.Interfaces
{
    public interface IPagoDetalle
    {
        string Error();
        IEnumerable<PagoDetalle> Listar();
        IEnumerable<PagoDetalle> ListarPorIdPago(int id);
    }
}
