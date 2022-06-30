namespace CommentParticipatorySystem.Editor.Utils.Tools
{
    using System.IO;
    using UnityEditor;
    using UnityEngine;

    public class ScreenshotGrabber : EditorWindow
    {
        string path;
        new string name;

        [MenuItem("Screenshot/Grab")]
        public static void Grab()
        {
            var window = GetWindow(typeof(ScreenshotGrabber));
            window.ShowUtility();
        }
        private void OnGUI()
        {
            EditorGUILayout.Space();

            var _path = path;
            if (CreateBrowsePathGUI(ref path, "Select Path", string.IsNullOrWhiteSpace(path) ? Application.dataPath : path, path))
            {
                if (string.IsNullOrWhiteSpace(path))
                {
                    path = _path;
                }
            }

            EditorGUILayout.Space();

            GUIStyle textStyle = new GUIStyle(EditorStyles.textField)
            {
                fontSize = 18,
                alignment = TextAnchor.MiddleLeft
            };

            name = EditorGUILayout.TextField(name, textStyle, GUILayout.Height(45));
            if (GUILayout.Button("Take Screenshot") && !string.IsNullOrWhiteSpace(name))
            {
                var dirPath = Path.Combine(Application.dataPath.Replace("Assets", "").Replace("\\", "/"), "Screenshots/");
                Debug.Log(dirPath);
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                ScreenCapture.CaptureScreenshot(dirPath + $"{name}.png", 2);
            }
        }
        bool CreateBrowsePathGUI(ref string path, string title, string folder, string display = default)
        {
            string _path = path;

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.TextField(title, display == default ? path : display);

            if (GUILayout.Button("Browse", EditorStyles.miniButton, GUILayout.Width(85)))
            {
                path = EditorUtility.OpenFolderPanel(title, folder, "");
            }
            EditorGUILayout.EndHorizontal();

            return _path != path;
        }
    }
}