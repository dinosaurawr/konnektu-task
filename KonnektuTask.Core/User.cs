using System;
using System.Linq;

namespace KonnektuTask.Core
{
    public class User
    {
        public Guid Id { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PasswordHash { get; set; }
        public bool PersonalDataAgree { get; set; }
        public bool EmailSubscribeAgree { get; set; }
        public string GenderId { get; set; }
        public string SecretKey { get; set; }
        public DateTime BirthDate { get; set; }
        public Source Source { get; set; }
    }
}