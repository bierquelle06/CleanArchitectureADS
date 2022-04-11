using System;
using System.Collections.Generic;
using MediatR;
using SampleProject.Application.Configuration.Queries;

namespace SampleProject.Application.Plannings.GetCustomerPlannings
{
    public class GetCustomerPlanningsQuery : IQuery<List<PlanningDto>>
    {
        public string Target { get; }
        
        public string Source { get; }

        public GetCustomerPlanningsQuery(string target, string source)
        {
            this.Target = target;
            this.Source = source;
        }
    }
}