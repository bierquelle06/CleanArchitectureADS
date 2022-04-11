using System;
using System.Collections.Generic;
using NUnit.Framework;
using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.Customers.Orders.Events;
using SampleProject.Domain.Customers.Rules;
using SampleProject.Domain.Plannings;
using SampleProject.Domain.SharedKernel;
using SampleProject.UnitTests.SeedWork;

namespace SampleProject.UnitTests.Customers
{
    [TestFixture]
    public class PlaceOrderTests : TestBase
    {
        [Test]
        public void PlaceOrder_WhenAtLeastOnePlanningIsAdded_IsSuccessful()
        {
            // Arrange
            var customer = CustomerFactory.Create();

            var orderPlanningsData = new List<OrderPlanningData>();
            orderPlanningsData.Add(new OrderPlanningData(SamplePlannings.Planning1Id, 2));
            
            // Act
            customer.PlaceOrder(orderPlanningsData, "Ankaradan izmire yolculuk", DateTime.Now.AddDays(1));

            // Assert
            var orderPlaced = AssertPublishedDomainEvent<OrderPlacedEvent>(customer);
            //Assert.That(orderPlaced.Value, Is.EqualTo(MoneyValue.Of(200, "EUR")));
        }

        [Test]
        public void PlaceOrder_WhenNoPlanningIsAdded_BreaksOrderMustHaveAtLeastOnePlanningRule()
        {
            // Arrange
            var customer = CustomerFactory.Create();

            var orderPlanningsData = new List<OrderPlanningData>();

            // Assert
            AssertBrokenRule<OrderMustHaveAtLeastOnePlanningRule>(() =>
            {
                // Act
                customer.PlaceOrder(orderPlanningsData, "Ankaradan izmire yolculuk", DateTime.Now.AddDays(1));
            });
        }

        [Test]
        public void PlaceOrder_GivenTwoOrdersInThatDayAlreadyMade_BreaksCustomerCannotOrderMoreThan2OrdersOnTheSameDayRule()
        {
            // Arrange
            var customer = CustomerFactory.Create();

            var orderPlanningsData = new List<OrderPlanningData>();
            orderPlanningsData.Add(new OrderPlanningData(SamplePlannings.Planning1Id, 2));

            customer.PlaceOrder(orderPlanningsData, "TEST 1", new DateTime(2020, 1, 10, 11, 0, 0));
            customer.PlaceOrder(orderPlanningsData, "TEST 2", new DateTime(2020, 1, 10, 11, 30, 0));

            // Assert
            AssertBrokenRule<CustomerCannotOrderMoreThan2OrdersOnTheSameDayRule>(() =>
            {
                // Act
                customer.PlaceOrder(orderPlanningsData, "GENEL", new DateTime(2020, 1, 10, 12, 00, 0));
            });
        }
    }



    public class SamplePlannings
    {
        public static readonly PlanningId Planning1Id = new PlanningId(Guid.NewGuid());

        public static readonly PlanningId Planning2Id = new PlanningId(Guid.NewGuid());
    }
}