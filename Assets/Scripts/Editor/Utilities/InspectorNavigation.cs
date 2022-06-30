namespace CommentParticipatorySystem.Editor.Modules
{
    using System;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    public class InspectorNavigation
    {
        public class Tab
        {
            public readonly string name = null;

            readonly Action guiDraw = null;

            public Tab(string name, Action guiDraw)
            {
                this.name = name;
                this.guiDraw = guiDraw;
            }

            public void Draw()
            {
                guiDraw();
            }
        }

        readonly List<Tab> tabs = new List<Tab>();

        int index = 0;

        public InspectorNavigation(List<Tab> tabs)
        {
            this.tabs = tabs;
        }
        public void Add(Tab tab)
        {
            tabs.Add(tab);
        }
        public void Draw()
        {
            if (tabs.Count == 0) { return; }

            EditorGUILayout.BeginHorizontal();

            if (tabs.Count == 1)
            {
                EditorGUI.BeginDisabledGroup(true);
                GUILayout.Button(tabs[0].name, EditorStyles.miniButton);
                EditorGUI.EndDisabledGroup();
            }
            else
            {
                for (int i = 0; i < tabs.Count; i++)
                {
                    GUIStyle style = i == 0 ? EditorStyles.miniButtonLeft : i == tabs.Count - 1 ? EditorStyles.miniButtonRight : EditorStyles.miniButtonMid;
                    Color defaultColor = GUI.color;

                    GUI.color = index == i ? new Color32(160, 215, 255, 255) : defaultColor;

                    if (GUILayout.Button(tabs[i].name, style))
                    {
                        index = i;
                    }

                    GUI.color = defaultColor;
                }
            }

            EditorGUILayout.EndHorizontal();

            GUILayout.Space(7f);

            tabs[index].Draw();
        }
    }
}