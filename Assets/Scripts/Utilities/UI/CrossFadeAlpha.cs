namespace CommentParticipatorySystem.Interface
{
    using App.Utils;
    using System.Collections;
    using UnityEngine;

    [RequireComponent(typeof(CanvasGroup))]
    public class CrossFadeAlpha : MonoBehaviour
    {
        [Header("Parameters")]
        [Tooltip("The current visibility state of the CanvasGroup.")]
        [SerializeField] bool state = true;

        #pragma warning disable IDE0044

        [Space]
    
        [Tooltip("Specify the disable container. The reference should not be this element.")]
        [SerializeField] RectTransform disableContainer = null;

        [Space]

        [Tooltip("Should alpha be cross faded?")]
        [SerializeField] bool animate = true;

        [Tooltip("How fast should the UI fade?")]
        [SerializeField, Range(.001f, 25f)] float fadeSpeed = 1;

        [Tooltip("What is the maximum alpha value the UI can fade to?")]
        [SerializeField, Range(0, 1)] float maxAlpha = 0.5f;

        #pragma warning restore IDE0044

        IEnumerator IE_CrossFade = null;
        CanvasGroup CanvasGroup  = null;

        public float Alpha => CanvasGroup.alpha;

        private void Awake()
        {
            CanvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            SetState(state);
        }

        public void SetState(bool state)
        {
            this.state = state;

            if (animate)
            {
                if (IE_CrossFade != null)
                {
                    StopCoroutine(IE_CrossFade);
                }
                IE_CrossFade = CrossFade();
                StartCoroutine(IE_CrossFade);
            }
            else 
            {
                CanvasGroup.alpha = state ? maxAlpha : 0;
                CanvasGroup.SetState(state);

                disableContainer.gameObject.SetActive(state);
            }
        }

        IEnumerator CrossFade()
        {
            float value = Alpha, newValue = state ? maxAlpha : 0;

            while (!Utils.Approximately(value, newValue, .01f))
            {
                value = Mathf.Lerp(value, newValue, Time.deltaTime * fadeSpeed);

                if (state == true && disableContainer.gameObject.activeSelf == false)
                {
                    disableContainer.gameObject.SetActive(true);
                }

                CanvasGroup.interactable = true;
                CanvasGroup.alpha = value;

                if (Utils.Approximately(Alpha, newValue, .025f))
                {
                    CanvasGroup.alpha = newValue;
                    CanvasGroup.SetState(state);

                    disableContainer.gameObject.SetActive(state);
                }

                yield return null;
            }
            //Ensure that everything is set even if alpha at the startup is set to the target alpha.
            CanvasGroup.SetState(state); IE_CrossFade = null;
        }
    }
}