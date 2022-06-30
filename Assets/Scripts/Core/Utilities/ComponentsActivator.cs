namespace CommentParticipatorySystem.Utils
{
    using CommentParticipatorySystem.Core;
    using UnityEngine;

    public class ComponentsActivator : MonoBehaviour
    {
        [SerializeField] GlobalEvents events = null;
        [Space]
        [SerializeField] Behaviour[] components = new Behaviour[0];

        private void OnEnable()
        {
            events.OnGameStateChanged += OnGameStateChanged;
        }
        private void OnDisable()
        {
            events.OnGameStateChanged -= OnGameStateChanged;
        }
        void OnGameStateChanged(bool state)
        {
            for (int i = 0; i < components.Length; i++)
            {
                components[i].enabled = state;
            }
        }
    }
}