using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class WeaponManager : MonoBehaviour
{
    [SerializeField] private Transform m_firingPos;
    [SerializeField] private float m_fireRate = 0.3f;
    private float m_fireRateTimer = 0f;
    private bool m_isFiring = false;
   

    [SerializeField] private float m_bulletVelocity;
    [SerializeField] private ParticleSystem m_bulletLaunchEffect;


    private ObjectPooler m_objectPooler;

    void Start()
    {
        m_objectPooler = ObjectPooler.Instance;
        m_bulletsLeft = m_magSize;
        m_fireRateTimer = m_fireRate;
        m_totalBullets = PublicVariables.TOTAL_BULLETS;
    }

    void Update()
    {
        if (m_onReload)
        {
            Reload();
        }
        else if (m_isFiring)
        {
            Fire();
        }
        else
        {
            m_fireRateTimer = m_fireRate;
            m_reloadTimer = 0;
        }
    }

    private void Fire()
    {
        if (!IsMagEmpty())
        {
            if (m_fireRateTimer >= m_fireRate)
            {
                // Do the Firing
                GameObject currentBullet = m_objectPooler.SpawnFromPool(PublicVariables.BULLET_TAG, m_firingPos.position, m_firingPos.rotation);
                Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
                rb.AddForce(m_firingPos.forward * m_bulletVelocity, ForceMode.Impulse);
                m_bulletsLeft--;

                // Effect
                m_bulletLaunchEffect.Play();

                // reset timer
                m_fireRateTimer = 0f;
            }
            m_fireRateTimer += Time.deltaTime;
        }
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

    public void StartReloading()
    {
        m_onReload = true;
    }

    #endregion
}
