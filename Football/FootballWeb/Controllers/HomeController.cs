using Football.Business.Abstract;
using FootballWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FootballWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILeagueService leagueService;

        public HomeController(ILogger<HomeController> logger, ILeagueService leagueService)
        {
            _logger = logger;
            this.leagueService = leagueService;
        }

        public async Task<IActionResult> Index()
        {
            var leagues = await leagueService.GetAllAsync();

            return View(leagues);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
