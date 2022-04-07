using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gallery.Core.DomainModels
{
    public class OrderStatus
    {
        [Key]
        public int OrderStatusId { get; set; }
        [Required]
        public string OrderStatusName { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
