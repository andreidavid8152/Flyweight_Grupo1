using Flyweight_Grupo1.Data;
using Flyweight_Grupo1.Flyweight;
using Flyweight_Grupo1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Flyweight_Grupo1.Controllers
{
    public class FlyweightController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly AutorFlyweightFactory _autorFactory;
        private readonly GeneroFlyweightFactory _generoFactory;

        public FlyweightController(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _autorFactory = new AutorFlyweightFactory();
            _generoFactory = new GeneroFlyweightFactory();
        }

        public IActionResult Index()
        {
            var libros = _context.Libros.ToList();
            var librosViewModel = new List<LibroViewModel>();

            foreach (var libro in libros)
            {
                var autor = _autorFactory.GetAutor(libro.AutorId, _context);
                var genero = _generoFactory.GetGenero(libro.GeneroId, _context);

                var libroViewModel = new LibroViewModel
                {
                    Titulo = libro.Titulo,
                    Autor = autor?.Nombre,
                    Genero = genero?.Descripcion
                };

                librosViewModel.Add(libroViewModel);
            }

            // Mostrar la eficiencia del patrón Flyweight
            ViewBag.TotalLibros = _context.Libros.Count();
            ViewBag.TotalAutoresUnicos = _autorFactory.CacheSize;
            ViewBag.TotalGenerosUnicos = _generoFactory.CacheSize;

            return View(librosViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CargarAutoresYGeneros();
            return View(new Libro());
        }

        [HttpPost]
        public IActionResult Create(Libro libro)
        {
            ModelState.Remove("Autor");
            ModelState.Remove("Genero");

            if (ModelState.IsValid)
            {
                _context.Libros.Add(libro);
                _context.SaveChanges();

                // Cargar explícitamente los objetos Autor y Genero basados en los IDs
                libro.Autor = _context.Autores.Find(libro.AutorId);
                libro.Genero = _context.Generos.Find(libro.GeneroId);

                return RedirectToAction(nameof(Index));
            }

            CargarAutoresYGeneros(); // Recargar en caso de fallo
            return View(libro);


        }

        private void CargarAutoresYGeneros()
        {
            if (!_context.Autores.Any())
            {
                var autoresPredeterminados = new Autor[]
                {
                    new Autor { Nombre = "J. K. Rowling" },
                    new Autor { Nombre = "George Orwell" },
                    // Agrega más autores predeterminados si lo deseas
                };

                _context.Autores.AddRange(autoresPredeterminados);
                _context.SaveChanges();
            }

            if (!_context.Generos.Any())
            {
                var generosPredeterminados = new Genero[]
                {
                    new Genero { Descripcion = "Ciencia ficcion" },
                    new Genero { Descripcion = "Distopía" },
                    // Agrega más géneros predeterminados si lo deseas
                };

                _context.Generos.AddRange(generosPredeterminados);
                _context.SaveChanges();
            }

            ViewBag.Autores = _context.Autores.ToList();
            ViewBag.Generos = _context.Generos.ToList();
        }
    }
}
