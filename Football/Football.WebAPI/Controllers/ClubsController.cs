using Football.API.Filters;
using Football.Business.Abstract;
using Football.Dtos.Request.Club;
using Football.Dtos.Response;
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
    public class ClubsController : ControllerBase
    {
        private readonly IClubService clubService;
        private readonly IMemoryCache cache;

        public ClubsController(IClubService clubService, IMemoryCache cache)
        {
            this.clubService = clubService;
            this.cache = cache;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetClubs()
        {
            var isInCache = cache.TryGetValue("Clubs", out ICollection<ListClubResponse> cachedClubs);
            if (!isInCache)
            {
                var clubs = await clubService.GetAllAsync();

                cache.Set("Clubs", clubs, new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5)
                });
            }
            
            return Ok(cachedClubs);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetByIdList(int id)
        {
            var club = await clubService.GetByIdList(id);
            if (club != null)
            {
                return Ok(club);
            }
            return NotFound();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetByIdDetails(int id)
        {
            var club = await clubService.GetByIdList(id);
            if (club != null)
            {
                return Ok(club);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> AddClub(AddClubRequest addClubRequest)
        {
            if (ModelState.IsValid)
            {
                var lastClubId = await clubService.Add(addClubRequest);
                return CreatedAtAction(nameof(GetByIdDetails), routeValues: new { id = lastClubId }, null);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        [IsExistClub]
        public async Task<IActionResult> UpdateClub(UpdateClubRequest updateClubRequest)
        {
            if (ModelState.IsValid)
            {
                await clubService.Update(updateClubRequest);
                return Ok();
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            await clubService.Delete(id);
            return NotFound();
        }
    }
}
