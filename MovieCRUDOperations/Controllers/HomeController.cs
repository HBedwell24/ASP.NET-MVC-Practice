using MovieCRUDOperations.Models;
using System.Linq;
using System.Web.Mvc;

namespace MovieCRUDOperations.Controllers
{
    public class HomeController : Controller
    {
        private MoviesDBEntities _db = new MoviesDBEntities();

        // GET: Home
        public ActionResult Index()
        {
            return View(_db.Movies.ToList());
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] Movie movieToCreate)
        {
            if (!ModelState.IsValid)
                return View();

            _db.Movies.Add(movieToCreate);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            var movieToEdit = (from m in _db.Movies
                               where m.Id == id
                               select m).First();

            return View(movieToEdit);
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(Movie movieToEdit)
        {
            var originalMovie = (from m in _db.Movies
                                 where m.Id == movieToEdit.Id
                                 select m).First();

            if (!ModelState.IsValid)
                return View(originalMovie);

            _db.Entry(originalMovie).CurrentValues.SetValues(movieToEdit);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            var movieToDelete = (from m in _db.Movies
                               where m.Id == id
                               select m).First();

            return View(movieToDelete);
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(Movie movieToDelete)
        {
            var originalMovie = (from m in _db.Movies
                                 where m.Id == movieToDelete.Id
                                 select m).First();

            if (!ModelState.IsValid)
                return View(originalMovie);

            _db.Movies.Remove(originalMovie);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
