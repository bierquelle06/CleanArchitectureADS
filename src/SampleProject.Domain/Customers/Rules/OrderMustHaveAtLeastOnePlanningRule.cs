using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers.Rules
{
    public class OrderMustHaveAtLeastOnePlanningRule : IBusinessRule
    {
        private readonly List<OrderPlanningData> _orderPlanningData;

        public OrderMustHaveAtLeastOnePlanningRule(List<OrderPlanningData> orderPlanningData)
        {
            _orderPlanningData = orderPlanningData;
        }

        public bool IsBroken() => !_orderPlanningData.Any();

        public string Message => "Order must have at least one planning";
    }
}