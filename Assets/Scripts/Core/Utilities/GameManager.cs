namespace CommentParticipatorySystem.Core
{
    using System;
    using UnityEngine;

    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField] GlobalEvents events = null;

        public Profile Profile { get; private set; } = null;
        public static Func<GameManager> get = delegate { return null; };

        private bool state = false;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void OnEnable()
        {
            get = () => { return this; };

            events.SetProfile += SetProfile;
        }
        private void OnDisable()
        {
            get = () => { return null; };

            events.SetProfile -= SetProfile;
        }

        private void Update()
        {
            Cursor.visible      = state;
            Cursor.lockState    = state ? CursorLockMode.None : CursorLockMode.Locked;
        }

        public void SetState(bool state)
        {
            this.state = state; events.OnGameStateChanged(!state);
        }

        void SetProfile(Profile profile)
        {
            Profile = profile;
        }
    }
}