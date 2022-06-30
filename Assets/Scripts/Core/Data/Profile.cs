namespace CommentParticipatorySystem.Core
{
    using UnityEngine;
    using App.Utils;

    [SerializeField]
    public class Profile : Identifiable
    {
        public string name      = "";
        public string password  = "";
        public string email     = "";

        public Profile(string name, string password, string email) : base(Utils.CreateID())
        {
            this.name       = name;
            this.password   = password;
            this.email      = email;
        }
    }
}