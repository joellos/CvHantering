using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore;
using CvHantering.Models;

namespace CvHantering.Data
{
    public class CvDbContext : DbContext
    {
        public CvDbContext(DbContextOptions<CvDbContext> options) : base(options)
        {
            

        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Experience> Experiences { get; set; }

    }
}
