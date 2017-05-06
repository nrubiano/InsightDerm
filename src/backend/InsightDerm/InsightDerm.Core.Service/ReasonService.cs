using AutoMapper;
using InsightDerm.Core.Data.Domain.Model;
using InsightDerm.Core.Dto;
using Microsoft.EntityFrameworkCore;

namespace InsightDerm.Core.Service
{
    public class ReasonService : BaseService<Reason, ReasonDto>
    {
        public ReasonService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
    }
}
