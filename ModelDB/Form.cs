using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.IO;

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

        [Display(Name = "Описание")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Возраст")]
        public string Age { get; set; }

        public int ProfileID { get; set; }
        //public virtual Profile Profile { get; set; }

        [Display(Name = "Категория")]
        [Required]
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

        [Display(Name = "Вид")]
        [Required]
        public int SpecieID { get; set; }
        public virtual Specie Species { get; set; }

        [Display(Name = "Изображения")]
        public virtual ICollection<Image> Images { get; set; }

        public Form()
        {
            Images = new List<Image>();
            Enable = true;
        }

        public List<Image> SaveImage(HttpPostedFileBase[] files)
        {
            List<Image> images = new List<Image>();

            foreach (var file in files)
            {
                if (file != null)
                {
                    if (file.ContentLength > 0 && IsImage(file))
                    {
                        var imageName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Content/Images"), imageName);
                        string dbPath = "/Content/Images/" + imageName;
                        file.SaveAs(path);

                        Image savedImage = new Image { Path = dbPath };
                        images.Add(savedImage);
                    }
                }
                else
                {
                    images.Add(new Image { Path = "/Content/Images/default.jpg" });
                }
            }

            return images;
        }

        private bool IsImage(HttpPostedFileBase img)
        {
            switch (img.ContentType)
            {
                case "image/jpeg":
                    return true;
                case "image/png":
                    return true;
                default:
                    return false;
            }
        }
    }
}
