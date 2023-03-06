using AutoMapper;
using Orderingg.Application.Features.Orders.Commands.CheckoutOrder;
using Orderingg.Application.Features.Orders.Commands.UpdateOrder;
using Orderingg.Application.Features.Orders.Queries.GetOrdersList;
using Orderingg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderingg.Application.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrdersVm>().ReverseMap();
            CreateMap<Order, CheckoutOrderCommand>().ReverseMap();
            CreateMap<Order, UpdateOrderCommand>().ReverseMap();
        }
    }
}
