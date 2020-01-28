using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using ModelDB;
using HomeForPets.Infrastructure;
using PagedList;

namespace HomeForPets.Models
{
    public class FormListViewModel : IFormView
    {
        public IPagedList<Form> Forms { get; set; }

        [Display(Name = "Категория")]
        public SelectList Categories { get; set; }

        [Display(Name = "Порода")]
        public SelectList Species { get; set; }

        public int? CategoryID { get; set; }
        public int? SpecieID { get; set; }

        [Display(Name = "Название анкеты")]
        public string Search { get; set; }

        public string Sort { get; set; }
        
        [Display(Name = "Пол")]
        public string Sex { get; set; }

        public int? CurrentCategory { get; set; }
        public int? CurrentSpecie { get; set; }
        public string CurrentSearch { get; set; }
        public string CurrentSort { get; set; }
        public string CurrentSex { get; set; }
    }
}
