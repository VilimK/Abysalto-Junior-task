using AbySalto.Junior.Infrastructure.Database;
using AbySalto.Junior.Models;
using AbySalto.Junior.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AbySalto.Junior.Services
{
    public class OrderService
    {
        private ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context) 
        { 
            _context = context;
        }

        public async Task<Order> CreateOrder(CreateOrderDTO createOrderDto)
        {
            if (!_context.PaymentType.Any(p => p.Id == createOrderDto.PaymentTypeId))
                throw new ArgumentException("Invalid payment type ID");

            if (!_context.Currency.Any(p => p.Id == createOrderDto.CurrencyId))
                throw new ArgumentException("Invalid currency ID");
           
            if (string.IsNullOrWhiteSpace(createOrderDto.BuyerName))
                throw new ArgumentException("Buyer name is required");

            if (string.IsNullOrWhiteSpace(createOrderDto.Addres))
                throw new ArgumentException("Address is required");

            if (string.IsNullOrWhiteSpace(createOrderDto.ContactNumber))
                throw new ArgumentException("Contact number is required");

            var newOrder = new Order
            {
                BuyerName = createOrderDto.BuyerName,
                Addres = createOrderDto.Addres,
                OrderTime = DateTime.Now,
                ContactNumber = createOrderDto.ContactNumber,
                Remark = createOrderDto.Remark,
                PaymentTypeId = createOrderDto.PaymentTypeId,
                StatusId = _context.Status.Where(s => s.Code == "PND").Select(s => s.Id).SingleOrDefault(),
                CurrencyId = createOrderDto.CurrencyId,
                Amount = 0,
                OrderItems = new List<OrderItem>()
            };

            _context.Order.Add(newOrder);
            await _context.SaveChangesAsync();
            return newOrder; 
        }

        public void AddMultipleItemsToOrder(int orderId, IEnumerable<int> itemIds)
        {
            foreach (var itemId in itemIds)
                AddSingleItemToOrder(orderId, itemId);       
        }

        public async void AddSingleItemToOrder(int orderId, int itemId)
        {
            var order = _context.Order
                .Include(o => o.OrderItems)
                .FirstOrDefault(o => o.Id == orderId);

            if (order == null)
                throw new ArgumentException("Invalid order ID");

            var orderItem = await _context.OrderItem.FirstOrDefaultAsync(oi => oi.Id == itemId);

            if (orderItem == null)
                throw new ArgumentException("Invalid order item ID");
            
            orderItem.OrderId = orderId;
            order.OrderItems.Add(orderItem);
            UpdateOrderAmount(order);
            await _context.SaveChangesAsync();
        }

        internal void UpdateOrderAmount(Order order)
        {
            if (order == null)
                throw new ArgumentNullException("Order is null");
            order.Amount = order.OrderItems.Sum(o => o.Article.Price * o.Quantity);
        }
    }
}
