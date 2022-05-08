using Football.Business.Abstract;
using Football.Dtos.Request.Coach;
using Football.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FootballWeb.Controllers
{
    [Authorize(Roles = "Client,Admin")]
    public class CoachesController : Controller
    {
        private readonly ICoachService coachService;
        public CoachesController(ICoachService coachService)
        {
            this.coachService = coachService;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var coaches = await coachService.GetAllAsync();
            return View(coaches);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddCoachRequest addCoachRequest)
        {
            if (ModelState.IsValid)
            {
                var addedCoachId = await coachService.Add(addCoachRequest);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            if (await coachService.IsExists(id))
            {
                var coach = await coachService.GetById(id);
                return View(coach);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(UpdateCoachRequest updateCoachRequest)
        {
            if (ModelState.IsValid)
            {
                int affactedRowsCount = await coachService.Update(updateCoachRequest);
                if (affactedRowsCount > 0)
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
            if (await coachService.IsExists(id))
            {
                var coach = await coachService.GetById(id);
                return View(coach);
            }
            return NotFound();
        }

        [HttpPost]
        [ActionName(nameof(Delete))]
        public async Task<IActionResult> DeleteOk(int id)
        {
            await coachService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (await coachService.IsExists(id))
            {
                var coach = await coachService.GetById(id);
                return View(coach);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var coaches = await coachService.GetAllAsync();
            return View(coaches);
        }
    }
}
