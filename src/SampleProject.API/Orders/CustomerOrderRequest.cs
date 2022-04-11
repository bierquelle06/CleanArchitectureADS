using System;
using System.Collections.Generic;

using SampleProject.Application.Orders;

using SampleProject.Application.Plannings.GetCustomerPlannings;

namespace SampleProject.API.Orders
{
    public class CustomerOrderRequest
    {
        public List<PlanningDto> Plannings { get; set; }

        public string Description { get; set; }

        public DateTime OrderDate { get; set; }
    }
}