using KonnektuTask.API.ValidationAttributes;

namespace KonnektuTask.API.Controllers.Files
{
    public class UploadFileRequestUserData
    {
        [ValidGuid(ErrorMessage = "Guid must be valid")]
        public string UserId { get; set; }
        public string SecretKey { get; set; }
    }
}