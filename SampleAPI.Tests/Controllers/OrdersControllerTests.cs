using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SampleAPI.Controllers;
using SampleAPI.Entities;
using SampleAPI.Repositories;
using SampleAPI.Requests;

namespace SampleAPI.Tests.Controllers
{
    public class OrdersControllerTests
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
        public async Task CreateNewOrder_ReturnsCreatedOrder()
        {
            var mockRepository = new Mock<IOrderRepository>();
            var controller = new OrdersController(mockRepository.Object);
            var newOrder = new CreateOrderRequest
            {
                OrderId = 3,
                EntryDate = DateTime.Now,
                Description = "Sample order description 3",
                Name = "Order 3"
            };

            var result = await controller.CreateOrder(newOrder);

            var order = Assert.IsAssignableFrom<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllOrders_ReturnsListOfOrders()
        {
            var mockRepository = new Mock<IOrderRepository>();
            mockRepository.Setup(repo => repo.GetAllOrders(2)).Returns(GetMockOrders());
            var controller = new OrdersController(mockRepository.Object);

            var result = await controller.GetOrders(2);

            var okResult = Assert.IsAssignableFrom<ActionResult>(result.Result);
        }
    }
}
