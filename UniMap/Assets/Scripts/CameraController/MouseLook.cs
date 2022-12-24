using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float m_mouseSensitivity = 100f;

    [SerializeField] private Transform m_playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * m_mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * m_mouseSensitivity * Time.deltaTime;

        Rotate(mouseX, mouseY);
    }

    // rotate around X axis => look up and down
    private float m_xRotation = 0f;
    private void Rotate(float x, float y)
    {
        m_playerTransform.Rotate(Vector3.up * x);

        m_xRotation -= y;
        m_xRotation = Mathf.Clamp(m_xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(m_xRotation, 0, 0);

    }
}
