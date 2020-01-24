using System.ComponentModel.DataAnnotations;

namespace ModelDB
{
    public class OrderForRegistration
    {
        public int OrderForRegistrationID { get; set; }

        [Display(Name = "Имя пользователя")]
        [Required]
        public string OrganizationName { get; set; }

        [Display(Name = "Номер телефона")]
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Адрес электронной почты")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Город")]
        [Required]
        public string City { get; set; }

        public int ImageID { get; set; }
        [Display(Name = "Изображение пользователя")]
        public virtual Image Image { get; set; }
    }
}
