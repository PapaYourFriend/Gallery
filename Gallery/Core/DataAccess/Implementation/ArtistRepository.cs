using Gallery.Core.DataAccess.Interfaces;
using Gallery.Core.DomainModels;
using System;
using System.Linq;

namespace Gallery.Core.DataAccess.Implementation
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly IGalleryDbContext _galleryDbContext;

        public ArtistRepository(IGalleryDbContext galleryDbContext)
        {
            _galleryDbContext = galleryDbContext;
        }

        public void AddArtist(Artist astist)
        {
            if(astist is null)
            {
                throw new ArgumentNullException("Artist was null");
            }

            _galleryDbContext.Artists.Add(astist);

            _galleryDbContext.SaveChanges();
        }

        public Artist GetArtistByIdOrDefault(int id)
        {
            return _galleryDbContext.Artists.FirstOrDefault(a => a.ArtistId == id);
        }
    }
}
