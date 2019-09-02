using System;
using InsightDerm.Core.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace InsightDerm.Host.Backend.Controllers
{
    public partial class ConsultationsController : ControllerBase
    {
        [HttpGet]
        [Route("{id}/images")]
        public IActionResult GetDiagnosticImages()
        {
            var entities = _diagnosticImageService.GetAll(null);

            return Ok(entities);
        }

        [HttpGet]
        [Route("{id}/images/{imageId}")]
        public IActionResult GetSingleDiagnosticImage(Guid id, Guid imageId)
        {
            if (id == Guid.Empty) return BadRequest();

            var entity = _diagnosticImageService.GetSingle(x => x.ConsultationId == id && x.Id == imageId);

            if (entity != null)
                return Ok(entity);

            return NotFound();
        }

        [HttpPost]
        [Route("{id}/images")]
        public IActionResult PostDiagnosticImage(Guid id, DiagnosticImageDto diagnosticImage)
        {
            diagnosticImage.ConsultationId = id;

            diagnosticImage = _diagnosticImageService.Create(diagnosticImage);

            return Ok(diagnosticImage);
        }

        [HttpPut]
        [Route("{id}/images/{imageId}")]
        public IActionResult PutDiagnosticImage(Guid id, Guid imageId, DiagnosticImageDto diagnosticImage)
        {
            var model = _diagnosticImageService.GetSingle(x => x.ConsultationId == id && x.Id == imageId);

            if (model == null)
                return NotFound();

            model = _mapper.Map(diagnosticImage, model);

            _diagnosticImageService.Update(model);

            return Ok(diagnosticImage);
        }

        [HttpPatch]
        [Route("{id}/images/{imageId}")]
        public IActionResult PutDiagnosticImage(Guid id, Guid imageId, JsonPatchDocument<DiagnosticImageDto> diagnosticImage)
        {
            var model = _diagnosticImageService.GetSingle(x => x.ConsultationId == id && x.Id == imageId);

            if (model == null)
                return NotFound();

            diagnosticImage.ApplyTo(model);

            _diagnosticImageService.Update(model);

            return Ok(diagnosticImage);
        }

        [HttpDelete]
        [Route("{id}/images/{imageId}")]
        public IActionResult DeleteDiagnosticImage(Guid id, Guid imageId)
        {
            var model = _diagnosticImageService.GetSingle(x => x.ConsultationId == id && x.Id == imageId);

            if (model == null)
                return NotFound();

            _diagnosticImageService.Remove(model);

            return Ok();
        }
    }
}