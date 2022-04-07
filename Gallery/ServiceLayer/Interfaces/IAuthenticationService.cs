using Gallery.Core.DomainModels;
using Gallery.ViewModel.DTOs;
using Gallery.ViewModel.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gallery.ServiceLayer.Interfaces
{
    public interface IAuthenticationService
    {
        string Login(LoginDTO login, ref bool check);
        string Register(RegisterDTO register);
        void DeleteAccount(int profileId);
        void UpdateAccount(ProfileModel profile);
        ProfileModel GetUserByIdOrDefault(int profileId);
        List<Profile> GetAllUsers();
    }
}
