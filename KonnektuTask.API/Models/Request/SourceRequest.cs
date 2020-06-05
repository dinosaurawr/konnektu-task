using System;
using System.ComponentModel.DataAnnotations;
using KonnektuTask.API.ValidationAttributes;

namespace KonnektuTask.API.Models.Request
{
    public class SourceRequest
    {
        [Required(ErrorMessage = "Source id can not be blank")]
        [ValidGuid]
        public string SourceId { get; set; }
        [Required(ErrorMessage = "Source secret key can not be blank")]
        public string SourceSecretKey { get; set; }
    }
}