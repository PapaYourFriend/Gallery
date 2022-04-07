using Gallery.Core.DataAccess.Interfaces;
using Gallery.Core.DomainModels;
using System.Data.Entity;

namespace Gallery.Core.DataAccess.Implementation
{
    public class GalleryDbContext : DbContext, IGalleryDbContext
    {
        public GalleryDbContext() : base("DefaultConnection")
        {

        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProfileSecret> ProfileSecrets {get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Style> Styles { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
    }
}
