using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModelDB
{
    public class Form
    {
        public int FormID { get; set; }

        [Display(Name = "Название анкеты")]
        [Required]
        public string FormName { get; set; }

        public DateTime CreateDate { get; set; }
        public bool Enable { get; set; }
        public bool UnPublished { get; set; }

        [Display(Name = "Здоровье")]
        [Required]
        public string Desieses { get; set; }

        [Display(Name = "Характер")]
        [Required]
        public string Temperament { get; set; }

        [Display(Name = "Трудности")]
        [Required]
        public string Difficulties { get; set; }

        [Display(Name = "Особенности")]
        [Required]
        public string Specificity { get; set; }

        [Display(Name = "Пол")]
        [Required]
        public string Sex { get; set; }

        [Display(Name = "Окрас")]
        public string Color { get; set; }

        [Display(Name = "Возраст")]
        [Required]
        public string Age { get; set; }

        [Required]
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }

        [Display(Name = "Категория")]
        [Required]
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

        [Display(Name = "Порода")]
        [Required]
        public int SpecieID { get; set; }
        public virtual Specie Species { get; set; }

        [Display(Name = "Изображения")]
        public virtual ICollection<Image> Images { get; set; }

        public Form()
        {
            Images = new List<Image>();
            Enable = true;
            UnPublished = true;
        }
    }
}
