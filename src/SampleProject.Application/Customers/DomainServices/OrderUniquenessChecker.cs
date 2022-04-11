using Dapper;
using SampleProject.Application.Configuration.Data;
using SampleProject.Domain.Customers;
using System;

namespace SampleProject.Application.Customers.DomainServices
{
    public class OrderUniquenessChecker : IOrderUniquenessChecker
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public OrderUniquenessChecker(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public bool IsUnique(Guid Id)
        {
            var connection = this._sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT TOP 1 1" +
                               "FROM [orders].[Orders] AS [Order] " +
                               "WHERE [Order].[Id] = @Id";

            var ordersNumber = connection.QuerySingleOrDefault<int?>(sql, new { Id = Id });

            return !ordersNumber.HasValue;
        }
    }
}