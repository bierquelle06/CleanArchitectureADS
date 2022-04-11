using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;

using SampleProject.Application.Configuration.Data;
using SampleProject.Application.Configuration.Queries;

using SampleProject.Application.Plannings.GetCustomerPlannings;

namespace SampleProject.Application.Orders.GetCustomerOrderDetails
{
    internal sealed class GetCustomerOrderDetailsQueryHandler : IQueryHandler<GetCustomerOrderDetailsQuery, OrderDetailsDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        internal GetCustomerOrderDetailsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            this._sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<OrderDetailsDto> Handle(GetCustomerOrderDetailsQuery request, CancellationToken cancellationToken)
        {
            var connection = this._sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT " +
                               "[Order].[Id], " +
                               "[Order].[IsRemoved], " +
                               "[Order].[Description], " +
                               "[Order].[OrderDate] " +
                               "FROM orders.v_Orders AS [Order] " +
                               "WHERE [Order].Id = @OrderId";

            var order = await connection.QuerySingleOrDefaultAsync<OrderDetailsDto>(sql, new { request.OrderId });

            const string sqlPlannings = "SELECT " +
                               "[Order].[PlanningId] AS [Id], " +
                               "[Order].[SeatCapacity], " +
                               "[Order].[FreeCapacity], " +
                               "[Order].[Target], " +
                               "[Order].[Source] " +
                               "FROM orders.v_OrderPlannings AS [Order] " +
                               "WHERE [Order].OrderId = @OrderId";

            var plannings = await connection.QueryAsync<PlanningDto>(sqlPlannings, new { request.OrderId });

            order.Plannings = plannings.AsList();

            return order;
        }
    }
}