using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using SampleProject.Application.Orders.ChangeCustomerOrder;
using SampleProject.Application.Orders.GetCustomerOrderDetails;
using SampleProject.Application.Orders.GetCustomerOrders;
using SampleProject.Application.Orders.PlaceCustomerOrder;
using SampleProject.Application.Orders.RemoveCustomerOrder;
using SampleProject.Application.Orders.UpdateStatusOrder;
using SampleProject.Application.Plannings.GetCustomerPlannings;

namespace SampleProject.API.Orders
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerOrdersController : Controller
    {
        private readonly IMediator _mediator;

        public CustomerOrdersController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// Get customer orders.
        /// </summary>
        /// <param name="customerId">Customer ID.</param>
        /// <returns>List of customer orders.</returns>
        [Route("{customerId}/orders")]
        [HttpGet]
        [ProducesResponseType(typeof(List<OrderDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCustomerOrders(Guid customerId)
        {
            var orders = await _mediator.Send(new GetCustomerOrdersQuery(customerId));

            return Ok(orders);
        }

        /// <summary>
        /// Get customer order details.
        /// </summary>
        /// <param name="orderId">Order ID.</param>
        [Route("{customerId}/orders/{orderId}")]
        [HttpGet]
        [ProducesResponseType(typeof(OrderDetailsDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCustomerOrderDetails(
            [FromRoute]Guid orderId)
        {
            var orderDetails = await _mediator.Send(new GetCustomerOrderDetailsQuery(orderId));

            return Ok(orderDetails);
        }


        /// <summary>
        /// Add customer order. (target, source, ...)
        /// </summary>
        /// <param name="customerId">Customer ID.</param>
        /// <param name="request">Planning list.</param>
        [Route("{customerId}/orders")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddCustomerOrder(
            [FromRoute]Guid customerId, 
            [FromBody]CustomerOrderRequest request)
        {
           await _mediator.Send(new PlaceCustomerOrderCommand(customerId, request.Plannings, request.Description, request.OrderDate));

           return Created(string.Empty, null);
        }

        /// <summary>
        /// Change customer order.
        /// </summary>
        /// <param name="customerId">Customer ID.</param>
        /// <param name="orderId">Order ID.</param>
        /// <param name="request">List of plannings.</param>
        [Route("{customerId}/orders/{orderId}")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> ChangeCustomerOrder(
            [FromRoute]Guid customerId, 
            [FromRoute]Guid orderId,
            [FromBody]CustomerOrderRequest request)
        {
            await _mediator.Send(new ChangeCustomerOrderCommand(customerId, orderId, request.Plannings));

            return Ok();
        }

        /// <summary>
        /// Remove customer order.
        /// </summary>
        /// <param name="customerId">Customer ID.</param>
        /// <param name="orderId">Order ID.</param>
        [Route("{customerId}/orders/{orderId}")]
        [HttpDelete]
        [ProducesResponseType(typeof(List<OrderDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RemoveCustomerOrder(
            [FromRoute]Guid customerId,
            [FromRoute]Guid orderId)
        {
            await _mediator.Send(new RemoveCustomerOrderCommand(customerId, orderId));

            return Ok();
        }


        /// <summary>
        /// Get Customer Planning (Target or Source)
        /// </summary>
        /// <param name="target"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        [Route("{customerId}/plannings")]
        [HttpGet]
        [ProducesResponseType(typeof(List<PlanningDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCustomerPlannings(string target, string source)
        {
            var orders = await _mediator.Send(new GetCustomerPlanningsQuery(target, source));

            return Ok(orders);
        }

        /// <summary>
        /// Change Status (Waiting = 0, Published = 1, UnPublished = 2)
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("{customerId}/plannings/{orderId}")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> ChangeStatusOrder(
           [FromRoute] Guid orderId,
           [FromBody] CustomerOrderStatusRequest request)
        {
            if (System.Enum.IsDefined(typeof(Domain.Customers.Orders.OrderStatus), request.StatusId))
            {
                await _mediator.Send(new UpdateStatusOrderCommand(orderId, request.StatusId));
            }
            else
            {
                return Ok("Expected Error for unknown enum types");
            }

            return Ok();
        }

    }
}
