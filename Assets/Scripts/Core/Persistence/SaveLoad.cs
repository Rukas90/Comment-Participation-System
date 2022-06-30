namespace CommentParticipatorySystem.Core
{
    using System.IO;
    using UnityEngine;

    [RequireComponent(typeof(Registry))]
    public sealed class SaveLoad : MonoBehaviour
    {
        [SerializeField] GlobalEvents events = null;

        private Registry registry = null;

        private void Awake()
        {
            registry = GetComponent<Registry>();
        }

        private void Start()
        {
            Load();
        }

        private void OnEnable()
        {
            events.SaveData += Save;
        }
        private void OnDisable()
        {
            events.SaveData -= Save;
        }
        private void Save()
        {
            Profile profile = GameManager.get().Profile;

            if (profile == null) { return; }

            string path = GetPath(profile.name);

            File.Open(path, FileMode.OpenOrCreate).Close();

            Data data   = registry.GetData();
            string json = JsonUtility.ToJson(data, true);

            File.WriteAllText(path, json);
        }
        private void Load()
        {
            Profile profile = GameManager.get().Profile;

            if (profile == null) { return; }

            string path = GetPath(profile.name);

            if (!File.Exists(path)) { return; }

            string json = File.ReadAllText(path);
            Data data = JsonUtility.FromJson<Data>(json);

            for (int i = 0; i < data.contributions.Length; i++)
            {
                if (data.contributions[i].Category == Category.Spot)
                {
                    var obj = ObservableSpot.Create(data.contributions[i].location);

                    obj.GetComponent<ObservableSpot>().Initialize(in data.contributions[i]);

                    continue;
                }
                registry.Get(data.contributions[i].ID).Initialize(in data.contributions[i]);
            }
        }
        private string GetPath(string name)
        {
            string directory = Path.Combine(Application.dataPath, "Saves");

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            return Path.Combine(directory, $"{name}.json");
        }
    }
}