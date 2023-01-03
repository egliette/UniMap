using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

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
    private void Update()
    {
        Aim();
        AimDownSight();
        Fire();
    }

    private void Aim()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, m_aimMask))
        {
            m_aimPos.position = Vector3.Lerp(m_aimPos.position, hit.point, m_aimSmoothSpeed * Time.deltaTime);
        }
    }

    private void Fire()
    {
        // left click
        if (Input.GetKey(KeyCode.Mouse0))
        {
            m_weapon.StartFiring();
        }
        else
        {
            m_weapon.StopFiring();
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

}
