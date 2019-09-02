using System;
using InsightDerm.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsightDerm.Host.Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MaritalStatusController : ControllerBase
    {
        private readonly MaritalStatusService _maritalStatusService;

        public MaritalStatusController(MaritalStatusService maritalStatusService)
        {
            _maritalStatusService = maritalStatusService;
        }

        public IActionResult Get()
        {
            var entities = _maritalStatusService.GetAll(null);

            return Ok(entities);
        }

        [Route("{id}")]
        public IActionResult GetSingle(Guid id)
        {
            if (id == Guid.Empty) return BadRequest();

            var entity = _maritalStatusService.GetSingle(x => x.Id == id);

            if (entity != null)
                return Ok(entity);

            return NotFound();
        }
    }
}