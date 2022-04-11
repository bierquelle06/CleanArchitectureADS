using SampleProject.Domain.Plannings;

namespace SampleProject.Domain.Customers.Orders
{
    public class OrderPlanningData
    {
        public OrderPlanningData(PlanningId planningId, int seatCapacity)
        {
            PlanningId = planningId;
            SeatCapacity = seatCapacity;
        }

        public PlanningId PlanningId { get; }

        public int SeatCapacity { get; }
    }
}