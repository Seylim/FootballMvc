using Football.Business.Abstract;
using Football.Dtos.Request.Player;
using Football.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballWeb.Controllers
{
    [Authorize(Roles = "Admin,Client")]
    public class PlayersController : Controller
    {
        private readonly IPlayerService playerService;
        private readonly IClubService clubService;
        public PlayersController(IPlayerService playerService, IClubService clubService)
        {
            this.playerService = playerService;
            this.clubService = clubService;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var players = await playerService.GetAllAsync();
            return View(players);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.ClubItems = GetClubsForDropDown();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddPlayerRequest addPlayerRequest)
        {
            if (ModelState.IsValid)
            {
                var addedPlayerId = await playerService.Add(addPlayerRequest);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            if (await playerService.IsExists(id))
            {
                var player = await playerService.GetByIdList(id);
                ViewBag.ClubItems = GetClubsForDropDown();
                return View(player);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(UpdatePlayerRequest updatePlayerRequest)
        {
            if (ModelState.IsValid)
            {
                var affectedRowsCount = await playerService.Update(updatePlayerRequest);
                if (affectedRowsCount > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                return BadRequest();
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (await playerService.IsExists(id))
            {
                var player = await playerService.GetByIdList(id);
                return View(player);
            }
            return NotFound();
        }

        [HttpPost]
        [ActionName(nameof(Delete))]
        public async Task<IActionResult> DeleteOk(int id)
        {
            await playerService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (await playerService.IsExists(id))
            {
                var player = await playerService.GetByIdList(id);
                return View(player);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var players = await playerService.GetAllAsync();
            return View(players);
        }

        private List<SelectListItem> GetClubsForDropDown()
        {
            var selectedItems = new List<SelectListItem>();

            clubService.GetAll().ToList().ForEach(c => selectedItems.Add(new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }));
            return selectedItems;
        }
    }
}
