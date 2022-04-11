using System;
using System.Runtime.InteropServices.ComTypes;

namespace SampleProject.Application.Plannings.GetCustomerPlannings
{
    public class PlanningDto
    {
        public Guid Id { get; set; }

        public int SeatCapacity { get; set; }

        public int FreeCapacity { get; set; }

        public string Target { get; set; }

        public string Source { get; set; }

        public PlanningDto()
        {
            
        }

        public PlanningDto(Guid id, int seatCapacity, string target, string source)
        {
            this.Id = id;
            this.SeatCapacity = seatCapacity;
            this.Target = target;
            this.Source = source;
        }
    }
}