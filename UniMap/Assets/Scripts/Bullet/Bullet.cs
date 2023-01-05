using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem m_bulletExplode;
    private void OnCollisionEnter(Collision collision)
    { 
        Debug.Log("Hit " + collision.gameObject.name); 
    }

}
