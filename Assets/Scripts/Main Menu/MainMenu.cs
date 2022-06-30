namespace CommentParticipatorySystem.MainMenu
{
    using UnityEngine;

    public enum SectionType { Main, ProfileSelectionMenu, ProfileSelectionList, ProfileCreation }

    public class MainMenu : MonoBehaviour
    {
        [Space]

        [SerializeField] MainMenuSection[] sections = new MainMenuSection[0];

        int currentSection = 0;

        private void Start()
        {
            Open(0);
        }

        public void Open(int section)
        {
            sections[currentSection].Close();
            currentSection = section;
            sections[currentSection].Open();
        }
    }
}