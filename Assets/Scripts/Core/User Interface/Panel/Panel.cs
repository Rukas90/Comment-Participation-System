namespace CommentParticipatorySystem.Core.Interface
{
    using UnityEngine;

    public abstract class Panel : UIListable
    {
        [Header("References")]

        [SerializeField] protected GlobalEvents events = null;

        protected FeedbackWindow window = null;

        protected Observable Target => window.Target;

        public abstract void Setup();

        public void Show(FeedbackWindow window)
        {
            this.window = window;
        }
        public void Hide()
        {
            Clear();
        }
        protected virtual void Save(Feedback feedback) { }

        public abstract void Create();
    }
}