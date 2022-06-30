namespace CommentParticipatorySystem.Editor.Inspectors
{
    using CommentParticipatorySystem.Core;
    using CommentParticipatorySystem.Core.Interface;
    using CommentParticipatorySystem.Editor.Modules;
    using System;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(CommentUI))]
    public class CommentUIEditor : FeedbackElementEditor
    {
        SerializedProperty contributionMenuProp         = null;
        SerializedProperty commentFieldProp             = null;

        SerializedProperty accentBackgroundProp         = null;
        SerializedProperty contributionLabelProp        = null;
        SerializedProperty contributionTextProp         = null;
        SerializedProperty contributionTypeAccentsProp  = null;

        protected override void OnEnable()
        {
            base.OnEnable();

            contributionMenuProp            = serializedObject.FindProperty("contributionMenu");
            commentFieldProp                = serializedObject.FindProperty("commentField");

            accentBackgroundProp            = serializedObject.FindProperty("accentBackground");
            contributionLabelProp           = serializedObject.FindProperty("contributionLabel");
            contributionTextProp            = serializedObject.FindProperty("contributionText");
            contributionTypeAccentsProp     = serializedObject.FindProperty("contributionTypeAccents");

            navigation.Add(new InspectorNavigation.Tab("Styles", Styles));
        }
        protected override void UI()
        {
            base.UI();

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(commentFieldProp);
            EditorGUILayout.PropertyField(contributionMenuProp);

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(accentBackgroundProp);
            EditorGUILayout.PropertyField(contributionLabelProp);
            EditorGUILayout.PropertyField(contributionTextProp);
        }
        void Styles()
        {
            int length = Enum.GetValues(typeof(ContributionType)).Length;
            if (contributionTypeAccentsProp.arraySize != length)
            {
                contributionTypeAccentsProp.arraySize = length;

                for (int i = 0; i < length; i++)
                {
                    var e = contributionTypeAccentsProp.GetArrayElementAtIndex(i);
                    e.FindPropertyRelative("type").enumValueIndex = i;
                }

                serializedObject.ApplyModifiedPropertiesWithoutUndo();
            }
            DrawAccentTypes();
        }
        void DrawAccentTypes()
        {
            for (int i = 0; i < contributionTypeAccentsProp.arraySize; i++)
            {
                var e = contributionTypeAccentsProp.GetArrayElementAtIndex(i);

                EditorGUILayout.BeginHorizontal();

                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.PropertyField(e.FindPropertyRelative("type"), GUIContent.none);
                EditorGUI.EndDisabledGroup();

                EditorGUILayout.PropertyField(e.FindPropertyRelative("color"), GUIContent.none);

                EditorGUILayout.EndHorizontal();
            }
        }
    }
}