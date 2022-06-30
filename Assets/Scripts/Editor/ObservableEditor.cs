namespace CommentParticipatorySystem.Editor.Inspectors
{
    using App.Utils;
    using CommentParticipatorySystem.Core;
    using UnityEditor;

    [CustomEditor(typeof(Observable))]
    public class ObservableEditor : Editor
    {
        SerializedProperty idProp = null;

        private void OnEnable()
        {
            idProp = serializedObject.FindProperty("objectID");

            if (string.IsNullOrEmpty(idProp.stringValue))
            {
                idProp.stringValue = Utils.CreateID();
                serializedObject.ApplyModifiedPropertiesWithoutUndo();
            }
        }

        [MenuItem("Scene/Initialize Observables")]
        public static void InitializeObservables()
        {
            Observable[] observables = FindObjectsOfType<Observable>();
            for (int i = 0; i < observables.Length; i++)
            {
                if (string.IsNullOrEmpty(observables[i].ObjectID))
                {
                    typeof(Observable).GetField("objectID").SetValue(observables[i], Utils.CreateID());
                }
            }
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUI.BeginDisabledGroup(true);

            EditorGUILayout.TextField("Object ID", (target as Observable).ObjectID);

            EditorGUI.EndDisabledGroup();

            serializedObject.ApplyModifiedProperties();
        }
    }
    [CustomEditor(typeof(ObservableObject))]
    public class ObservableObjectEditor : ObservableEditor { }

    [CustomEditor(typeof(ObservableSpot))]
    public class ObservableSpotEditor : ObservableEditor { }
}