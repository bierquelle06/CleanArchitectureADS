using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;

using SampleProject.Application.Configuration.Commands;
using SampleProject.Application.Orders.UpdateStatusOrder;

using SampleProject.Application.Configuration.Data;
using SampleProject.Application.Configuration.Queries;

namespace SampleProject.Application.Orders.RemoveCustomerOrder
{
    internal sealed class UpdateStatusOrderCommandHandler : ICommandHandler<UpdateStatusOrderCommand, Unit>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        
        public UpdateStatusOrderCommandHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            this._sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Unit> Handle(UpdateStatusOrderCommand request, CancellationToken cancellationToken)
        {
            var connection = this._sqlConnectionFactory.GetOpenConnection();

            string sql = "UPDATE orders.Orders SET [StatusId] = " + request.StatusId + " WHERE [Id] = '" + request.OrderId  + "'";

            await connection.ExecuteAsync(sql);


            return Unit.Value;
        }
    }
}