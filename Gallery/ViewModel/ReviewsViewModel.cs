using Gallery.Core.DomainModels;
using Gallery.ViewModel.Command;
using Gallery.ViewModel.Common;
using Gallery.ViewModel.Models;
using System.Collections.ObjectModel;

namespace Gallery.ViewModel
{
    public class ReviewsViewModel : BaseViewModel
    {
        private readonly MainWindowViewModel _main;
        private ObservableCollection<ReviewModel> _reviews;
        private RelayCommand _putReviewCommand;
        private ReviewModel _review = new ReviewModel();

        public ReviewsViewModel(MainWindowViewModel main)
        {
            _main = main;

            Reviews = new ObservableCollection<ReviewModel>(_main.ReviewService.GetAllReviews());
        }

        public ObservableCollection<ReviewModel> Reviews
        {
            get { return _reviews; }
            set
            {
                _reviews = value;
                OnPropertyChanged(nameof(Reviews));
            }
        }

        public ReviewModel Review
        {
            get
            {
                return _review;
            }
            set
            {
                _review = value;
                OnPropertyChanged(nameof(Review));
            }
        }

        public RelayCommand PutReviewCommand
        {
            get
            {
                return _putReviewCommand ??
                    (_putReviewCommand = new RelayCommand((obj) =>
                    {
                        if(CurrentUser.ProfileId is null)
                        {
                            _main.CurrentViewModel = new AuthorizationViewModel(this, _main);
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(Review.Header) && !string.IsNullOrEmpty(Review.Message))
                            {
                                _main.ReviewService.AddReview(Review);
                                Reviews.Add(Review);
                                Review = new ReviewModel();

                            }
                        }

                    },(obj) => !Review.HasErrors));
            }
        } 
    }
}
