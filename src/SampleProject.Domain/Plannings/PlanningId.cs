using System;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Plannings
{
    public class PlanningId : TypedIdValueBase
    {
        public PlanningId(Guid value) : base(value)
        {
        }
    }
}