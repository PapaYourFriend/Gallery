using Gallery.Core.DataAccess.Interfaces;
using Gallery.Core.DomainModels;
using Gallery.ServiceLayer.Interfaces;
using Gallery.ViewModel.Common;
using Gallery.ViewModel.Models;
using System;
using System.Collections.Generic;

namespace Gallery.ServiceLayer.Implementations
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IProfileRepository _profileRepository;

        public ReviewService(IReviewRepository reviewRepository,
            IProfileRepository profileRepository)
        {
            _reviewRepository = reviewRepository;
            _profileRepository = profileRepository;
        }

        public void AddReview(ReviewModel model)
        {
            var user = _profileRepository.GetUserByIdOrDefault((int)CurrentUser.ProfileId);

            if(model is null)
            {
                throw new ArgumentNullException("Model is null");
            }

            if(user is null)
            {
                throw new ArgumentNullException("Profile is null");
            }
            model.UpdatedAt = DateTime.Now;
            model.ProfileId = user.ProfileId;
            model.ProfileDataPath = user.DataPath;
            model.ProfileName = user.Name;

            var review = new Review
            {
                Header = model.Header,
                Message = model.Message,
                UpdatedAt = model.UpdatedAt,
                ProfileId = model.ProfileId
            };

            _reviewRepository.AddReview(review);

        }

        public IEnumerable<ReviewModel> GetAllReviews()
        {
            var reviews = _reviewRepository.GetAllReviews();    

            foreach(var review in reviews)
            {
                yield return new ReviewModel
                {
                    ReviewId = review.ReviewId,
                    Header = review.Header,
                    Message = review.Message,
                    UpdatedAt = review.UpdatedAt,
                    ProfileId = review.ProfileId,
                    ProfileDataPath = review.Profile.DataPath,
                    ProfileName = review.Profile.Name
                };
            }
        }

        public void RemoveReview(ReviewModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException("Model is null");
            }

            var review = _reviewRepository.GetReviewOrDefault(model.ReviewId);

            if (review != null) 
            {
                _reviewRepository.RemoveReview(review);
            }

        }
    }
}
