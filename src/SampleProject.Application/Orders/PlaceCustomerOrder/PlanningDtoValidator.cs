using FluentValidation;
using SampleProject.Application.Plannings.GetCustomerPlannings;

namespace SampleProject.Application.Orders.PlaceCustomerOrder
{
    public class PlanningDtoValidator : AbstractValidator<PlanningDto>
    {
        public PlanningDtoValidator()
        {
            this.RuleFor(x => x.SeatCapacity).GreaterThan(0)
                .WithMessage("At least one planning has invalid seat capacity");
        }
    }
}