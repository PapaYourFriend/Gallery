using System;
using System.ComponentModel.DataAnnotations;

namespace Gallery.Core.DomainModels
{
    public class Picture
    {
        [Key]
        public int PictureId { get; set; }
        [Required]
        public string PictureName { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Location { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
        [Required]
        public string DataPath { get; set; }
        [Required]
        public int SizeX { get; set; }

        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
        public int StyleId { get; set; }
        public Style Style { get; set; }
    }
}
