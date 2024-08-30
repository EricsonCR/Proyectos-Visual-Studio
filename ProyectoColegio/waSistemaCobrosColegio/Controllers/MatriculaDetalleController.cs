using Microsoft.AspNetCore.Mvc;
using waSistemaCobrosColegio.Interfaces;
using waSistemaCobrosColegio.Models;
using waSistemaCobrosColegio.Repositorys;

namespace waSistemaCobrosColegio.Controllers
{
    public class MatriculaDetalleController : Controller
    {
        private readonly IMatriculaDetalle repoMatriculaDetalle;

        public MatriculaDetalleController()
        {
            repoMatriculaDetalle = new RepositoryMatriculaDetalle();
        }
        public IActionResult Index() { return View(); }

        [HttpGet]
        public PartialViewResult _ListarIdMatricula(int id)
        {
            var lista = repoMatriculaDetalle.ListarPorIdMatriculaPendiente(id);
            return PartialView("_MatriculaDetallePartial", lista);
        }
    }
}
