using System;
using System.Collections.Generic;
using MediatR;
using SampleProject.Application.Configuration.Commands;
using SampleProject.Application.Plannings.GetCustomerPlannings;
using SampleProject.Domain.Plannings;

namespace SampleProject.Application.Orders.ChangeCustomerOrder
{
    public class ChangeCustomerOrderCommand : CommandBase<Unit>
    {
        public Guid CustomerId { get; }

        public Guid OrderId { get; }

        public List<PlanningDto> Plannings { get; }

        public ChangeCustomerOrderCommand(Guid customerId, Guid orderId, List<PlanningDto> plannings)
        {
            this.CustomerId = customerId;
            this.OrderId = orderId;
            this.Plannings = plannings;
        }
    }
}
