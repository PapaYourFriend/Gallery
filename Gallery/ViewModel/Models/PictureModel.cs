using System;

namespace Gallery.ViewModel.Models
{
    public class PictureModel
    {
        public int PictureId { get; set; }
        public string PictureName { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
        public string DataPath { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public string ArtistName { get; set; }
        public string ArtistSurname { get; set; }
        public string StyleName { get; set; }
        public bool IsOrdered { get; set; } = false;
    }
}
