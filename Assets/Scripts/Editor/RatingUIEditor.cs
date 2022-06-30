namespace CommentParticipatorySystem.Editor.Inspectors
{
    using CommentParticipatorySystem.Core.Interface;
    using UnityEditor;

    [CustomEditor(typeof(RatingUI))]
    public class RatingUIEditor : FeedbackElementEditor
    {
        SerializedProperty rateSliderProp = null;
        SerializedProperty ratingTextProp = null;

        protected override void OnEnable()
        {
            base.OnEnable();

            rateSliderProp = serializedObject.FindProperty("rateSlider");
            ratingTextProp = serializedObject.FindProperty("ratingText");
        }
        protected override void UI()
        {
            base.UI();

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(rateSliderProp);
            EditorGUILayout.PropertyField(ratingTextProp);
        }
    }
}