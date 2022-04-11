using System;
using MediatR;
using SampleProject.Application.Configuration.Commands;

namespace SampleProject.Application.Orders.UpdateStatusOrder
{
    public class UpdateStatusOrderCommand : CommandBase<Unit>
    {
        public Guid OrderId { get; }

        public int StatusId { get; }

        public UpdateStatusOrderCommand(Guid orderId, int statusId)
        {
            this.OrderId = orderId;
            this.StatusId = statusId;
        }
    }
}