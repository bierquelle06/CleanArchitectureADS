using SampleProject.Application.Plannings.GetCustomerPlannings;
using System;
using System.Collections.Generic;

namespace SampleProject.Application.Orders.GetCustomerOrderDetails
{
    public class OrderDetailsDto
    {
        public Guid Id { get; set; }

        public bool IsRemoved { get; set; }

        public string Description { get; set; }

        public DateTime OrderDate { get; set; }

        public List<PlanningDto> Plannings { get; set; }
    }
}