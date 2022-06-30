namespace CommentParticipatorySystem.SceneLoader.Modules
{ 
    using UnityEngine;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// Class that is used for invoking load scene operations. Mainly for the Unity's UI Button OnClick Event. 
    /// </summary>
    public class SceneInvoker : MonoBehaviour
    {
        LoadInfo loadInfo = new LoadInfo();

        public void SetSceneName(string name)
        {
            loadInfo = new LoadInfo(name, loadInfo.Mode, loadInfo.Active);
        }
        public void SetLoadMode(int mode)
        {
            loadInfo = new LoadInfo(loadInfo.Name, (LoadSceneMode)mode, loadInfo.Active);
        }
        public void SetActive(bool active)
        {
            loadInfo = new LoadInfo(loadInfo.Name, loadInfo.Mode, active);
        }

        public void Load()
        {
            SceneLoader.LoadScene(loadInfo);
        }
    }
}