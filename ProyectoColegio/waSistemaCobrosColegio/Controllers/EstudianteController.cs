using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Core.Types;
using waSistemaCobrosColegio.Interfaces;
using waSistemaCobrosColegio.Models;
using waSistemaCobrosColegio.Repositorys;

namespace waSistemaCobrosColegio.Controllers
{
    public class EstudianteController : Controller
    {
        private IEstudiante repoEstudiante;
        private ICategoriaDetalle repoCategoriaDetalle;
        public EstudianteController()
        {
            repoEstudiante = new RepositoryEstudiante();
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
            return View(repoEstudiante.Listar());
        }

        [HttpGet]
        public IActionResult Crear()
        {
            ViewBag.listaTiposDocumentos = new SelectList(repoCategoriaDetalle.Listar().Where(x => x.Id_Categoria == 100).Select(x => x.Nombre));
            ViewBag.listaGeneros = new SelectList(repoCategoriaDetalle.Listar().Where(x => x.Id_Categoria == 102).Select(x => x.Nombre));
            return View(new Estudiante());
        }

        [HttpPost]
        public IActionResult Crear(Estudiante estudiante)
        {
            ViewBag.listaTiposDocumentos = new SelectList(repoCategoriaDetalle.Listar().Where(x => x.Id_Categoria == 100).Select(x => x.Nombre));
            ViewBag.listaGeneros = new SelectList(repoCategoriaDetalle.Listar().Where(x => x.Id_Categoria == 102).Select(x => x.Nombre));
            if (ModelState.IsValid)
            {
                if (repoEstudiante.ValidaExistencia(estudiante.Numero_Documento!))
                {
                    ViewBag.mensajeError = "Ya existe un estudiante con mismo DNI, agregue uno Nuevo";
                    return View(estudiante);
                }
                if (repoEstudiante.Crear(estudiante))
                {
                    ViewBag.mensajeOk = "Estudiante creado correctamente";
                    return View();
                }
                ViewBag.mensajeError = repoEstudiante.Error();
                return View();
            }
            ViewBag.mensajeError = "Debe ingresar todos los datos";
            return View(estudiante);
        }

        [HttpGet]
        public Estudiante LeerEstudiantePorDni(string dni)
        {
            return repoEstudiante.LeerPorDni(dni);
        }

        [HttpGet]
        public PartialViewResult ListarEstudiantes()
        {
            return PartialView("_ListaEstudiantesPartial", repoEstudiante.Listar());
        }

        [HttpGet]
        public PartialViewResult ListarMatriculados()
        {
            var lista = repoEstudiante.ListarMatriculados();
            return PartialView("_ListaEstudiantesPartial", lista);
        }
    }
}
