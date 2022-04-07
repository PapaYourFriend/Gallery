using Gallery.Core.DomainModels;
using System.Collections.Generic;

namespace Gallery.Core.DataAccess.Interfaces
{
    public interface IReviewRepository
    {
        void AddReview(Review review);
        void RemoveReview(Review review);
        Review GetReviewOrDefault(int reviewId);
        List<Review> GetAllReviews();
    }
}
