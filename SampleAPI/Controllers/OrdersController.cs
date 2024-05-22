using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleAPI.Entities;
using SampleAPI.Repositories;
using SampleAPI.Requests;

namespace SampleAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet("GetOrders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Order>>> GetOrders([FromQuery] int noOfOrders)
        {
            try
            {
                List<Order> orders = _orderRepository.GetAllOrders(noOfOrders);
                if(orders == null || orders.Count == 0)
                {
                    return NotFound("No order found");
                }
                return Ok(orders);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving orders: {ex.Message}");
                return StatusCode(500, $"Error retrieving orders: {ex.Message}");
            }
        }

        [HttpPost("CreateNewOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _orderRepository.AddNewOrder(order);
                }

                return Ok("Order created successfully");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error while creating order: {ex.Message}");
                return BadRequest(ModelState);
            }
            
        }
    }
}
