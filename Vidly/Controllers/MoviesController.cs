using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Movies
        public ActionResult Index()
        {
            var movies = GetMovies();

            return View(movies);
        }
        public ActionResult Random()
        {
            var customers = new List<Customer>
            {
                new Customer {Name = "Customer 1"},
                new Customer {Name = "Customer 2"}
            };

            var movie = new Movie()
            {
                Name = "Shrek"
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
            // return View(movie);


            // return Content("Hello");
            // return HttpNotFound();
            // return new EmptyResult();
            // return RedirectToAction("index", "Home", new { page = 1, sortBy = "name" });
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1, 12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

        private IEnumerable<Movie> GetMovies()
        {
            var movies = _context.Movies.Include(m => m.Genre).ToList();

            return movies;

            //return new List<Movie>
            //{
            //    new Movie {Id = 1, Name = "Shrek"},
            //    new Movie {Id = 2, Name = "Top Gun"},
            //    new Movie {Id = 3, Name = "Spiderman"},
            //    new Movie {Id = 4, Name = "Avengers"},
            //    new Movie {Id = 5, Name = "Toy Story"},
            //    new Movie {Id = 6, Name = "Unknown Movie"},
            //};
        }
    }
}