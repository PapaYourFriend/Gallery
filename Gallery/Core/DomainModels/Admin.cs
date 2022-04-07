using System.ComponentModel.DataAnnotations;

namespace Gallery.Core.DomainModels
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
