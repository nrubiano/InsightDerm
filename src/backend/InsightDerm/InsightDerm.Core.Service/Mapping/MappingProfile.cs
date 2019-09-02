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
            
            CreateMap<CityDto, City>();
            
            CreateMap<Consultation, ConsultationDto>();
            
            CreateMap<ConsultationDto, Consultation>()
                .ForMember(mn => mn.ConsultationDiagnoses, con => con.Ignore())
                .ForMember(mn => mn.DiagnosticImages, con => con.Ignore());

            CreateMap<MedicalLaboratory, MedicalLaboratoryDto>();
            
            CreateMap<MedicalLaboratoryDto, MedicalLaboratory>()
                .ForMember(mn => mn.ConsultationDiagnosis, con => con.Ignore())
                .ForMember(mn => mn.Type, con => con.Ignore())
                .ForMember(mn => mn.RequestedBy, con => con.Ignore());

            CreateMap<Speciality, SpecialityDto>();
            CreateMap<SpecialityDto, Speciality>();
            
			CreateMap<Doctor, DoctorDto>();
            CreateMap<DoctorDto, Doctor>();

            CreateMap<MedicalCenter, MedicalCenterDto>();

            CreateMap<MedicalCenterDto, MedicalCenter>()
                .ForMember(mn => mn.CityId, con => con.Condition(c => c.CityId.HasValue));
            
            CreateMap<MaritalStatus, MaritalStatusDto>();
            
            CreateMap<MaritalStatusDto, MaritalStatus>();

            CreateMap<Patient, PatientDto>();
            
            CreateMap<PatientDto, Patient>();

            CreateMap<DiagnosticImage, DiagnosticImageDto>();
            CreateMap<DiagnosticImageDto, DiagnosticImage>()
                .ForMember(mn => mn.Consultation, con => con.Ignore());
        }
    }
}
