using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera m_Camera;
    private float m_XRotation = 0f;

    public float m_XSensitivity = 30f;
    public float m_YSensitivity = 30f;

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        m_XRotation -= (mouseY * Time.deltaTime) * m_YSensitivity;
        m_XRotation = Mathf.Clamp(m_XRotation, -80f, 80f);

        m_Camera.transform.localRotation = Quaternion.Euler(m_XRotation, 0, 0);

        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * m_XSensitivity);
    }
}
