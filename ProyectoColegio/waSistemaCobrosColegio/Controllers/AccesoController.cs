using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Core.Types;
using waSistemaCobrosColegio.Interfaces;
using waSistemaCobrosColegio.Models;
using waSistemaCobrosColegio.Repositorys;

namespace waSistemaCobrosColegio.Controllers
{
    public class AccesoController : Controller
    {
        private readonly IUsuario repoUsuario;

        public AccesoController()
        {
            repoUsuario = new RepositoryUsuario();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login() { return View(); }

        [HttpPost]
        public IActionResult Login(string email, string clave)
        {
            //if (email == "" || clave == "")
            //{
            //    ViewBag.mensajeError = "Debe ingresar email o contraseña";
            //    return View();
            //}
            //Usuario user = repoUsuario.Autenticar(new Usuario() { Email = email, Clave = clave });
            //if (user == null)
            //{
            //    ViewBag.mensajeError = "Email o Contraseña NO validos.";
            //    return View();
            //}
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Registrar() { return View(new Usuario()); }

        [HttpPost]
        public IActionResult Registrar(Usuario usuario)
        {
            if (!ModelState.IsValid) { return View(); }
            repoUsuario.Registrar(usuario);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            return RedirectToAction("Login");
        }
    }
}
