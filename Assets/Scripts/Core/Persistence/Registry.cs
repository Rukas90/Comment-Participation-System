namespace CommentParticipatorySystem.Core
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public class Data
    {
        public string nameFull = "";
        public Contribution[] contributions = null;

        public Data(Contribution[] contributions)
        {
            this.contributions = contributions;
        }
    }

    public class Registry : MonoBehaviour
    {
        readonly Dictionary<string, Observable> Observables = new Dictionary<string, Observable>();

        [SerializeField] protected GlobalEvents events = null;

        private void OnEnable()
        {
            events.RegisterObservable   += Register;
            events.UnregisterObservable += Unregister;
        }
        private void OnDisable()
        {
            events.RegisterObservable   -= Register;
            events.UnregisterObservable -= Unregister;
        }

        void Register(in Observable observable)
        {
            if (Contains(observable.ObjectID)) { return; }
        
            Observables.Add(observable.ObjectID, observable);
        }
        void Unregister(in Observable observable)
        {
            if (!Contains(observable.ObjectID)) { return; }

            Observables.Remove(observable.ObjectID);
        }

        /// <summary>
        /// Returns an observable object from the registry. If the observable by the provided ID does not exist, the function will return a null value.
        /// </summary>
        public Observable Get(string ID)
        {
            if (!Contains(ID)) { return null; }

            return Observables[ID];
        }

        /// <summary>
        /// Returns whenever the registry contains an observable by the provided ID.
        /// </summary>
        public bool Contains(string ID) => Observables.ContainsKey(ID);

        /// <summary>
        /// Returns the current data containing all the scene contributions and contributor profile details.
        /// </summary>
        public Data GetData()
        {
            Contribution[] array = new Contribution[Observables.Count];

            int index = 0;
            foreach (var observable in Observables)
            {
                array[index] = new Contribution(observable.Value);
                index++;
            }

            return new Data(array);
        }
    }
}