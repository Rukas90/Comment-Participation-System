namespace CommentParticipatorySystem.SceneLoader
{
    using System;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// Information used by the SceneLoader class when executing LoadScene() operation.
    /// </summary>
    [Serializable()]
    public struct LoadInfo
    {
        [SerializeField] string name;
        public string Name { get { return name; } private set { name = value; } }

        [SerializeField] LoadSceneMode mode;
        public LoadSceneMode Mode { get { return mode; } private set { mode = value; } }

        [SerializeField] bool active;
        public bool Active { get { return active; } private set { active = value; } }

        public bool NonReferenced => string.IsNullOrEmpty(name);

        public LoadInfo(string name)
        {
            this.name    = name;
            mode         = LoadSceneMode.Single;
            active       = false;
        }
        public LoadInfo(string name, LoadSceneMode mode, bool active)
        {
            this.name    = name;
            this.mode    = mode;
            this.active  = active;
        }
    }
}