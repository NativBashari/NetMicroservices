using Microsoft.EntityFrameworkCore;
using Orderingg.Application.Contracts.Persistence;
using Orderingg.Domain.Entities;
using Orderingg.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orderingg.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext dbContext): base(dbContext)
        {
        }
        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
            var orderList = await _dbContext.Orders
                        .Where(o => o.UserName == userName)
                        .ToListAsync();
            return orderList;
        }
    }
}
