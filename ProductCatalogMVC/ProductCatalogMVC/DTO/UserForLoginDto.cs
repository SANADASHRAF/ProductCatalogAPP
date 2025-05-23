using System.ComponentModel.DataAnnotations;

namespace ProductCatalogMVC.DTO
{
    public class UserForLoginDto
    {
        [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
        public string Email { get; set; }

        [Required(ErrorMessage = "كلمة المرور مطلوبة")]
        public string Password { get; set; }
    }
}
