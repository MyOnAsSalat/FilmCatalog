using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using JobTest.Models;
using JobTest.Repositories;
using Microsoft.AspNet.Identity;

namespace JobTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFilmsRepository films;

        public HomeController(IFilmsRepository r)
        {
            films = r;
        }

        public async Task<ActionResult> Index(int index = 0, int count = 10)
        {
            ViewBag.FilmsCount = await films.ReadFilmsCountAsync();
            ViewBag.Index = index;
            ViewBag.Count = count;
            return View(await films.ReadFilmsByIndex(index, count));
        }

        [Authorize]
        public async Task<ActionResult> UserFilms()
        {
            return View(await films.ReadFilmsByUserAsync(User.Identity.GetUserId()));
        }

        [Authorize]
        public async Task<ActionResult> EditFilm(int? FilmId)
        {
            if (FilmId == null) return RedirectToAction("Index");
            var film = await films.ReadFilmOrDefaultAsync((int) FilmId);
            if (User.Identity.GetUserId() != film.User) return RedirectToAction("Index");
            ViewBag.UserFilmId = (int) FilmId;
            return View(film);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> SaveFilm(int? UserFilmId, Film film, HttpPostedFileBase image)
        {
            if (UserFilmId == null) return RedirectToAction("UserFilms");
            var filmById = await films.ReadFilmOrDefaultAsync((int) UserFilmId);
            if (filmById == null) return RedirectToAction("UserFilms");
            if (User.Identity.GetUserId() != filmById.User)
                return RedirectToAction("UserFilms");
            film.FilmId = filmById.FilmId;
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var target = new MemoryStream();
                    image.InputStream.CopyTo(target);
                    film.Poster = target.ToArray();
                }
                else
                {
                    film.Poster = filmById.Poster;
                }

                film.User = User.Identity.GetUserId();
                await films.UpdateFilmAsync(film);
            }

            return RedirectToAction("UserFilms");
        }

        [Authorize]
        public ActionResult NewFilm()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddFilm(Film film, HttpPostedFileBase image)
        {
            if (ModelState.IsValid && image != null)
            {
                var target = new MemoryStream();
                image.InputStream.CopyTo(target);
                film.Poster = target.ToArray();
                film.User = User.Identity.GetUserId();
                await films.CreateFilmAsync(film);
            }

            return RedirectToAction("UserFilms");
        }

        [Authorize]
        public async Task<ActionResult> DeleteFilm(int? FilmId)
        {
            if (FilmId == null) return RedirectToAction("UserFilms");
            var film = await films.ReadFilmOrDefaultAsync((int) FilmId);
            if (film.User != User.Identity.GetUserId()) return RedirectToAction("UserFilms");
            await films.DeleteFilmAsync((int) FilmId);
            return RedirectToAction("UserFilms");
        }

        public async Task<ActionResult> Film(int? FilmId)
        {
            if (FilmId == null) return RedirectToAction("Index");
            var film = await films.ReadFilmOrDefaultAsync((int) FilmId);
            return View(film);
        }

        public async Task<ActionResult> Poster(int? FilmId)
        {
            if (FilmId == null) return HttpNotFound();
            return File((await films.ReadFilmOrDefaultAsync((int) FilmId)).Poster, "image/png");
        }
    }
}