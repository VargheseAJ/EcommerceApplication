using Ecommercer.Data.DTO;
using System.Threading.Tasks;

namespace Ecommercer.Data
{
    public interface ICustomerRepo
    {
        Task<Customer> GetCustomerByEmailAndIdAsync(string email, string customerId);
        Task<Order> GetMostRecentOrderAsync(string customerId);
    }
}
