using Football.Business.Abstract;
using Football.Entities;
using FootballWeb.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FootballWeb.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userService.ValidateUser(model.UserName, model.Password);
                if (user != null)
                {
                    List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Role, user.Role),
                    };
                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    if (Url.IsLocalUrl("/Home/Index"))
                    {
                        return Redirect("/Home/Index");
                    }
                }
                ModelState.AddModelError(key: "Login", errorMessage: "Username or password is not valid");
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            var findUser = await userService.GetByUserName(user.UserName);
            if(findUser != null)
            {
                ModelState.AddModelError(key: "Login", errorMessage: "Username is used");
            }
            else
            {
                var addedUserId = userService.Add(user);
            }
            return View(nameof(Login));
        }

        public async Task<IActionResult> Profile(string userName)
        {
            var user = await userService.GetByUserName(userName);
            return View(user);
        }

        public IActionResult AccessDeniedPath()
        {
            return View();
        }
    }
}
