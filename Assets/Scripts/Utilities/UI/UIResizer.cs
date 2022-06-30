namespace CommentParticipatorySystem.Interface
{ 
    using System.Collections;
    using UnityEngine;
    using App.Utils;

    [RequireComponent(typeof(RectTransform))]
    public class UIResizer : MonoBehaviour
    {
        [Header("Parameters")]
        [Tooltip("The current size of the UI element.")]
        [SerializeField] Vector2 size = Vector2.zero;

        [Tooltip("How fast should the size change?")]
        [SerializeField, Range(.001f, 25f)] float fadeSpeed = 1;

        IEnumerator IE_CrossFade = null;

        public RectTransform RectTransform => transform as RectTransform;
        public Vector2 Size => RectTransform.sizeDelta;

        private void Start()
        {
            SetSize(size);
        }

        public void SetSize(Vector2 size)
        {
            this.size = size;

            if (IE_CrossFade != null)
            {
                StopCoroutine(IE_CrossFade);
            }
            IE_CrossFade = CrossFade();
            StartCoroutine(IE_CrossFade);
        }
    
        /// <summary>
        /// Set the UI to resize to a desired width and height. If any value is set as -1 the current value will be used.
        /// </summary>
        public void SetSize(float width, float height)
        {
            size = new Vector2(width < 0 ? size.x : width, height < 0 ? size.y : height);

            if (IE_CrossFade != null)
            {
                StopCoroutine(IE_CrossFade);
            }
            IE_CrossFade = CrossFade();
            StartCoroutine(IE_CrossFade);
        }
        IEnumerator CrossFade()
        {
            Vector2 value = Size;

            while (!Utils.Approximately(value, size, .01f))
            {
                value = Vector2.Lerp(value, size, Time.deltaTime * fadeSpeed);

                RectTransform.sizeDelta = value;

                yield return null;
            }
            IE_CrossFade = null;
        }
    }
}