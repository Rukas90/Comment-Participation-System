namespace CommentParticipatorySystem.Core.Interface
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public abstract class UIListable : MonoBehaviour
    {
        [Header("UI Listable References")]

        [SerializeField] AssetReference element = null;
        [SerializeField] RectTransform content = null;

        protected readonly Dictionary<string, GameObject> elements = new Dictionary<string, GameObject>();

        private AsyncOperationHandle<GameObject> handle;

        protected virtual void Awake() => LoadElement();

        public async void LoadElement()
        {
            handle = element.LoadAssetAsync<GameObject>(); await handle.Task;
        }
        public GameObject InstantiateElement(string ID)
        {
            AsyncOperationHandle<GameObject> elementHandle = element.InstantiateAsync(content);
            elements.Add(ID, elementHandle.Result); return elementHandle.Result;
        }

        public void Clear()
        {
            foreach (var item in elements)
            {
                Destroy(item.Value);
            }
            elements.Clear();
        }

        protected virtual void Delete(string ID)
        {
            if (!elements.ContainsKey(ID)) { return; }

            Destroy(elements[ID]); elements.Remove(ID);
        }
    }
}