using System;
using InsightDerm.Core.Data.Domain.Model;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace InsightDerm.Host.Backend.Controllers
{
    public partial class ConsultationsController : ControllerBase
    {
        [HttpGet]
        [Route("{id}/diagnosis/{diagnosisId}/treatments")]
        public IActionResult GetTreatments(Guid id, Guid diagnosisId)
        {
            var entities = _treatmentService.GetAll(x => x.ConsultationDiagnosisId == diagnosisId);

            return Ok(entities);
        }

        [HttpGet]
        [Route("{id}/diagnosis/{diagnosisId}/treatments/{treatmentId}")]
        public IActionResult GetSingleTreatments(Guid id, Guid diagnosisId, Guid treatmentId)
        {
            if (id == Guid.Empty) return BadRequest();

            var entity = _treatmentService.GetSingle(x => x.ConsultationDiagnosisId == diagnosisId && x.Id == treatmentId);

            if (entity != null)
                return Ok(entity);

            return NotFound();
        }

        [HttpPost]
        [Route("{id}/diagnosis/{diagnosisId}/treatments")]
        public IActionResult PostTreatment(Guid id, Guid diagnosisId, ConsultationTreatmentDto treatment)
        {
            treatment.ConsultationDiagnosisId = diagnosisId;

            treatment = _treatmentService.Create(treatment);

            return Ok(treatment);
        }

        [HttpPut]
        [Route("{id}/diagnosis/{diagnosisId}/treatments/{treatmentId}")]
        public IActionResult PutTreatment(Guid id, Guid diagnosisId, Guid treatmentId, ConsultationTreatmentDto treatment)
        {
            var model = _treatmentService.GetSingle(x => x.ConsultationDiagnosisId == diagnosisId && x.Id == treatmentId);

            if (model == null)
                return NotFound();

            model = _mapper.Map(treatment, model);

            _treatmentService.Update(model);

            return Ok(treatment);
        }

        [HttpPatch]
        [Route("{id}/diagnosis/{diagnosisId}/treatments/{treatmentId}")]
        public IActionResult PatchTreatment(Guid id, Guid diagnosisId, Guid treatmentId, JsonPatchDocument<ConsultationTreatmentDto> treatment)
        {
            var model = _treatmentService.GetSingle(x => x.ConsultationDiagnosisId == diagnosisId && x.Id == treatmentId);

            if (model == null)
                return NotFound();

            treatment.ApplyTo(model);

            _treatmentService.Update(model);

            return Ok(treatment);
        }

        [HttpDelete]
        [Route("{id}/diagnosis/{diagnosisId}/treatments/{treatmentId}")]
        public IActionResult DeleteDiagnosis(Guid id, Guid diagnosisId, Guid treatmentId)
        {
            var model = _treatmentService.GetSingle(x => x.ConsultationDiagnosisId == diagnosisId && x.Id == treatmentId);

            if (model == null)
                return NotFound();

            _treatmentService.Remove(model);

            return Ok();
        }
    }
}