using Gallery.Core.DataAccess.Interfaces;
using Gallery.Core.DomainModels;
using Gallery.ServiceLayer.Interfaces;
using Gallery.ViewModel.Common;
using Gallery.ViewModel.DTOs;
using Gallery.ViewModel.Helpers;
using Gallery.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gallery.ServiceLayer.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IOrderRepository _orderRepository;

        public AuthenticationService(IProfileRepository profileRepository,
            IOrderRepository orderRepository)
        {
            _profileRepository = profileRepository;
            _orderRepository = orderRepository;
        }

        public ProfileModel GetUserByIdOrDefault(int profileId)
        {
            var tmp = _profileRepository.GetUserByIdOrDefault(profileId);

            if(tmp != null)
            {
                return new ProfileModel
                {
                    ProfileId = tmp.ProfileId,
                    DataPath = tmp.DataPath,
                    Email = tmp.ProfileSecret.EmailLogin,
                    Address = tmp.Address,
                    City = tmp.City,
                    Country = tmp.Country,
                    Name = tmp.Name,
                    Surname = tmp.Surname
                };
            }

            return null;
        }

        public string Login(LoginDTO login, ref bool check)
        {
            var user = _profileRepository.GetUserByIdOrDefault(login.Email);

            if(user is null)
            {
                var admin = _profileRepository.GetAdminByLoginOrDefault(login.Email);

                if(admin != null)
                {
                    if (HashPassword.VerifyHashed(admin.Password, login.Password))
                    {
                        check = true;
                        return $"Добро пожаловать, {admin.Login}<3";
                    }
                    else
                    {
                        return "Пароли не совпадают!";
                    }
                }

                return "Такого пользователя нет!";
            }
            else
            {
                if(user.IsDeleted)
                {
                    return "Аккаунт был удален! Пройдите регистрацию заново!";
                }
                if(HashPassword.VerifyHashed(user.ProfileSecret.Password, login.Password))
                {
                    CurrentUser.ProfileId = user.ProfileId;
                    CurrentUser.DataPath = user.DataPath;
                    return "Успешно!";
                }
                else
                {
                    return "Пароли не совпадают!";
                }
            }
        }

        public void DeleteAccount(int profileId)
        {
            var user = _profileRepository.GetUserByIdOrDefault(profileId);

            if(user is null)
            {
                throw new ArgumentNullException("Такого пользователя нет!");
            }

            var userOrders = _orderRepository.GetAllOrdersByProfileId(profileId);

            foreach(var order in userOrders)
            {
                if(order.OrderStatusId != (int)OrderStatuses.Accepted 
                    || order.OrderStatusId != (int)OrderStatuses.Abandoned)
                {
                    order.OrderStatusId = (int)OrderStatuses.Abandoned;
                }
            }

            _orderRepository.UpdateOrders(userOrders);

            user.IsDeleted = true;

            _profileRepository.UpdateUser(user);
        }

        public string Register(RegisterDTO register)
        {
            var user = _profileRepository.GetUserByIdOrDefault(register.Email);

            var hashedPassword = HashPassword.Hash(register.Password);

            if(user is null)
            {
                var profile = new Profile
                {
                    Name = register.Name,
                    DataPath = "/View/Windows/Pics/default.png",
                    ProfileSecret = new ProfileSecret
                    {
                        EmailLogin = register.Email,
                        Password = hashedPassword
                    }
                };
                try
                {
                    _profileRepository.AddUser(profile);
                }
                catch(ArgumentNullException ex)
                {
                    return ex.Message;
                }

                return "Зарегистрирован!";
            }
            else if(user.IsDeleted)
            {
                user.IsDeleted = false;
                _profileRepository.UpdateUser(user);

                return "Аккуант восстановлен!";
            }
            else
            {
                return "Пользователь с таким ником существует!";
            }
        }

        public void UpdateAccount(ProfileModel profile)
        {
            var user = _profileRepository.GetUserByIdOrDefault(profile.ProfileId);

            if (user is null)
            {
                throw new ArgumentNullException("Такого пользователя нет!");
            }

            user.Address = profile.Address;
            user.City = profile.City;
            user.Country = profile.Country;
            user.Name = profile.Name;
            user.Surname = profile.Surname;
            user.DataPath = profile.DataPath;

            _profileRepository.UpdateUser(user);
        }

        public List<Profile> GetAllUsers()
        {
            return _profileRepository.GetAllUsers().Where(u => !u.IsDeleted).ToList();
        }
    }
}
