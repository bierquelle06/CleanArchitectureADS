using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Plannings
{
    public class PlanningRegisteredEvent : DomainEventBase
    {
        public PlanningId PlanningId { get; }

        public PlanningRegisteredEvent(PlanningId planningId)
        {
            this.PlanningId = planningId;
        }
    }
}