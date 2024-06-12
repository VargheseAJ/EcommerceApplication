namespace Ecommerce.Domain.Model
{
    public class Customer
    {
        public string CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int HouseNo { get; set; }
        public string Street { get; set; }
        public string Town { get; set; }
        public int Postcode { get; set; }
    }
}
