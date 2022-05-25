using Football.Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Football.API.Filters
{
    public class CheckExisting : IAsyncActionFilter
    {
        private readonly IClubService clubService;

        public CheckExisting(IClubService clubService)
        {
            this.clubService = clubService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idExist = context.ActionArguments.ContainsKey("id");
            if (!idExist)
            {
                context.Result = new BadRequestObjectResult(new { message = $"id parametresi yok!" });
            }
            else
            {
                var id = (int)context.ActionArguments["id"];
                if (await clubService.IsExists(id))
                {
                    await next.Invoke();
                }
                context.Result = new BadRequestObjectResult(new { message = $"{id} id'li kulüp bulunamadı." });
            }
        }
    }
}
