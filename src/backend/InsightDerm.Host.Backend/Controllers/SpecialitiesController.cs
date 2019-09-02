using System;
using InsightDerm.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsightDerm.Host.Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialitiesController : ControllerBase
    {
        private readonly SpecialityService _specialityService;

        public SpecialitiesController(SpecialityService specialityService)
        {
            _specialityService = specialityService;
        }

        public IActionResult Get()
        {
            var entities = _specialityService.GetAll(null);

            return Ok(entities);
        }

        [Route("{id}")]
        public IActionResult GetSingle(Guid id)
        {
            if (id == Guid.Empty) return BadRequest();

            var entity = _specialityService.GetSingle(x => x.Id == id);

            if (entity != null)
                return Ok(entity);

            return NotFound();
        }
    }
}