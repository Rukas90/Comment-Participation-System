namespace CommentParticipatorySystem.MainMenu
{
    using CommentParticipatorySystem.Core;
    using CommentParticipatorySystem.Core.Interface;
    using Newtonsoft.Json;
    using System.IO;
    using TMPro;
    using UnityEngine;
    using Security = App.Utils.Security;

    [RequireComponent(typeof(MainMenu))]
    public class ProfilesSection : UIListable
    {
        [Header("Profile Creation Fields")]

        [SerializeField] TMP_InputField nameField      = null;
        [SerializeField] TMP_InputField passwordField  = null;
        [SerializeField] TMP_InputField emailField     = null;

        MainMenu mainMenu = null;

        string ProfilesDirectoryPath => Path.Combine(Application.persistentDataPath, "Data/Profiles/");

        private void Start()
        {
            mainMenu = GetComponent<MainMenu>();
        }

        public void InitializeProfiles()
        {
            if (!Directory.Exists(ProfilesDirectoryPath)) { return; }

            FileInfo[] files = new DirectoryInfo(ProfilesDirectoryPath).GetFiles("*.json");

            for (int i = 0; i < files.Length; i++)
            {
                CreateElement(in files[i]);
            }
        }
        void CreateElement(in FileInfo file)
        {
            string json = File.ReadAllText(file.FullName);
            Profile profile = JsonConvert.DeserializeObject<Profile>(json);

            if (profile == null) { return; }

            ProfileUI element = InstantiateElement(profile.ID).GetComponent<ProfileUI>();
            element.Initialize(profile);
        }

        public void CreateProfile()
        {
            Profile profile = new Profile(nameField.text, Security.HashToBase64(passwordField.text, Security.GenerateSalt(Security.SaltSize)), emailField.text);

            nameField.text = string.Empty; passwordField.text = string.Empty; emailField.text = string.Empty;

            string json = JsonConvert.SerializeObject(profile);
            string path = Path.Combine(ProfilesDirectoryPath, profile.name + ".json");

            if (!Directory.Exists(ProfilesDirectoryPath))
            {
                Directory.CreateDirectory(ProfilesDirectoryPath);
            }

            File.Open(path, FileMode.OpenOrCreate).Close();
            File.WriteAllText(path, json);

            mainMenu.Open((int)SectionType.ProfileSelectionList);
        }
    }
}