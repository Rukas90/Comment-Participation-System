namespace CommentParticipatorySystem.Core
{
    using UnityEngine;

    public interface IInteractable
    {
        void Interact(RaycastHit hit);
    }
}