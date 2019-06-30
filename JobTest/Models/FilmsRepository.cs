using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace JobTest.Models
{
    public class FilmsRepository
    {
        public async Task<List<Film>> GetFilmsAsync()
        {
            using (var context = new FilmsContext())
            {
                return await context.Films.ToListAsync();
            }
        }

        public async Task<int> GetFilmsCountAsync()
        {
            using (var context = new FilmsContext())
            {
                return await context.Films.CountAsync();
            }
        }
        public async Task<List<Film>> GetFilmsByIndex(int index,int count)
        {
            using (var context = new FilmsContext())
            {
                return await context.Films.OrderBy(f=>f.FilmId).Skip(index).Take(count).ToListAsync();
            }
        }
        public async Task<List<Film>> GetFilmsByUserAsync(string user)
        {
            using (var context = new FilmsContext())
            {
                return await context.Films.Where(f => f.User == user).ToListAsync();
            }
        }

        public async Task<Film> GetFilmAsync(int Id)
        {
            using (var context = new FilmsContext())
            {
                return await context.Films.FirstOrDefaultAsync(f => f.FilmId == Id);
            }
        }

        public async Task AddFilmAsync(Film film)
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