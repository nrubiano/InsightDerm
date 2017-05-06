using AutoMapper;
using InsightDerm.Core.Data.Domain.Model;
using InsightDerm.Core.Dto;
using Microsoft.EntityFrameworkCore;

namespace InsightDerm.Core.Service
{
    public class PhysicalExamService : BaseService<PhysicalExam, PhysicalExamDto>
    {
        public PhysicalExamService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
    }
}
