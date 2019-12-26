using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ModelDB
{
    public class Specie
    {
        [Required]
        public int SpecieID { get; set; }

        [Display(Name = "Название породы")]
        [Required]
        public string SpecieName { get; set; }

        [Required]
        [Display(Name = "Категория")]
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<Form> Forms { get; set; }

        public Specie()
        {
            Forms = new List<Form>();
        }
    }
}
