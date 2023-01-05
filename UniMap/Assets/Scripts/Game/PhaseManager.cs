using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    [SerializeField] private Transform[] m_DominationPointsLocation;


    [Header ("Prefabs")]
    [SerializeField] private GameObject m_DominationPoint;
    [SerializeField] private GameObject m_Enemy;
    [SerializeField] private GameObject m_BigEnemy;
    [SerializeField] private GameObject m_RangedEnemy;

    private int m_Phase = 1;
    private bool m_PhaseCompleted = true;
    private List<int> m_LocationPass = new List<int>();

    private void Start()
    {
        m_Phase = 1;
    }

    private void Update()
    {
        if (m_PhaseCompleted)
        {
            m_PhaseCompleted = false;
            int index = Random.Range(0, m_DominationPointsLocation.Length);
            while (m_LocationPass.Contains(index) && !m_LocationPass.Count.Equals(0))
                index = Random.Range(0, m_DominationPointsLocation.Length);
            m_LocationPass.Add(index);
            Debug.Log(index);

            if (m_Phase == 1)
                StartCoroutine(Phase1(40, m_DominationPointsLocation[index]));
            else if (m_Phase == 2)
                StartCoroutine(Phase2(40, m_DominationPointsLocation[index]));
        }
    }
    
    private IEnumerator Phase1(int delayTime, Transform dominationPointLocation)
    {
        var dominationPoint = Instantiate(m_DominationPoint, 
                                          dominationPointLocation.position, 
                                          dominationPointLocation.rotation);

        foreach (Transform spawnPoint in dominationPointLocation)
        {
            Instantiate(m_Enemy, spawnPoint.position, spawnPoint.rotation);  
        }

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

            yield return new WaitForSeconds(delayTime);
        }
    }

    private IEnumerator Phase2(int delayTime, Transform dominationPointLocation)
    {
        var dominationPoint = Instantiate(m_DominationPoint, 
                                          dominationPointLocation.position, 
                                          dominationPointLocation.rotation);

        foreach (Transform spawnPoint in dominationPointLocation)
        {
            Instantiate(m_BigEnemy, spawnPoint.position, spawnPoint.rotation);  
        }

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

            yield return new WaitForSeconds(delayTime);
        }
    }


}
