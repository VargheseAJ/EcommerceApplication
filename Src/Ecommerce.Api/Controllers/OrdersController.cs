using Ecommerce.Api.Model;
using Ecommerce.Domain.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ICustomerInfo _customerRepo;

        public OrdersController(ICustomerInfo customerRepo)
        {
            _customerRepo = customerRepo;
        }

        [HttpPost]
        public async Task<IActionResult> GetOrderDetails([FromBody] CustomerRequest request)
        {
            try
            {
                var orderDetails = await _customerRepo.GetCustomerInfoAsync(request.User, request.CustomerId);

                if (orderDetails == null)
                {
                    return NotFound("Order details not found.");
                }

                if (orderDetails.Order == null)
                {
                    // Return user details with empty order content
                    return Ok(new
                    {
                        customer = orderDetails.Customer,
                        order = new { }
                    });
                }

                return Ok(orderDetails);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
