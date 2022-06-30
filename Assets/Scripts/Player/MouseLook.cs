namespace CommentParticipatorySystem.Examples
{
    using System;
    using UnityEngine;
    using UnityInput = UnityEngine.Input;

    [Serializable]
    public class MouseLook
    {
        public float XSensitivity = 2.0f;
        public float YSensitivity = 2.0f;
        public bool clampVerticalRotation = true;
        public float MinimumX = -90.0F;
        public float MaximumX = 90.0f;
        public bool smooth = false;
        public float smoothTime = 5.0f;
        public bool lockCursor = true;

        private Quaternion m_CharacterTargetRot;
        private Quaternion m_CameraTargetRot;

        public void Init(Transform character)
        {
            m_CharacterTargetRot = character.localRotation;
            m_CameraTargetRot = Quaternion.identity;
        }

        public bool LookRotation(Transform character, Transform camera)
        {
            float yRot = UnityInput.GetAxis("Mouse X") * XSensitivity;
            float xRot = UnityInput.GetAxis("Mouse Y") * YSensitivity;

            m_CharacterTargetRot    *= Quaternion.Euler(0f, yRot, 0f);
            m_CameraTargetRot       *= Quaternion.Euler(-xRot, 0f, 0f);

            if (clampVerticalRotation)
            {
                m_CameraTargetRot = ClampRotationAroundXAxis(m_CameraTargetRot);
            }

            if (smooth)
            {
                character.localRotation = Quaternion.Slerp(character.localRotation, m_CharacterTargetRot, smoothTime * Time.deltaTime);
                camera.localRotation = Quaternion.Slerp(camera.localRotation, m_CameraTargetRot, smoothTime * Time.deltaTime);
            }
            else
            {
                character.localRotation = m_CharacterTargetRot;
                camera.localRotation = m_CameraTargetRot;
            }
            return xRot > 0;
        }

        Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

            angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

            q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }
    }
}