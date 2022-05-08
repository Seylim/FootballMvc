using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Entities
{
    public class Coach : IEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Firstname is can not be empty")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Lastame is can not be empty")]
        public string LastName { get; set; }
        public string Info { get; set; }

    }
}
