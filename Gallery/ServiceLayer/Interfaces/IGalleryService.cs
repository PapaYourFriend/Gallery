using Gallery.Core.DomainModels;
using Gallery.ViewModel.DTOs;
using Gallery.ViewModel.Helpers;
using Gallery.ViewModel.Models;
using System.Collections.Generic;

namespace Gallery.ServiceLayer.Interfaces
{
    public interface IGalleryService
    {
        IEnumerable<PictureModel> GetAllPictures();
        List<Item> GetAllStyles();
        PictureModel GetPictureById(int id);
        PictureModel AddPicture(PictureDTO picture);
        bool DeletePicture(int id);
    }
}
