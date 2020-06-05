using System.ComponentModel.DataAnnotations;
using KonnektuTask.API.Models.Request;

namespace KonnektuTask.API.Controllers.Login
{
    public class LoginRequest : SourceRequest
    {
        [Required(ErrorMessage = "User data can not be blank")]
        public LoginRequestUserData UserData { get; set; }
    }
}