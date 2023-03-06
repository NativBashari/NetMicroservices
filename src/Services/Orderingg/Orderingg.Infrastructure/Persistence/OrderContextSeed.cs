using Microsoft.Extensions.Logging;
using Orderingg.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orderingg.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext , ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order(){UserName="nativ", FirstName="nativ" , LastName ="Bashari", EmailAddress ="nativbas2000@gmail.com", AddressLine="Shtulim", Country= "Israel", TotlaPrice= 350}
            };
        }
    }
}
