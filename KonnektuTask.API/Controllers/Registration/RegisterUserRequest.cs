using System.ComponentModel.DataAnnotations;

namespace KonnektuTask.API.Models.Request
{
    public class RegisterUserRequest : SourceRequest
    {
        [Required(ErrorMessage = "User registration data cant be blank")]
        public UserDataDto UserData { get; set; }
    }
}