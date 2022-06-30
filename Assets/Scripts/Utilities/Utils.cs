namespace App.Utils
{
    using System;
    using UnityEngine;

    public enum InitState { NonInitialized = 0, Initializing = 1, Initialized = 2 }

    [Serializable()]
    public struct Anchor
    {
        [SerializeField] Vector2 min;
        public Vector2 Min => min;

        [SerializeField] Vector2 max;
        public Vector2 Max => max;

        [SerializeField] Vector2 pivot;
        public Vector2 Pivot => pivot;

        public Anchor(Vector2 min, Vector2 max, Vector2 pivot) : this()
        {
            this.min = min;
            this.max = max;
            this.pivot = pivot;
        }
        public Anchor(RectTransform reference) : this()
        {
            min = reference.anchorMin;
            max = reference.anchorMax;
            pivot = reference.pivot;
        }

        public static Anchor Stretched => new Anchor(new Vector2(0F, 0F), new Vector2(1F, 1F), new Vector2(.5F, .5F));
    }

    public static class Utils
    {
        public static bool Approximately(float a, float b, float t)
        {
            return Mathf.Abs(a - b) <= t;
        }
        public static bool Approximately(Vector2 a, Vector2 b, float t)
        {
            return Approximately(a.x, b.x, t) && Approximately(a.y, b.y, t);
        }
        public static string CreateID()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
    }
}