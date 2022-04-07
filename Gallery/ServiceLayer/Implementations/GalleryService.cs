using Gallery.Core.DataAccess.Interfaces;
using Gallery.Core.DomainModels;
using Gallery.ServiceLayer.Interfaces;
using Gallery.ViewModel.DTOs;
using Gallery.ViewModel.Helpers;
using Gallery.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Gallery.ServiceLayer.Implementations
{
    public class GalleryService : IGalleryService
    {
        private readonly IPicturesRepository _picturesRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IArtistRepository _artistRepository;

        public GalleryService(IPicturesRepository picturesRepository,
            IOrderRepository orderRepository,
            IArtistRepository artistRepository)
        {
            _picturesRepository = picturesRepository;
            _orderRepository = orderRepository;
            _artistRepository = artistRepository;
        }

        public PictureModel AddPicture(PictureDTO picture)
        {
            if(picture is null)
            {
                throw new ArgumentNullException("Picture was null");
            }
            var picToAdd = new Picture();

            if(picture.ExistArtist)
            {
                var artist = _artistRepository.GetArtistByIdOrDefault(picture.ArtistId);
                if(artist is null)
                {
                    throw new ArgumentNullException("Artist was null");
                }
                picToAdd.ArtistId = artist.ArtistId;
            }
            else
            {
                var artist = new Artist
                {
                    Name = picture.ArtistName,
                    Surname = picture.ArtistSurname
                };
                _artistRepository.AddArtist(artist);

                picToAdd.ArtistId = artist.ArtistId;
            }
            if(picture.ExistStyle)
            {
                var style = _picturesRepository.GetStyleByIdOrDefault(picture.StyleId);
                if (style is null)
                {
                    throw new ArgumentNullException("Style was null");
                }
                picToAdd.StyleId = style.StyleId;
            }
            else
            {
                var style = new Style
                {
                    StyleName = picture.StyleName
                };
                _picturesRepository.AddStyle(style);

                picToAdd.StyleId = style.StyleId;
            }

            picToAdd.CreatedAt = picture.CreatedAt;
            picToAdd.Location = picture.Location;
            picToAdd.PictureName = picture.PictureName;
            picToAdd.Price = decimal.Parse(picture.Price);
            picToAdd.DataPath = picture.DataPath;
            picToAdd.Description = picture.Description;
            picToAdd.SizeX = picture.SizeX;

            _picturesRepository.AddPicture(picToAdd);

            return new PictureModel
            {
                PictureId = picToAdd.PictureId,
                PictureName = picToAdd.PictureName,
                Location = picToAdd.Location,
                CreatedAt = picToAdd.CreatedAt,
                ArtistName = picToAdd.Artist.Name,
                ArtistSurname = picToAdd.Artist.Surname,
                Price = picToAdd.Price,
                Description = picToAdd.Description,
                DataPath = picToAdd.DataPath,
                SizeX = picToAdd.SizeX,
                StyleName = picToAdd.Style.StyleName
            };
        }

        public bool DeletePicture(int id)
        {
            var pic = _picturesRepository.GetPictureById(id);

            if(pic is null)
            {
                throw new ArgumentNullException("Picture was null");
            }

            var order = _orderRepository.GetOrderByPictureIdOrDefault(pic.PictureId);

            if(order is null)
            {
                _picturesRepository.DeletePicture(pic);
                return true;
            }

            return false;
        }

        public IEnumerable<PictureModel> GetAllPictures()
        {
            var list = _picturesRepository.GetAllPictures();

            foreach (var picture in list)
            {
                var pictureModel = new PictureModel
                {
                    PictureId = picture.PictureId,
                    PictureName = picture.PictureName,
                    Location = picture.Location,
                    CreatedAt = picture.CreatedAt,
                    ArtistName = picture.Artist.Name,
                    ArtistSurname = picture.Artist.Surname,
                    Price = picture.Price,
                    Description = picture.Description,
                    DataPath = picture.DataPath,
                    SizeX = picture.SizeX,
                    StyleName = picture.Style.StyleName
                };

                if(_orderRepository.GetOrderByPictureIdOrDefault(picture.PictureId) != null 
                    && _orderRepository.GetOrderByPictureIdOrDefault(picture.PictureId).OrderStatusId != (int)OrderStatuses.Abandoned
                    && _orderRepository.GetOrderByPictureIdOrDefault(picture.PictureId).OrderStatusId != (int)OrderStatuses.Accepted)
                {
                    pictureModel.IsOrdered = true;
                }
                else
                {
                    pictureModel.IsOrdered = false;
                }

                yield return pictureModel;
            }
        }

        public List<Item> GetAllStyles()
        {
            var tmp = _picturesRepository.GetAllStyles();

            var result = new List<Item>();

            foreach (var item in tmp)
            {
                result.Add(new Item(item.StyleName));
            }

            return result;
        }

        public PictureModel GetPictureById(int id)
        {
            var picture = _picturesRepository.GetPictureById(id);
            
            var result =  new PictureModel
            {
                PictureId = picture.PictureId,
                PictureName = picture.PictureName,
                Location = picture.Location,
                CreatedAt = picture.CreatedAt,
                ArtistName = picture.Artist.Name,
                ArtistSurname = picture.Artist.Surname,
                Price = picture.Price,
                Description = picture.Description,
                DataPath = picture.DataPath,
                SizeX = picture.SizeX,
                StyleName = picture.Style.StyleName
            };

            if (_orderRepository.GetOrderByPictureIdOrDefault(picture.PictureId) != null
                    && _orderRepository.GetOrderByPictureIdOrDefault(picture.PictureId).OrderStatusId != (int)OrderStatuses.Abandoned
                    && _orderRepository.GetOrderByPictureIdOrDefault(picture.PictureId).OrderStatusId != (int)OrderStatuses.Accepted)
            {
                result.IsOrdered = true;
            }
            else
            {
                result.IsOrdered = false;
            }

            return result;
        }
    }
}
