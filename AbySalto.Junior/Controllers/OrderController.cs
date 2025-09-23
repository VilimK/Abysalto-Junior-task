using AbySalto.Junior.Infrastructure.Database;
using AbySalto.Junior.Models;
using AbySalto.Junior.Models.DTOs;
using AbySalto.Junior.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbySalto.Junior.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(CreateOrderDTO createOrderDto)
        {
            try
            {
                var result = await _orderService.CreateOrder(createOrderDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new {error = ex.Message});
            }
        }

        //Only pending and preparing orders
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<Order>>> GetActiveOrders()
        {
            var ordersPND = await _orderService.GetOrdersDependingOnStatusCode("PND");
            var ordersPRP = await _orderService.GetOrdersDependingOnStatusCode("PRP");
            var orders = new List<Order>();
            orders.AddRange(ordersPND);
            orders.AddRange(ordersPRP);
            return Ok(orders);
        }

        [HttpGet("completed")]
        public async Task<ActionResult<IEnumerable<Order>>> GetCompletedOrders()
        {
            var orders = await _orderService.GetOrdersDependingOnStatusCode("CMP");
            return Ok(orders);
        }
    }
}
