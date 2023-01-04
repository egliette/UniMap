using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class WeaponManager : MonoBehaviour
{
    [SerializeField] private int m_totalBullets;
    [SerializeField] private int m_magSize = 30;
    [SerializeField] private int m_bulletsLeft;
    private float m_reloadDuration = PublicVariables.RELOAD_TIME;
    private float m_reloadTimer = 0f;
    private bool m_onReload;

    public bool IsOutOfBullet()
    {
        return m_totalBullets <= 0;
    }

    public bool IsMagEmpty()
    {
        return m_bulletsLeft <= 0;
    }

    public bool IsLowOnBullet()
    {
        return m_bulletsLeft < m_magSize;
    }

    public void Reload()
    {
        if (m_reloadTimer < m_reloadDuration)
        {
            m_reloadTimer += Time.deltaTime;
        }
        else
        {
            int bulletToAdd = m_magSize - m_bulletsLeft;
            m_bulletsLeft = (m_totalBullets >= m_magSize)? m_magSize : m_totalBullets;
            m_totalBullets -= bulletToAdd;
            m_onReload = false;
        }
    }

    public bool OnReloading()
    {
        return m_onReload ;
    }
}
