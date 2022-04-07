using Gallery.Core.DomainModels;
using Gallery.ViewModel.DTOs;
using Gallery.ViewModel.Helpers;
using Gallery.ViewModel.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Gallery.ServiceLayer.Interfaces
{
    public interface IOrderService
    {
        Profile GetProfileForOrder(int profileId);
        List<Order> GetAllUserOrders(int profileId);
        ObservableCollection<OrderModel> GetAllUserActiveOrders(int profileId);
        ObservableCollection<OrderModel> GetAllUserEndedOrders(int profileId);
        Task MakeOrderAsync(OrderDTO order, Profile profile, IMailService mailService);
        OrderAction GetOrderByPictureId(int pictureId);
        List<OrderModel> GetAllOrderModels();
        List<Order> GetAllOrders();
        void UpdateOrders(List<Order> orders);
        Task DeleteOrderAsync(OrderModel order, IMailService mailService);
        List<Item> GetAllOrdersStatuses();
    }
}
