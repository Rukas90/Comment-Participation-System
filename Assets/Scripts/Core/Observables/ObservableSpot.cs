namespace CommentParticipatorySystem.Core
{
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    public class ObservableSpot : Observable
    {
        protected override Category Category => Category.Spot;

        [SerializeField] Transform marker = null;

        bool hovered = false;
        float yPos = 0.0F;

        protected override void Awake()
        {
            base.Awake(); Contribution.location = transform.position;
        }

        protected void Start()
        {
            yPos = marker.transform.localPosition.y;
        }

        private void Update()
        {
            Vector3 position = marker.localPosition;

            position.y = Mathf.Lerp(position.y, hovered ? yPos + .25F : yPos, Time.deltaTime * 10);

            marker.localPosition = position;
        }

        public override void OnMouseOver()
        {
            base.OnMouseOver(); hovered = true;
        }

        public override void OnMouseExit()
        {
            base.OnMouseExit(); hovered = false;
        }

        public static GameObject Create(Vector3 position)
        {
            GameObject container = GameObject.Find("Spot Observables");
            if (container == null)
            {
                container = new GameObject("Spot Observables");
            }

            const string key = "Observable Spot";

            Addressables.LoadAssetAsync<GameObject>(key).WaitForCompletion();

            return Addressables.InstantiateAsync(key, position, Quaternion.identity, container.transform).WaitForCompletion();
        }
    }
}