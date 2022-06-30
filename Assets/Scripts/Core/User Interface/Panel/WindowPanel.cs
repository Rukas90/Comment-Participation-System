namespace CommentParticipatorySystem.Core.Interface
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class WindowPanel<T, E> : Panel where T : Feedback where E : FeedbackElement<T>
    {
        protected abstract List<T> Collection { get; }

        protected void AddItem(T item) => CreateElement(item);

        protected void CreateElement(T item)
        {
            GameObject element = InstantiateElement(item.ID);

            E comp = element.GetComponent<E>(); comp.Initialize(item);

            comp.OnSaved      += Save;
            comp.OnDeleted    += Delete;
        }

        public override void Setup()
        {
            if (Collection == null) { return; }

            for (int i = 0; i < Collection.Count; i++)
            {
                AddItem(Collection[i]);
            }
        }

        protected override void Save(Feedback feedback)
        {
            base.Save(feedback); Set(feedback as T);
        }

        public override void Create()
        {
            if (Target == null) { return; }

            Profile profile = GameManager.get().Profile;

            T feedback = (T)Activator.CreateInstance(typeof(T), profile);

            Collection.Add(feedback);

            AddItem(feedback);
        }

        protected void Set(T feedback)
        {
            if (Target == null) { return; }

            int index = GetIndex(feedback.ID);

            if (index == -1) { return; }

            Collection[index] = feedback;
        }

        protected override void Delete(string ID)
        {
            base.Delete(ID);

            if (Target == null) { return; }

            int index = GetIndex(ID);

            if (index == -1) { return; }

            Collection.RemoveAt(index);
        }

        int GetIndex(string ID)
        {
            if (Target == null) { return -1; }

            for (int i = 0; i < Collection.Count; i++)
            {
                if (Collection[i].ID == ID)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}