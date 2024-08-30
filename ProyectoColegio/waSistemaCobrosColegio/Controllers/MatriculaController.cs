using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using waSistemaCobrosColegio.Interfaces;
using waSistemaCobrosColegio.Models;
using waSistemaCobrosColegio.Repositorys;

namespace waSistemaCobrosColegio.Controllers
{
    public class MatriculaController : Controller
    {
        private IMatricula repoMatricula;
        private ICategoriaDetalle repoCategoriaDetalle;
        private IEstudiante repoEstudiante;
        private IApoderado repoApoderado;
        private IMatriculaDetalle repoMatriculaDetalle;
        public MatriculaController()
        {
            repoMatriculaDetalle = new RepositoryMatriculaDetalle();
            repoMatricula = new RepositoryMatricula();
            repoCategoriaDetalle = new RepositoryCategoriaDetalle();
            repoEstudiante = new RepositoryEstudiante();
            repoApoderado = new RepositoryApoderado();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Listar()
        {
            ViewBag.listaEstudiante = repoEstudiante.Listar();
            return View(repoMatricula.Listar());
        }

        [HttpGet]
        public IActionResult Crear()
        {
            ViewBag.listaPeriodo = new SelectList(repoCategoriaDetalle.Listar().Where(x => x.Id_Categoria == 104).Select(x => x.Nombre), "2024");
            ViewBag.listaNivel = new SelectList(repoCategoriaDetalle.Listar().Where(x => x.Id_Categoria == 105).Select(x => x.Nombre), "SECUNDARIA");
            ViewBag.listaGrado = new SelectList(repoCategoriaDetalle.Listar().Where(x => x.Id_Categoria == 106).Select(x => x.Nombre));
            ViewBag.listaSeccion = new SelectList(repoCategoriaDetalle.Listar().Where(x => x.Id_Categoria == 107).Select(x => x.Nombre));
            ViewBag.listaSituacion = new SelectList(repoCategoriaDetalle.Listar().Where(x => x.Id_Categoria == 108).Select(x => x.Nombre), "PROMOVIDO");
            return View(new Matricula());
        }

        [HttpPost]
        public IActionResult Crear(Matricula matricula)
        {
            ViewBag.listaPeriodo = new SelectList(repoCategoriaDetalle.Listar().Where(x => x.Id_Categoria == 104).Select(x => x.Nombre));
            ViewBag.listaNivel = new SelectList(repoCategoriaDetalle.Listar().Where(x => x.Id_Categoria == 105).Select(x => x.Nombre));
            ViewBag.listaGrado = new SelectList(repoCategoriaDetalle.Listar().Where(x => x.Id_Categoria == 106).Select(x => x.Nombre));
            ViewBag.listaSeccion = new SelectList(repoCategoriaDetalle.Listar().Where(x => x.Id_Categoria == 107).Select(x => x.Nombre));
            ViewBag.listaSituacion = new SelectList(repoCategoriaDetalle.Listar().Where(x => x.Id_Categoria == 108).Select(x => x.Nombre));

            if (ModelState.IsValid)
            {
                if (repoMatricula.ValidarExistencia(matricula.Id_Estudiante, matricula.Periodo!))
                {
                    ViewBag.mensajeError = "Ya existe una matricula con codigo estudiante";
                    return View();
                }

                if (repoMatricula.Crear(matricula))
                {
                    ViewBag.mensajeOk = "Estudiante Matriculado correctamente";
                    return View();
                }
                ViewBag.mensajeError = repoMatricula.Error();
                return View();
            }

            ViewBag.mensajeError = "Ingresar todos los datos solicitados.";
            return View();
        }

        [HttpGet]
        public IActionResult Detalle(int id)
        {
            Matricula matricula = repoMatricula.LeerPorId(id);
            ViewBag.Estudiante = repoEstudiante.LeerPorId(matricula.Id_Estudiante);
            ViewBag.Apoderado = repoApoderado.LeerPorId(matricula.Id_Apoderado);
            ViewBag.MatriculaDetalle = repoMatriculaDetalle.ListarPorIdMatricula(matricula.Id);
            return View(matricula);
        }

        [HttpGet]
        public Matricula JS_BuscarId(int id)
        {
            Matricula matricula = repoMatricula.LeerPorIdEstudiante(id);
            return matricula;
        }
    }
}
