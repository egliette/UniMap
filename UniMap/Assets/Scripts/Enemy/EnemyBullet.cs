using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private ParticleSystem m_Particle;
    
    void Start() 
    {
        m_Particle = GetComponent<ParticleSystem>();
    }
    
    void Update() 
    {
        if(!m_Particle.IsAlive())
        {
            Destroy(gameObject);
        }
    }
 }
