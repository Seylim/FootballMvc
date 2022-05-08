﻿using Football.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Dtos.Response.Leagues
{
    public class DetailsLeagueResponse
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is can not be empty")]
        public string Name { get; set; }
        public string Country { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<Club> Clubs { get; set; }
    }
}
