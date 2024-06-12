namespace Ecommerce.Domain.Model
{
    public class CustomerOrderDetails
    {
        public Customer Customer { get; set; }
        public Order Order { get; set; }
    }
}
