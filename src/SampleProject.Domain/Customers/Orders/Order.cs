using System;
using System.Collections.Generic;
using System.Linq;

using SampleProject.Domain.Plannings;
using SampleProject.Domain.SeedWork;
using SampleProject.Domain.SharedKernel;

namespace SampleProject.Domain.Customers.Orders
{
    public class Order : Entity
    {
        internal OrderId Id;

        private bool _isRemoved;

        private string _description;

        private List<OrderPlanning> _orderPlannings;

        private OrderStatus _status;

        private DateTime _orderDate;

        private DateTime? _orderChangeDate;

        private Order()
        {
            this._orderPlannings = new List<OrderPlanning>();
            this._isRemoved = false;
            this._description = "";
        }

        private Order(List<OrderPlanningData> orderPlanningsData, string description, DateTime orderDate)
        {
            this._orderDate = orderDate;
            this._description = description;
            this.Id = new OrderId(Guid.NewGuid());
            this._orderPlannings = new List<OrderPlanning>();

            foreach (var orderPlanningData in orderPlanningsData)
            {
                var orderPlanning = OrderPlanning.CreateForPlanning(orderPlanningData.SeatCapacity, orderPlanningData.PlanningId);

                _orderPlannings.Add(orderPlanning);
            }

            this._status = OrderStatus.Waiting;
        }

        internal static Order CreateNew(List<OrderPlanningData> orderPlanningsData, string description, DateTime orderDate)
        {
            return new Order(orderPlanningsData, description, orderDate);
        }

        internal void Change(List<OrderPlanningData> orderPlanningsData)
        {
            var orderPlanningsToCheck = _orderPlannings.ToList();
            foreach (var existingPlanning in orderPlanningsToCheck)
            {
                var Planning = orderPlanningsData.SingleOrDefault(x => x.PlanningId == existingPlanning.PlanningId);
                if (Planning == null)
                {
                    this._orderPlannings.Remove(existingPlanning);
                }
            }

            this._orderChangeDate = DateTime.UtcNow;
        }

        internal void Remove()
        {
            this._isRemoved = true;
        }

        internal bool IsOrderedToday()
        {
           return this._orderDate.Date == SystemClock.Now.Date;
        }
    }
}