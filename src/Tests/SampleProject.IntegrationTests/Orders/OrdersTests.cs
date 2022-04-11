using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using NUnit.Framework;
using SampleProject.Application.Customers.IntegrationHandlers;
using SampleProject.Application.Customers.RegisterCustomer;
using SampleProject.Application.Orders;
using SampleProject.Application.Orders.GetCustomerOrderDetails;
using SampleProject.Application.Orders.PlaceCustomerOrder;
using SampleProject.Application.Plannings.GetCustomerPlannings;
using SampleProject.Domain.Customers;
using SampleProject.Domain.Customers.Orders;
using SampleProject.Infrastructure.Processing;
using SampleProject.IntegrationTests.SeedWork;

namespace SampleProject.IntegrationTests.Orders
{
    [TestFixture]
    public class OrdersTests : TestBase
    {
        [Test]
        public async Task PlaceOrder_Test()
        {
            var customerEmail = "aykut@aykutaktas.net";
            var customer = await CommandsExecutor.Execute(new RegisterCustomerCommand(customerEmail, "Sample Customer"));

            List<PlanningDto> Plannings = new List<PlanningDto>();
            var PlanningId = Guid.Parse("9DB6E474-AE74-4CF5-A0DC-BA23A42E2566");
            Plannings.Add(new PlanningDto(PlanningId, 5, "ANKARA", "İZMİR"));

            var orderId = await CommandsExecutor.Execute(new PlaceCustomerOrderCommand(customer.Id, Plannings, "Ankaradan izmire yolculuk", DateTime.Now.AddDays(1)));

            var orderDetails = await QueriesExecutor.Execute(new GetCustomerOrderDetailsQuery(orderId));

            Assert.That(orderDetails, Is.Not.Null);
            Assert.That(orderDetails.Plannings.Count, Is.EqualTo(1));
            Assert.That(orderDetails.Plannings[0].SeatCapacity, Is.EqualTo(5));
            Assert.That(orderDetails.Plannings[0].Id, Is.EqualTo(PlanningId));

            var connection = new SqlConnection(ConnectionString);
            var messagesList = await OutboxMessagesHelper.GetOutboxMessages(connection);
            
            Assert.That(messagesList.Count, Is.EqualTo(3));
            
            var customerRegisteredNotification =
                OutboxMessagesHelper.Deserialize<CustomerRegisteredNotification>(messagesList[0]);

            Assert.That(customerRegisteredNotification.CustomerId, Is.EqualTo(new CustomerId(customer.Id)));

            var orderPlaced =
                OutboxMessagesHelper.Deserialize<OrderPlacedNotification>(messagesList[1]);

            Assert.That(orderPlaced.OrderId, Is.EqualTo(new OrderId(orderId)));
        }
    }
}