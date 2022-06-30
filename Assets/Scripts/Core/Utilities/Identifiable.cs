namespace CommentParticipatorySystem.Core
{
    using UnityEngine;

    public abstract class Identifiable
    {
        [SerializeField] protected string id = "";
        public string ID => id;

        protected Identifiable(string id)
        {
            this.id = id;
        }
    }
}