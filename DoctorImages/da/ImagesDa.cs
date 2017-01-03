using DoctorImages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorImages.da
{
    public class ImagesDa
    {
        ImageContext dbContext = new ImageContext();

        public List<ImageModel> GetAllImage()
        {
            return dbContext.Images.OrderBy(t => t.Id).ToList();
        }

        public bool AddCategory(ImageModel master)
        {
            try
            {
                
                dbContext.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
