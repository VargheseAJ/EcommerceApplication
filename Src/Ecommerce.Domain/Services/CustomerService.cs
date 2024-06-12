using Ecommerce.Domain.Abstract;
using Ecommerce.Domain.Model;
using Ecommercer.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Services
{
    public class CustomerService : ICustomerInfo
    {
        private readonly ICustomerRepo _customerRepo;

        public CustomerService(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public async Task<CustomerOrderDetails> GetCustomerInfoAsync(string email, string customerId)
        {
            var customer = await _customerRepo.GetCustomerByEmailAndIdAsync(email, customerId);

            if (customer == null)
            {
                throw new ArgumentNullException("Invalid customer email or ID.");
            }

            var order = await _customerRepo.GetMostRecentOrderAsync(customerId);

            if (order == null)
            {
                // User does not have any orders
                return new CustomerOrderDetails
                {
                    Customer = MapToCustomer(customer),
                    Order = null
                };
            }

            return new CustomerOrderDetails
            {
                Customer = MapToCustomer(customer),
                Order = MapToOrder(order)
            };
        }

        private Order MapToOrder(Ecommercer.Data.DTO.Order order)
        {
            return new Order
            {
                OrderDate = order.OrderDate,
                DeliveryAddress = order.DeliveryAddress,
                DeliveryExpected = order.DeliveryExpected,
                OrderItems = MapToOrderItems(order.OrderItems, order.ContainsGift),
                OrderNumber = order.OrderNumber
            };
        }

        private List<OrderItem> MapToOrderItems(List<Ecommercer.Data.DTO.OrderItem> orderItems, bool containsGift)
        {
            var orderItemDetails = new List<OrderItem>();

            foreach (var item in orderItems)
            {
                orderItemDetails.Add(new OrderItem
                {
                    PriceEach = item.PriceEach,
                    Product = containsGift ? "Gift" : item.Product,
                    Quantity = item.Quantity
                });
            }

            return orderItemDetails;
        }

        private Customer MapToCustomer(Ecommercer.Data.DTO.Customer customer)
        {
            return new Customer
            {
                CustomerId = customer.CustomerId,
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName
            };
        }
    }
}
