namespace CommentParticipatorySystem.Core.Interface
{
    using System.Collections.Generic;

    public class CommentsPanel : WindowPanel<Comment, FeedbackElement<Comment>>
    {
        protected override List<Comment> Collection => Target ? Target.Contribution.comments : null;
    }
}