using System;
using System.ComponentModel.DataAnnotations;

namespace Gallery.Core.DomainModels
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        [Required]
        public string Header { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }

        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
    }
}
