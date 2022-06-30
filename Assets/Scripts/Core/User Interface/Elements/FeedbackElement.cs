namespace CommentParticipatorySystem.Core.Interface
{
    using CommentParticipatorySystem.Interface;
    using System;
    using TMPro;
    using UnityEngine;

    public abstract class FeedbackElement<T> : MonoBehaviour where T : Feedback
    {
        [SerializeField] float              CollapsedHeight     = 100.0F;
        [SerializeField] float              ExpandedHeight      = 335.0F;

        [SerializeField] TextMeshProUGUI    authorText          = null;
        [SerializeField] TMP_InputField     headlineField       = null;
        [SerializeField] TextMeshProUGUI    dateText            = null;

        [SerializeField] CrossFadeAlpha     menuGroup           = null;
        [SerializeField] CrossFadeAlpha     contentGroup        = null;
        [SerializeField] ButtonToggle       openButton          = null;
        [SerializeField] UIResizer          containerResizer    = null;

        [SerializeField] GlobalEvents       events              = null;

        public event Action<Feedback>       OnSaved             = delegate { };
        public event Action                 OnCanceled          = delegate { };
        public event Action<string>         OnDeleted           = delegate { };

        protected T feedback = null;
        protected T backup   = null;

        public virtual void Initialize(in T feedback)
        {
            this.feedback = feedback;

            authorText.text = "<color=#D9D9D9D9>from</color> " + feedback.Author;

            headlineField.text = feedback.title;
            headlineField.onValueChanged.AddListener(title => { this.feedback.title = title; });

            dateText.text = feedback.dateCreated.ToString();
        }

        public virtual void Save()
        {
            OnSaved(feedback); Close(); events.SaveData();
        
        }
        public virtual void Cancel()
        {
            feedback = (T)Activator.CreateInstance(typeof(T), backup); ;

            headlineField.SetTextWithoutNotify(backup.title);
            dateText.text = feedback.dateCreated.ToString();

            OnCanceled(); Close();
        }
        public virtual void Delete()
        {
            OnDeleted(feedback.ID); events.SaveData();
        }
        public virtual void Open()
        {
            menuGroup.SetState(true); contentGroup.SetState(true);
            containerResizer.SetSize(-1, ExpandedHeight);

            headlineField.interactable = true; openButton.SetState(false);

            backup = (T)Activator.CreateInstance(typeof(T), feedback);
        }
        public virtual void Close()
        {
            menuGroup.SetState(false); contentGroup.SetState(false);
            containerResizer.SetSize(-1, CollapsedHeight);

            headlineField.interactable = false; openButton.SetState(true);
        }
    }
}