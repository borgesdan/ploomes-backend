using Ploomes.Application.Data.Context;

namespace Ploomes.Application.Repositories
{
    public class SellerRepository : BaseRepository
    {
        public SellerRepository(AppDbContext context) : base(context)
        {
        }
    }
}
