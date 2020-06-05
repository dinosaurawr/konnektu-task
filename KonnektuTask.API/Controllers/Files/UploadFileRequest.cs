using System.Collections.Generic;
using KonnektuTask.API.Models.Request;

namespace KonnektuTask.API.Controllers.Files
{
    public class UploadFileRequest : SourceRequest
    {
        public UploadFileRequestUserData UserData { get; set; }
        public IEnumerable<UploadFileRequestFileData> Files { get; set; }
    }
}