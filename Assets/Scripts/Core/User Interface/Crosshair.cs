namespace CommentParticipatorySystem.Core
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public enum CrosshairType { Default, Target }

    public class Crosshair : MonoBehaviour
    {
        [Serializable]
        class State
        {
            [SerializeField] CrosshairType type = CrosshairType.Default;
            public CrosshairType Type => type;

            [Space]

            [SerializeField] Vector2 size = Vector2.zero;
            public Vector2 Size => size;

            [SerializeField] Material material = null;
            public Material Material => material;

            [SerializeField] Color color = Color.white;
            public Color Color => color;
        }

        [SerializeField] State[] states = new State[0];

        [Space]

        [SerializeField] GlobalEvents   events      = null;
        [SerializeField] Image          crosshair   = null;

        private CrosshairType currentType = CrosshairType.Default;
        private int index = 0;

        private void OnEnable()
        {
            events.SetCrosshairType += SetCrosshairType;
        }
        private void OnDisable()
        {
            events.SetCrosshairType -= SetCrosshairType;
        }

        private void Update()
        {
            crosshair.rectTransform.sizeDelta = Vector2.Lerp(crosshair.rectTransform.sizeDelta, states[index].Size, Time.deltaTime * 10.0F);
        }

        void SetCrosshairType(CrosshairType type)
        {
            currentType = type; UpdateCrosshair();
        }

        void UpdateCrosshair()
        {
            index = GetIndex(currentType);

            crosshair.color     = states[index].Color;
            crosshair.material  = states[index].Material;
        }

        int GetIndex(CrosshairType type)
        {
            for (int i = 0; i < states.Length; i++)
            {
                if (states[i].Type == type)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}