using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DominationPoint : MonoBehaviour
{
    [SerializeField] private float m_RadiusRange = 10f;

    private ParticleSystem m_PS;
    private ParticleSystem.EmissionModule particleEmission;
    private LayerMask m_PlayerMask;
    private int m_Rate = 1;
    private float m_Timer = 0;

    private void Start()
    {
        m_PS = GetComponent<ParticleSystem>();  
        particleEmission = m_PS.emission;
        m_PlayerMask = LayerMask.GetMask("Player");
        particleEmission.rateOverTime = m_Rate;
    }

    private void FixedUpdate()
    {
        CheckPlayer();
    }

    public int GetSeconds()
    {
        int seconds = (int)m_Timer % 60;
        return seconds;
    }

    private void CheckPlayer()
    {
        bool playerInRange = Physics.CheckSphere(transform.position, m_RadiusRange, m_PlayerMask);

        if (playerInRange)
        {
            m_Timer += Time.deltaTime;
            int seconds = GetSeconds();
            m_Rate = seconds * 10;
            particleEmission.rateOverTime = m_Rate;
        }
    }
}
