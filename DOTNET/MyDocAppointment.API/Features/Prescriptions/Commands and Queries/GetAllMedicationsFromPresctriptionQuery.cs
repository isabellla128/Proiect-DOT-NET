using MediatR;
using MyDocAppointment.API.Features.MedicationDosage;
using MyDocAppointment.API.Features.Medications;

namespace MyDocAppointment.API.Features.Prescriptions.Commands_and_Queries
{
    public class GetAllMedicationsFromPresctriptionQuery : IRequest<List<MedicationDosagePrescriptionDto>>
    {
        public GetAllMedicationsFromPresctriptionQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
