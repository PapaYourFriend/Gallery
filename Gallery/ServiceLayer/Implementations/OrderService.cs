using Gallery.Core.DataAccess.Interfaces;
using Gallery.Core.DomainModels;
using Gallery.ServiceLayer.Interfaces;
using Gallery.ViewModel.Common;
using Gallery.ViewModel.DTOs;
using Gallery.ViewModel.Helpers;
using Gallery.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Gallery.ServiceLayer.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IPicturesRepository _picturesRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderService(IPicturesRepository picturesRepository,
            IProfileRepository profileRepository,
            IOrderRepository orderRepository)
        {
            _picturesRepository = picturesRepository;
            _profileRepository = profileRepository;
            _orderRepository = orderRepository;
        }

        public async Task DeleteOrderAsync(OrderModel order, IMailService mailService)
        {
            var delOrder = _orderRepository.GetOrderByPictureIdOrDefault(order.PictureId);

            var user = _profileRepository.GetUserByIdOrDefault(order.ProfileId);

            if(delOrder != null)
            {
                order.OrderStatus = OrderStatuses.Abandoned;
                order.OrderStatusDataPath = "/View/Windows/Pics/denied.png";
                order.CanCancel = false;
                _orderRepository.DeleteOrder(delOrder);

                await mailService.SendAsync("Отмена заказа", $"Вы отменили заказ {order.OrderId}", user.ProfileSecret.EmailLogin);
            }
        }
        public List<Order> GetAllOrders()
        {
            return _orderRepository.GetAllOrders();
        }
        public List<OrderModel> GetAllOrderModels()
        {
            var orders = _orderRepository.GetAllOrders();

            var result = new List<OrderModel>();

            foreach (var order in orders)
            {
                string img = "";
                bool cancel = true;
                if (order.OrderStatusId == (int)OrderStatuses.InProcessing)
                {
                    img = "/View/Windows/Pics/work-process.png";
                }
                else if (order.OrderStatusId == (int)OrderStatuses.Packaging)
                {
                    img = "/View/Windows/Pics/package.png";
                }
                else if(order.OrderStatusId == (int)OrderStatuses.OnTheWay)
                {
                    img = "/View/Windows/Pics/delivery.png";
                }
                else if (order.OrderStatusId == (int)OrderStatuses.Abandoned)
                {
                    img = "/View/Windows/Pics/denied.png";
                    cancel = false;
                }
                else
                {
                    img = "/View/Windows/Pics/delivered.png";
                    cancel = false;
                }

                result.Add(new OrderModel
                {
                    OrderId = order.OrderId,
                    ProfileId = order.ProfileId,
                    PictureId = order.PictureId,
                    OrderStatus = (OrderStatuses)order.OrderStatusId,
                    CreatedAt = order.CreatedAt,
                    OrderStatusDataPath = img,
                    OrderStatusName = order.OrderStatus.OrderStatusName,
                    CanCancel = cancel
                });
            }


            return result; 
        }

        public ObservableCollection<OrderModel> GetAllUserActiveOrders(int profileId)
        {
            var tmp = _orderRepository.GetAllOrdersByProfileId(profileId).Where(
                o => o.OrderStatusId == (int)OrderStatuses.InProcessing
                || o.OrderStatusId == (int)OrderStatuses.Packaging
                || o.OrderStatusId == (int)OrderStatuses.OnTheWay);

            var result = new List<OrderModel>();

            foreach (var order in tmp)
            {
                string img = "";
                
                if (order.OrderStatusId == (int)OrderStatuses.InProcessing)
                {
                    img = "/View/Windows/Pics/work-process.png";
                }
                else if (order.OrderStatusId == (int)OrderStatuses.Packaging)
                {
                    img = "/View/Windows/Pics/package.png";
                }
                else
                {
                    img = "/View/Windows/Pics/delivery.png";
                }
                

                result.Add(new OrderModel
                {
                    OrderId = order.OrderId,
                    ProfileId = order.ProfileId,
                    PictureId = order.PictureId,
                    OrderStatus = (OrderStatuses)order.OrderStatusId,
                    CreatedAt = order.CreatedAt,
                    OrderStatusDataPath = img,
                    OrderStatusName = order.OrderStatus.OrderStatusName
                });
            }

            return new ObservableCollection<OrderModel>(result);
        }

        public ObservableCollection<OrderModel> GetAllUserEndedOrders(int profileId)
        {
            var tmp = _orderRepository.GetAllOrdersByProfileId(profileId).Where(
                o => o.OrderStatusId == (int)OrderStatuses.Abandoned
                || o.OrderStatusId == (int)OrderStatuses.Accepted);

            var result = new List<OrderModel>();

            foreach(var order in tmp)
            {
                string img = "";
                if(order.OrderStatusId == (int)OrderStatuses.Abandoned)
                {
                    img = "/View/Windows/Pics/denied.png";
                }
                else
                {
                    img = "/View/Windows/Pics/delivered.png";
                }

                result.Add(new OrderModel
                {
                    OrderId = order.OrderId,
                    ProfileId = order.ProfileId,
                    PictureId= order.PictureId,
                    OrderStatus = (OrderStatuses)order.OrderStatusId,
                    CreatedAt = order.CreatedAt,
                    OrderStatusDataPath = img,
                    OrderStatusName= order.OrderStatus.OrderStatusName
                });
            }

            return new ObservableCollection<OrderModel>(result);
        }

        public List<Order> GetAllUserOrders(int profileId)
        {
            return _orderRepository.GetAllOrdersByProfileId(profileId);
        }

        public OrderAction GetOrderByPictureId(int pictureId)
        {
            var order = _orderRepository.GetOrderByPictureIdOrDefault(pictureId);

            if(order is null)
            {
                return new OrderAction { Unordered = true };
            }

            return order.ProfileId == CurrentUser.ProfileId ? new OrderAction { MyOrder = true } : new OrderAction { NotMyOrder = true };
        }

        public Profile GetProfileForOrder(int profileId)
        {
            var profile = _profileRepository.GetUserByIdOrDefault(profileId);

            return profile is null ? throw new ArgumentNullException("Такого пользователя нет") : profile;
        }

        public async Task MakeOrderAsync(OrderDTO order, Profile profile, IMailService mailService)
        {
            if (order.Picture.IsOrdered)
            {
                throw new ArgumentException("Заказ уже был сделан!");
            }
            profile.Address = order.Address;
            profile.City = order.City;
            profile.Country = order.Country;

            _profileRepository.UpdateUser(profile);

            order.Picture.IsOrdered = true;

            Order newOrder = new Order
            {
                CreatedAt = DateTime.Now,
                PictureId = order.Picture.PictureId,
                ProfileId = profile.ProfileId,
                OrderStatusId = 1
            };

            _orderRepository.AddOrder(newOrder);

            await mailService.SendAsync("Заказ картины", $"Вы заказали картину {order.Picture.PictureName}. Заказ в обработке", profile.ProfileSecret.EmailLogin);
        }

        public void UpdateOrders(List<Order> orders)
        {
            _orderRepository.UpdateOrders(orders);
        }

        public List<Item> GetAllOrdersStatuses()
        {
            var statuses = _orderRepository.GetAllOrdersStatus();

            var result = new List<Item>();

            foreach (var stat in statuses)
            {
                result.Add(new Item
                {
                    Status = stat.OrderStatusName
                });
            }

            return result;
        }
    }
}
