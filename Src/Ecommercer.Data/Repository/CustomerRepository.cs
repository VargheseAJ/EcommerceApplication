using Dapper;
using Ecommercer.Data.DTO;
using Ecommercer.Data.Factory;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommercer.Data.Repository
{
    public class CustomerRepository : ICustomerRepo
    {
        public async Task<Customer> GetCustomerByEmailAndIdAsync(string email, string customerId)
        {
            using (var connection = DataBaseConnection.GetConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Customer>(
                    "SELECT * FROM Customers WHERE Email = @Email AND CustomerId = @CustomerId",
                    new { Email = email, CustomerId = customerId });
            }
        }

        public async Task<Order> GetMostRecentOrderAsync(string customerId)
        {
            using (var connection = DataBaseConnection.GetConnection())
            {
                var order = await connection.QuerySingleOrDefaultAsync<Order>(
                    "SELECT TOP 1 * FROM Orders WHERE CustomerId = @CustomerId ORDER BY OrderDate DESC",
                    new { CustomerId = customerId });

                if (order != null)
                {
                    order.OrderItems = (await connection.QueryAsync<OrderItem>(
                        "SELECT " +
                        "CASE WHEN o.ContainsGift = 1 THEN 'Gift' ELSE p.ProductName END as Product, " +
                        "oi.Quantity, oi.Price as PriceEach " +
                        "FROM OrderItems oi " +
                        "JOIN Products p ON oi.ProductId = p.ProductId " +
                        "JOIN Orders o ON oi.OrderId = o.OrderId " +
                        "WHERE oi.OrderId = @OrderId",
                        new { OrderId = order.OrderNumber })).ToList();

                    var customer = await connection.QuerySingleOrDefaultAsync<Customer>(
                        "SELECT * FROM Customers WHERE CustomerId = @CustomerId",
                        new { CustomerId = customerId });

                    order.DeliveryAddress = $"{customer.HouseNo} {customer.Street}, {customer.Town}, {customer.Postcode}";
                }

                return order;
            }
        }
    }
}
