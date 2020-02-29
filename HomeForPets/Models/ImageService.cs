using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Hosting;
using ModelDB;
using ImageResizer;

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
                        //var imageName = Path.GetFileName(file.FileName);
                        //var path = Path.Combine(HostingEnvironment.MapPath("~/Content/Images"), imageName);
                        //string dbPath = "/Content/Images/" + imageName;
                        //file.SaveAs(path);

                        //Image savedImage = new Image { Path = dbPath };
                        //images.Add(savedImage);

                        var path = HostingEnvironment.MapPath("~/Content/Images");
                        
                        file.InputStream.Seek(0, SeekOrigin.Begin);
                        
                        ImageBuilder.Current.Build(
                            new ImageJob(
                                file.InputStream,
                                path + file.FileName,
                                new Instructions("width=320&height=220"),
                                false,
                                false));

                        File.Move(path + file.FileName, path + DateTime.Now.ToString());
                        string dbPath = "/Content/Images" + Path.GetFileName(file.FileName);

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

        public static Image SaveAvatar(HttpPostedFileBase file)
        {
            Image image;

            if(file != null && file.ContentLength > 0 && IsImage(file))
            {
                var imageName = Path.GetFileName(file.FileName);
                var path = Path.Combine(HostingEnvironment.MapPath("~/Content/Avatars"), imageName);
                string dbPath = "/Content/Avatars/" + imageName;
                file.SaveAs(path);

                image = new Image { Path = dbPath };
            }
            else
            {
                image = new Image { Path = "/Content/Avatars/default.jpg" };
            }

            return image;
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