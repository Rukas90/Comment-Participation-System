namespace CommentParticipatorySystem.Core
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using App.Utils;

    public enum RatingType 
    { 
        Star1 = 1, 
        Star2 = 2, 
        Star3 = 3, 
        Star4 = 4, 
        Star5 = 5 
    }
    public enum ContributionType    { Suggestion, Criticism, Opinion }
    public enum Category            { Object, Spot }

    [Serializable]
    public class Contribution : Identifiable
    {
        public List<Rating>             ratings     = new List<Rating>();
        public List<Comment>            comments    = new List<Comment>();

        public Vector3                  location    = Vector3.zero;

        [SerializeField] Category       category    = Category.Object;
        public Category                 Category    => category;

        public Contribution(Category category) : base(Utils.CreateID())
        {
            this.category = category;
        }
        public Contribution(Observable observable) : base(observable.ObjectID)
        {
            ratings     = observable.Contribution.ratings;
            comments    = observable.Contribution.comments;

            location    = observable.Contribution.location;

            category    = observable.Contribution.category;
        }
        public Contribution(Contribution other) : base(other.ID)
        {
            ratings     = other.ratings;
            comments    = other.comments;

            location    = other.location;

            category    = other.category;
        }
    }
}