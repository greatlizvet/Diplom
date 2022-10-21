using System.ComponentModel.DataAnnotations;
using ModelDB;

namespace HomeForPets.Models
{
    public class CreateUserViewModel : OrderForRegistrationViewModel
    {
        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}