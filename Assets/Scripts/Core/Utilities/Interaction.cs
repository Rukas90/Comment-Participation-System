namespace CommentParticipatorySystem.Core.Modules
{
    using CommentParticipatorySystem.Examples;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public class Interaction : MonoBehaviour
    {
        [SerializeField] float distance = 4f;
        [SerializeField] LayerMask mask = 0;

        [SerializeField] protected GlobalEvents events = null;

        [Space]

        [SerializeField] AssetReference observableSpot = null;

        private AsyncOperationHandle<GameObject> handle;

        private RaycastHit hit;
        private Transform Interacted => hit.transform;

        void Awake() => Preload();

        async void Preload()
        {
            handle = observableSpot.LoadAssetAsync<GameObject>(); await handle.Task;
        }

        private void Update()
        {
            DoRaycast(); HandleInput();
        }

        void DoRaycast()
        {
            Ray ray = new Ray(transform.position, transform.forward);
            Physics.Raycast(ray, out hit, distance, mask);
        }
        void HandleInput()
        {
            events.SetCrosshairType(Interacted ? CrosshairType.Target : CrosshairType.Default);

            if (!Interacted) { return; }

            if (Input.GetMouseButtonDown(0))
            {
                Interact();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                CreateSpotObservable(hit);
            }
        }

        void Interact()
        {
            var interactable = Interacted.GetComponent<IInteractable>();

            if (interactable == null) { return; }

            interactable.Interact(hit);
        }
        void CreateSpotObservable(RaycastHit hit)
        {
            var observable = ObservableSpot.Create(hit.point);

            events.DisplayObservable(observable.GetComponent<Observable>());
            events.SaveData();
        }
    }
}