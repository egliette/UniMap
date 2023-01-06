using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    [SerializeField] private AudioClip[] m_BGMList;

    private AudioSource m_Audio;

    private void Start()
    {
        m_Audio = GetComponent<AudioSource>();

        if (!m_Audio.playOnAwake)
        {
            m_Audio.clip = m_BGMList[Random.Range(0, m_BGMList.Length)];
            m_Audio.Play();
        }
    }

    private void Update()
    {
        if (!m_Audio.isPlaying)
        {
            m_Audio.clip = m_BGMList[Random.Range(0, m_BGMList.Length)];
            m_Audio.Play();
        }
    }

}
