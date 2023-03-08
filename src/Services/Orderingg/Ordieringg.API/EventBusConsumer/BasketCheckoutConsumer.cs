using AutoMapper;
using EventBus.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Orderingg.Application.Features.Orders.Commands.CheckoutOrder;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Ordieringg.API.EventBusConsumer
{
    public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILogger<BasketCheckoutConsumer> _logger;
        public BasketCheckoutConsumer(IMapper mapper, IMediator mediator, ILogger<BasketCheckoutConsumer> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            var command = _mapper.Map<CheckoutOrderCommand>(context.Message);
            var result = await _mediator.Send(command);
            _logger.LogInformation("BusketCheckoutEvent consumed successfully. Create order Id: {newOrderId}", result);   
        }
    }
}
