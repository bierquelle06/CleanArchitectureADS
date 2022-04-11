using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using SampleProject.Application.Configuration.Data;
using SampleProject.Application.Configuration.Queries;

namespace SampleProject.Application.Plannings.GetCustomerPlannings
{
    internal sealed class GetCustomerPlanningsQueryHandler : IQueryHandler<GetCustomerPlanningsQuery, List<PlanningDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        internal GetCustomerPlanningsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            this._sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<PlanningDto>> Handle(GetCustomerPlanningsQuery request, CancellationToken cancellationToken)
        {
            var connection = this._sqlConnectionFactory.GetOpenConnection();
            
            const string sql = "SELECT " +
                               "[Planning].[Id], " +
                               "[Planning].[SeatCapacity], " +
                               "[Planning].[Target], " +
                               "[Planning].[Source] " +
                               "FROM orders.v_Plannings AS [Planning] " +
                               "WHERE [Planning].Target LIKE '%@Target%' OR [Planning].Source LIKE '%@Source%'";

            var orders = await connection.QueryAsync<PlanningDto>(sql, new { request.Target, request.Source });

            return orders.AsList();
        }
    }
}