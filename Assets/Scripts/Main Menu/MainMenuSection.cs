namespace CommentParticipatorySystem.MainMenu
{
    using CommentParticipatorySystem.Interface;
    using System;
    using UnityEngine;
    using UnityEngine.Events;

    [Serializable]
    public class MainMenuSection
    {
        [SerializeField] string         name    = "";
        [SerializeField] SectionType    type    = SectionType.Main;
        [SerializeField] CrossFadeAlpha group   = null;

        [Space]

        [SerializeField] UnityEvent onOpen  = new UnityEvent();
        [SerializeField] UnityEvent onClose = new UnityEvent();

        public void Open()
        {
            group.SetState(true); onOpen.Invoke();
        }
        public void Close()
        {
            group.SetState(false); onClose.Invoke();
        }
    }
}