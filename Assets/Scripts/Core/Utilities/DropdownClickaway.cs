namespace CommentParticipatorySystem.Core.Interface
{
    using UnityEngine;

    public class DropdownClickaway : MonoBehaviour
    {
        private DropdownMenu menu = null;

        public void SetMenu(DropdownMenu menu)
        {
            this.menu = menu;
        }

        public void Clickaway()
        {
            menu.Hide();
        }
    }
}