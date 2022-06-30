namespace CommentParticipatorySystem.MainMenu
{
    using CommentParticipatorySystem.Core;
    using CommentParticipatorySystem.SceneLoader;
    using TMPro;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class ProfileUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI    labelText   = null;
        [SerializeField] GlobalEvents       events      = null;

        Profile profile = null;

        public void Initialize(Profile profile)
        {
            this.profile = profile;

            labelText.text = profile.name;
        }

        public void Select()
        {
            events.SetProfile(profile); SceneLoader.LoadScene(new LoadInfo(Scenes.ScenePark, LoadSceneMode.Single, true));
        }
    }
}