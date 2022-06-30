namespace CommentParticipatorySystem.Examples
{
    using UnityEngine;

    public class LookAtPlayer : MonoBehaviour
    {
        Transform target = null;

        private void Start()
        {
            target = FindObjectOfType<SimpleCharacterController>().transform;
        }

        void Update()
        {
            transform.LookAt(target);
        }
    }
}