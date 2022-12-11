using MyDocAppointment.API.Features.Events;
using FluentValidation;

namespace MyDocAppointment.API.Validations
{
    public class EventValidator:AbstractValidator<CreateEventDto>
    {
        public EventValidator() {
            RuleFor(e => e.Name).NotNull().NotEmpty();
            RuleFor(e => e.StartDate).NotNull().NotEmpty();
            RuleFor(e => e.EndDate).NotNull().NotEmpty();
        }
    }
}
