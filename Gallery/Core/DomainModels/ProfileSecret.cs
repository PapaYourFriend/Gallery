using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gallery.Core.DomainModels
{
    public class ProfileSecret
    {
        [Key]
        [ForeignKey("Profile")]
        public int ProfileId { get; set; }
        [Required]
        public string EmailLogin { get; set; }
        [Required]
        public string Password { get; set; }
        
        public Profile Profile { get; set; }
    }
}
