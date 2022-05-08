using Football.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Dtos.Response
{
    public class ListClubResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public int LeagueId { get; set; }
        public League League { get; set; }
        public int CoachId { get; set; }
        public Coach Coach { get; set; }
    }
}
