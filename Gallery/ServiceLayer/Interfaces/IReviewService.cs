using Gallery.ViewModel.Models;
using System.Collections.Generic;

namespace Gallery.ServiceLayer.Interfaces
{
    public interface IReviewService
    {
        void AddReview(ReviewModel model);
        void RemoveReview(ReviewModel model);
        IEnumerable<ReviewModel> GetAllReviews();
    }
}
