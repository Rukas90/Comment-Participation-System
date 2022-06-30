namespace CommentParticipatorySystem.Core.Interface
{
    using App.Utils;
    using CommentParticipatorySystem.Interface;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [Serializable]
    public class DropdownItem
    {
        [SerializeField] Button button = null;
        public Button Button => button;
    }
    public class DropdownMenu : MonoBehaviour
    {
        [Header("References")]

        [SerializeField] CrossFadeAlpha group = null;

        [SerializeField] DropdownItem[] items = new DropdownItem[0];

        public event Action<int> OnValueChanged = delegate { };

        int selected = 0;
        GameObject background = null;

        private void Start()
        {
            for (int i = 0; i < items.Length; i++)
            {
                int index = i;
                items[i].Button.onClick.AddListener(() => Select(index));
            }
            Hide();
        }
        public void Show()
        {
            group.SetState(true); CreateClickaway();
        }
        public void Hide()
        {
            group.SetState(false); 
        
            if (background)
            { Destroy(background); }
        }
        void Select(int index)
        {
            items[selected].Button.interactable = true;
            selected = index;
            items[selected].Button.interactable = false;

            OnValueChanged(selected); Hide();
        }
        void CreateClickaway()
        {
            if (background != null)
            {
                Destroy(background);
            }
            background = new GameObject("Dropdown Background");

            Canvas canvas = background.AddComponent<Canvas>();
            canvas.overrideSorting = true; canvas.sortingOrder = 998;
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            background.AddComponent<GraphicRaycaster>();

            GameObject mc = new GameObject("__MouseClick");
            mc.transform.SetParent(background.transform);

            Image image = mc.AddComponent<Image>();
            image.color = Color.clear;
            image.rectTransform.anchoredPosition = Vector2.zero;
            image.rectTransform.SetAnchor(Anchor.Stretched);

            DropdownClickaway clickaway = image.gameObject.AddComponent<DropdownClickaway>();
            clickaway.SetMenu(this);

            Button button = image.gameObject.AddComponent<Button>();
            button.onClick.AddListener(() => clickaway.Clickaway());
            button.transition = Selectable.Transition.None;
        }
    }
}