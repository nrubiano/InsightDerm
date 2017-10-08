using AutoMapper;
using InsightDerm.Core.Data;
using InsightDerm.Core.Data.Domain.Model;
using InsightDerm.Core.Dto;

namespace InsightDerm.Core.Service
{
    public class CityService : BaseService<City, CityDto>
    {
        public CityService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
    }
}
