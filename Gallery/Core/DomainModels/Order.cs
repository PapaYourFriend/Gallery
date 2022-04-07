using System;
using System.ComponentModel.DataAnnotations;

namespace Gallery.Core.DomainModels
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }

        public int OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
        public int PictureId { get; set; }
        public Picture Picture { get; set; }
    }
}
