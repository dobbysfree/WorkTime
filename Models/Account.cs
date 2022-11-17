using System;
using System.ComponentModel.DataAnnotations;

namespace works.Models
{
    public class Account
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Contact { get; set; }
        [Required]
        public DateTime JoinDate { get; set; }

        public string Team { get; set; }
    }
}