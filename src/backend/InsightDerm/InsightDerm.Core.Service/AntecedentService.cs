using AutoMapper;
using InsightDerm.Core.Data.Domain.Model;
using InsightDerm.Core.Dto;
using Microsoft.EntityFrameworkCore;

namespace InsightDerm.Core.Service
{
    public class AntecedentService : BaseService<Antecedent, AntecedentDto>
    {
        public AntecedentService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
    }
}
