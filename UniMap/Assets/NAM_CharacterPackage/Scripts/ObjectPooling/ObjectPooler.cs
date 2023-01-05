using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    #region Singleton
    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion


    [System.Serializable]
    public class Pool
    {
        public string m_tag;
        public GameObject m_prefab;
        public int m_size;
    }

    public Dictionary<string, Queue<GameObject>> m_poolDictionary;
    public List<Pool> m_pools;


    private void Start()
    {
        m_poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in m_pools){
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for(int i = 0; i < pool.m_size; ++i)
            {
                GameObject obj = Instantiate(pool.m_prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }


            m_poolDictionary.Add(pool.m_tag, objectPool);
        }

    }


    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!m_poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag" + tag + " does not exist");
            return null;
            
        }
        GameObject objToSpawn = m_poolDictionary[tag].Dequeue();
        objToSpawn.SetActive(true);
        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;

        m_poolDictionary[tag].Enqueue(objToSpawn);

        return objToSpawn;
    }
}
 