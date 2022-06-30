namespace CommentParticipatorySystem.Core
{
    using System;

    [Serializable]
    public class Rating : Feedback
    {
        public RatingType ratingType = RatingType.Star5;

        public Rating(Profile profile) : base(profile) { }
        public Rating(Rating rating) : base(rating)
        {
            ratingType = rating.ratingType;
        }
    }
}