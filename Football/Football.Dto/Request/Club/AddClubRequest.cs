using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Dtos.Request.Club
{
    public class AddClubRequest
    {
        [Required(ErrorMessage = "Name is can not be empty")]
        public string Name { get; set; }
        public int LeagueId { get; set; }
        public int CoachId { get; set; }
        public string LogoUrl { get; set; }
    }
}
