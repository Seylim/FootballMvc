using Football.Business.Abstract;
using Football.Dtos.Request.Club;
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
    public class ClubsController : Controller
    {
        private readonly IClubService clubService;
        private readonly ILeagueService leagueService;
        private readonly ICoachService coachService;
        public ClubsController(IClubService clubService, ILeagueService leagueService, ICoachService coachService)
        {
            this.clubService = clubService;
            this.leagueService = leagueService;
            this.coachService = coachService;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var clubs = await clubService.GetAllAsync();
            return View(clubs);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.LeagueItems = GetLeagueForDropDown();
            ViewBag.CoachItems = GetCoachForDropDown();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddClubRequest addClubRequest)
        {
            if (ModelState.IsValid)
            {
                var addedClubId = await clubService.Add(addClubRequest);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            if (await clubService.IsExists(id))
            {
                var club = await clubService.GetByIdList(id);
                ViewBag.LeagueItems = GetLeagueForDropDown();
                ViewBag.CoachItems = GetCoachForDropDown();
                return View(club);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(UpdateClubRequest updateClubRequest)
        {
            if (ModelState.IsValid)
            {
                int affectedRowsCount = await clubService.Update(updateClubRequest);
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
            if (await clubService.IsExists(id))
            {
                var club = await clubService.GetByIdList(id);
                return View(club);
            }
            return NotFound();
        }

        [HttpPost]
        [ActionName(nameof(Delete))]
        public async Task<IActionResult> DeleteOk(int id)
        {
            await clubService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (await clubService.IsExists(id))
            {
                var club = await clubService.GetByIdDetails(id);
                return View(club);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var clubs = await clubService.GetAllAsync();
            return View(clubs);
        }

        private List<SelectListItem> GetCoachForDropDown()
        {
            var selectedItems = new List<SelectListItem>();

            coachService.GetAll().ToList().ForEach(c => selectedItems.Add(new SelectListItem
            {
                Text = c.FirstName + " " + c.LastName,
                Value = c.Id.ToString()
            }));
            return selectedItems;
        }

        private List<SelectListItem> GetLeagueForDropDown()
        {
            var selectedItems = new List<SelectListItem>();

            leagueService.GetAll().ToList().ForEach(l => selectedItems.Add(new SelectListItem
            {
                Text = l.Name,
                Value = l.Id.ToString()
            }));
            return selectedItems;
        }
    }
}
