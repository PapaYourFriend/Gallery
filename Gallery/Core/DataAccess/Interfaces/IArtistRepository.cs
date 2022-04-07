using Gallery.Core.DomainModels;

namespace Gallery.Core.DataAccess.Interfaces
{
    public interface IArtistRepository
    {
        void AddArtist(Artist astist);
        Artist GetArtistByIdOrDefault(int id);
    }
}
