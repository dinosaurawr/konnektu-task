using System.ComponentModel.DataAnnotations;

namespace KonnektuTask.API.Controllers.Login
{
    public class LoginRequestUserData
    {
        [Required(ErrorMessage = "Email can't be blank")]
        [EmailAddress(ErrorMessage = "Email should be valid")]
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required(ErrorMessage = "Password can't be blank")]
        public string Password { get; set; }
    }
}