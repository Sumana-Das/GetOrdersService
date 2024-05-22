using SampleAPI.Entities;
using SampleAPI.Requests;

namespace SampleAPI.Repositories
{
    public interface IOrderRepository
    {
        public List<Order> GetAllOrders(int noOfOrders);
        public void AddNewOrder(CreateOrderRequest order);
    }
}
