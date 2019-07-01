using System.Threading.Tasks;
using JobTest.Models;

namespace JobTest.Repositories
{
    public interface IFilmsRepository
    {
        Task<Film[]> ReadFilmsAsync();
        Task<int> ReadFilmsCountAsync();
        Task<Film[]> ReadFilmsByIndex(int index, int count);
        Task<Film[]> ReadFilmsByUserAsync(string user);
        Task<Film> ReadFilmOrDefaultAsync(int Id);
        Task CreateFilmAsync(Film film);
        Task UpdateFilmAsync(Film film);
        Task DeleteFilmAsync(int Id);
    }
}