namespace CommentParticipatorySystem.Editor.Inspectors
{
    using CommentParticipatorySystem.Editor.Modules;
    using System.Collections.Generic;
    using UnityEditor;

    public class FeedbackElementEditor : Editor
    {
        SerializedProperty CollapsedHeightProp          = null;
        SerializedProperty ExpandedHeightProp           = null;

        SerializedProperty eventsProp                   = null;

        SerializedProperty authorProp                   = null;
        SerializedProperty headlineProp                 = null;
        SerializedProperty dateTextProp                 = null;
        SerializedProperty menuGroupProp                = null;
        SerializedProperty contentGroupProp             = null;
        SerializedProperty openButtonProp               = null;
        SerializedProperty containerResizerProp         = null;

        protected InspectorNavigation navigation        = null;

        protected virtual void OnEnable()
        {
            CollapsedHeightProp             = serializedObject.FindProperty("CollapsedHeight");
            ExpandedHeightProp              = serializedObject.FindProperty("ExpandedHeight");

            eventsProp                      = serializedObject.FindProperty("events");

            authorProp                      = serializedObject.FindProperty("authorText");
            headlineProp                    = serializedObject.FindProperty("headlineField");
            dateTextProp                    = serializedObject.FindProperty("dateText");
            menuGroupProp                   = serializedObject.FindProperty("menuGroup");
            contentGroupProp                = serializedObject.FindProperty("contentGroup");
            openButtonProp                  = serializedObject.FindProperty("openButton");
            containerResizerProp            = serializedObject.FindProperty("containerResizer");

            navigation = new InspectorNavigation(new List<InspectorNavigation.Tab>()
            {
                new InspectorNavigation.Tab("Parameters", Parameters),
                new InspectorNavigation.Tab("References", References),
                new InspectorNavigation.Tab("UI", UI),
            });
        }
        public override void OnInspectorGUI()
        {
            navigation.Draw();

            serializedObject.ApplyModifiedProperties();
        }
        protected virtual void Parameters()
        {
            EditorGUILayout.PropertyField(CollapsedHeightProp);
            EditorGUILayout.PropertyField(ExpandedHeightProp);
        }
        protected virtual void References()
        {
            EditorGUILayout.PropertyField(eventsProp);
        }
        protected virtual void UI()
        {
            EditorGUILayout.PropertyField(authorProp);
            EditorGUILayout.PropertyField(headlineProp);
            EditorGUILayout.PropertyField(dateTextProp);

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(menuGroupProp);
            EditorGUILayout.PropertyField(contentGroupProp);
            EditorGUILayout.PropertyField(openButtonProp);
            EditorGUILayout.PropertyField(containerResizerProp);
        }
    }
}