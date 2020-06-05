using System;
using System.ComponentModel.DataAnnotations;
using KonnektuTask.API.Tools;

namespace KonnektuTask.API.Models.Request
{
    public class UserDataDto
    {
        [Required(ErrorMessage = "Name can't be blank")]
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Email can't be blank")]
        [EmailAddress(ErrorMessage = "Email should be valid")]
        public string Email { get; set; }
        public string Phone { get; set; }
        [StringLength(50, ErrorMessage = "Password length must be at least {2} symbols long", MinimumLength = 6)]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [IsTrue(ErrorMessage = "You should agree to personal data policy")]
        [Required(ErrorMessage = "You should agree to personal data policy")]
        public bool PersonalDataAgree { get; set; }
        public bool EmailSubscribeAgree { get; set; }
        public string GenderId { get; set; }
        public DateTime BirthDate { get; set; }
    }
}