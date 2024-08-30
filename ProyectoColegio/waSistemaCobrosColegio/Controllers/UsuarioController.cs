using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using waSistemaCobrosColegio.Interfaces;
using waSistemaCobrosColegio.Models;
using waSistemaCobrosColegio.Repositorys;

namespace waSistemaCobrosColegio.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuario repoUsuario;
        private readonly ICategoriaDetalle repoCategoriaDetalle;

        public UsuarioController()
        {
            repoUsuario = new RepositoryUsuario();
            repoCategoriaDetalle = new RepositoryCategoriaDetalle();
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Listar()
        {
            return View(repoUsuario.Listar());
        }

        [HttpGet]
        public IActionResult Crear()
        {
            ViewBag.listaTipoDocumento = new SelectList(repoCategoriaDetalle.Listar().Where(x => x.Id_Categoria == 100).Select(x => x.Nombre));
            ViewBag.listaRol = new SelectList(repoCategoriaDetalle.Listar().Where(x => x.Id_Categoria == 101).Select(x => x.Nombre));
            ViewBag.listaGenero = new SelectList(repoCategoriaDetalle.Listar().Where(x => x.Id_Categoria == 102).Select(x => x.Nombre));
            return View(new Usuario());
        }

        [HttpPost]
        public IActionResult Crear(Usuario usuario)
        {
            repoUsuario.Crear(usuario);
            return RedirectToAction("Listar");
        }
    }
}
