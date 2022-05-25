using Microsoft.AspNetCore.Mvc;

namespace Football.API.Filters
{
    public class IsExistClubAttribute : TypeFilterAttribute
    {
        public IsExistClubAttribute():base(typeof(CheckExisting))
        {

        }
    }
}
