using FluentValidation;

namespace SampleProject.Application.Orders.PlaceCustomerOrder
{
    public class PlaceCustomerOrderCommandValidator : AbstractValidator<PlaceCustomerOrderCommand>
    {
        public PlaceCustomerOrderCommandValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId is empty");
            RuleFor(x => x.Plannings).NotEmpty().WithMessage("Plannings list is empty");
            RuleForEach(x => x.Plannings).SetValidator(new PlanningDtoValidator());
        }
    }
}