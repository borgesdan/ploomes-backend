using Microsoft.EntityFrameworkCore;
using Ploomes.Application.Data.Context;
using Ploomes.Application.Data.Entities.Sql;

namespace Ploomes.Application.Repositories
{
    public class ProductRepository : BaseRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<ProductEntity> Create(ProductEntity product)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<List<ProductEntity>> GetAll(int sellerId)
            => await _context.Products.Where(p => p.SellerId == sellerId).ToListAsync();
    }
}
