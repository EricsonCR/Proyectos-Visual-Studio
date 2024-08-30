using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using waSistemaCobrosColegio.Interfaces;
using waSistemaCobrosColegio.Models;
using waSistemaCobrosColegio.Repositorys;

namespace waSistemaCobrosColegio.Controllers
{
    public class ApoderadoController : Controller
    {
        private IApoderado repoApoderado;
        private ICategoriaDetalle repoCategoriaDetalle;
        public ApoderadoController()
        {
            repoApoderado = new RepositoryApoderado();
            repoCategoriaDetalle = new RepositoryCategoriaDetalle();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Listar()
        {
            return View(repoApoderado.Listar());
        }

        [HttpGet]
        public IActionResult Crear()
        {
            ViewBag.listaTiposDocumentos = new SelectList(repoCategoriaDetalle.Listar().Where(x => x.Id_Categoria == 100).Select(x => x.Nombre));
            ViewBag.listaRelacion = new SelectList(repoCategoriaDetalle.Listar().Where(x => x.Id_Categoria == 103).Select(x => x.Nombre));
            return View(new Apoderado());
        }

        [HttpPost]
        public IActionResult Crear(Apoderado apoderado)
        {
            repoApoderado.Crear(apoderado);
            return RedirectToAction("Listar");
        }

        [HttpGet]
        public Apoderado LeerApoderadoPorDni(string dni)
        {
            return repoApoderado.LeerPorDni(dni);
        }

        [HttpGet]
        public PartialViewResult ListarApoderados()
        {
            return PartialView("_ListaApoderadosPartial", repoApoderado.Listar());
        }
    }
}
