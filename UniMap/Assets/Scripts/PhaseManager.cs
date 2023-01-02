using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    [Header ("Phase 1")]
    [SerializeField] private Transform[] m_Phase1SpawnPoints;
    [SerializeField] private Transform m_Phase1DominationPointLocation;

    [Header ("Phase 2")]
    [SerializeField] private Transform[] m_Phase2SpawnPoints;
    [SerializeField] private Transform m_Phase2DominationPointLocation;
    
    [Header ("Prefabs")]
    [SerializeField] private GameObject m_DominationPoint;
    [SerializeField] private GameObject m_Enemy;
    [SerializeField] private GameObject m_BigEnemy;
    [SerializeField] private GameObject m_RangedEnemy;

    private int m_Phase = 1;
    private bool m_PhaseCompleted = true;

    private void Start()
    {
        m_Phase = 1;
    }

    private void Update()
    {
        if (m_PhaseCompleted)
        {
            m_PhaseCompleted = false;
            if (m_Phase == 1)
                StartCoroutine(Phase1(40));
            else if (m_Phase == 2)
                StartCoroutine(Phase2(40));
        }
    }
    
    private IEnumerator Phase1(int delayTime)
    {
        int  numberOfSpawnPoint = m_Phase1SpawnPoints.Length;
        var dominationPoint = Instantiate(m_DominationPoint, 
                                          m_Phase1DominationPointLocation.position, 
                                          m_Phase1DominationPointLocation.rotation);
        while (m_Phase == 1)
        {    
            int time = dominationPoint.GetComponent<DominationPoint>().GetSeconds();
            
            if (time >= 20)
            {
                Destroy(dominationPoint);
                m_Phase = 2;
                m_PhaseCompleted = true;
                break;
            }

            for (int i = 0; i < numberOfSpawnPoint; i++) 
            {
                Instantiate(m_Enemy, m_Phase1SpawnPoints[i].position, m_Phase1SpawnPoints[i].rotation);   
            }
            yield return new WaitForSeconds(delayTime);
        }
    }

    private IEnumerator Phase2(int delayTime)
    {
        int  numberOfSpawnPoint = m_Phase2SpawnPoints.Length;
        var dominationPoint = Instantiate(m_DominationPoint, 
                                          m_Phase2DominationPointLocation.position, 
                                          m_Phase2DominationPointLocation.rotation);
        while (m_Phase == 2)
        {       
            int time = dominationPoint.GetComponent<DominationPoint>().GetSeconds();
            
            if (time >= 20)
            {
                Destroy(dominationPoint);
                m_Phase = 3;
                m_PhaseCompleted = true;
                break;
            }

            for (int i = 0; i < numberOfSpawnPoint; i++) 
            {
                Instantiate(m_BigEnemy, m_Phase2SpawnPoints[i].position, m_Phase2SpawnPoints[i].rotation);   
            }
            yield return new WaitForSeconds(delayTime);
        }
    }
}
