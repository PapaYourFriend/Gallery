using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gallery.Core.DomainModels
{
    public class Artist
    {
        [Key]
        public int ArtistId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }

        public ICollection<Picture> Pictures { get; set; } = new List<Picture>();
    }
}
