namespace CommentParticipatorySystem.Core.Interface
{
    using System;
    using UnityEngine;

    [Serializable]
    public class WindowTab
    {
        [SerializeField] string name = null;
        [SerializeField] Panel panel = null;

        public Panel Panel => panel;

        protected FeedbackWindow window = null;

        public void Initialize(FeedbackWindow window)
        {
            this.window = window;
        }

        public void Open()
        {
            panel.Show(window);
        }
        public void Close()
        {
            panel.Hide();
        }
    }
}