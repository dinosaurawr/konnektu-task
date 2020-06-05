using System;

namespace KonnektuTask.Core
{
    public class UserParameter
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}