using System.Data.Entity;
using JobTest.Models;

namespace JobTest.DatabaseContexts
{
    public class FilmsContext : DbContext
    {
        public FilmsContext()
            : base("name=LocalDbConnection")
        {
        }

        public virtual DbSet<Film> Films { get; set; }
    }
}