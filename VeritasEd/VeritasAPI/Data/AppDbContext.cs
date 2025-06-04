using Microsoft.EntityFrameworkCore;
using VeritasEd.Api.Models;

namespace VeritasEd.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Grade> Grades => Set<Grade>();
    }
}