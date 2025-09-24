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
        public async Task<ActionResult<List<Order>>> GetActiveOrders()
        {
            var ordersPND = await _orderService.GetOrdersDependingOnStatusCode("PND");
            var ordersPRP = await _orderService.GetOrdersDependingOnStatusCode("PRP");
            var orders = ordersPND.Concat(ordersPRP).ToList();
            return Ok(orders);
        }

        [HttpGet("completed")]
        public async Task<ActionResult<IEnumerable<Order>>> GetCompletedOrders()
        {
            var orders = await _orderService.GetOrdersDependingOnStatusCode("CMP");
            return Ok(orders);
        }

        [HttpPatch("{orderId}/statusupdate")]
        public async Task<ActionResult<Order>> UpdateOrderStatus(int orderId)
        {
            try
            {
                var order = await _orderService.UpdateOrderStatusAsync(orderId);
                return Ok(order);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("{orderId}/amount")]
        public async Task<ActionResult<decimal>> GetOrderAmount(int orderId)
        {
            try
            {
                var amount = await _orderService.GetOrderAmount(orderId);
                return Ok(amount);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
       
        [HttpGet("sorted-by-amount")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersSortedByAmount()
        {
            var orders = await _orderService.GetOrdersSortedByAmountAsync();
            return Ok(orders);
        }

    }
}
