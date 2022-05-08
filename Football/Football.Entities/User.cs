using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Fullname is can not be empty")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Username is can not be empty")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is can not be empty")]
        public string Password { get; set; }
        public string Role { get; set; }

    }
}
