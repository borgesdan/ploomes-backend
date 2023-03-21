using Ploomes.Application.Data.Context;
using Ploomes.Application.Data.Entities.Sql;

namespace Ploomes.Application.Repositories
{
    public class PersonRepository : BaseRepository
    {
        public PersonRepository(AppDbContext context) : base(context) { }        
    }
}
