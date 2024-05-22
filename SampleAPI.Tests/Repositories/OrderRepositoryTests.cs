using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using SampleAPI.Controllers;
using SampleAPI.Entities;
using SampleAPI.Repositories;
using SampleAPI.Requests;
using System.Data.SqlClient;

namespace SampleAPI.Tests.Repositories
{
    public class OrderRepositoryTests
    {
        public static List<Order> GetMockOrders()
        {
            return new List<Order>
            {
                new Order
                {
                    OrderId = 1,
                    EntryDate = DateTime.Now.AddDays(-5),
                    Description = "Sample order description 1",
                    Name = "Order 1"
                },
                new Order
                {
                    OrderId = 2,
                    EntryDate = DateTime.Now.AddDays(-3),
                    Description = "Sample order description 2",
                    Name = "Order 2"
                },
            };
        }

        [Fact]
        public void AddOrder_ShouldIncreaseOrderCount()
        {
            var mockDbContext = new Mock<SampleApiDbContext>();
            var orderToAdd = new Order {
                OrderId = 4,
                EntryDate = DateTime.Now,
                Description = "Sample order description 4",
                Name = "Order 4"
            };

            mockDbContext.Object.Add(orderToAdd);

            mockDbContext.Verify(db => db.Add(orderToAdd), Times.Once);
        }

        [Fact]
        public void GetOrders_ShouldRetrieveAllOrders()
        {
            var options = new DbContextOptionsBuilder<SampleApiDbContext>()
                .UseInMemoryDatabase(databaseName: "SampleDB")
                .Options;

            using (var context = new SampleApiDbContext(options))
            {
                context.Orders.Add(new Order
                {
                    OrderId = 1,
                    Name = "Order 1",
                    Description = "Description 1",
                    EntryDate = DateTime.Now
                });
                context.Orders.Add(new Order
                {
                    OrderId = 2,
                    Name = "Order 2",
                    Description = "Description 2",
                    EntryDate = DateTime.Now.AddDays(-1)
                });
                context.SaveChanges();
            }

            using (var context = new SampleApiDbContext(options))
            {
                var repository = new OrderRepository(context);

                var orders = repository.GetAllOrders(2);

                Assert.Equal(2, orders.Count());
            }
        }
    }
}