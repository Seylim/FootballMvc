using Football.Business.Abstract;
using Football.Dtos.Request.Leagues;
using Football.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FootballWeb.Controllers
{
    [Authorize(Roles = "Admin,Client")]
    public class LeaguesController : Controller
    {
        private readonly ILeagueService leagueService;
        public LeaguesController(ILeagueService leagueService)
        {
            this.leagueService = leagueService;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var leagues = await leagueService.GetAllAsync();
            return View(leagues);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddLeagueRequest addLeagueRequest)
        {
            if (ModelState.IsValid)
            {
                var addedLeagueId = await leagueService.Add(addLeagueRequest);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            if (await leagueService.IsExists(id))
            {
                var league = await leagueService.GetByIdList(id);
                return View(league);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(UpdateLeagueRequest updateLeagueRequest)
        {
            if (ModelState.IsValid)
            {
                var affectedRowsCount = await leagueService.Update(updateLeagueRequest);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (await leagueService.IsExists(id))
            {
                var league = await leagueService.GetByIdList(id);
                return View(league);
            }
            return NotFound();
        }

        [HttpPost]
        [ActionName(nameof(Delete))]
        public async Task<IActionResult> DeleteOk(int id)
        {
            await leagueService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (await leagueService.IsExists(id))
            {
                var league = await leagueService.GetByIdDetails(id);
                return View(league);
            }
            return NotFound();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> List()
        {
            var leagues = await leagueService.GetAllAsync();
            return View(leagues);
        }
    }
}
