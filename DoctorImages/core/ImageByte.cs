using DoctorImages.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace DoctorImages.core
{
    public class ImageByte
    {
        private List<ImageModel> imagesList = new List<ImageModel>();
        private ImageContext items = new ImageContext();
        IQueryable<ImageModel> result = null;

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
       

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        public List<ImageModel> GetImagesDirectoryToList(string pathDirectory)
        {
            DirectoryInfo d = new DirectoryInfo(pathDirectory);
            IEnumerable<FileInfo> Files = FileData.GetFilesByExtensions(d, ".jpg", ".png", ".gif");
            foreach ( FileInfo file in Files)
            {
                ImageModel image = new ImageModel();
                byte[] b = File.ReadAllBytes(file.FullName);
                image.Name = Path.GetFileNameWithoutExtension(file.Name);
                image.Data = b;
                image.SaveDate = DateTime.Now;
                image.ModifyDate = file.LastWriteTime;
                imagesList.Add(image);
            }

            return imagesList;
        }

        public IQueryable<ImageModel> GetImagesDbToList()
        {

            ImageContext context = new ImageContext();
            var images = context.Images;
            return images;
        }

    }
}
