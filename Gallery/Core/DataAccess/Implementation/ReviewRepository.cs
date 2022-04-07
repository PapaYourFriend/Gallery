using Gallery.Core.DataAccess.Interfaces;
using Gallery.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Gallery.Core.DataAccess.Implementation
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IGalleryDbContext _galleryDbContext;

        public ReviewRepository(IGalleryDbContext galleryDbContext)
        {
            _galleryDbContext = galleryDbContext;
        }

        public void AddReview(Review review)
        {
            if(review is null)
            {
                throw new ArgumentNullException("Review is null");
            }

            _galleryDbContext.Reviews.Add(review);

            _galleryDbContext.SaveChanges();
        }

        public List<Review> GetAllReviews()
        {
            return _galleryDbContext.Reviews.Include(r => r.Profile).ToList();
        }

        public Review GetReviewOrDefault(int reviewId)
        {
            return _galleryDbContext.Reviews.FirstOrDefault(r => r.ReviewId == reviewId);
        }

        public void RemoveReview(Review review)
        {
            _galleryDbContext.Reviews.Remove(review);

            _galleryDbContext.SaveChanges();
        }
    }
}
