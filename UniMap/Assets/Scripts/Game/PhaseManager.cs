using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhaseManager : MonoBehaviour
{
    [SerializeField] private Transform[] m_DominationPointsLocation;
    [SerializeField] private Transform m_FinalDominationPointLocation;


    [Header ("Prefabs")]
    [SerializeField] private TextMeshProUGUI m_LocationText;
    [SerializeField] private GameObject m_DominationPoint;
    [SerializeField] private GameObject m_Enemy;
    [SerializeField] private GameObject m_BigEnemy;
    [SerializeField] private GameObject m_RangedEnemy;
    [SerializeField] private GameObject m_Boss;
    [SerializeField] private GameObject m_RangedBoss;

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
            UpdateLocationText(index);

            if (m_Phase == 1)
                StartCoroutine(Phase1(40, m_DominationPointsLocation[index]));
            else if (m_Phase == 2)
                StartCoroutine(Phase2(40, m_DominationPointsLocation[index]));
            else if (m_Phase == 3)
                StartCoroutine(Phase3(40, m_DominationPointsLocation[index]));
            else if (m_Phase == 4)
                StartCoroutine(Phase4(40, m_DominationPointsLocation[index]));
            else if (m_Phase == 5)
                StartCoroutine(Phase5(40));
        }
    }
    
    private void UpdateLocationText(int index)
    {
        string locationName = m_DominationPointsLocation[index].GetComponent<LocationName>().LocationText;
        m_LocationText.text = "Stay at " + locationName + " the location for 20 seconds";
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

    private IEnumerator Phase3(int delayTime, Transform dominationPointLocation)
    {
        var dominationPoint = Instantiate(m_DominationPoint, 
                                          dominationPointLocation.position, 
                                          dominationPointLocation.rotation);

        foreach (Transform spawnPoint in dominationPointLocation)
        {
            Instantiate(m_RangedEnemy, spawnPoint.position, spawnPoint.rotation);  
        }

        while (m_Phase == 3)
        {    
            int time = dominationPoint.GetComponent<DominationPoint>().GetSeconds();
            
            if (time >= 20)
            {
                Destroy(dominationPoint);
                m_Phase = 4;
                m_PhaseCompleted = true;
                break;
            }

            yield return new WaitForSeconds(delayTime);
        }
    }

    private IEnumerator Phase4(int delayTime, Transform dominationPointLocation)
    {
        var dominationPoint = Instantiate(m_DominationPoint, 
                                          dominationPointLocation.position, 
                                          dominationPointLocation.rotation);

        foreach (Transform spawnPoint in dominationPointLocation)
        {
            Instantiate(m_Boss, spawnPoint.position, spawnPoint.rotation);  
        }

        while (m_Phase == 4)
        {    
            int time = dominationPoint.GetComponent<DominationPoint>().GetSeconds();
            
            if (time >= 20)
            {
                Destroy(dominationPoint);
                m_Phase = 5;
                m_PhaseCompleted = true;
                break;
            }

            yield return new WaitForSeconds(delayTime);
        }
    }

    private IEnumerator Phase5(int delayTime)
    {
        var dominationPoint = Instantiate(m_DominationPoint, 
                                          m_FinalDominationPointLocation.position, 
                                          m_FinalDominationPointLocation.rotation);

        foreach (Transform spawnPoint in m_FinalDominationPointLocation)
        {
            Instantiate(m_RangedBoss, spawnPoint.position, spawnPoint.rotation);  
        }

        while (m_Phase == 5)
        {    
            int time = dominationPoint.GetComponent<DominationPoint>().GetSeconds();
            
            if (time >= 20)
            {
                Destroy(dominationPoint);
                m_Phase = 6;
                m_PhaseCompleted = true;
                break;
            }

            yield return new WaitForSeconds(delayTime);
        }
    }
}
