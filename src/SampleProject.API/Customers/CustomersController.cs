using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using SampleProject.Application.Customers;
using SampleProject.Application.Customers.RegisterCustomer;
using SampleProject.Application.Customers.RegisterFreeSeatPlace;

namespace SampleProject.API.Customers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : Controller
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// Register customer.
        /// </summary>
        [Route("")]
        [HttpPost]
        [ProducesResponseType(typeof(CustomerDto), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> RegisterCustomer([FromBody]RegisterCustomerRequest request)
        {
           var customer = await _mediator.Send(new RegisterCustomerCommand(request.Email, request.Name));

           return Created(string.Empty, customer);
        }

        /// <summary>
        /// Find Free Place for order
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("RegisterFreeSeatPlace")]
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> RegisterFreeSeatPlace([FromBody]RegisterFreeSeatPlaceRequest request)
        {
            var result = await _mediator.Send(new RegisterFreeSeatPlaceCommand(request.OrderId));

            if (!result.HasValue)
                return Ok("Order Id is not match or missing in the system.");
            else
                return Ok(result.Value ? "Success, 1 free place reserved for you!" : "Sorry, There is no free sit place.");
        }
    }
}
