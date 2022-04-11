using SampleProject.Domain.SeedWork;
using System;

namespace SampleProject.Domain.Customers.Rules
{
    public class OrderMustBeUniqueRule : IBusinessRule
    {
        private readonly IOrderUniquenessChecker _orderUniquenessChecker;

        private readonly Guid _orderId;

        public OrderMustBeUniqueRule(
            IOrderUniquenessChecker orderUniquenessChecker,
            Guid orderId)
        {
            _orderUniquenessChecker = orderUniquenessChecker;
            _orderId = orderId;
        }

        public bool IsBroken() => !_orderUniquenessChecker.IsUnique(_orderId);

        public string Message => "Orders with this order id already exists.";
    }
}