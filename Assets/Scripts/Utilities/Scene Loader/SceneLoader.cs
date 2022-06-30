namespace CommentParticipatorySystem.SceneLoader
{
    using App.Utils;
    using CommentParticipatorySystem.Interface;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class SceneLoader : MonoBehaviour
    {
        struct LoaderOperation
        {
            public readonly LoadInfo info;
            public readonly bool unload;

            public LoaderOperation(LoadInfo info, bool unload)
            {
                this.info   = info;
                this.unload = unload;
            }
        }

        [Header("Load Visual Feedback")]
        [SerializeField] CrossFadeAlpha fadeGroup = null;

        [Header("Settings")]
        [SerializeField] bool debug = false;

        /// <summary>
        /// Use this function to load a new scene.
        /// </summary>
        public static Action<LoadInfo> LoadScene = delegate (LoadInfo i)
        {
            if (state == InitState.NonInitialized)
            {
                state = InitState.Initializing;
                SceneManager.LoadScene(Scenes.Loader, LoadSceneMode.Additive);

                AddBeforeSerializationOperation(i, false);
            }
            else if (state == InitState.Initializing)
            {
                AddBeforeSerializationOperation(i, false);
            }
        };
        /// <summary>
        /// Use this function to unload an existing scene.
        /// </summary>
        public static Action<string> UnloadScene = delegate { };

        private static InitState state = InitState.NonInitialized;

        private static List<LoaderOperation> BeforeSerializationSentOperations = new List<LoaderOperation>();
        private readonly List<IEnumerator> awaiting = new List<IEnumerator>();

        private static void AddBeforeSerializationOperation(LoadInfo info, bool unload)
        {
            int count = BeforeSerializationSentOperations.Count;
            int index = count > 0 ? count - 1 : 0;

            BeforeSerializationSentOperations.Insert(index, new LoaderOperation(info, unload));
        }

        private IEnumerator process = null;
        bool NowLoading => process != null;

        private void OnEnable()
        {
            state = InitState.Initialized;

            LoadScene   += Load;
            UnloadScene += Unload;
        }
        private void OnDisable()
        {
            state = InitState.NonInitialized;

            LoadScene   -= Load;
            UnloadScene -= Unload;
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);

            for (int i = 0; i < BeforeSerializationSentOperations.Count; i++)
            {
                switch (BeforeSerializationSentOperations[i].unload)
                {
                    case true:
                        UnloadScene(BeforeSerializationSentOperations[i].info.Name);
                        break;
                    case false:
                        LoadScene(BeforeSerializationSentOperations[i].info);
                        break;
                }
            }
        }

        public void Load(LoadInfo info)
        {
            DebugLog("<color=yellow>Started Load Scene operation request.</color> <i>Info: name - {0}; mode - {1}; active - {2};</i>", info.Name, info.Mode, info.Active);
            StartOperation(LoadAsync(info));
        }
        public void Unload(string name)
        {
            DebugLog("<color=yellow>Started Unload Scene operation request.</color> <i>Info: name - {0};</i>", name);
            StartOperation(UnloadAsync(name));
        }

        void StartOperation(IEnumerator IEnumerator)
        {
            if (NowLoading)
            {
                DebugLog("<color=gray> NowLoading.. Operation added to awaiting list.</color>");

                awaiting.Add(IEnumerator);
                return;
            }
            fadeGroup.SetState(true);

            process = IEnumerator;
            StartCoroutine(process);
        }

        IEnumerator LoadAsync(LoadInfo info)
        {
            if (info.NonReferenced) { Debug.LogError("Process aborted! Unable to start Load operation. The passed load information is missing scene name."); yield break; }

            DebugLog("<color=green>Started Load Scene operation.</color> <i>Info: name - {0}; mode - {1}; active - {2};</i>", info.Name, info.Mode, info.Active);

            AsyncOperation operation = SceneManager.LoadSceneAsync(info.Name, info.Mode);

            while (!operation.isDone)
            {
                yield return null;
            }

            if (info.Active) { SceneManager.SetActiveScene(SceneManager.GetSceneByName(info.Name)); }

            fadeGroup.SetState(false); Next();
        }
        IEnumerator UnloadAsync(string name)
        {
            if (SceneManager.GetActiveScene().name.Equals(name) && SceneManager.sceneCount == 2)
            {
                Debug.LogErrorFormat("<color=green>Unable to execute unload operation!</color> <b>Requested scene to unload name:</b> <i>{0}</i>. <color=gray>Unity does not support unloading the last loaded scene.</color> Use <b>SceneLoader.LoadScene()</b> to switch to the other scene instead.");
                yield break;
            }

            DebugLog("<color=red>Started Unload Scene operation.</color> <i>Info: name - {0};</i>", name);

            AsyncOperation operation = SceneManager.UnloadSceneAsync(name);

            while (!operation.isDone)
            {
                yield return null;
            }

            fadeGroup.SetState(false); Next();
        }

        void Next()
        {
            DebugLog("<color=blue>Operation is successfully finished.</color>");

            process = null;
            if (awaiting.Count > 0)
            {
                var operation = awaiting[0];
                awaiting.RemoveAt(0);

                DebugLog("<color=cyan>Starting new awaiting operation..</color>");
                StartOperation(operation);
            }
        }

        void DebugLog(string message, params object[] args)
        {
            if (debug)
            {
                Debug.LogFormat(message, args);
            }
        }
    }
}