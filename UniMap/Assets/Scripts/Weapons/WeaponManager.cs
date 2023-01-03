using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Transform m_firingPos;
    [SerializeField] private float m_fireRate = 0.3f;
    [SerializeField] private float m_bulletVelocity;
    [SerializeField] private ParticleSystem m_bulletLaunchEffect;
    private float m_fireRateTimer = 0f;
    private bool m_isFiring = false;


    [SerializeField] private GameObject m_bulletPrefab;
    void Start()
    {
        m_fireRateTimer = m_fireRate;
    }

    void Update()
    {
        if (m_isFiring)
        {
            Fire();
        }
        else
        {
            m_fireRateTimer = m_fireRate;
        }
    }

    private void Fire()
    {
        if (m_fireRateTimer >= m_fireRate)
        {
            // Do the Firing
            GameObject currentBullet = Instantiate(m_bulletPrefab, m_firingPos.position, m_firingPos.rotation);
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(m_firingPos.forward * m_bulletVelocity, ForceMode.Impulse);
            m_bulletLaunchEffect.Play();

            // reset timer
            m_fireRateTimer = 0f;
        }
        m_fireRateTimer += Time.deltaTime;
    }

    #region public methods
    public void StartFiring()
    {
        m_isFiring = true;
    }

    public void StopFiring()
    {
        m_isFiring = false;
    }
    #endregion
}
