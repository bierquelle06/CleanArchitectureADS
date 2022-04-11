using MediatR;
using SampleProject.Application.Configuration.Commands;
using System;

namespace SampleProject.Application.Customers.RegisterFreeSeatPlace
{
    public class RegisterFreeSeatPlaceCommand : CommandBase<bool?>
    {
        public Guid OrderId { get; }

        public RegisterFreeSeatPlaceCommand(Guid OrderId)
        {
            this.OrderId = OrderId;
        }      
    }
}