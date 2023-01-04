using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using UnityEngine.Animations.Rigging;

public class PlayerFiringController : MonoBehaviour
{
    // Aiming
    [SerializeField] private Transform m_aimPos;
    [SerializeField] private float m_aimSmoothSpeed = 20f;
    [SerializeField] private LayerMask m_aimMask;

    // Aim Down sight
    [SerializeField] private GameObject m_aimCamera;

    // Firing
    [SerializeField] private WeaponManager m_weapon;
    private bool m_isFiring;

    // Reloading
    private bool m_isReloading;

    // Animation rig set up for reloading
    [SerializeField] private MultiAimConstraint m_rightHandConstraint;
    [SerializeField] private TwoBoneIKConstraint m_leftHandIKConstraint;
    private void Update()
    {
        AimDownSight();
        WeaponUpdate();
    }

    private void FixedUpdate()
    {
        Aim();
    }

    private void Aim()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, m_aimMask))
        {
            //m_aimPos.position = Vector3.Lerp(m_aimPos.position, hit.point, m_aimSmoothSpeed * Time.deltaTime);
            m_aimPos.position = hit.point;
        }
    }

    private void WeaponUpdate()
    {
        if (!m_weapon.IsOutOfBullet() || !m_weapon.IsMagEmpty())
        {
            if (!m_weapon.IsOutOfBullet())
            {
                if(Input.GetKeyDown(KeyCode.R) && m_weapon.IsLowOnBullet() || m_weapon.IsMagEmpty())
                {
                    StartReloading();
                }
                else if (IsPlayerReloading() && !m_weapon.OnReloading())
                {
                    StopReloading();
                }
            }
           


            if (Input.GetKey(KeyCode.Mouse0))
            {
                m_weapon.StartFiring();
                m_isFiring = true;
            }
            else
            {
                m_weapon.StopFiring();
                m_isFiring = false;
            }
        }
        else
        {
            m_weapon.StopFiring();
            m_isFiring = false;
            StopReloading();
        }
    }
    private void AimDownSight()
    {
        // right click
        if (Input.GetKey(KeyCode.Mouse1))
        {
            m_aimCamera.SetActive(true);
        }
        else
        {
            m_aimCamera.SetActive(false);
        }
    }

    private void StartReloading()
    {
        m_isReloading = true;
        m_weapon.StartReloading();
        m_rightHandConstraint.weight = 0;
        m_leftHandIKConstraint.weight = 0;
    }

    private void StopReloading()
    {
        m_isReloading = false;
        m_rightHandConstraint.weight = 1;
        m_leftHandIKConstraint.weight = 1;
    }
    public bool IsPlayerReloading()
    {
        return m_isReloading;
    }

    public bool IsPlayerFiring()
    {
        return m_isFiring;
    }

    

}
