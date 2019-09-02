using AutoMapper;
using InsightDerm.Core.Data;
using InsightDerm.Core.Data.Domain.Model;

namespace InsightDerm.Core.Service
{
    public class ConsultationTreatmentService : BaseService<ConsultationTreatment, ConsultationTreatmentDto>
    {
        public ConsultationTreatmentService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
    }
}
