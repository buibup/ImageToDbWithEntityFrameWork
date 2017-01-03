using System.Data.Entity;
using System.Reflection.Emit;

namespace DoctorImages.Models
{
    public class ImageContext : DbContext
    {
        public ImageContext() : base("DbImage")
        {

        }

        public DbSet<ImageModel> Images { get; set; }
        
    }
}
