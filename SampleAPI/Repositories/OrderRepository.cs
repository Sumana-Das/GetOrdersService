using Microsoft.EntityFrameworkCore;
using SampleAPI.Entities;
using SampleAPI.Requests;

namespace SampleAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SampleApiDbContext _dbContext;

        public OrderRepository(SampleApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Order> GetAllOrders(int noOfOrders)
        {
            var recentOrders = _dbContext.Orders
                                    .Where(o => o.IsDeleted == false)
                                    .OrderByDescending(o => o.EntryDate)
                                    .Take(noOfOrders)
                                    .ToList();
            return recentOrders;
        }

        public void AddNewOrder(CreateOrderRequest newOrder)
        {
            var order = new Order
            {
                EntryDate = newOrder.EntryDate,
                Description = newOrder.Description,
                Name = newOrder.Name,
                IsInvoiced = newOrder.IsInvoiced,
                IsDeleted = newOrder.IsDeleted,
            };
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
        }
    }
}
