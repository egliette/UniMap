using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    [Header ("Parameters")]
    public float m_Damage = 10f;
    public float m_Range = 100f;
    public int m_TotalAmmo = 30;
    public float m_ReloadingTime = 2f;
    public float m_FireCooldown = 0.1f;

    private float m_FireCooldownTimer = 0f;
    private int m_CurrentAmmo;
    private bool isReloading = false;

    [Header ("GameObjects")]
    public GameObject m_ShootingPoint;
    public GameObject m_OldRotation;
    public ParticleSystem m_Flash;
    public GameObject m_ImpactEffect;
    public TextMeshProUGUI m_AmmoText;
    public GameObject m_ReloadText;
    public Transform m_Gun;

    [Header ("SFX")]
    [SerializeField] private GameObject m_FireSound;
    [SerializeField] private AudioSource m_ReloadSound;

    private void Start()
    {
        m_CurrentAmmo = m_TotalAmmo;
    }

    private void Update() 
    {
        if (m_CurrentAmmo <= 0) {
            m_ReloadText.active = true;
        }
        else if (Input.GetButton("Fire1") && 
                 Time.time >= m_FireCooldownTimer &&
                 !isReloading)
        {
            Shoot();
            m_FireCooldownTimer = Time.time + m_FireCooldown;
        }


        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {   
        m_ReloadSound.Play();
        isReloading = true;
      
        Quaternion startRot = m_OldRotation.transform.rotation;
        float t = 0.0f;
        while (t < m_ReloadingTime)
        {
            t += Time.deltaTime;
            m_Gun.rotation = startRot * Quaternion.AngleAxis(t / m_ReloadingTime * 360f, Vector3.right);
            yield return null;
        }
       
        m_Gun.rotation = m_OldRotation.transform.rotation;
       
        m_ReloadText.active = false;
        isReloading = false;
        m_CurrentAmmo = m_TotalAmmo;
        UpdateAmmo();    
    }

    private void UpdateAmmo()
    {
        m_AmmoText.text = m_CurrentAmmo.ToString() + "/" + m_TotalAmmo.ToString();
    }

    private void Shoot()
    {
        GameObject fireSound = Instantiate(m_FireSound, m_ShootingPoint.transform.position, m_ShootingPoint.transform.rotation);
        Destroy(fireSound, 0.5f);

        m_CurrentAmmo -= 1;
        UpdateAmmo();
        m_Flash.Play();

        RaycastHit hit;
        if (Physics.Raycast(m_ShootingPoint.transform.position, m_ShootingPoint.transform.forward, out hit, m_Range))
        {
            Debug.Log(hit.transform.name);

            EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();

            if (enemyHealth != null) 
            {
                enemyHealth.TakeDamage(m_Damage);
            }

            GameObject impactGO = Instantiate(m_ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }
}
