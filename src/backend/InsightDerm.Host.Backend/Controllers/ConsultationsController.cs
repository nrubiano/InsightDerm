using System;
using System.Security.Claims;
using AutoMapper;
using InsightDerm.Core.Dto;
using InsightDerm.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace InsightDerm.Host.Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public partial class ConsultationsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly DoctorService _doctorService;
        private readonly ConsultationService _consultationService;
        private readonly DiagnosticImageService _diagnosticImageService;
        private readonly ConsultationDiagnosisService _diagnosisService;
        private readonly ConsultationTreatmentService _treatmentService;
        private readonly MedicalLaboratoryService _medicalLaboratoryService;

        public ConsultationsController(ConsultationService consultationService, 
                                        DiagnosticImageService diagnosticImageService,
                                        ConsultationDiagnosisService diagnosisService,
                                        ConsultationTreatmentService treatmentService,
                                        MedicalLaboratoryService medicalLaboratoryService,
                                        DoctorService doctorService,
                                        IMapper mapper)
        {
            _mapper = mapper;
            _medicalLaboratoryService = medicalLaboratoryService;
            _doctorService = doctorService;
            _treatmentService = treatmentService;
            _consultationService = consultationService;
            _diagnosticImageService = diagnosticImageService;
            _diagnosisService = diagnosisService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var entities = _consultationService.GetAll(null);

            return Ok(entities);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSingle(Guid id)
        {
            if (id == Guid.Empty) return BadRequest();

            var entity = _consultationService.GetSingle(x => x.Id == id);

            if (entity != null)
                return Ok(entity);

            return NotFound();
        }

        [HttpPost]
        public IActionResult Post(ConsultationDto consultation)
        {
            var currentUser = User.FindFirst(ClaimTypes.Name);

            if (currentUser == null) return BadRequest("The user is not authenticated");

            var currentUserId = Guid.Parse(currentUser.Value);

            var requestDoctor = _doctorService.GetSingle(x => x.UserId == currentUserId);

            consultation.CreationDate = DateTime.Now;
            consultation.RequestedById = requestDoctor.Id;

            consultation = _consultationService.Create(consultation);

            return Ok(consultation);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(Guid id, ConsultationDto consultation)
        {
            var model = _consultationService.GetSingle(x => x.Id == id);

            if (model == null)
                return NotFound();

            model = _mapper.Map(consultation, model);

            _consultationService.Update(model);

            return Ok(consultation);
        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult Put(Guid id, JsonPatchDocument<ConsultationDto> consultation)
        {
            var model = _consultationService.GetSingle(x => x.Id == id);

            if (model == null)
                return NotFound();

            consultation.ApplyTo(model);

            _consultationService.Update(model);

            return Ok(consultation);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(Guid id)
        {
            var model = _consultationService.GetSingle(x => x.Id == id);

            if (model == null)
                return NotFound();

            _consultationService.Remove(model);

            return Ok();
        }
    }
}