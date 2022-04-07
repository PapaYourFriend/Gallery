using Gallery.Core.DomainModels;
using System.Collections.Generic;

namespace Gallery.Core.DataAccess.Interfaces
{
    public interface IProfileRepository
    {
        Profile GetUserByIdOrDefault(string email);
        Profile GetUserByIdOrDefault(int id);
        Admin GetAdminByLoginOrDefault(string login);
        List<Profile> GetAllUsers();
        void AddUser(Profile profile);
        void UpdateUser(Profile profile);
    }
}
