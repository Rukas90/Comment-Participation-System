namespace CommentParticipatorySystem.Core.Interface
{
    using App.Utils;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Slider))]
    public class RateSlider : MonoBehaviour
    {
        [Header("References")]

        [SerializeField]
        protected Image fill    = null;
        [SerializeField]
        protected Image handle  = null;
        [SerializeField]
        protected Image glow    = null;

        [Space]

        [SerializeField]
        protected Slider slider = null;

        protected Color[] colors = new Color[]
        {
            new Color32(235, 52,  52, 255),
            new Color32(235, 104, 52, 255),
            new Color32(235, 153, 52, 255),
            new Color32(235, 208, 52, 255),
            new Color32(177, 235, 52, 255)
        };

        protected RatingType rating = RatingType.Star1;

        public Slider Slider => slider;

        protected void Awake()
        {
            slider.onValueChanged.AddListener(ValueChanged);
            slider.minValue = 1.0F; slider.maxValue = 5.0F;
        }

        public void Set(RatingType type, bool notify = false)
        {
            if (notify)
            {
                slider.value = (int)type;
                return;
            }
            slider.SetValueWithoutNotify((int)type);
            UpdateUIColor();
        }

        void ValueChanged(float value)
        {
            rating = (RatingType)slider.value;
            UpdateUIColor();
        }
        void UpdateUIColor()
        {
            Color color = GetColor();

            fill.color    = color.Saturate(.5f);

            handle.color  = color;
            glow.color    = color;
        }
        Color GetColor()
        {
            return colors[(int)slider.value - 1];
        }
    }
}