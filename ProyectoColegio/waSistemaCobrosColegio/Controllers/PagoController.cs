using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using waSistemaCobrosColegio.Interfaces;
using waSistemaCobrosColegio.Models;
using waSistemaCobrosColegio.Repositorio;
using waSistemaCobrosColegio.Repositorys;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace waSistemaCobrosColegio.Controllers
{
    public class PagoController : Controller
    {
        private readonly IMatriculaDetalle repoMatriculaDetalle;
        private readonly ICategoriaDetalle repoCategoriaDetalle;
        private readonly IPago repoPago;
        private readonly IPagoDetalle repoPagoDetalle;
        private readonly IMatricula repoMatricula;
        private readonly IEstudiante repoEstudiante;

        public PagoController()
        {
            repoMatricula = new RepositoryMatricula();
            repoEstudiante = new RepositoryEstudiante();
            repoMatriculaDetalle = new RepositoryMatriculaDetalle();
            repoCategoriaDetalle = new RepositoryCategoriaDetalle();
            repoPago = new RepositoryPago();
            repoPagoDetalle = new RepositoryPagoDetalle();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Listar()
        {
            return View(repoPago.Listar());
        }

        [HttpGet]
        public IActionResult Crear()
        {
            ViewBag.listaPeriodo = new SelectList(repoCategoriaDetalle.Listar().Where(x => x.Id_Categoria == 109).Select(x => x.Nombre), "EFECTIVO");
            return View(new Pago());
        }

        [HttpPost]
        public string Crear(Pago pago)
        {
            if (repoPago.Crear(pago)) { return "OK"; }
            return "ERROR";
        }

        [HttpGet]
        public IActionResult Detalle(int id)
        {
            Pago pago = repoPago.LeerPorId(id);
            ViewBag.PagoDetalle = repoPagoDetalle.ListarPorIdPago(pago.Id);
            return View(pago);
        }

        [HttpGet]
        public IActionResult VerPdf(int id)
        {
            var pago = repoPago.LeerPorId(id);
            pago.Pago_Detalle = repoPagoDetalle.ListarPorIdPago(pago.Id);
            var matricula = repoMatricula.LeerPorId(pago.Id_Matricula);
            var estudiante = repoEstudiante.LeerPorId(matricula.Id_Estudiante);
            string path = generarPdf(pago, matricula, estudiante);
            return Redirect(path);
        }

        public string generarPdf(Pago pago, Matricula matricula, Estudiante estudiante)
        {
            var data = Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Margin(20);
                    page.Size(PageSizes.A4);

                    page.Header().ShowOnce().Column(colum =>
                    {
                        colum.Item().PaddingBottom(20).Row(row =>
                        {
                            var rutaImagen = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/logo_colegio_02.png");
                            byte[] imageData = System.IO.File.ReadAllBytes(rutaImagen);

                            row.RelativeItem().Column(colum =>
                            {
                                colum.Item().Height(50).Image(imageData);
                            });

                            row.RelativeItem().Column(col =>
                            {
                                col.Item().AlignCenter().Text("I.E.P. Jose Maria").Bold().FontSize(14);
                                col.Item().AlignCenter().Text("Jr. Las mercedes N378 - Lima").FontSize(9);
                                col.Item().AlignCenter().Text("987 987 123 / 02 213232").FontSize(9);
                                col.Item().AlignCenter().Text("codigo@example.com").FontSize(9);

                            });

                            row.RelativeItem().Column(col =>
                            {
                                col.Item().Border(1).BorderColor("#257272")
                                .AlignCenter().Text("RUC 21312312312");

                                col.Item().Background("#257272").Border(1)
                                .BorderColor("#257272").AlignCenter()
                                .Text("Boleta de venta").FontColor("#fff");

                                col.Item().Border(1).BorderColor("#257272").
                                AlignCenter().Text("B0001 - 234");
                            });
                        });
                        colum.Item().Border(1).Row(row =>
                        {
                            row.RelativeItem(2).Padding(10).Column(col =>
                            {
                                col.Item().Text("Codigo Alumno").Bold();
                                col.Item().Text("Nombre").Bold();
                                col.Item().Text("Periodo").Bold();
                                col.Item().Text("Nivel").Bold();
                                col.Item().Text("Grado").Bold();
                                col.Item().Text("Seccion").Bold();
                            });
                            row.RelativeItem(3).Padding(10).Column(col =>
                            {
                                col.Item().Text(estudiante.Id.ToString());
                                col.Item().Text(estudiante.Nombre_Completo);
                                col.Item().Text(matricula.Periodo);
                                col.Item().Text(matricula.Nivel);
                                col.Item().Text(matricula.Grado);
                                col.Item().Text(matricula.Seccion);
                            });
                            row.RelativeItem(3).Padding(10).Column(col =>
                            {
                                col.Item().AlignRight().Text("");
                                col.Item().AlignRight().Text("Codigo Matricula").Bold();
                                col.Item().AlignRight().Text("Codigo pago").Bold();
                                col.Item().AlignRight().Text("Metodo").Bold();
                                col.Item().AlignRight().Text("OP").Bold();
                                col.Item().AlignRight().Text("Fecha").Bold();
                            });
                            row.RelativeItem(2).Padding(10).Column(col =>
                            {
                                col.Item().AlignLeft().Text("");
                                col.Item().AlignLeft().Text(pago.Id_Matricula.ToString());
                                col.Item().AlignLeft().Text(pago.Id.ToString());
                                col.Item().AlignLeft().Text(pago.Metodo_Pago);
                                col.Item().AlignLeft().Text(pago.Numero_Op);
                                col.Item().AlignLeft().Text(pago.Fecha_Registro.ToString("d"));
                            });
                        });
                    });
                    page.Content().PaddingVertical(10).Column(col1 =>
                    {
                        col1.Item().Table(tabla =>
                        {
                            tabla.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(8);
                                columns.RelativeColumn(1);
                            });

                            tabla.Header(header =>
                            {
                                header.Cell().Background("#257272").Padding(2).Text("Item").FontColor("#fff");
                                header.Cell().Background("#257272").Padding(2).Text("Concepto").FontColor("#fff");
                                header.Cell().Background("#257272").Padding(2).Text("Total").FontColor("#fff");
                            });

                            var item = 0;
                            foreach (PagoDetalle pd in pago.Pago_Detalle!)
                            {
                                item++;
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(item.ToString()).FontSize(10);
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(pd.Concepto).FontSize(10);
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(pd.Monto.ToString()).FontSize(10);
                            }
                        });

                        col1.Item().AlignRight().Text("Total (S/.) :   " + pago.Monto_Total).FontSize(12).BackgroundColor("#FFFC00");

                        if (1 == 1)
                            col1.Item().Background(Colors.Grey.Lighten3).Padding(10)
                            .Column(column =>
                            {
                                column.Item().Text("Comentarios").FontSize(14);
                                column.Item().Text(Placeholders.LoremIpsum());
                                column.Spacing(5);
                            });

                        col1.Spacing(10);
                    });
                    page.Footer().AlignRight().Text(txt =>
                    {
                        txt.Span("Pagina ").FontSize(10);
                        txt.CurrentPageNumber().FontSize(10);
                        txt.Span(" de ").FontSize(10);
                        txt.TotalPages().FontSize(10);
                    });
                });
            }).GeneratePdf();
            string name = pago.Id.ToString() + ".pdf";
            var stream = new MemoryStream(data);
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\pdf", name);
            using (var file = new FileStream(path, FileMode.Create)) { stream.CopyTo(file); }
            //return Content("<script>window.open('{/pdf/mongodb.pdf}','_blank')</script>");
            //return Redirect("/pdf/mongodb.pdf");
            //return File(stream, "application/pdf", "report.pdf");
            return "/pdf/" + name;
        }
    }
}
