using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gallery.Core.DomainModels
{
    public class Style
    {
        [Key]
        public int StyleId { get; set; }
        [Required]
        public string StyleName { get; set; }

        public ICollection<Picture> Pictures { get; set; } = new List<Picture>();
    }
}
