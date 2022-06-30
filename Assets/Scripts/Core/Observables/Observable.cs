namespace CommentParticipatorySystem.Core
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    [DisallowMultipleComponent]
    public abstract class Observable : MonoBehaviour, IInteractable
    {
        [SerializeField, HideInInspector] string objectID = "";
        public string ObjectID => objectID + transform.position.sqrMagnitude;

        [SerializeField, HideInInspector] Contribution contribution = null;

        /// <summary>
        /// Observable contribution details
        /// </summary>
        public Contribution Contribution => contribution;

        protected abstract Category Category { get; }

        [SerializeField] protected GlobalEvents events = null;

        protected virtual void Awake()
        {
            contribution = new Contribution(Category);
        }

        protected virtual void OnEnable()
        {
            events.RegisterObservable(this);
        }
        protected virtual void OnDisable()
        {
            events.UnregisterObservable(this);
        }

        public void Initialize(in Contribution contribution)
        {
            events.UnregisterObservable(this);

            this.contribution = new Contribution(contribution);

            events.RegisterObservable(this);
        }

        public virtual void Interact(RaycastHit hit)
        {
            if (EventSystem.current.IsPointerOverGameObject()) { return; }

            events.DisplayObservable(this);
        }

        public virtual void OnMouseOver() { }
        public virtual void OnMouseExit() { }
    }
}