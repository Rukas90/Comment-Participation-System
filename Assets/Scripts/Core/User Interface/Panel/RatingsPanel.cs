namespace CommentParticipatorySystem.Core.Interface
{
    using System.Collections.Generic;

    public class RatingsPanel : WindowPanel<Rating, FeedbackElement<Rating>>
    {
        protected override List<Rating> Collection => Target ? Target.Contribution.ratings : null;
    }
}