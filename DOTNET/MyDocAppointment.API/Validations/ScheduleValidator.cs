using FluentValidation;
using MyDocAppointment.API.Features.Schedules;

namespace MyDocAppointment.API.Validations
{
    public class ScheduleValidator:AbstractValidator<CreateScheduleDto>
    {
        public ScheduleValidator() {
            RuleFor(s => s.StartDate).NotNull().NotEmpty();
            RuleFor(s => s.EndDate).NotNull().NotEmpty();
        }  
    }
}
