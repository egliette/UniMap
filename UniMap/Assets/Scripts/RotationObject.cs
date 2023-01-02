using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObject : MonoBehaviour
{
    private Vector3 m_StartPosition;
    private float m_RotationSpeed = 100f;

    void Start () 
    {
        m_StartPosition = transform.position;
    }
    
    void Update()
    {
        transform.Rotate(new Vector3(m_RotationSpeed, m_RotationSpeed, m_RotationSpeed) * Time.deltaTime);
        transform.position = m_StartPosition + new Vector3(0f, 2 * Mathf.Sin(Time.time), 0f);
    }
}
