namespace CommentParticipatorySystem.Core.Interface
{
    using UnityEngine;
    using TMPro;

    public class RatingUI : FeedbackElement<Rating>
    {
        #pragma warning disable IDE0044

        [SerializeField] RateSlider rateSlider      = null;
        [SerializeField] TextMeshProUGUI ratingText = null;

        #pragma warning restore IDE0044

        public override void Initialize(in Rating rating)
        {
            base.Initialize(rating);

            UpdateRatingText((int)rating.ratingType);

            rateSlider.Slider.onValueChanged.AddListener(rating => { feedback.ratingType = (RatingType)(int)rating; });
            rateSlider.Set(rating.ratingType);
        }
        public override void Cancel()
        {
            base.Cancel();

            rateSlider.Set(feedback.ratingType, true);
        }
        public void UpdateRatingText(float rating)
        {
            ratingText.text = (int)rating + "/5";
        }
    }
}