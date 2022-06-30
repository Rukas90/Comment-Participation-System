namespace CommentParticipatorySystem.Core
{
    using System;

    [Serializable]
    public class Comment : Feedback
    {
        public string commentContent = "";
        public ContributionType contributionType = ContributionType.Suggestion;

        public Comment(Profile profile) : base(profile) { }
        public Comment(Comment comment) : base(comment)
        {
            commentContent = comment.commentContent;
        }
    }
}