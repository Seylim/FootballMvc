using Football.Business.Abstract;
using Football.Entities;
using Football.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Football.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        public UsersController(IUserService  userService)
        {
            this.userService = userService;
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await userService.GetById(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                var lastUserId = await userService.Add(user);
                return CreatedAtAction(nameof(GetById), routeValues: new { id = lastUserId }, null);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(User user)
        {
            if (await userService.IsExists(user.Id))
            {
                if (ModelState.IsValid)
                {
                    await userService.Update(user);
                    return Ok();
                }
                return BadRequest();
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await userService.Delete(id);
            return NotFound();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            var user = await userService.ValidateUser(model.UserName, model.Password);
            if (user != null)
            {
                //Claim
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role),
                };

                //Gizli cümle
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Burası çok gizli bölge"));
                var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //Token
                var token = new JwtSecurityToken(
                    issuer: "turkcell.com.tr",
                    audience: "turkcell.com.tr",
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: credential
                    );

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            return BadRequest();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByUserName(string userName)
        {
            var user = await userService.GetByUserName(userName);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }
    }
}
