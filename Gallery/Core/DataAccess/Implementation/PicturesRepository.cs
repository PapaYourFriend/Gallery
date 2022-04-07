using Gallery.Core.DataAccess.Interfaces;
using Gallery.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Gallery.Core.DataAccess.Implementation
{
    public class PicturesRepository : IPicturesRepository
    {
        private readonly IGalleryDbContext _galleryDbContext;
        public PicturesRepository(IGalleryDbContext galleryDbContext)
        {
            _galleryDbContext = galleryDbContext;
        }

        public void AddPicture(Picture picture)
        {
            if (picture is null)
            {
                throw new ArgumentNullException("Picture was null");
            }

            _galleryDbContext.Pictures.Add(picture);

            _galleryDbContext.SaveChanges();
        }

        public void AddStyle(Style style)
        {
            if (style is null)
            {
                throw new ArgumentNullException("Style was null");
            }

            _galleryDbContext.Styles.Add(style);

            _galleryDbContext.SaveChanges();
        }

        public void DeletePicture(Picture picture)
        {
            if(picture is null)
            {
                throw new ArgumentNullException("Picture was null");
            }

            _galleryDbContext.Pictures.Remove(picture);

            _galleryDbContext.SaveChanges();
        }

        public List<Picture> GetAllPictures()
        {
            return _galleryDbContext.Pictures.Include(p => p.Artist).Include(p => p.Style).ToList();
        }

        public List<Style> GetAllStyles()
        {
            return _galleryDbContext.Styles.AsNoTracking().ToList();
        }

        public Picture GetPictureById(int id)
        {
            return _galleryDbContext.Pictures
                .Include(p => p.Artist)
                .Include(p => p.Style)
                .Single(p => p.PictureId == id);
        }

        public Style GetStyleByIdOrDefault(int id)
        {
            return _galleryDbContext.Styles.FirstOrDefault(s => s.StyleId == id);
        }

        public void UpdatePicture(Picture picture)
        {
            if(picture is null)
            {
                throw new ArgumentNullException("Picture was null");
            }

            _galleryDbContext.Entry(picture).State = EntityState.Modified;

            _galleryDbContext.SaveChanges();
        }
    }
}
