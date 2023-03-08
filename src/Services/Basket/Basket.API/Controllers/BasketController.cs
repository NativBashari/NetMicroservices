using AutoMapper;
using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using EventBus.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Net;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController: ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly DiscountGrpcService _discountGrpcService;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        public BasketController(IBasketRepository repository, DiscountGrpcService discountGrpcService, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _discountGrpcService = discountGrpcService;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet("{userName}" , Name ="GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart) , (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket = await _repository.GetBasket(userName);
            return Ok(basket ?? new ShoppingCart(userName)); // if there is no shoppingCart in memory, create and return a new one.
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            //TODO : Communicate with Discount.Grpc and calculate latest prices of product into shpping cart.
            foreach (var item in basket.Items)
            {
                var coupon = await _discountGrpcService.GetDiscount(item.ProductName!);
                item.Price -= coupon.Amount;
            }
            return Ok(await _repository.UpdateBasket(basket));
        }

        [HttpDelete("{userName}", Name ="DeleteBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await _repository.DeleteBasket(userName);
            return Ok();
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            // Get the existint basket with total price
            // Create basketCheckoutEvent -- Set totalPrice on basketCheckout eventMessage
            // send checkout event to rabbitmq
            // remove the basket

            var basket = await _repository.GetBasket(basketCheckout.UserName);
            if (basket == null) return BadRequest();

            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.TotlaPrice = basket.TotalPrice;
            await _publishEndpoint.Publish(eventMessage);

            await _repository.DeleteBasket(basket.UserName);

            return Accepted();


        }
    }
}
