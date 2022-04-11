using System;
using System.Collections.Generic;
using MediatR;
using SampleProject.Application.Configuration.Commands;
using SampleProject.Application.Plannings.GetCustomerPlannings;

namespace SampleProject.Application.Orders.PlaceCustomerOrder
{
    public class PlaceCustomerOrderCommand : CommandBase<Guid>
    {
        public Guid CustomerId { get; }

        public List<PlanningDto> Plannings { get; }

        public string Description { get; }

        public DateTime OrderDate { get; }

        public PlaceCustomerOrderCommand(Guid customerId, List<PlanningDto> planningDtos, string description, DateTime orderDate)
        {
            this.CustomerId = customerId;
            this.Plannings = planningDtos;
            this.OrderDate = orderDate;
            this.Description = description;
        }
    }
}