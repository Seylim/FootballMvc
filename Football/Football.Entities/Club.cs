using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Entities
{
    public class Club : IEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is can not be empty")]
        public string Name { get; set; }
        public int LeagueId { get; set; }
        public int CoachId { get; set; }
        public string LogoUrl { get; set; }
        public League League { get; set; }
        public Coach Coach { get; set; }
        public ICollection<Player> Players { get; set; }

    }
}
