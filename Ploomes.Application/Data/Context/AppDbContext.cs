using Microsoft.EntityFrameworkCore;
using Ploomes.Application.Data.Entities.Sql;

namespace Ploomes.Application.Data.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<PersonEntity> Persons { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
