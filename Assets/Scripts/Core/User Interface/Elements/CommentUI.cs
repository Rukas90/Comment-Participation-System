namespace CommentParticipatorySystem.Core.Interface
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using System;

    public class CommentUI : FeedbackElement<Comment>
    {
        [Serializable]
        class ContributionTypeAccent
        {
            [SerializeField] ContributionType type;
            [SerializeField] Color color;

            public Color Color => color;
        }

        #pragma warning disable IDE0044

        [SerializeField] DropdownMenu contributionMenu                      = null;
        [SerializeField] TMP_InputField commentField                        = null;

        [Space]

        [SerializeField] Image accentBackground                             = null;
        [SerializeField] Image contributionLabel                            = null;
        [SerializeField] TextMeshProUGUI contributionText                   = null;
        [SerializeField] ContributionTypeAccent[] contributionTypeAccents   = null;

        #pragma warning restore IDE0044

        public override void Initialize(in Comment comment)
        {
            base.Initialize(comment);

            contributionMenu.OnValueChanged += SetContributionType;

            commentField.text   = comment.commentContent;
            commentField.onValueChanged.AddListener(text => { feedback.commentContent = text; });

            SetContributionType((int)comment.contributionType);
        }

        void SetContributionType(int type)
        {
            feedback.contributionType = (ContributionType)type; 
            UpdateContributionTypeUI();
        }

        void UpdateContributionTypeUI()
        {
            contributionText.text = feedback.contributionType.ToString();

            float textWidth = contributionText.GetPreferredValues().x;
            Vector2 size = contributionLabel.rectTransform.sizeDelta;

            size.x = textWidth + 50.0F;
            contributionLabel.rectTransform.sizeDelta = size;

            int index = (int)feedback.contributionType;

            accentBackground.color  = contributionTypeAccents[index].Color;
            contributionLabel.color = contributionTypeAccents[index].Color;
        }

        public override void Cancel()
        {
            base.Cancel();

            commentField.text = feedback.commentContent;
        }
    }
}