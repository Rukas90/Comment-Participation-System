namespace CommentParticipatorySystem.Utils
{
    using CommentParticipatorySystem.Core;
    using UnityEngine;

    public class SetMouse : MonoBehaviour
    {
        [SerializeField] bool state = false;

        void Start()
        {
            GameManager.get().SetState(state);
        }
    }
}