using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MyForum.Data
{
    public class User : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string SecondName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PhotoName { get; set; }
        public string AboutMe { get; set; }
        [Required]
        public DateTime RegisterDate { get; set; }
        public int IdGender { get; set; }
        public virtual Gender Gender { get; set; }

    }
}
