using System;
using InsightDerm.Core.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace InsightDerm.Host.Backend.Controllers
{
    public partial class ConsultationsController : ControllerBase
    {
        [HttpGet]
        [Route("{id}/diagnosis")]
        public IActionResult GetDiagnosis(Guid id)
        {
            var entities = _diagnosisService.GetAll(x => x.ConsultationId == id);

            return Ok(entities);
        }

        [HttpGet]
        [Route("{id}/diagnosis/{diagnosisId}")]
        public IActionResult GetSingleDiagnosis(Guid id, Guid diagnosisId)
        {
            if (id == Guid.Empty) return BadRequest();

            var entity = _diagnosisService.GetSingle(x => x.ConsultationId == id && x.Id == diagnosisId);

            if (entity != null)
                return Ok(entity);

            return NotFound();
        }

        [HttpPost]
        [Route("{id}/diagnosis")]
        public IActionResult PostDiagnosis(Guid id, ConsultationDiagnosisDto diagnosis)
        {
            diagnosis.ConsultationId = id;

            diagnosis = _diagnosisService.Create(diagnosis);

            return Ok(diagnosis);
        }

        [HttpPut]
        [Route("{id}/diagnosis/{diagnosisId}")]
        public IActionResult PutDiagnosis(Guid id, Guid diagnosisId, ConsultationDiagnosisDto diagnosis)
        {
            var model = _diagnosisService.GetSingle(x => x.ConsultationId == id && x.Id == diagnosisId);

            if (model == null)
                return NotFound();

            model = _mapper.Map(diagnosis, model);

            _diagnosisService.Update(model);

            return Ok(diagnosis);
        }

        [HttpPatch]
        [Route("{id}/diagnosis/{diagnosisId}")]
        public IActionResult PutDiagnosis(Guid id, Guid diagnosisId, JsonPatchDocument<ConsultationDiagnosisDto> diagnosis)
        {
            var model = _diagnosisService.GetSingle(x => x.ConsultationId == id && x.Id == diagnosisId);

            if (model == null)
                return NotFound();

            diagnosis.ApplyTo(model);

            _diagnosisService.Update(model);

            return Ok(diagnosis);
        }

        [HttpDelete]
        [Route("{id}/diagnosis/{diagnosisId}")]
        public IActionResult DeleteDiagnosis(Guid id, Guid imageId)
        {
            var model = _diagnosisService.GetSingle(x => x.ConsultationId == id && x.Id == imageId);

            if (model == null)
                return NotFound();

            _diagnosisService.Remove(model);

            return Ok();
        }
    }
}