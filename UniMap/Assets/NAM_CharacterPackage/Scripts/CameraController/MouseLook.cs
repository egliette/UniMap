using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float m_mouseSensitivity = 100f;

    [SerializeField] private Transform m_playerTransform;

    // rotate around X axis => look up and down
    private float m_xRotation = 0f;
    private float m_maxXRotation = 60f;
    private float m_minXRotation = -60f;

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

    
    private void Rotate(float x, float y)
    {
        m_playerTransform.Rotate(Vector3.up * x);

        m_xRotation -= y;
        m_xRotation = Mathf.Clamp(m_xRotation, m_minXRotation, m_maxXRotation);
        transform.localRotation = Quaternion.Euler(m_xRotation, 0, 0);

    }
}
