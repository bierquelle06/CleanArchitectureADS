using System;

namespace SampleProject.Application.Orders.GetCustomerOrders
{
    public class OrderDto
    {
        public Guid Id { get; set; }

        public bool IsRemoved { get; set; }

        public string Description { get; set; }
    }
}