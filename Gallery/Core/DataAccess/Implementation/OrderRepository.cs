using Gallery.Core.DataAccess.Interfaces;
using Gallery.Core.DomainModels;
using Gallery.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Gallery.Core.DataAccess.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IGalleryDbContext _galleryDbContext;

        public OrderRepository(IGalleryDbContext galleryDbContext)
        {
            _galleryDbContext = galleryDbContext;
        }

        public void AddOrder(Order order)
        {
            if(order is null)
            {
                throw new ArgumentNullException("Order was null");
            }

            _galleryDbContext.Orders.Add(order);

            _galleryDbContext.SaveChanges();
        }

        public void DeleteOrder(Order order)
        {
            order.OrderStatusId = (int)OrderStatuses.Abandoned;

            _galleryDbContext.SaveChanges();
        }

        public List<Order> GetAllOrders()
        {
            return _galleryDbContext.Orders.Include(o => o.OrderStatus).ToList();
        }

        public List<Order> GetAllOrdersByProfileId(int profileId)
        {
            return _galleryDbContext.Orders
                .Include(o => o.Profile)
                .Include(o => o.OrderStatus)
                .Where(o => o.ProfileId == profileId)
                .ToList();
        }

        public List<OrderStatus> GetAllOrdersStatus()
        {
            return _galleryDbContext.OrderStatuses.ToList();
        }

        public Order GetOrderByPictureIdOrDefault(int pictureId)
        {
            return _galleryDbContext.Orders.Where(o => o.PictureId == pictureId && o.OrderStatusId != (int)OrderStatuses.Abandoned).FirstOrDefault();
        }

        public void UpdateOrders(List<Order> orders)
        {
            _galleryDbContext.SaveChanges();
        }
    }
}
