using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using JobTest.DatabaseContexts;
using JobTest.Models;

namespace JobTest.Repositories
{
    public class FilmsRepository : IFilmsRepository
    {
        public async Task<Film[]> ReadFilmsAsync()
        {
            var result = new Film[0];
            using (var context = new FilmsContext())
            {
                result = await context.Films.ToArrayAsync();
            }

            return result;
        }

        public async Task<int> ReadFilmsCountAsync()
        {
            var result = 0;
            using (var context = new FilmsContext())
            {
                result = await context.Films.CountAsync();
            }

            return result;
        }

        public async Task<Film[]> ReadFilmsByIndex(int index, int count)
        {
            Film[] result;
            using (var context = new FilmsContext())
            {
                result = await context.Films.OrderBy(f => f.FilmId).Skip(index).Take(count).ToArrayAsync();
            }

            return result;
        }

        public async Task<Film[]> ReadFilmsByUserAsync(string user)
        {
            Film[] result;
            using (var context = new FilmsContext())
            {
                result = await context.Films.Where(f => f.User == user).ToArrayAsync();
            }

            return result;
        }

        public async Task<Film> ReadFilmOrDefaultAsync(int Id)
        {
            Film result;
            using (var context = new FilmsContext())
            {
                result = await context.Films.FirstOrDefaultAsync(f => f.FilmId == Id);
            }

            return result;
        }

        public async Task CreateFilmAsync(Film film)
        {
            using (var context = new FilmsContext())
            {
                context.Films.Add(film);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateFilmAsync(Film film)
        {
            using (var context = new FilmsContext())
            {
                context.Entry(film).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteFilmAsync(int Id)
        {
            using (var context = new FilmsContext())
            {
                var film = await context.Films.FirstOrDefaultAsync(f => f.FilmId == Id);
                context.Entry(film).State = EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }
    }
}