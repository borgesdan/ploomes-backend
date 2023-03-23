using Microsoft.EntityFrameworkCore;
using Ploomes.Application.Data.Context;
using Ploomes.Application.Data.Entities.Sql;
using Ploomes.Application.Data.Shared;

namespace Ploomes.Application.Repositories
{
    public class ProductRepository : BaseRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<ProductEntity>> GetAllAsync(int page, int pageSize)
            => await _context.Products
            .OrderBy(s => s.Title)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        public async Task<ProductEntity> CreateAsync(ProductEntity product)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<ProductEntity> UpdateAsync(ProductEntity product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<List<ProductEntity>> GetAllAsync(int sellerId)
            => await _context.Products.Where(p => p.SellerId == sellerId && p.Status == EntityStatus.Active).ToListAsync();

        public async Task<ProductEntity?> GetByUidAsync(string uid)
            => await _context.Products.Where(p => p.Uid == new Guid(uid)).FirstOrDefaultAsync();
    }
}
