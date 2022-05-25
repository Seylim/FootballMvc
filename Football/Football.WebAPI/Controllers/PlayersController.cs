using Football.Business.Abstract;
using Football.Dtos.Request.Player;
using Football.Dtos.Response.Players;
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
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService playerService;
        private readonly IMemoryCache cache;

        public PlayersController(IPlayerService playerService, IMemoryCache cache)
        {
            this.playerService = playerService;
            this.cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlayers()
        {
            var isInCache = cache.TryGetValue("Players", out ICollection<ListPlayerResponse> cachedPlayers);
            if (!isInCache)
            {
                var players = await playerService.GetAllAsync();
                cachedPlayers = players;
                cache.Set("Players", players, new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5)
                });
            }

            return Ok(cachedPlayers);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetByIdList(int id)
        {
            var player = await playerService.GetByIdList(id);
            if (player != null)
            {
                return Ok(player);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> AddPlayer(AddPlayerRequest entity)
        {
            if (ModelState.IsValid)
            {
                var lastPlayerId = await playerService.Add(entity);
                return CreatedAtAction(nameof(GetByIdList), routeValues: new { id = lastPlayerId }, null);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlayer(UpdatePlayerRequest entity)
        {
            if (await playerService.IsExists(entity.Id))
            {
                if (ModelState.IsValid)
                {
                    await playerService.Update(entity);
                    return Ok();
                }
                return BadRequest(ModelState);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            await playerService.Delete(id);
            return NotFound();
        }
    }
}
