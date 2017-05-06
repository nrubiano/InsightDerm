using AutoMapper;
using InsightDerm.Core.Data.Domain.Model;
using InsightDerm.Core.Dto;
using Microsoft.EntityFrameworkCore;

namespace InsightDerm.Core.Service
{
    public class PatientService : BaseService<Patient, PatientDto>
    {
        public PatientService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
    }
}
