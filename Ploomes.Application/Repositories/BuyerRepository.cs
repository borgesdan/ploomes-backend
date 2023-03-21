using Ploomes.Application.Data.Context;
using Ploomes.Application.Data.Entities.Sql;
using System.Runtime.CompilerServices;

namespace Ploomes.Application.Repositories
{
    public class BuyerRepository : BaseRepository
    {
        public BuyerRepository(AppDbContext context) : base(context)
        {
        }        
    }
}
