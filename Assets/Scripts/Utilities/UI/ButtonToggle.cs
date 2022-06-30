namespace CommentParticipatorySystem.Interface
{ 
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Button))]
    public class ButtonToggle : MonoBehaviour
    {
        [Header("Parameters")]
        [Tooltip("The current state of the button.")]
        [SerializeField] bool state = true;

        Button  button  = null;
        Graphic graphic = null;

        private void Awake()
        {
            button  = GetComponent<Button>();
            graphic = GetComponent<Graphic>();
        }

        private void Start()
        {
            SetState(state);
        }

        public void SetState(bool state)
        {
            this.state = state;

            button.interactable = state;
            if (graphic)
            {
                graphic.raycastTarget = state;
            }
        }
    }
}