using AutoMapper;
using InsightDerm.Core.Data;
using InsightDerm.Core.Data.Domain.Model;
using InsightDerm.Core.Dto;

namespace InsightDerm.Core.Service
{
    public class DoctorService : BaseService<Doctor, DoctorDto>
    {
        public DoctorService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
    }
}
