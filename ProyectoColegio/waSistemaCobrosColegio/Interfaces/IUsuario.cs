using waSistemaCobrosColegio.Models;

namespace waSistemaCobrosColegio.Interfaces
{
    public interface IUsuario
    {
        IEnumerable<Usuario> Listar();
        Usuario LeerPorId(int id);
        bool Crear(Usuario m);
        bool Actualizar(Usuario m);
        bool EliminarPorId(int id);
        Usuario Autenticar(Usuario m);
        bool Registrar(Usuario m);
        bool ValidarExistencia(Usuario m);
    }
}
