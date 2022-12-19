using System.ComponentModel.DataAnnotations;

namespace SiteManagement.UI.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Kullanıcı Adı boş geçilemez")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifre boş geçilemez")]
        public string Password { get; set; }
    }
}
