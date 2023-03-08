using AutoMapper;
using EventBus.Events;
using Orderingg.Application.Features.Orders.Commands.CheckoutOrder;

namespace Ordieringg.API.Mapping
{
    public class OrderingProfile: Profile
    {
        public OrderingProfile()
        {
            CreateMap<CheckoutOrderCommand, BasketCheckoutEvent>().ReverseMap();

        }
    }
}
