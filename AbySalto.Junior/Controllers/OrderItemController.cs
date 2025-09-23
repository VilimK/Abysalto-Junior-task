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
    public class OrderItemController:Controller
    {
        private readonly OrderItemService _orderItemService;

        public OrderItemController(OrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpPost]
        public async Task<ActionResult<OrderItem>> CreateOrder(CreateOrderItemDTO createOrderItemDTO)
        {
            try
            {
                var result = await _orderItemService.CreateOrderItem(createOrderItemDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
