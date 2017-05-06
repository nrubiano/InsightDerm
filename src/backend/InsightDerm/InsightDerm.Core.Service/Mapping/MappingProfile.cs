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

            CreateMap<Antecedent, AntecedentDto>();
            CreateMap<AntecedentDto, Antecedent>();

            CreateMap<CurrentIllness, CurrentIllnessDto>();
            CreateMap<CurrentIllnessDto, CurrentIllness>();

            CreateMap<Doctor, DoctorDto>();
            CreateMap<DoctorDto, Doctor>();

            CreateMap<MedicalCenter, MedicalCenterDto>();
            CreateMap<MedicalCenterDto, MedicalCenter>();

            CreateMap<MedicalHistory, MedicalHistoryDto>();
            CreateMap<MedicalHistoryDto, MedicalHistory>();

            CreateMap<PatientAntecedent, PatientAntecedentDto>();
            CreateMap<PatientAntecedentDto, PatientAntecedent>();

            CreateMap<Patient, PatientDto>();
            CreateMap<PatientDto, Patient>();

            CreateMap<PhysicalExam, PhysicalExamDto>();
            CreateMap<PhysicalExamDto, PhysicalExam>();

            CreateMap<Reason, ReasonDto>();
            CreateMap<ReasonDto, Reason>();

            CreateMap<TreatmentPlan, TreatmentPlanDto>();
            CreateMap<TreatmentPlanDto, TreatmentPlan>();
        }
    }
}
