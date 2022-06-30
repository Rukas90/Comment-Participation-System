namespace CommentParticipatorySystem.Core.Interface
{
    using UnityEngine;

    public class SlideIndicator : MonoBehaviour
    {
        [Header("Parameters")]

        [SerializeField, Range(1.0F, 20.0F)] float slideSpeed = 10.0F;
        [SerializeField] float offset = 25.0F;

        [Space]

        [SerializeField] RectTransform[] containers = new RectTransform[0];

        RectTransform RectTransform => transform as RectTransform;

        private int index = 0;

        private void Update()
        {
            Vector2 position = RectTransform.anchoredPosition;

            position.x = Mathf.Lerp(position.x, containers[index].anchoredPosition.x + offset, Time.deltaTime * slideSpeed);

            RectTransform.anchoredPosition = position;

            Vector2 size = RectTransform.sizeDelta;

            size.x = Mathf.Lerp(size.x, containers[index].sizeDelta.x - (offset * 2), Time.deltaTime * slideSpeed);

            RectTransform.sizeDelta = size;
        }

        public void SetIndex(int index) => this.index = index;
    }
}