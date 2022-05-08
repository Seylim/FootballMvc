﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Dtos.Request.Player
{
    public class UpdatePlayerRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Lastname is can not be empty")]
        public string LastName { get; set; }
        public string Info { get; set; }
        public int ClubId { get; set; }
    }
}
