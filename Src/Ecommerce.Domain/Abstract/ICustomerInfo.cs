using Ecommerce.Domain.Model;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Abstract
{
    public interface ICustomerInfo
    {
        Task<CustomerOrderDetails> GetCustomerInfoAsync(string email, string customerId);
    }
}
