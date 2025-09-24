using AbySalto.Junior.Infrastructure.Database;
using AbySalto.Junior.Models;
using System;
using System.Diagnostics;

namespace AbySalto.Junior
{
    public static class DbInitializer
    {
        public static void Seed(ApplicationDbContext context)
        {
            
            if (!context.Currency.Any())
            {
                var currencies = new List<Currency>
            {
                new Currency { Code = "HRK", Name = "Croatian Kuna" },
                new Currency { Code = "EUR", Name = "Euro" },
                new Currency { Code = "USD", Name = "US Dollar" }
            };
                context.Currency.AddRange(currencies);
                context.SaveChanges();
            }

            
            if (!context.PaymentType.Any())
            {
                var payments = new List<PaymentType>
            {
                new PaymentType { Code = "CASH", Description = "Cash Payment" },
                new PaymentType { Code = "CARD", Description = "Credit/Debit Card" },
                new PaymentType { Code = "ONLINE", Description = "Online Payment" }
            };
                context.PaymentType.AddRange(payments);
                context.SaveChanges();
            }

            if (!context.Status.Any())
            {
                var statuses = new List<Status>
            {
                new Status { Name = "Pending", Code = "PND" },
                new Status { Name = "Preparing", Code = "PRP" },
                new Status { Name = "Completed", Code = "CMP" }
            };
                context.Status.AddRange(statuses);
                context.SaveChanges();
            }

            if (!context.Article.Any())
            {
                var articles = new List<Article>
            {
                new Article { Name = "Margherita Pizza", Price = 50.00m, Description = "Classic cheese pizza" },
                new Article { Name = "Pepperoni Pizza", Price = 60.00m, Description = "Pizza with pepperoni" },
                new Article { Name = "Coca-Cola 0.33L", Price = 10.00m, Description = "Soft drink" },
                new Article { Name = "French Fries", Price = 20.00m, Description = "Crispy fries" },
                new Article { Name = "Cheeseburger", Price = 55.00m, Description = "Beef burger with cheese" }
            };
                context.Article.AddRange(articles);
                context.SaveChanges();
            }

            if (!context.Order.Any())
            {
                var hrk = context.Currency.First(c => c.Code == "HRK").Id;
                var eur = context.Currency.First(c => c.Code == "EUR").Id;
                var statusPending = context.Status.First(s => s.Code == "PND").Id;
                var statusPreparing = context.Status.First(s => s.Code == "PRP").Id;
                var cash = context.PaymentType.First(p => p.Code == "CASH").Id;
                var card = context.PaymentType.First(p => p.Code == "CARD").Id;

                var margherita = context.Article.First(a => a.Name == "Margherita Pizza");
                var pepperoni = context.Article.First(a => a.Name == "Pepperoni Pizza");
                var coke = context.Article.First(a => a.Name == "Coca-Cola 0.33L");
                var fries = context.Article.First(a => a.Name == "French Fries");

           
                var order1 = new Order
                {
                    BuyerName = "Ivan Ivković",
                    Addres = "Glavna 100, Zagreb",
                    ContactNumber = "0914582582",
                    OrderTime = DateTime.Now,
                    Remark = "Bez luka",
                    PaymentTypeId = cash,
                    StatusId = statusPending,
                    CurrencyId = hrk,
                    OrderItems = new List<OrderItem>()
                };
                order1.OrderItems.Add(new OrderItem { ArticleId = margherita.Id, Quantity = 1 });
                order1.OrderItems.Add(new OrderItem { ArticleId = coke.Id, Quantity = 2 });
                order1.OrderItems.Add(new OrderItem { ArticleId = fries.Id, Quantity = 1 });
                order1.Amount = order1.OrderItems.Sum(i =>
                    context.Article.First(a => a.Id == i.ArticleId).Price * i.Quantity);

                
                var order2 = new Order
                {
                    BuyerName = "Ana Horvat",
                    Addres = "Ulica 55, Split",
                    ContactNumber = "098123456",
                    OrderTime = DateTime.Now,
                    Remark = "Extra napkins",
                    PaymentTypeId = card,
                    StatusId = statusPreparing,
                    CurrencyId = eur,
                    OrderItems = new List<OrderItem>()
                };
                order2.OrderItems.Add(new OrderItem { ArticleId = pepperoni.Id, Quantity = 1 });
                order2.OrderItems.Add(new OrderItem { ArticleId = coke.Id, Quantity = 1 });
                order2.OrderItems.Add(new OrderItem { ArticleId = fries.Id, Quantity = 2 });
                order2.Amount = order2.OrderItems.Sum(i =>
                    context.Article.First(a => a.Id == i.ArticleId).Price * i.Quantity);

                context.Order.AddRange(order1, order2);
                context.SaveChanges();
            }
        }
    }
}
