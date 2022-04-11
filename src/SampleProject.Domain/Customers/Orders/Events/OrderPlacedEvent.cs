﻿using System;
using SampleProject.Domain.SeedWork;
using SampleProject.Domain.SharedKernel;

namespace SampleProject.Domain.Customers.Orders.Events
{
    public class OrderPlacedEvent : DomainEventBase
    {
        public OrderId OrderId { get; }

        public CustomerId CustomerId { get; }

        public OrderPlacedEvent(
            OrderId orderId, 
            CustomerId customerId)
        {
            this.OrderId = orderId;
            this.CustomerId = customerId;
        }
    }
}