namespace CommentParticipatorySystem.Core.Interface
{
    using CommentParticipatorySystem.Interface;
    using UnityEngine;

    public class FeedbackWindow : MonoBehaviour
    {
        #pragma warning disable IDE0044

        [Header("References")]

        [SerializeField] CrossFadeAlpha windowGroup = null;
        [SerializeField] SlideIndicator slideIndicator = null;

        [Space]

        [SerializeField] WindowTab[] tabs = new WindowTab[0];

        [SerializeField] protected GlobalEvents events = null;

        private Observable target = null;
        private int currentTab = 0;

        public Observable Target => target;

        #pragma warning restore IDE0044

        protected void OnEnable()
        {
            events.DisplayObservable += DisplayObservable;
        }
        protected void OnDisable()
        {
            events.DisplayObservable -= DisplayObservable;
        }

        private void Start()
        {
            foreach (var tab in tabs)
            {
                tab.Initialize(this);
            }
        }

        void DisplayObservable(Observable observable)
        {
            target = observable;
            Open();
        }

        public void Open()
        {
            windowGroup.SetState(true); GameManager.get().SetState(true);
            SetTab(0);
        }
        public void Close()
        {
            windowGroup.SetState(false); GameManager.get().SetState(false);

            SetTab(-1); currentTab = 0;

            target = null;
        }

        public void SetTab(int index)
        {
            tabs[currentTab].Close();

            if (index == -1) { return; }

            currentTab = index;

            tabs[currentTab].Open();
            tabs[currentTab].Panel.Setup();

            slideIndicator.SetIndex(currentTab);
        }
        public void Create() => tabs[currentTab].Panel.Create();
    }
}