using System.ComponentModel.DataAnnotations;
using ModelDB;

namespace HomeForPets.Models
{
    public class CreateUserViewModel
    {
        public OrderForRegistration OrderForRegistration { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}