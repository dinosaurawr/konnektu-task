using System;
using System.ComponentModel.DataAnnotations;

namespace KonnektuTask.API.ValidationAttributes
{
    public class ValidGuid : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var guidString = (string) value;
            return Guid.TryParse(guidString, out _);
        }
    }
}