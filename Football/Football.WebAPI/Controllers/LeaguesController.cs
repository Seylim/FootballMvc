using Football.Business.Abstract;
using Football.Dtos.Request.Leagues;
using Football.Dtos.Response.Leagues;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Football.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaguesController : ControllerBase
    {
        private readonly ILeagueService leagueService;
        private readonly IMemoryCache cache;
        public LeaguesController(ILeagueService leagueService, IMemoryCache cache)
        {
            this.leagueService = leagueService;
            this.cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> GetLeagues()
        {
            var isInCache = cache.TryGetValue("Leagues", out ICollection<ListLeagueResponse> cachedLeagues);
            if (!isInCache)
            {
                var leagues = await leagueService.GetAllAsync();
                cachedLeagues = leagues;
                cache.Set("Leagues", leagues, new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5)
                });
            }
            return Ok(cachedLeagues);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetByIdList(int id)
        {
            var league = await leagueService.GetByIdList(id);
            if (league != null)
            {
                return Ok(league);
            }
            return NotFound();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetByIdDetails(int id)
        {
            var league = await leagueService.GetByIdDetails(id);
            if (league != null)
            {
                return Ok(league);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> AddLeague(AddLeagueRequest addLeagueRequest)
        {
            if (ModelState.IsValid)
            {
                var lastLeagueId = await leagueService.Add(addLeagueRequest);
                return CreatedAtAction(nameof(GetByIdDetails), routeValues: new { id = lastLeagueId }, null);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLeague(UpdateLeagueRequest updateLeagueRequest)
        {
            if (await leagueService.IsExists(updateLeagueRequest.Id))
            {
                if (ModelState.IsValid)
                {
                    await leagueService.Update(updateLeagueRequest);
                    return Ok();
                }
                return BadRequest(ModelState);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeague(int id)
        {
            await leagueService.Delete(id);
            return NotFound();
        }
    }
}
