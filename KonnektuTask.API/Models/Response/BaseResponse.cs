using System.Collections.Generic;
using System.Security.Cryptography;

namespace KonnektuTask.API.Models
{
    public class BaseResponse
    {
        public object Response { get; set; }
        public string SourceId { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public int ErrorCode { get; set; }
        public IEnumerable<FieldWarning> Warnings { get; set; } = new List<FieldWarning>();
    }
}