using System.ComponentModel.DataAnnotations;

namespace Football.WebAPI.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Kullanıcı Adı ......")]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre Boş Olamaz ......")]
        public string Password { get; set; }
    }
}
