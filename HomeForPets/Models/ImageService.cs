using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using ModelDB;

namespace HomeForPets.Models
{
    public static class ImageService
    {
        public static List<Image> SaveImage(HttpPostedFileBase[] files)
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

        private static bool IsImage(HttpPostedFileBase img)
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