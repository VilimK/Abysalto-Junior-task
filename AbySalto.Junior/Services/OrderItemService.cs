using AbySalto.Junior.Infrastructure.Database;
using AbySalto.Junior.Models;
using AbySalto.Junior.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace AbySalto.Junior.Services
{
    public class OrderItemService
    {
        private ApplicationDbContext _context;
        private OrderService _orderService; 
        
        public OrderItemService(ApplicationDbContext context, OrderService orderService)
        {
            _context = context;
            _orderService = orderService; 
        }

        public async Task<OrderItem> CreateOrderItem(CreateOrderItemDTO createOrderItemDTO)
        {
            if (createOrderItemDTO.Quantity < 1)
                throw new ArgumentException("Quantity must be greater than 0!");

            if (!_context.Article.Any(p => p.Id == createOrderItemDTO.ArticleId))
                throw new ArgumentException("Invalid article ID");
            
            if (!_context.Order.Any(p => p.Id == createOrderItemDTO.OrderId))
                throw new ArgumentException("Invalid order ID");
            
            var newOrderItem = new OrderItem
            {
                OrderId = createOrderItemDTO.OrderId,
                Quantity = createOrderItemDTO.Quantity,
                ArticleId = createOrderItemDTO.ArticleId
            };
            
            _context.OrderItem.Add(newOrderItem);
            await _context.SaveChangesAsync();
            var order = _context.Order
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Article)
                .FirstOrDefault(o => o.Id == createOrderItemDTO.OrderId);
            _orderService.UpdateOrderAmount(order);
            return newOrderItem;
        }
    }
}
