using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyForum.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Електронна адреса")]
        public string Email { get; set; }

        [Required] [Display(Name = "Ім'я")] public string FirstName { get; set; }

        [Required] [Display(Name = "Прізвище")] public string SecondName { get; set; }

        [Required]
        [Display(Name = "Дата народження")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Країна")] public string Country { get; set; }
        [Display(Name = "Місто")] public string City { get; set; }
        [Display(Name = "Телефон")] [Phone] public string NumberPhone { get; set; }
        [Display(Name = "Про мене")] public string AboutMe { get; set; }
        [Required] [Display(Name = "Стать")] public int IdGender { get; set; }

        [RequiredAttribute]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        [DataType(DataType.Password)]
        [Display(Name = "Підтвердити пароль*")]
        public string PasswordConfirm { get; set; }
    }
}
