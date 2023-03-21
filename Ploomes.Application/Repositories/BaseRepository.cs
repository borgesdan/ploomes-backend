using Ploomes.Application.Data.Context;

namespace Ploomes.Application.Repositories
{
    public class BaseRepository
    {
        protected readonly AppDbContext _context;

        public AppDbContext Context => _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}
