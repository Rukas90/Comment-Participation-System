namespace CommentParticipatorySystem.Core
{
    using App.Utils;
    using System;
    using UnityEngine;

    [Serializable]
    public abstract class Feedback : Identifiable
    {
        [SerializeField]
        string nameFull = "";

        public DateTime dateCreated = DateTime.Now;

        public string title = "";

        public string Author => nameFull;

        public Feedback(Profile profile) : base(Utils.CreateID())
        {
            nameFull    = profile != null ? profile.name : "";
            dateCreated = DateTime.Now;
        }

        public Feedback(Feedback feedback) : base(feedback.ID)
        {
            nameFull    = feedback.nameFull;
            dateCreated = feedback.dateCreated;
            title       = feedback.title;
        }
    }
}