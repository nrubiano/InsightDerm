using AutoMapper;
using InsightDerm.Core.Data.Domain.Model;
using InsightDerm.Core.Dto;

namespace InsightDerm.Core.Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<City, CityDto>();

			CreateMap<Doctor, DoctorDto>();

            CreateMap<MedicalCenter, MedicalCenterDto>();
        }
    }
}
