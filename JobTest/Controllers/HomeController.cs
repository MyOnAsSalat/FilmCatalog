using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobTest.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using WebGrease.Css.Extensions;

namespace JobTest.Controllers
{
    public class HomeController : Controller
    {
        private FilmsRepository films = new FilmsRepository();
        public async Task<ActionResult> Index()
        { 
            // (await films.GetFilmsAsync()).ForEach(async x=>await films.DeleteFilmAsync(x.FilmId));
            return View(await films.GetFilmsAsync());
        }
        [Authorize]
        public async Task<ActionResult> UserFilms()
        {
            return View(await films.GetFilmsByUserAsync(User.Identity.GetUserId()));
        }
        [Authorize]
        public ActionResult EditFilm()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> SaveFilm(Film film)
        {

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
                MemoryStream target = new MemoryStream();
                image.InputStream.CopyTo(target);
                film.Poster = target.ToArray();
                film.User = User.Identity.GetUserId();
                await films.AddFilmAsync(film);
            }

            return RedirectToAction("UserFilms");
        }
        public async Task<ActionResult> Film(int FilmId)
        {
            
            return RedirectToAction("UserFilms");
        }
        public async Task<ActionResult> Poster(int FilmId)
        {
            return File((await films.GetFilmAsync(FilmId)).Poster, "image/png");
        }
    }
}