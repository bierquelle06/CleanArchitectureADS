using System;
using System.Collections.Generic;
using System.Linq;
using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.Customers.Orders.Events;
using SampleProject.Domain.Customers.Rules;
using SampleProject.Domain.Plannings;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers
{
    public class Customer : Entity, IAggregateRoot
    {
        public CustomerId Id { get; private set; }

        private string _email;

        private string _name;

        private readonly List<Order> _orders;

        private Customer()
        {
            this._orders = new List<Order>();
        }
         
        private Customer(string email, string name)
        {
            this.Id = new CustomerId(Guid.NewGuid());

            _email = email;
            _name = name;
            _orders = new List<Order>();

            this.AddDomainEvent(new CustomerRegisteredEvent(this.Id));
        }

        public static Customer CreateRegistered(
            string email, 
            string name,
            ICustomerUniquenessChecker customerUniquenessChecker)
        {
            //CHECK RULE
            CheckRule(new CustomerEmailMustBeUniqueRule(customerUniquenessChecker, email));

            return new Customer(email, name);
        }

        public OrderId PlaceOrder(List<OrderPlanningData> orderPlanningsData, string description, DateTime orderDate)
        {
            //CHECK RULE
            CheckRule(new CustomerCannotOrderMoreThan2OrdersOnTheSameDayRule(_orders));
            CheckRule(new OrderMustHaveAtLeastOnePlanningRule(orderPlanningsData));

            var order = Order.CreateNew(orderPlanningsData, description, orderDate);

            this._orders.Add(order);

            this.AddDomainEvent(new OrderPlacedEvent(order.Id, this.Id));

            return order.Id;
        }

        public void ChangeOrder(OrderId orderId,List<OrderPlanningData> newOrderPlanningsData)
        {
            //CHECK RuLE
            CheckRule(new OrderMustHaveAtLeastOnePlanningRule(newOrderPlanningsData));

            var order = this._orders.Single(x => x.Id == orderId);
            order.Change(newOrderPlanningsData);

            this.AddDomainEvent(new OrderChangedEvent(orderId));
        }

        public void RemoveOrder(OrderId orderId)
        {
            var order = this._orders.Single(x => x.Id == orderId);
            order.Remove();

            this.AddDomainEvent(new OrderRemovedEvent(orderId));
        }
    }
}