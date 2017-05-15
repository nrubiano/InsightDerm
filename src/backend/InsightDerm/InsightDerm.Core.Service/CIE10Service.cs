using AutoMapper;
using InsightDerm.Core.Data.Domain.Model;
using InsightDerm.Core.Dto;
using Microsoft.EntityFrameworkCore;

namespace InsightDerm.Core.Service
{
    public class Cie10Service : BaseService<Cie10, Cie10Dto>
    {
        public Cie10Service(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
    }
}
