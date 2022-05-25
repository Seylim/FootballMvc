using Football.Business.Abstract;
using Football.Dtos.Request.Coach;
using Football.Dtos.Response.Coaches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Football.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachesController : ControllerBase
    {
        private readonly ICoachService coachService;
        private readonly IMemoryCache cache;

        public CoachesController(ICoachService coachService, IMemoryCache cache)
        {
            this.coachService = coachService;
            this.cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> GetCaches()
        {
            var isInCache = cache.TryGetValue("Coaches", out ICollection<ListCoachResponse> cachedCoaches);
            if (!isInCache)
            {
                var coaches = await coachService.GetAllAsync();
                cachedCoaches = coaches;
                cache.Set("Coaches", coaches, new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5)
                });
            }
            
            return Ok(cachedCoaches);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var coach = await coachService.GetById(id);
            if (coach != null)
            {
                return Ok(coach);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> AddCoach(AddCoachRequest addCoachRequest)
        {
            if (ModelState.IsValid)
            {
                var lastCoachId = await coachService.Add(addCoachRequest);
                return CreatedAtAction(nameof(GetById), routeValues: new { id = lastCoachId }, null);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCoaach(UpdateCoachRequest updateCoachRequest)
        {
            if (await coachService.IsExists(updateCoachRequest.Id))
            {
                if (ModelState.IsValid)
                {
                    await coachService.Update(updateCoachRequest);
                    return Ok();
                }
                return BadRequest(ModelState);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoach(int id)
        {
            await coachService.Delete(id);
            return NotFound();
        }
    }
}
