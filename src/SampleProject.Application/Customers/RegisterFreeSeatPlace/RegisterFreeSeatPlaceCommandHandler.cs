using System.Threading;
using System.Threading.Tasks;

using Dapper;
using MediatR;

using SampleProject.Application.Configuration.Commands;
using SampleProject.Application.Configuration.Data;

using SampleProject.Domain.Customers;
using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Application.Customers.RegisterFreeSeatPlace
{
    /// <summary>
    /// 
    /// </summary>
    internal sealed class RegisterFreeSeatPlaceCommandHandler : ICommandHandler<RegisterFreeSeatPlaceCommand, bool?>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IOrderUniquenessChecker _orderUniquenessChecker;

        internal RegisterFreeSeatPlaceCommandHandler(ISqlConnectionFactory sqlConnectionFactory,
            IOrderUniquenessChecker orderUniquenessChecker)
        {
            this._sqlConnectionFactory = sqlConnectionFactory;
            this._orderUniquenessChecker = orderUniquenessChecker;
        }

        public async Task<bool?> Handle(RegisterFreeSeatPlaceCommand request, CancellationToken cancellationToken)
        {
            var connection = this._sqlConnectionFactory.GetOpenConnection();

            if (_orderUniquenessChecker.IsUnique(request.OrderId))
            {
                return null;
            }

            string sql = "UPDATE orders.OrderPlannings SET [FreeCapacity] = (ISNULL([FreeCapacity],0) + 1) WHERE [OrderId] = '" + request.OrderId + "' AND ISNULL([FreeCapacity],0) < [SeatCapacity]";

            var result = await connection.ExecuteAsync(sql);

            return result > 0;
        }
    }
}