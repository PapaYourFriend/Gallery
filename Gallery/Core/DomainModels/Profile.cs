using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gallery.Core.DomainModels
{
    public class Profile
    {
        [Key]
        public int ProfileId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string DataPath { get; set; }
        [Required]
        public bool IsDeleted { get; set; } = false;

        public ProfileSecret ProfileSecret { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
