using System;
using System.Collections.Generic;
using System.Linq;

using SampleProject.Domain.Plannings;
using SampleProject.Domain.SeedWork;
using SampleProject.Domain.SharedKernel;

namespace SampleProject.Domain.Customers.Orders
{
    public class OrderPlanning : Entity
    {
        public int SeatCapacity { get; private set; }

        public int FreeCapacity { get; private set; }

        public PlanningId PlanningId { get; private set; }

        private OrderPlanning()
        {

        }

        private OrderPlanning(int seatCapacity, PlanningId planningId)
        {
            this.SeatCapacity = seatCapacity;
            this.PlanningId = planningId;
            this.FreeCapacity = 0;
        }

        internal static OrderPlanning CreateForPlanning(int seatCapacity, PlanningId planningId)
        {
            return new OrderPlanning(seatCapacity, planningId);
        }

        internal void ChangeSeatCapacity(int seatCapacity)
        {
            this.SeatCapacity = seatCapacity;
        }
    }
}