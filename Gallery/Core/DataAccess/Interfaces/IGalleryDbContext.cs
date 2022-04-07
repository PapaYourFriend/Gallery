using Gallery.Core.DomainModels;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace Gallery.Core.DataAccess.Interfaces
{
    public interface IGalleryDbContext
    {
        DbSet<Profile> Profiles { get; set; }
        DbSet<Admin> Admins { get; set; }
        DbSet<ProfileSecret> ProfileSecrets { get; set; }
        DbSet<Artist> Artists { get; set; }
        DbSet<Picture> Pictures { get; set; }
        DbSet<Review> Reviews { get; set; }
        DbSet<OrderStatus> OrderStatuses { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Style> Styles { get; set; }
        int SaveChanges();
        DbEntityEntry Entry(object entity);
    }
}
