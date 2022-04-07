using Gallery.Core.DomainModels;
using System.Collections.Generic;

namespace Gallery.Core.DataAccess.Interfaces
{
    public interface IPicturesRepository
    {
        List<Picture> GetAllPictures();
        void UpdatePicture(Picture picture);
        List<Style> GetAllStyles();
        Picture GetPictureById(int id);
        void AddPicture(Picture picture);
        void DeletePicture(Picture picture);
        Style GetStyleByIdOrDefault(int id);
        void AddStyle(Style style);
    }
}
