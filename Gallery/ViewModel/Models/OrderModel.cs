using Gallery.ViewModel.Helpers;
using System;

namespace Gallery.ViewModel.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public int ProfileId { get; set; }
        public int PictureId { get; set; }
        public DateTime CreatedAt { get; set; }
        public OrderStatuses OrderStatus { get; set; }
        public string OrderStatusName { get; set; }
        public string OrderStatusDataPath { get; set; }
        public bool CanCancel { get; set; }
    }
}
