using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SampleProject.Application.Configuration.Commands;
using SampleProject.Application.Configuration.Data;
using SampleProject.Application.Orders.PlaceCustomerOrder;
using SampleProject.Domain.Customers;
using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.Plannings;

namespace SampleProject.Application.Orders.ChangeCustomerOrder
{
    internal sealed class ChangeCustomerOrderCommandHandler : ICommandHandler<ChangeCustomerOrderCommand,Unit>
    {
        private readonly ICustomerRepository _customerRepository;

        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        internal ChangeCustomerOrderCommandHandler(
            ICustomerRepository customerRepository,
            ISqlConnectionFactory sqlConnectionFactory)
        {
            this._customerRepository = customerRepository;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Unit> Handle(ChangeCustomerOrderCommand request, CancellationToken cancellationToken)
        {
            var customer = await this._customerRepository.GetByIdAsync(new CustomerId(request.CustomerId));

            var orderId = new OrderId(request.OrderId);

            var orderPlannings = request
                    .Plannings
                    .Select(x => new OrderPlanningData(new PlanningId(x.Id), x.SeatCapacity))
                    .ToList();

            customer.ChangeOrder(
                orderId,
                orderPlannings);

            return Unit.Value;
        }
    }
}
