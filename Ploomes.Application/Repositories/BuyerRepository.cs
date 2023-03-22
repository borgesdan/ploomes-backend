using Ploomes.Application.Data.Context;

namespace Ploomes.Application.Repositories
{
    public class BuyerRepository : BaseRepository
    {
        public BuyerRepository(AppDbContext context) : base(context)
        {
        }
    }
}