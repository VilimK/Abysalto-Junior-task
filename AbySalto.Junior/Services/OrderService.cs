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
            if (order == null || order.OrderItems == null )
                throw new ArgumentNullException("Order is null");
            order.Amount = order.OrderItems.Sum(o => o.Article.Price * o.Quantity);
        }

        //Status Codes:
        //PND-pending, PRP-preparing,CMP-completed
        public async Task<List<Order>> GetOrdersDependingOnStatusCode(string statusCode)
        {
            var orders = await _context.Order
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Article)
            .Include(o => o.PaymentType)
            .Include(o => o.Status)
            .Include(o => o.Currency)
            .Where(o => o.Status.Code == statusCode)
            .ToListAsync();
            return orders; 
        }

        public async Task<Order> UpdateOrderStatusAsync(int orderId)
        {
            Order? order = await GetOrderById(orderId);

            var status = await _context.Status.FirstOrDefaultAsync(s => s.Id == order.StatusId);
            if (status == null)
                throw new ArgumentException("Invalid status ID");
            
            Status newStatus = null;
            if (status.Code == "PND")
                newStatus = await _context.Status.FirstOrDefaultAsync(s => s.Code == "PRP");
            else if (status.Code == "PRP")
                newStatus = await _context.Status.FirstOrDefaultAsync(s => s.Code == "CMP");
            else if (status.Code == "CMP")
                throw new InvalidOperationException("Cannot change status of completed order");

            if (newStatus == null)
                throw new InvalidOperationException("Next status not found in database.");
            
            order.StatusId = newStatus.Id;
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<decimal> GetOrderAmount(int orderId)
        {
            var order = await GetOrderById(orderId);
            UpdateOrderAmount(order);
            await _context.SaveChangesAsync();
            return order.Amount;
        }

        private async Task<Order?> GetOrderById(int orderId)
        {
            var order = await _context.Order
                .Include(o => o.OrderItems)             
                .ThenInclude(oi => oi.Article)           
                .FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
                throw new ArgumentException("Invalid order ID");
            return order;
        }

        public async Task<List<Order>> GetOrdersSortedByAmountAsync()
        {
            var orders = await _context.Order
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Article)
                .Include(o => o.PaymentType)
                .Include(o => o.Status)
                .Include(o => o.Currency)
                .ToListAsync();
            foreach (var order in orders) UpdateOrderAmount(order);
            return orders.OrderByDescending(o => o.Amount).ToList();
        }

    }
}
