namespace CommentParticipatorySystem.Core
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "Global Events", menuName = "Game/Utilities/Create GlobalEvents")]
    public class GlobalEvents : ScriptableObject
    {
        public delegate void OpenPanelCallback(Observable target);
        public OpenPanelCallback DisplayObservable = delegate { };

        public delegate void RegisterObservableCallback(in Observable observable);
        public RegisterObservableCallback RegisterObservable = delegate { };

        public delegate void UnregisterObservableCallback(in Observable observable);
        public UnregisterObservableCallback UnregisterObservable = delegate { };

        public delegate void OnGameStateChangedCallback(bool state);
        public OnGameStateChangedCallback OnGameStateChanged = delegate { };

        public delegate void SaveDataCallback();
        public SaveDataCallback SaveData = delegate { };

        public delegate void SetCrosshairTypeCallback(CrosshairType type);
        public SetCrosshairTypeCallback SetCrosshairType = delegate { };

        public delegate void SetProfileCallback(Profile profile);
        public SetProfileCallback SetProfile = delegate { };
    }
}