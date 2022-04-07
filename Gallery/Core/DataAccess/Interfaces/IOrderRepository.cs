using Gallery.Core.DomainModels;
using System.Collections.Generic;

namespace Gallery.Core.DataAccess.Interfaces
{
    public interface IOrderRepository
    {
        void AddOrder(Order order);
        List<Order> GetAllOrdersByProfileId(int profileId);
        void DeleteOrder(Order order);
        Order GetOrderByPictureIdOrDefault(int pictureId);
        List<Order> GetAllOrders();
        void UpdateOrders(List<Order> orders);
        List<OrderStatus> GetAllOrdersStatus();
    }
}
