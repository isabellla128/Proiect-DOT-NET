using FluentValidation;
using MyDocAppointment.API.Features.Patients;
using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.API.Validations
{
    public class PatientValidator:AbstractValidator<CreatePatientDto>
    {
        public PatientValidator() { 
            RuleFor(p => p.FirstName).NotNull().NotEmpty(); 
            RuleFor(p=>p.LastName).NotNull().NotEmpty();
            RuleFor(p=>p.Email).EmailAddress();
            RuleFor(p => p.Phone).NotNull().MaximumLength(12);   
        }
    }
}
