namespace App.Utils
{ 
    using System;
    using UnityEngine;

    public static class Extensions
    {
        public static CanvasGroup SetState(this CanvasGroup canvasGroup, bool state)
        {
            if (!canvasGroup) { return canvasGroup; }

            canvasGroup.blocksRaycasts  = state;
            canvasGroup.interactable    = state;
            canvasGroup.alpha           = state ? 1.0f : 0.0f;

            return canvasGroup;
        }
        /// <summary>
        /// Saturates the color depending on the percentage. The range is from 0 to 2. Passing 1 will return same color.
        /// </summary>
        public static Color Saturate(this Color color, float percentage)
        {
            percentage = Mathf.Clamp(percentage, 0, 2);

            Color.RGBToHSV(color, out float hue, out float saturation, out float value);
            saturation *= percentage;

            return Color.HSVToRGB(hue, saturation, value);
        }

        public static RectTransform SetAnchor(this RectTransform rectTransform, Anchor anchor)
        {
            rectTransform.anchorMin = anchor.Min;
            rectTransform.anchorMax = anchor.Max;
            rectTransform.pivot     = anchor.Pivot;

            return rectTransform;
        }

        public static float GetLeft(this RectTransform transform)       => transform.offsetMin.x;
        public static float GetRight(this RectTransform transform)      => transform.offsetMax.x;
        public static float GetTop(this RectTransform transform)        => transform.offsetMax.y;
        public static float GetBottom(this RectTransform transform)     => transform.offsetMin.y;

        public static void SetLeft(this RectTransform transform, float left)        => transform.offsetMin = new Vector2(left, transform.offsetMin.y);
        public static void SetRight(this RectTransform transform, float right)      => transform.offsetMax = new Vector2(right, transform.offsetMax.y);
        public static void SetTop(this RectTransform transform, float top)          => transform.offsetMax = new Vector2(transform.offsetMax.x, top);
        public static void SetBottom(this RectTransform transform, float bottom)    => transform.offsetMin = new Vector2(transform.offsetMin.x, bottom);

        public static T[] SubArray<T>(this T[] array, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(array, index, result, 0, length);
            return result;
        }
    }
}