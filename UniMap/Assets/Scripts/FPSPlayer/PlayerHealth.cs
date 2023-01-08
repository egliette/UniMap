using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int m_MaxHealth = 100;

    private float m_CurrentHealth;

    [SerializeField] private TextMeshProUGUI m_HealthText;
    [SerializeField] private AudioSource m_HurtSound;

    private void Start()
    {
        m_CurrentHealth = m_MaxHealth;
        m_HealthText.text = m_CurrentHealth.ToString();
    }

    private void OnParticleCollision(GameObject other) {
        TakeDamage(10);
    }

    public void TakeDamage(float amount)
    {
        m_CurrentHealth -= amount;
        m_HealthText.text = m_CurrentHealth.ToString();
        m_HurtSound.Play();
        if (m_CurrentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("You Die!");
    }
}
