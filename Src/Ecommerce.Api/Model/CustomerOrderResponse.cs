using Ecommerce.Domain.Model;

namespace Ecommerce.Api.Model
{
    public class CustomerOrderResponse
    {
        public Customer Customer { get; set; }
        public Order Order { get; set; }
    }
}
