using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoctorImages.Models
{
    public class ImageModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public DateTime SaveDate { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
