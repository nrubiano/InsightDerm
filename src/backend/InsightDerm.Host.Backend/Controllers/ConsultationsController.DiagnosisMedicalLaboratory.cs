using System;
using System.Security.Claims;
using InsightDerm.Core.Dto;
using Microsoft.AspNetCore.Mvc;

namespace InsightDerm.Host.Backend.Controllers
{
    public partial class ConsultationsController : ControllerBase
    {
        [HttpGet]
        [Route("{id}/diagnosis/{diagnosisId}/medical-laboratories")]
        public IActionResult GetDiagnosisMedicalLaboratory(Guid id, Guid diagnosisId)
        {
            var entities = _medicalLaboratoryService.GetAll(x => x.ConsultationDiagnosisId == diagnosisId);

            return Ok(entities);
        }

        [HttpGet]
        [Route("{id}/diagnosis/{diagnosisId}/medical-laboratories/{laboratoryId}")]
        public IActionResult GetSingleDiagnosisMedicalLaboratory(Guid id, Guid diagnosisId, Guid laboratoryId)
        {
            if (id == Guid.Empty) return BadRequest();

            var entity = _medicalLaboratoryService.GetAll(x => x.ConsultationDiagnosisId == diagnosisId && x.Id == laboratoryId);

            if (entity != null)
                return Ok(entity);

            return NotFound();
        }

        [HttpPost]
        [Route("{id}/diagnosis/{diagnosisId}/medical-laboratories")]
        public IActionResult PostDiagnosisMedicalLaboratory(Guid id, Guid diagnosisId, MedicalLaboratoryDto medicalLaboratory)
        {
            var currentUser = User.FindFirst(ClaimTypes.Name);

            if (currentUser == null) return BadRequest("The user is not authenticated");

            var currentUserId = Guid.Parse(currentUser.Value);

            var requestDoctor = _doctorService.GetSingle(x => x.UserId == currentUserId);

            medicalLaboratory.ConsultationDiagnosisId = diagnosisId;
            medicalLaboratory.RequestedById = requestDoctor.Id;
            medicalLaboratory.RequestedDate = DateTime.Now;

            medicalLaboratory = _medicalLaboratoryService.Create(medicalLaboratory);

            return Ok(medicalLaboratory);
        }

        [HttpDelete]
        [Route("{id}/diagnosis/{diagnosisId}/medical-laboratories/{laboratoryId}")]
        public IActionResult DeleteDiagnosisMedicalLaboratory(Guid diagnosisId, Guid laboratoryId)
        {
            var model = _medicalLaboratoryService.GetSingle(x => x.Id == laboratoryId && x.ConsultationDiagnosisId == diagnosisId);

            if (model == null)
                return NotFound();

            _medicalLaboratoryService.Remove(model);

            return Ok();
        }
    }
}