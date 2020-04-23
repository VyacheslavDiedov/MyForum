using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MyForum.Data
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public int Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

    }
}
