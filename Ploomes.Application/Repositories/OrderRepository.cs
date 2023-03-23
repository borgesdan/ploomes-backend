using Microsoft.EntityFrameworkCore;
using Ploomes.Application.Data.Context;
using Ploomes.Application.Data.Entities.Sql;

namespace Ploomes.Application.Repositories
{
    public class OrderRepository : BaseRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<OrderEntity> Create(OrderEntity order)
        {
            _context.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<IEnumerable<OrderEntity>> GetAllByBuyerIdAsync(string buyerId)
            => await _context.Orders.Where(o => o.BuyerUid == new Guid(buyerId)).ToListAsync();
    }
}
