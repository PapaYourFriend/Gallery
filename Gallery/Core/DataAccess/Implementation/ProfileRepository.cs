using Gallery.Core.DataAccess.Interfaces;
using Gallery.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Gallery.Core.DataAccess.Implementation
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly IGalleryDbContext _galleryDbContext;

        public ProfileRepository(IGalleryDbContext galleryDbContext)
        {
            _galleryDbContext = galleryDbContext;
        }

        public void AddUser(Profile profile)
        {
            if(profile is null)
            {
                throw new ArgumentNullException("Profile was null");
            }

            _galleryDbContext.Profiles.Add(profile);

            _galleryDbContext.SaveChanges();
        }

        public Admin GetAdminByLoginOrDefault(string login)
        {
            return _galleryDbContext.Admins.FirstOrDefault(x => x.Login == login);
        }

        public List<Profile> GetAllUsers()
        {
            return _galleryDbContext.Profiles.Include(p => p.ProfileSecret).ToList();
        }

        public Profile GetUserByIdOrDefault(string email)
        {
            if(email is null || string.IsNullOrWhiteSpace(email))
            {
                return null;
            }

            return _galleryDbContext.Profiles.Include(p => p.ProfileSecret).SingleOrDefault(p => p.ProfileSecret.EmailLogin == email);
        }
        public Profile GetUserByIdOrDefault(int id)
        {
            return _galleryDbContext.Profiles.Include(p => p.ProfileSecret).SingleOrDefault(p => p.ProfileId == id);
        }

        public void UpdateUser(Profile profile)
        {
            if(profile is null)
            {
                throw new ArgumentNullException("Profile was null");
            }

            _galleryDbContext.Entry(profile).State = EntityState.Modified;

            _galleryDbContext.SaveChanges();
        }
    }
}
